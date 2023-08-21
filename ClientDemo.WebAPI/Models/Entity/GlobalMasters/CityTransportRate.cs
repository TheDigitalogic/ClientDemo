using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class CityTransportRate: City
    {
        public long Transport_rate_id { get; set; }
        public string Transport_rate_name { get; set; }
        public double Transport_starting_price { get; set; }
        public List<TransportRate> TransportRateList { get; set; }
    }
}
