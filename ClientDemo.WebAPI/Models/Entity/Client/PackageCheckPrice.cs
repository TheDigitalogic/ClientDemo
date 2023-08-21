using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class PackageCheckPrice:Package
    {
        public long Package_check_price_id { get; set; }
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

        /*New changes */
        //Already  Inherited within Package Entity
        //public List<PackageCity> PackageCityList { get; set; }

    }
}
