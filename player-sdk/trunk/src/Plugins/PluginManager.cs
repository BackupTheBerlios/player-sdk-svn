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

namespace Player.Plugins 
{
	using System.IO;
    using System;
	using System.Collections;
	using System.Reflection;
	using Player.Configuration;
	using Player.Addins;

	public class PluginManager : IEnumerable
	{

		private Hashtable pluginCollection = new Hashtable ();
		private static Configuration config = Configuration.GetInstance ();
		private string systemPluginsDir = config.SystemAddinsDir + Path.DirectorySeparatorChar + "Plugins";
		private string userPluginsDir = config.UserAddinsDir + Path.DirectorySeparatorChar + "Plugins";
		private static PluginManager manager;
		
		private PluginManager () {
			LoadAllPlugins ();
		}

		internal static PluginManager GetInstance ()
		{
			if (manager == null)
				manager = new PluginManager ();
			return manager;
		}

		internal void LoadAllPlugins ()
		{
			string[] pluginFiles = Directory.GetFiles (systemPluginsDir);
			foreach (string pluginFile in pluginFiles)
			{
				Assembly pluginAssembly = Assembly.LoadFrom (pluginFile);
				Type attrType = typeof (PluginInfoAttribute);
				if (pluginAssembly.IsDefined (attrType, false))
				{
					PluginInfoAttribute attr = (PluginInfoAttribute) pluginAssembly.GetCustomAttributes (attrType, false)[0];
					IAddin plugin = (IAddin) pluginAssembly.CreateInstance (attr.Class);
					if (plugin != null)
						AddPluginToCollections (plugin);
						
				} else
					Console.WriteLine ("Invalid plugin");
			}

		}

		public IEnumerator GetEnumerator ()
		{
			return pluginCollection.GetEnumerator ();
		}

		public ArrayList GetLoadedPlugins ()
		{
			return new ArrayList (pluginCollection.Values);
		}

		public void UnloadPlugin (string pluginId)
		{
		}

		public void UpdatePlugin (string pluginId)
		{
		}

		public void LoadPlugin (string pluginId)
		{
		}

		public void DisablePlugin (string pluginId)
		{
		}

		public void EnablePlugin (string pluginId)
		{
		}

		public string MakePluginHash (IAddin plugin)
		{
			return plugin.Name;	
		}

		//FIXME: Check if the plugin has been disabled and do not load it
		private void AddPluginToCollections (IAddin plugin)
		{
			string hash = MakePluginHash (plugin);
			if (pluginCollection.Contains (hash))
				throw new PluginLoadingException ("Plugin already loaded.");
			else
			{
				pluginCollection.Add (hash, plugin);
			}
		}
	}

}

