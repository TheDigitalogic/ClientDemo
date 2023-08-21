using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Configuration
{
    public class EmailSettings
    {
        public string FromEmail { get; set; }
        public bool EnableBCC { get; set; }
        public string BCCEmail { get; set; }
        public string AdminEmail { get; set; }
        public string SupportEmail { get; set; }

    }

}
