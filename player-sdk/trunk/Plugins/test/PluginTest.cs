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

using System;    
using Player.Addins;

[assembly:PluginInfo ("PluginTest")]
public class PluginTest : AbstractAddin
{
	public PluginTest ()
	{
	}

	private string version = "0.1";
	public override string Version {
		get {
			return version;
		}
	}

	private string name = "PluginTest";
	public override string Name {
		get {
			return name;
		}
	}
	
	private string description = "Plugin Test example";
	public override string Description {
		get {
			return description;
		}
	}

	protected override void LoadAddin ()
	{
		base.Load ();
		Console.WriteLine ("Calling Load in {0}.", Name);
	}

	protected override void UnloadAddin ()
	{
		base.Unload ();
		Console.WriteLine ("Calling Unload in {0}.", Name);
	}
}

