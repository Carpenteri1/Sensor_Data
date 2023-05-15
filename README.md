# Sensor_Data

An assignment for a future employer.
Assignment.pdf includes the description of the assignment.


## Description

The assignment
The assignment is to read input from a number of sensors and output log files. If you feel adventurous, you are encouraged to include network communication between the software reading the sensor values and the software creating the log files, as we often have an embedded client communicating with a server software.
        input:
+-------------------+
| sensor simulator  |
+-------------------+
      output:
+-------------------+
| sensor log file   |
+-------------------+ |^
(stdout) | || v|
+-------------------+
|   sensor reader   |
|   (part of the    |
|     assignment)   |
+-------------------+
+-------------------+
|   sensor logger   |
|   (part of the    |
|     assignment)   |
+-------------------+
|^ ^^ | may be combined | +-------------------------+
Sensor data
A process simulates sensors sending data to you. Instead of reading from SPI, I2C, some register or something similar, typical embedded, our simulator just outputs data on stdout. The data is in a binary format, described below in the Sensor data format. The simulator can be spawned with a --name argument. If no name is given, a random name will be generated. This means that your program can communicate with several sensor simulator processes.
The simulator will most of the time send both temperature and humidity but can also choose to send one or none of them.
Log file
The log file shall contain logs of the sensor data in JSON-format. Each log line shall contain one JSON document, with the following format:
 Key
Required Yes
Format/Unit
Type
UTF-8 String UTF-8 String Float
Float
 timestamp
name Yes temperature If present °C humidity If present %
ISO 8601 with time zone
 1
E.g:
```
{
    "timestamp": "2008-09-15T15:53:00+05:00",
    "name": "sensor1",
    "temperature": 273.15
    "humidity": 99.1
}
```

### Sensor data format
The sensor data is a binary format. Each reading will be packaged with the length of the packet, a timestamp, a name of the sensor, and the available sensor readings. Strings are in UTF-8. Numerical values are in network byte order.
 Offset
0
4
12
13
13 + nlen
16 + nlen (13 + nlen)
Field name
plength
timestamp
nlen
name
temperature
humidity
Type
unsigned
integer
unsigned
integer
unsigned
integer
string
unsigned
integer
unsigned
integer
Size
4 bytes
8 bytes
1 byte
nlen
bytes
3 bytes
2 bytes
Description
The length of package, including this field
Unix timestamp, in milliseconds
The length of the name The length of the name In hundredths of K Relative humidity in ‰
  Note: Both temperature and humidity are optional. This means that the offset for humidity can also be 13 + nlen.
