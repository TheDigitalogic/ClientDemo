
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class MealPlan:EntityBase
    {
        public int Meal_plan_id { get; set; }
        public string Meal_plan_code { get; set; }
        public string Meal_plan_desc { get; set; }
        public string Adult_price { get; set; }
        public string Child_price { get; set; }
        public string Child_price_without_bed { get; set; }
    }
}
