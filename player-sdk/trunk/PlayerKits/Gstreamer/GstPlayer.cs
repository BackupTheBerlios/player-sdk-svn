/*
* Copyright (C) 2004 Jorn Baayen <jorn@nl.linux.org>
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
using System.IO;
using System.Runtime.InteropServices;
using GLib;
using Player.Player;
using Player.Playlist;
using Player.Data;

public class GstPlayer : PlayerKit
{

    private bool stopped = true;
    private IPlaylist playlist;
	private PlayerObject player = new PlayerObject ();
    
    public GstPlayer (IPlaylist playlist)
    {
	    this.playlist = playlist;
		player.TickEvent += TickCallback;
		player.EOSEvent += EosCallback;
	    playing = false;
    }
    
	public override event TickEventHandler TickEvent;
    public override event EOSEventHandler EOSEvent;
    public override event StateEventHandler StateEvent;

    private bool playing;
    public override bool Playing {
	    get {
		    return playing;
	    }

	    set {
		    if (playing == value)
			    return;
			    
		    playing = value;

		    if (playing)
				Play ();
		    else
				Pause ();

		    if (StateEvent != null)
			    StateEvent (playing);
	    }
    }

    public override int Position {
	    get {
			return player.Tell ();
	    }

	    set {
		    player.Seek (value);

		    if (TickEvent != null)
			    TickEvent (value);
	    }
    }

    public override int Volume {
	    get {
		    return player.Volume;
	    }

	    set {
			player.Volume = value;
	    }
    }

    public override IPlaylist Playlist {
		get {
			return playlist;
		}
		set {
			Stop ();
			this.playlist = value;
		}
    }

	private string name = "GstPlayerKit";
	public override string Name {
		get {
			return name;
		}
	}

	private string description = "Gstreamer Player Kit";
	public override string Description {
		get {
			return description;
		}
	}

	private string version = "0.1";
	public override string Version {
		get {
			return version;
		}
	}
	
	protected override void LoadAddin ()
	{
	}

	protected override void UnloadAddin ()
	{
	}

    public override void Play ()
    {

	    Song song = playlist.Current;
		if (song == null || !File.Exists(song.Filename))
		{
			Console.WriteLine ("WARNING: Playlist empty or invalid.");
			return;
		}
	    if (TickEvent != null)
		    TickEvent (0);

	    if (!playing)
			playing = true;
		player.Play (song);
		
    }

    public override void Pause ()
    {
		player.Pause ();
    }
    
    public override void Stop ()
    {
	    if (stopped)
		    return;
		    
	    player.Stop ();
	    stopped = true;

	    if (playing == false)
		    return;

	    playing = false;

	    if (StateEvent != null)
		    StateEvent (playing);
    }
    
    public override void Next ()
    {
		throw new NotImplementedException ();
    }
    
    public override void Previous ()
    {
		throw new NotImplementedException ();
    }
    
    public override void Repeat (RepetitionType type)
    {
		throw new NotImplementedException ();
    }

    public GstPlayer () : this (new Playlist ())
    {
    }

    private void TickCallback (int pos)
    {	
	    if (TickEvent != null)
		    TickEvent (pos);
    }
	private void EosCallback ()
    {
	    if (EOSEvent != null)
		{
		    EOSEvent ();
		}
    }

}

class PlayerObject : GLib.Object
{

    public PlayerObject () : base (IntPtr.Zero)
    {
		g_type_init ();
	    IntPtr error_ptr;
	    
	    Raw = player_new (out error_ptr);
	    if (error_ptr != IntPtr.Zero) {
		    string error = GLib.Marshaller.PtrToStringGFree (error_ptr);

		    throw new Exception (error);
	    }
	    
		ConnectInt.g_signal_connect_data (Raw, "tick", new IntSignalDelegate (TickCallback),
					      IntPtr.Zero, IntPtr.Zero, 0);
	    Connect.g_signal_connect_data (Raw, "end_of_stream", new SignalDelegate (EosCallback),
					   IntPtr.Zero, IntPtr.Zero, 0);
	    ConnectString.g_signal_connect_data (Raw, "error", new StringSignalDelegate (ErrorCallback),
						 IntPtr.Zero, IntPtr.Zero, 0);
    }
	
	~PlayerObject ()
	{
		Dispose ();
	}
    
	public event TickEventHandler TickEvent;
    public event EOSEventHandler EOSEvent;

	public int Tell ()
	{
		return player_tell (Raw);
	}

	public void Seek (int value)
	{
		player_seek (Raw, value);
	}
    
	public int Volume {
	    get {
		    return player_get_volume (Raw);
	    }

	    set {
		    player_set_volume (Raw, value);
	    }
    }
    
	public void Play (Song song)
    {

	    IntPtr error_ptr;

	    player_set_file (Raw, song.Filename , out error_ptr);
	    if (error_ptr != IntPtr.Zero) {
		    string error = GLib.Marshaller.PtrToStringGFree (error_ptr);
		    Console.WriteLine ("Error opening the player");
	    }
	    
	    player_set_replaygain (Raw, song.Gain, song.Peak);
		player_play (Raw);
    }
    
	public void Pause ()
    {
		player_pause (Raw);
    }
    
	public void Stop ()
    {
	    player_stop (Raw);
    }
    
	private void TickCallback (IntPtr obj, int pos)
    {	
	    if (TickEvent != null)
		    TickEvent (pos);
    }
	
	private void ErrorCallback (IntPtr obj, string error)
    {
		Console.WriteLine ("ERROR ErrorCallback: error");
    }
	
	private void EosCallback (IntPtr obj)
    {
	    if (EOSEvent != null)
		    EOSEvent ();
    }

    private delegate void SignalDelegate (IntPtr obj);
    private delegate void IntSignalDelegate (IntPtr obj, int i);
    private delegate void StringSignalDelegate (IntPtr obj, string s);
    /***********************************************************/	
    /********************** Native calls ***********************/
    /***********************************************************/	
	[DllImport("libgobject-2.0.so")]
	private static extern void g_type_init ();
	
    private class Connect
    {
	    [DllImport ("libgobject-2.0-0.dll")]
	    public static extern uint g_signal_connect_data (IntPtr obj, string name,
							     SignalDelegate cb, IntPtr data,
							     IntPtr p, int flags);
    }

    private class ConnectInt
    {
	    [DllImport ("libgobject-2.0-0.dll")]
	    public static extern uint g_signal_connect_data (IntPtr obj, string name,
							     IntSignalDelegate cb, IntPtr data,
							     IntPtr p, int flags);
    }

    private class ConnectString
    {
	    [DllImport ("libgobject-2.0-0.dll")]
	    public static extern uint g_signal_connect_data (IntPtr obj, string name,
							     StringSignalDelegate cb, IntPtr data,
							     IntPtr p, int flags);
    }

    [DllImport ("libgstplayer")]
    private static extern bool player_set_file (IntPtr player,
						string filename,
						out IntPtr error_ptr);
    [DllImport ("libgstplayer")]
    private static extern IntPtr player_new (out IntPtr error_ptr);

    [DllImport ("libgstplayer")]
    private static extern void player_set_volume (IntPtr player,
						  int volume);
    [DllImport ("libgstplayer")]
    private static extern int player_get_volume (IntPtr player);

    [DllImport ("libgstplayer")]
    private static extern void player_seek (IntPtr player,
					    int t);
    [DllImport ("libgstplayer")]
    private static extern int player_tell (IntPtr player);

    [DllImport ("libgstplayer")]
    private static extern void player_play (IntPtr player);

    [DllImport ("libgstplayer")]
    private static extern void player_pause (IntPtr player);

    [DllImport ("libgstplayer")]
    private static extern void player_stop (IntPtr player);

    [DllImport ("libgstplayer")]
    private static extern void player_set_replaygain (IntPtr player,
						      double gain,
						      double peak);

}
