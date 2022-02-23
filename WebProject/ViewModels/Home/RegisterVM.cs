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
        [MinLength(6)]
        [MaxLength(15)]
        [Display(Prompt = "Username")]
        
        public string Username { get; set; }

        [MinLength(6)]
        [MaxLength(15)]
        [Display(Prompt = "Password")]
        public string Password { get; set; }


        [Required]
        [MaxLength(15)]
        [Display(Prompt = "Retype the password")]
        public string rPassword { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Prompt = "First name")]
        public string FirstName { get; set; 
        }
        [Required]
        [MaxLength(15)]
        [Display(Prompt = "Last name")]
        public string LastName { get; set; }
    }
}
