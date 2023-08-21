using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class TransportRate: Transport
    {
        public long Transport_rate_id { get; set; }

       // public long Transport_id { get; set; }
        public string Vehicle_price { get; set; }
    }
}
