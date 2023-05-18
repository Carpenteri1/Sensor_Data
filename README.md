# Sensor_Data
An assignment to see in what level Iam in my coding and understanding on solving this task.

## The assignment

The assignment is to read input from a number of sensors and output log files. 
If you feel adventurous, you are encouraged to include network communication between the software reading 
the sensor values and the software creating the log files, 
as we often have an embedded client communicating with a server software.


## Sensor data

A process simulates sensors sending data to you. Instead of reading from SPI, I2C, 
some register or something similar, typical embedded, our simulator just outputs data on stdout. 
The data is in a binary format, described below in the Sensor data format. 
The simulator can be spawned with a --name argument. 

If no name is given, a random name will be generated. 
This means that your program can communicate with several sensor simulator processes.
The simulator will most of the time send both temperature and humidity but can also choose to send one or none of them.


## Log file
The log file shall contain logs of the sensor data in JSON-format. 
Each log line shall contain one JSON document, with the following format:

```
----------------------------------------------------------------------------------
Key.              Required                 Format/Unit               Type
----------------------------------------------------------------------------------
timespan         Yes.                 ISO 8601 with time zone.       UTF-8 String
name             Yes                                                 UTF-8 String
temperature.     If present °C.                                      Float
humidity         If present %                                        Float
----------------------------------------------------------------------------------
```

### E.g:
```
{
    "timestamp": "2008-09-15T15:53:00+05:00",
    "name": "sensor1",
    "temperature": 273.15
    "humidity": 99.1
}
```

## Sensor data format
The sensor data is a binary format. 
Each reading will be packaged with the length of the packet, 

a timestamp, a name of the sensor, and the available sensor readings. 
Strings are in UTF-8. Numerical values are in network byte order.

## Note
For a deeper explaination see the assignment pdf
