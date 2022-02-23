using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Entities;

namespace WebProject.ViewModels.Songs
{
    public class ShareVM
    {
        public int SongId { get; set; }
        public List<int> UserIds { get; set; }
        public List<UserToSong> Shares { get; set; }

        public List<User> Users { get; set; }
            
        public Song Song { get; set; }
    }
}
