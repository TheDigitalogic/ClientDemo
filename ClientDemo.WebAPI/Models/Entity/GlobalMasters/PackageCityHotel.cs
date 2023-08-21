using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{

    /// <summary>
    /// This class is associated with Package City Hotel 
    /// Package City can have One or Hotel
    /// </summary>
    public class PackageCityHotel
    {
        /// <summary>
        ///  PackageCityHotelId - PK
        /// </summary>
        public Int64 PackageCityHotelId { get; set; }

        /// <summary>
        /// Hotel, associated with this PackageCity (FK)
        /// </summary>
        public Hotel Hotel { get; set; }

        /// <summary>
        ///  Hotel Nights
        /// </summary>
        public Int16 Nights { get; set; }


        ////HotelMealPlanList is included wih Hotel Property//

        ///// <summary>
        /////  Hotel Meal plan selected for this package
        ///// </summary>
        //public HotelMealPlan SelectedHotelMealPlan { get; set; }

    }
}
