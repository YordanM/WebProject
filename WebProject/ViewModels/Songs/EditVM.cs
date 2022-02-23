using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ViewModels.Songs
{
    public class EditVM
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        [DisplayName("Tittle: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Tittle { get; set; }

        [DisplayName("Description: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Genre { get; set; }
    }
}
