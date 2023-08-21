using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IPackageRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 19, 2022</created_date>
        /// <summary>
        ///  Get All Package
        /// </summary>
        /// <returns> Gets List of all Package</returns>
        List<Entity.Package> GetPackageList(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "");


        /// <summary>
        ///  For a particular Package - Get he list of Cities and its Hotels (Along with Hotels MealPlan list)
        /// </summary>
        /// <created_by>Manisha Tripathi</created_date>
        /// <created_date>July 24, 2023</created_date>
        /// <summary>
        ///  Get All PackageList
        /// </summary>
        /// <returns> Gets List of all Package City</returns>
        List<Entity.PackageCity> PackageCityHotelMealPlanList(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "");


        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 19, 2022</created_date>
        /// <summary>
        ///   Save Package details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String[]  SavePackage(string package_json,string UserId);
    }
}
