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

namespace Player.Kits 
{
    using System;    
	using Player.Addins;
	using Player.Player;
	using Player.Data;
	using Player.Configuration;
	using System.Collections;

	public class KitManager
	{
		private Hashtable kits = new Hashtable ();
		private string playerKitLocation = "PlayerKits";
		private string dataKitLocation = "DataKits";
		private static KitManager instance;
		private Configuration config = Configuration.GetInstance ();
		
		private KitManager ()
		{
			LoadKits ();
		}

		public IAddin this [string kitName]
		{
			get {
				return (IAddin) kits[kitName];
			}

			set {
				((IAddin)kits[kitName]).Unload ();
				value.Load ();
				kits[kitName] = value; 
			}
		}

		internal static KitManager GetInstance ()
		{
			if (instance == null)
				instance = new KitManager ();
			return instance;
		}

		public void UpdateKits ()
		{
		}

		public void SwapKit (string kitType, IAddin kit)
		{
		}

		private void LoadKits ()
		{
			kits.Add ("DataKit", AddinLoader.LoadAddin (dataKitLocation, config.DataKitAssembly, config.DataKitType));
			kits.Add ("PlayerKit", AddinLoader.LoadAddin (playerKitLocation, config.PlayerKitAssembly, config.PlayerKitAssembly));
		}
	}

}

