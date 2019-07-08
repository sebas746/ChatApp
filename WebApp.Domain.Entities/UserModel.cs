using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.Entities
{
    public class UserModel
    {
        public int Userid { get; set; }
        [Required(ErrorMessage = "Email id Is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile Number Is required")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Password Is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is required")]
        [Compare("password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }        
    }
}
