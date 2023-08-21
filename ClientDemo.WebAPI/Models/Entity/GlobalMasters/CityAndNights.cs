using System;
using System.Collections.Generic;


namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{

    /// <summary>
    /// This Entity will be replaced with PackageCity entity
    /// </summary>
    public class CityAndNights:EntityBase
    {
            public Int64 City_nights_id { get; set; }
            public Int32 City_id { get; set; }
            public string City_name { get; set; }
            public Int64 Hotel_id { get; set; }
            public Int32 Order_no { get; set; }
            public string Meal_plan_code { get; set; }
            public Int32 Nights { get; set; }
           
            public Int64 Package_city_id { get; set; }
            public List<SiteSeeing> SiteSeeingList { get; set; }
            public String[] SelectedSiteSeeingList { get; set; } //This is list for selected siteseeingEmpty list purpose
            public List<Hotel> HotelList { get; set; }
            public List<HotelMealPlan> MealPlanList { get; set; }
            public HotelMealPlan SelectedHotelMealPlan { get; set; }
            public String[] BlankHotelMealPlanList { get; set; } 



    }
}
