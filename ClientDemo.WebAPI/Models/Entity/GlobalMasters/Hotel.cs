using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class Hotel : EntityBase
    {
        public long City_id { get; set; }
        public string City_name { get; set; }
        public long Destination_id { get; set; }
        public string Destination_name { get; set; }
        public long Destination_type_id { get; set; }

        public long Hotel_id { get; set; }
        public string Hotel_name { get; set; }
        public double Hotel_room_starting_price { get; set; }
        public long Hotel_type { get; set; }

        //Newly Added columns
        public long Hotel_rating { get; set; }  
        public string Address { get; set; }
        public long Country_id { get; set; }

        public string Zip_code { get; set; }
        public string Phone_number { get; set; }
        public string Website { get; set; }

        public List<HotelMealPlan> HotelMealPlanList { get; set; }

        /// <summary>
        ///  Hotel Meal plan selected for this package city
        /// </summary>
        public HotelMealPlan SelectedHotelMealPlan { get; set; }
    }
}
