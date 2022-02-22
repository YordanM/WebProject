using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Entities
{
    public class UserToSong
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("SongId")]
        public virtual Song Song { get; set; }
    }
}
