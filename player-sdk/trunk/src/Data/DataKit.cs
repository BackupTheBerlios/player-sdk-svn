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

namespace Player.Data 
{
    using System;
    using System.Collections;
	using Player.Addins;

    public abstract class DataKit : AbstractAddin
    {
		public abstract ArrayList Songs { get; }

		public abstract ArrayList Albums { get; }

		public abstract bool AddSong (Song song);

		public abstract bool AddAlbum (Album album);

		public abstract bool RemoveSong (Song song);

		public abstract bool RemoveAlbum (Album album);
    }

}
