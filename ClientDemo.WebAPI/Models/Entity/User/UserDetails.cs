using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class UserCredentialDetails: EntityBase
    {
        [Required]
        public string UserName { get; set; }

       
        public string Password { get; set; }

     
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

      
        public string OldRole { get; set; }             

        public List<string> Roles { get; set; }
    }
}
