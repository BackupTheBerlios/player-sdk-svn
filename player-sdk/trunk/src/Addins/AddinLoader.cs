/*
 * Copyright (C) 2004 Sergio Rubio <sergio.rubio@hispalinux.es>
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation; either version 2 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public
 * License along with this program; if not, write to the
 * Free Software Foundation, Inc., 59 Temple Place - Suite 330,
 * Boston, MA 02111-1307, USA.
 */

namespace Player.Addins 
{
    using System;    
	using System.Reflection;
	using System.IO;
	using Player.Configuration;

	public class AddinLoader
	{

		public static IAddin LoadAddin (string addinDir, string addinAssembly, string addinType)
		{
			char separator = Path.DirectorySeparatorChar;
			IAddin addinObject = null;
			Assembly asm = null;
			//First, the addin is loaded from the user config dir.
			//If not, try to the system wide addin.
			try {
				string userConfigDir = Configuration.GetInstance ().UserConfigDir;
				string userAddinLocation;
				if (addinAssembly.EndsWith (".dll"))
					userAddinLocation = userConfigDir + separator + addinDir + separator + addinAssembly;
				else
					userAddinLocation = userConfigDir + separator + addinDir + separator + addinAssembly + ".dll";

				if (File.Exists (userAddinLocation))
				{
					Console.WriteLine ("Loading {0} from USER addins", addinAssembly);
					asm = Assembly.LoadFrom (userAddinLocation);
					if (asm != null)
						Console.WriteLine ("{0} loaded succesfully", addinAssembly);
					addinObject = (IAddin) asm.CreateInstance (addinType);

				//Loading from system addins
				} else {
					Console.WriteLine ("Loading {0} from SYSTEM addins", addinAssembly);
					string sdkDir = Path.GetDirectoryName (Assembly.GetCallingAssembly ().Location);
					string systemAddinLocation = sdkDir + separator + "Player.Sdk" + 
								separator + addinDir + separator + addinAssembly + ".dll";
					Console.WriteLine ("Addin location: {0}", systemAddinLocation);
					asm = Assembly.LoadFrom (systemAddinLocation);
					if (asm != null)
						Console.WriteLine ("Assembly {0}.dll loaded succesfully", addinAssembly);
					addinObject = (IAddin) asm.CreateInstance (addinType);
				}

				if (addinObject == null)
				{
					Console.WriteLine ("Error initiating {0}", addinType);
					throw new AddinUnavailableException (String.Format ("ERROR: Addin {0} unavailable.", addinType));
				}
				return addinObject;
						
			} catch (Exception e) {
				Console.WriteLine (e.StackTrace);
				Console.WriteLine (e.Message);
				throw new AddinLoadingException (String.Format ("ERROR: Error loading addin {0}.", addinType), e);
			}
		}
	}

}

