using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class SiteSeeing:EntityBase
    {
        public Int64 City_site_seeing_id { get; set; }
        public Int64 Package_city_id { get; set; }
        public string Site { get; set; }
        public string Rate { get; set; } 
        public Boolean Is_selected { get; set; }

        public Int64 Package_quotation_siteseeing_id { get; set; }


    }
}
