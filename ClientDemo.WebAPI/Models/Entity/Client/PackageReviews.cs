using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class PackageReviews : EntityBase
    {
        public long Package_reviews_id { get; set; }
        public long Package_id { get; set; }
        public string Reviews { get; set; }
        public int Reviews_rating { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
    }
}
