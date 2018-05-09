#!/bin/bash

#
# To start, run script and specify as arguments: 
#     1. mod ( ra, cnc, d2k, ts )
#     2. version ( release, playtest, bleed )
#     3. port ( 1230, 1231, ... )

Name="Abarrat's Abode"
Mod="$1"
Dedicated="True"
DedicatedLoop="False"
ListenPort="$3"
ExternalPort="$3"
AdvertiseOnline="True"
ServerTimeOut="10800000"
SN="$4"
Name="Abarrat's Abode $4 North America"

while true; do

	cd "${HOME}/servers/bin/current-$2/"

	if [ "$2" = "release" ]; then
		mono OpenRA.Server.exe Game.Mod=$Mod Server.Dedicated=True Server.DedicatedLoop=$DedicatedLoop \
			Server.Name="$Name" Server.ListenPort=$ListenPort Server.ExternalPort=$ExternalPort \
			Server.AdvertiseOnline=$AdvertiseOnline \
			Server.LockBots=True Server.TimeOut=$ServerTimeOut | nodejs ${HOME}/ircpipe.js $4 $3
	else
		mono --debug OpenRA.Server.exe Game.Mod=$Mod \
			Server.Name="$Name" Server.ListenPort=$ListenPort Server.ExternalPort=$ExternalPort \
			Server.AdvertiseOnline=$AdvertiseOnline Server.TimeOut=$ServerTimeOut
	fi
done
