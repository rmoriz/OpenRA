#region Copyright & License Information
/*
 * Copyright 2007-2017 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using BeaconLib;
using OpenRA.Network;
using OpenRA.Server;
using OpenRA.Primitives;
using OpenRA.Support;
using S = OpenRA.Server.Server;

namespace OpenRA.Mods.Common.Server
{
	public class GameStatusExport : ServerTrait, ITick
	{

		static readonly int PingInterval = 5000;
		

		public int TickTimeout { get { return PingInterval * 100; } }

		long lastPing = 0;
		bool isInitialPing = true;




		public void Tick(S server)
		{
		string statfilename = "statbuf" + Game.Settings.Server.ListenPort + ".txt";
		var statfilepath = Platform.ResolvePath("^",statfilename);
			if ((Game.RunTime - lastPing > PingInterval) || isInitialPing) {
			if (!File.Exists(statfilepath)) { File.WriteAllText(statfilepath, "Placeholder"); }
				isInitialPing = false;
				lastPing = Game.RunTime;
				File.WriteAllText(statfilepath, String.Format("CurMap: {0} \n", server.Map.Title));
				File.AppendAllText(statfilepath, String.Format("MapHash: {0} \n", server.LobbyInfo.GlobalSettings.Map));
				File.AppendAllText(statfilepath, String.Format("CurConnections: {0} \n", server.LobbyInfo.Clients.Count()));
				File.AppendAllText(statfilepath, String.Format("MapMaxPlayers: {0} \n", server.LobbyInfo.Slots.Count()));
				File.AppendAllText(statfilepath, String.Format("GameState: {0} \n", server.State.ToString()));
				//File.AppendAllText(statfilepath, "PlayerIndex,IpAddress,Team,Observer,Name \n");
				var testbuffer = server.Conns.ToArray(); // Make an array of server connections? could also be server.LobbyInfo.Clients
								foreach(var item in testbuffer) {
										var ClientIdT = server.GetClient(item); // make the temporary array the client ID
				File.AppendAllText(statfilepath, String.Format("ID: {0} IP: {1} TEAM: {2} SPEC: {3}, NAME: {4} \n", ClientIdT.Index.ToString(), ClientIdT.IpAddress.ToString(), ClientIdT.Team.ToString(), ClientIdT.IsObserver.ToString(), ClientIdT.Name.ToString()));
				
								}
			
			}
		}
		
	}
}
