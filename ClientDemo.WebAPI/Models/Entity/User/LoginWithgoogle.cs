using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class LoginWithgoogle:EntityBase
    {
        //[Required]
        //public string Token { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public Boolean Email_verified { get; set; }
        public String Role { get; set; }
        public Int32 UserId { get; set; }
        public string AspNetUserId { get; set; }
        public string Social_media_type { get; set; }
        public string FromApplication { get; set; }
        public List<string> Roles { get; set; }
    }
}
