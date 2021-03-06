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
using Player.Player;
using Player.Playlist;
using Player.Data;
using Player.Kits;
using Player.Global;
using Gtk;

public class PlayerTest
{
	public static void Main ()
	{
		Application.Init ();
		PlayerServices.Init ();
		Playlist p = new Playlist ();
		p.Add (new Song ("test.ogg"));
		PlayerKit kit = (PlayerKit) PlayerServices.KitManager ["PlayerKit"];
		kit.EOSEvent += EndOfStream;
		kit.Volume = 50;
		kit.Playlist = p;
		kit.Play ();
		Application.Run ();
	}
	public static void EndOfStream ()
	{
		Application.Quit ();
	}
}

