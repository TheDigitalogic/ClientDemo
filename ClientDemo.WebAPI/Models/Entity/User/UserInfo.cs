using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class UserInfo: EntityBase
    {
        public int UserId { get; set; }
        public string AspNetUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Profession { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Int32 Zipcode { get; set; }
        public string Email { get; set; }
        public string Profile_image { get; set; }
        //public string FullName
        //{
        //    get { return this.FirstName + " " + this.LastName; }
        //}
        //public string Email { get; set; }

        //public string Token { get; set; }

        //public string UserStatus { get; set; }

        //public string UserName { get; set; }

        //public string PhoneNumber { get; set; }

        public bool IsLoggedIn { get; set; } 
    }

}
