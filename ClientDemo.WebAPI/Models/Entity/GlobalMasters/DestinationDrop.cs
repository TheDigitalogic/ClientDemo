using System;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class DestinationDrop:EntityBase
    {
        public Int32 Drop_location_id { get; set; }
        public string Drop_location_name { get; set; }
    }
}
