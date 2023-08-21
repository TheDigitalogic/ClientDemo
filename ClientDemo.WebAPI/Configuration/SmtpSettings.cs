using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Configuration
{
    public class SmtpSettings
    {
        //public string ServerName { get; set; }
        //public string Port { get; set; }
        //public bool EnableSSL { get; set; }
        //public bool AuthRequired { get; set; }
        //public string Username { get; set; }
        //public string Password { get; set; }
        //public string SendGrideKey { get; set; }

        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }



    }
}
