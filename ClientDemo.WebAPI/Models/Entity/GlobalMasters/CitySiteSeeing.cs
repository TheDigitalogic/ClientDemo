using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class CitySiteSeeing : City
    {
        public long City_site_seeing_id { get; set; }
        public List<SiteSeeing> SiteSeeingList { get; set; }
    }
}

