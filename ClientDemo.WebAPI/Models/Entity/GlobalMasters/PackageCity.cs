using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    /// <summary>
    /// This class is associated with Package master
    /// Package can have One or Many City
    /// </summary>
    public class PackageCity
    {
        /// <summary>
        /// Package_city_id - Primary Key
        /// </summary>
        public Int64 Package_city_id { get; set; }

        /// <summary>
        ///  Key - Unique key generated for each row
        /// </summary>
        public Int64 Key { get; set; }


        /// <summary>
        /// City Id
        /// </summary>
        public long City_id { get; set; }

        /// <summary>
        /// City Name
        /// </summary>
        public string City_name { get; set; }

        /// <summary>
        /// City Order No (It will be used to differenciate cities in package. It is possible that one city is included multiple times in a single package) 
        /// Package Himalay - cities included are [Chandigarh, Kasol, Shimla, Manali, Chandigarh]. Here Order of chandigarh will be 1, 5 respectively
        /// </summary>
        public Int32 Order_no { get; set; }

        /// <summary>
        /// Package Id that is associated with this City (FK)
        /// </summary>
        public Int64 Package_id { get; set; }

        /// <summary>
        /// Package City and its Hotel List
        /// It can be One or Many
        /// </summary>
        public List<PackageCityHotel> PackageCityHotelList { get; set; }


        ///// <summary>
        ///// All list of City and Hotel Master
        ///// </summary>
        //public List<Hotel> HotelList { get; set; }

        ////HotelMealPlanList is included wih Hotel Property//

        ///// <summary>
        /////  Hotel Meal plan selected for this package city
        ///// </summary>
        //public HotelMealPlan SelectedHotelMealPlan { get; set; }

        /// <summary>
        /// List of Site Seeing that are associated with this City
        /// It can be One or Many
        /// </summary>
        public List<SiteSeeing> SiteSeeingList { get; set; }

        /// <summary>
        /// List of Site Seeing that are already saved in Database for this City
        /// It can be One or Many
        /// </summary>
        public List<SiteSeeing> SelectedSiteSeeingList { get; set; }

     


    }
}
