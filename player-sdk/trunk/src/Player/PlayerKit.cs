// created on 08/31/2004 at 13:24
namespace Player.Player
{

    using Player.Playlist;
	using Player.Addins;

	public enum RepetitionType {
				    RepeatSong,
				    RepeatPlaylist
				    }
	
	public delegate void EOSEventHandler ();
	public delegate void TickEventHandler (int pos);
	public delegate void StateEventHandler (bool playing);
						
	public abstract class PlayerKit : AbstractAddin {
	
	    public abstract bool Playing {get; set;}
	    public abstract int Position {get; set;}
	    public abstract int Volume {get; set;}
	    public abstract IPlaylist Playlist {get; set;}
	    public abstract void Play ();
	    public abstract void Pause ();
	    public abstract void Stop ();
	    public abstract void Next ();
	    public abstract void Previous ();
	    public abstract void Repeat (RepetitionType type);
	    
	    public abstract event EOSEventHandler EOSEvent;
	    public abstract event TickEventHandler TickEvent;
	    public abstract event StateEventHandler StateEvent;
		
	}
}
