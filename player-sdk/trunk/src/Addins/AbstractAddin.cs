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

	public abstract class AbstractAddin : IAddin
	{

		public abstract string Version { get; }
		public abstract string Name { get; }
		public abstract string Description { get; }
		
		//Template method	
		public void Load ()
		{
			Load ();
			if (Loaded != null)
				Loaded (this, EventArgs.Empty);
		}
			
		protected abstract void LoadAddin ();

		//Template method
		public  void Unload ()
		{
			Unload ();
			if (Unloaded != null)
				Unloaded (this, EventArgs.Empty);
		}
		
		protected abstract void UnloadAddin ();

		public event EventHandler Loaded;
		public event EventHandler Unloaded;
	}

}

