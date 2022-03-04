using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ViewModels.Songs
{
    public class CreateVM
    {
        public int OwnerId { get; set; }

        [DisplayName("Music Title: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Tittle { get; set; }

        [DisplayName("Music Genre: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Genre { get; set; }


        [DisplayName("Music Language: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Lang { get; set; }
        
    }
}
