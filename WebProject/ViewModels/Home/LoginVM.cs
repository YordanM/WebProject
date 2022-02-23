using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ViewModels.Home
{
    public class LoginVM
    {
        [Display(Prompt = "Username")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Username { get; set; }

        [Display(Prompt = "Password")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Password { get; set; }
    }
}
