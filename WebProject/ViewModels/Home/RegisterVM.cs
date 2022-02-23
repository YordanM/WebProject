using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ViewModels.Home
{
    public class RegisterVM
    {
        [Display(Prompt = "Username")]
        public string Username { get; set; }

        [Display(Prompt = "Password")]
        public string Password { get; set; }

        [Display(Prompt = "Retype the password")]
        public string rPassword { get; set; }

        [Display(Prompt = "First name")]
        public string FirstName { get; set; }

        [Display(Prompt = "Last name")]
        public string LastName { get; set; }
    }
}
