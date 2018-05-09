using System.Linq;
using OpenRA.Server;
using S = OpenRA.Server.Server;
using System;
using System.IO;
using System.Net;
using System.Threading;
using OpenRA.Support;

namespace OpenRA.Mods.Common.Server
{
	public class IRCTextIn : ServerTrait, ITick
	{
		static readonly int PingInterval = 1000; // Do this every 3 seconds
		//static readonly int ConnReportInterval = 20000; // Report every 20 seconds
		//static readonly int ConnTimeout = 60000; // Drop unresponsive clients after 60 seconds

		// TickTimeout is in microseconds
		public int TickTimeout { get { return PingInterval * 100; } }

		long lastPing = 0;
		//long lastConnReport = 0;
		bool isInitialPing = true;
		int counter = 0;


		public void Tick(S server)
		{
		string irclogname = "ircbuf" + Game.Settings.Server.ListenPort + ".txt";
		var ircpath = Platform.ResolvePath("^",irclogname);
			if ((Game.RunTime - lastPing > PingInterval) || isInitialPing)
			{
			if (File.Exists(ircpath)){
				var ircarray = File.ReadAllLines(ircpath);
				isInitialPing = false;
				lastPing = Game.RunTime;
				foreach(var item in ircarray) {
						var x = item.Split(new[] { ',' }, 2);
						int id = Int32.Parse(x[0]);
						string ircmsg = x[1];
						if(id > counter){
						counter = id;					
						//Console.WriteLine(ircmsg);
						server.SendMessage(ircmsg);
						//Console.WriteLine("Debug: That was message number {0}", id);
					}
				}
			}

			} 
		}
	}
}

				//if (!File.Exists(ircpath)){ Console.WriteLine("No file detected at {0}", ircpath);}
				//if (File.Exists(ircpath)){ Console.WriteLine("File detected at {0}", ircpath);}
								//Console.WriteLine("IRC File logging is {0}", ircpath); 