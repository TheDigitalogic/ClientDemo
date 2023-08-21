using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class DestinationPickup:EntityBase
    {
        public Int32 Pickup_location_id { get; set; }
        public string Pickup_location_name { get; set; }

    }
}
