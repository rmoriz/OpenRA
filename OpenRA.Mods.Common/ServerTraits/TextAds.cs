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
	public class TextAds : ServerTrait, ITick
	{
		static readonly int PingInterval = 600000; // Text Ads spam every 10 minutes
		//static readonly int ConnReportInterval = 20000; // Report every 20 seconds
		//static readonly int ConnTimeout = 60000; // Drop unresponsive clients after 60 seconds

		// TickTimeout is in microseconds
		public int TickTimeout { get { return PingInterval * 100; } }

		long lastPing = 0;
		//long lastConnReport = 0;
		bool isInitialPing = true;
		

		public void Tick(S server)
		{
		var advertfile = Platform.ResolvePath("^", "adverts.txt");
		var xlines = File.ReadAllLines(advertfile);
			if ((Game.RunTime - lastPing > PingInterval) || isInitialPing)
			{
				isInitialPing = false;
				lastPing = Game.RunTime;
					var line = xlines.Random(server.Random);
					//Console.WriteLine(line);
					server.SendMessage(line);

			}  
		}
	}
}
    
