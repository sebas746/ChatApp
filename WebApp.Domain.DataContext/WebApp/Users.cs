using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.DataContext.WebApp
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Mobile { get; set; }

        [StringLength(500)]
        public string Password { get; set; }
    }
}
