
// To use:
// Put in folder
// Run npm init
// Run npm install irc-factory
// Use by piping whatever to nodejs /path/to/ircpipe.js


process.stdin.resume();
process.stdin.setEncoding('utf8');

var connected = false
var factory = require('irc-factory');
var cmd=require('node-cmd');
api = new factory.Api();

var client = api.createClient('mrbot', {
	nick : 'BetaBotNA',
	user : 'testuser',
	server : 'irc.server.ip.here',
	realname: 'realbot',
	port: 6667,
	secure: false
});

//Define the broadcast channel
var bchannel = '#channel'
//Set welcome message on join
var wmesg = 'MaceRegulator v0.000 ONLINE'

//Figure out the server port to define the filename
var ora_port = ar_num[1]; //CHANGED <<<<<
var ircbuffer = 'ircbuf' + ora_port + '.txt';
console.log(ircbuffer);
cmd.run('echo "999,IRC Chat Buffer Initialized" > ~/.openra/'+ircbuffer+'');

api.hookEvent('mrbot', 'registered', function(message) {
	client.irc.join(bchannel);
	client.irc.privmsg(bchannel, wmesg)
	connected = true
});

//Kick function test
api.hookEvent('mrbot', 'privmsg', function(message){
    if(message.target[0] != '#'){
        var sourcehst = message.hostname
        var matches
        if((matches = /^kick: (.+)$/.exec(message.message)) && sourcehst == "hostmask.here"){
          var player = matches[1]
          client.irc.privmsg(bchannel, "kicking: " + player)
	cmd.run('echo "'+player.replace('"', '\"')+'" > test.txt');
  	cmd.run('sudo tcpkill  -i eth0 host "'+player.replace('"', '\"')+'"');
	cmd.run('sleep 5 && sudo pkill -f tcpkill');
        }
    }
})

//New msg function test
api.hookEvent('mrbot', 'privmsg', function(message){
    if(message.target[0] != '#'){
        var sourcehst = message.hostname
        var matches
        if((matches = /^csay: (.+)$/.exec(message.message)) && sourcehst == "hostmask.here"){
          var chatstring = matches[1]
	cmd.run('echo "'+chatstring+'" >> ~/.openra/'+ircbuffer+'');
        }
    }
})

var buffer = ''
var lines = []

process.stdin.on('data', function(chunk) {
	buffer += chunk;
	var parts = buffer.split("\n")
	while(parts.length > 1){
		lines.push(parts.shift())
	}
	buffer = parts[0]
});

setInterval(function(){
	if(connected && lines.length > 0){
		while(lines.length > 0){
			line = lines.shift()
			client.irc.privmsg(bchannel, line)
		}
	}
}, 10000)
