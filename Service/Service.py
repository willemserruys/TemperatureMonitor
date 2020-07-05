#!/usr/bin/python

import sys
import Adafruit_DHT
import sqlite3
import redis

r = redis.Redis(host='localhost',port=6379,db=0)

conn = sqlite3.connect('/home/pi/TemperatureMonitor/Data.db')

humidity,temperature = Adafruit_DHT.read_retry(11,4)

c = conn.cursor()
c.execute('INSERT INTO TemperatureReading (Temperature,Humidity) values({0:0.1f},{1:0.1f})'.format(temperature,humidity))

conn.commit()

conn.close()

r.publish('TemperatureUpdated','')