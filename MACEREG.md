MaceRegulator Documentation -


MaceRegulator (MR) is a modified version of the OpenRA Project. See AUTHORS, COPYING and README.md for more information about OpenRA Project.
The intent of MaceRegulator is to help with the administration of OpenRA servers. MaceRegulator is currently unsupported. MR is in an open alpha stage.


Current System Requirements:

Currently, OpenRA runs on virtually all platforms. MaceRegulator is designed to work on a Linux (Debian) OS, with portions run within Windows OS.

The main features of MaceRegulator are:

1. Automated Messages sent to players ingame
2. A bridged connection between an IRC server and OpenRA with communications
3. Creation of an ingame moderator system, allowing users to kick players (through a TCPKILL command via sudo) through nodeJS and mIRC


Components of MaceRegulator:

MR requires the following to function correctly:
OpenRA release-20180307 with mono and all the needed things to compile
Launching the server with the ihptru-style server Set up script: https://github.com/ihptru/OpenRA-Servers-AutoUpdating
a nodeJs bot that acts as a pipe stream from the OpenRA console to an IRC channel
An mIRC bot that acts as a permissions and features granter 

List of Prerequisites:

OpenRA Version:

This version of MR uses OpenRA release-20180307. Modifications to default rules are made for the RA and CNC mods.
Use the ihptru-style server Set up script: https://github.com/ihptru/OpenRA-Servers-AutoUpdating

OS/Environment:

Debian 8 or Debian 9 OS (Same requirements as OpenRA)
nodeJs with npm and irc-factory installed (and its prerequisities, some other npm modules may be required)
Debian 8 or 9 with bash, tcpkill, and sudo privileges to run tcpkill (to enable the current version of the kick command)
An IRC server (Highly reccomended to have a private one to manage access permissions with custom hostmasks)

Windows OS:
mIRC with the MR mIRC scripts (the moderator bot)


TO install:

1. Installation steps for the environment running the OpenRA dedicated server

Install nodejs and npm
Install tcpkill and pkill
Install the irc-factory npm package 
Modify the sudo permissions to allow the appropriate system username to run the tcpkill command.

Recommend running the following script in the HOME folder.
Run the ihptru setup script, ensure all appropriate OpenRA dependencies and packages are installed. 
Merge in the release of OpenRA into the servers/tmp/OpenRA-release-VERSION HERE folder
make dependencies
make all
make VERSION="release-VERSION HERE"

Copy the start_game.sh from MACEBOT to the home folder

2. Identify or install/launch an IRCd (recommend using UnrealIRCd or ratbox) and services (highly recommended: Anope)
3. Configure the start_game.sh and ircpipe.js to meet your needs, and to match to the IRCd address.
4. Launch mIRC, connect the appropriate server. Add the contents of mr-mirc-alias into mIRC aliases, and MR-mircremotes into Remotes. 
5. Configure botchans.ini with the appropriate IRC channels, and place botchans.ini in the AppData/Roaming/mIRC folder.
6. Run /initialize in mIRC, launch the OpenRA dedicated server from the start_game.sh edited previously.

