using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class LoginCredentials
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        //public Common.SocialMediaType Social_media_type { get; set; }
        public string Social_media_type { get; set; }
        public LoginCredentials()
        {
        }

        //Google OR Facebook
        public LoginCredentials(string username, string social_media_type)
        {
            Username = username;
            Social_media_type = social_media_type;
        }

        //Application 
        public LoginCredentials(string username, string password, string social_media_type)
        {
            Username = username;
            Password = password;
            Social_media_type = social_media_type;
        }
    }

  
}
