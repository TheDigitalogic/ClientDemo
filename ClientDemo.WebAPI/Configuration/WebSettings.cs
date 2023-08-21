using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Configuration
{
    public class WebSettings
    {
        public string ApplicationName { get; set; }

        public string WebURL { get; set; }
        public bool EnableAuditLog { get; set; }

        public string ExpoAsset { get; set; }

        public string AppURL { get; set; }
       
        public string UniversalURL { get; set; }

        public string SystemMode { get; set; }
    }
}
