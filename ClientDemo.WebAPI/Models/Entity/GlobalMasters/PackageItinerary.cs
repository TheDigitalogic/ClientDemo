using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class PackageItinerary:EntityBase
    {
        public Int64 Package_itinerary_id { get; set; }
        public Int64 Day { get; set; }
        public string Itinerary_title{ get; set; }
        public string Itinerary_description { get; set; }
    }
}
