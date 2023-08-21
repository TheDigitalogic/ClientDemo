using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IMealPlanRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 21, 2022</created_date>
        /// <summary>
        ///  Get All Meal Plan List
        /// </summary>
        /// <returns> Gets List of all Meal Plan</returns>
        List<Entity.MealPlan> GetMealPlanList();
    }
}
