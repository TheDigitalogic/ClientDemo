using System;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class Destination: EntityBase
    {
        public long Destination_id { get; set; }
        public string Destination_name { get; set; }
        public int Destination_type_id { get; set; }
        public string Destination_type_name { get; set; }
        public string Destination_description { get; set; }
        public string Destination_image { get; set; }
        public Boolean Is_best_selling { get; set; }

    }
}
