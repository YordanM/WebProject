using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Entities;

namespace WebProject.ViewModels.Playlists
{
    public class ContextVM
    {
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
        public List<Song> Songs { get; set; }

        [DisplayName("Add a new comment: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        [MinLength(10, ErrorMessage = "*Comment must be minimum 10 charachters.")]
        [MaxLength(50, ErrorMessage = "*Comment must be maximum 50 charachters.")]
        public String AddComment { get; set; }
        public List<Comment> Comments { get; set; }

        
    }
}
