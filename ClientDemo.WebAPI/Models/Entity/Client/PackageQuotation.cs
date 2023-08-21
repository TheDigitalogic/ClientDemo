using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class PackageQuotation : Package
    {

        public long Package_quotation_id { get; set; }

        public string Quotation_id { get; set; }

        public long Transport_id { get; set; }

        public long Pickup_location_id { get; set; }

        public long Drop_location_id { get; set; }
        public string Company_name { get; set; }
        public string Email { get; set; }
        public Boolean Flight_booked { get; set; }
        public Boolean Is_favourite { get; set; }
        public string Lead_pasanger_name { get; set; }
        public Int64 Number_of_cabs { get; set; }
        public Int64 Number_of_childrens { get; set; }
        public Int64 Number_of_kids { get; set; }
        public Int64 Number_of_persons { get; set; }
        public Int64 Number_of_rooms { get; set; }

        public DateTime Travel_date { get; set; }
        public String Phone { get; set; }
        public Decimal Package_calculate_price { get; set; }
        public Decimal Margin { get; set; }
        public String MarginDescription { get; set; }

        public Int64 Package_reviews_rating_count { get; set; }
        public Decimal Package_reviews_rating_average { get; set; }

    }
}
