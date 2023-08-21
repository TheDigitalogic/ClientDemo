using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IHotelMealPlanRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 22, 2022</created_date>
        /// <summary>
        ///  Get All Hotel Meal Plan List
        /// </summary>
        /// <returns> Gets List of all Hotel Meal Plan</returns>
        List<Entity.Hotel> GetHotelMealPlanList(Int64 city_id = 0, Int64 hotel_id = 0);
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 28, 2022</created_date>
        /// <summary>
        ///   Save Hotel Meal plan details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String SaveHotelMealPlan(String hotel_meal_plan_json,String UserId);
    }
}
