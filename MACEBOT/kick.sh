#!/bin/bash
sudo /usr/sbin/tcpkill -i eth0 host $1 2>&1 &
APP_PID=$!
sleep 5
sudo kill -9 $APP_PID

