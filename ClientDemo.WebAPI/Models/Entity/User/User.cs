using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class User: UserCredentialDetails
    {
        public Int32 UserId { get; set; }
        public string AspNetUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string FullName  { get; set; }

        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
            set { }
        }

        public string Phone { get; set; }
        public string Profession { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Int32 Zipcode { get; set; }
        public string Title { get; set; }
         
        public string ProfileImageBase64 { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileImageTemp { get; set; }
        public string Social_media_type { get; set; }
        public string FromApplication { get; set; }
        public string Profile_image { get; set; }
        public UserCompany Usercompany { get; set; }

    }
}
