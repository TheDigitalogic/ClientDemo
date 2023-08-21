using System.Collections.Generic;

/// <summary>
///  1. Hotel and its Meal Plan details
///  2. It inherits MealPlan
///  3. Its One to One Relationship
/// </summary>

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class HotelMealPlan: MealPlan
    {
        public int Hotel_meal_plan_id { get; set; }

        public int Hotel_id { get; set; }
        public int Package_city_id { get; set; }
  
    }
}
