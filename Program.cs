
using System.Text;
using System.Diagnostics;

        // Spawn the sensor simulator process and capture its stdout
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "./sensor_data.x86_64-apple-darwin",
            RedirectStandardOutput = true,
            //Arguments = "",
            UseShellExecute = false
        };
        Process sensorSimulator = Process.Start(startInfo);
        StreamReader stdoutReader = sensorSimulator.StandardOutput;

        while (!stdoutReader.EndOfStream)
        {
            byte[] packetLengthBytes = new byte[4];
            stdoutReader.BaseStream.Read(packetLengthBytes, 0, 4);
            int packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

            byte[] packetBytes = new byte[packetLength];
            stdoutReader.BaseStream.Read(packetBytes, 0, packetLength);

    uint timestampMillis = BitConverter.ToUInt32(packetBytes, 4);

    // Extract the sensor data fields from the packet
    //long timestampMillis = BitConverter.ToInt64(packetBytes, 4);
    DateTime timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestampMillis).DateTime;

    int nameLength = packetBytes[12];
            string name = Encoding.UTF8.GetString(packetBytes, 13, nameLength);
            int temperatureOffset = 13 + nameLength;
            int temperature = BitConverter.ToInt32(packetBytes, temperatureOffset);
            int humidityOffset = temperatureOffset + 3;
            int humidity = BitConverter.ToInt16(packetBytes, humidityOffset);

    // Output the sensor data to log files
    string logFileName = $"_{DateTime.Now:yyyyMMdd}.log";

    string logEntry = $"Timestamp: {timestamp:yyyy-MM-ddTHH:mm:sszzz}, Name: {name}, Temperature: {temperature}°C, Humidity: {humidity}‰";
            File.AppendAllText(logFileName, logEntry + Environment.NewLine);

            Console.WriteLine(logEntry);
        }

        // Cleanup resources
        sensorSimulator.WaitForExit();
        stdoutReader.Close();



