using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Entities;

namespace WebProject.ViewModels.Playlists
{
    public class AddSongVM
    {
        public int SongId { get; set; }
        public Song Song { get; set; }
        public List<int> PlaylistIds { get; set; }
        public List<SongToPlaylist> Shares { get; set; }
        public List<Song> Songs { get; set; }
    }
}
