# Readme

## Service

The service reads data from a temperature sensor and puts it into the SQLite database.

### Setup Humidity sensor

- See [This article](https://www.circuitbasics.com/how-to-set-up-the-dht11-humidity-sensor-on-the-raspberry-pi/)

### Setup Cron job in Raspberry pi 

- See [This article](https://askubuntu.com/questions/799023/how-to-set-up-a-cron-job-to-run-every-10-minutes)

### Todo's

- [x] Find out why accuracy is so bad -> bad sensor; see [this article](https://howtomechatronics.com/tutorials/arduino/dht11-dht22-sensors-temperature-and-humidity-tutorial-using-arduino/)
- [x] Time in SQLite DB is not correct. Because DateTime now is default set to UTC time. [This article](https://stackoverflow.com/questions/6087691/datetimenow-gives-wrong-time) shows how to correct it.


## API

The API can send the latest temperature, along with a formatted string, depending on the temperature.

### Logging

- See [This article](https://wakeupandcode.com/logging-in-asp-net-core-3-1/) for implementing serilog

### Todo's

- [ ] Send Emoji's with string -> see [this W3Schools subject](https://www.w3schools.com/charsets/ref_emoji_smileys.asp)
- [ ] Put messages to send in SQLite database
