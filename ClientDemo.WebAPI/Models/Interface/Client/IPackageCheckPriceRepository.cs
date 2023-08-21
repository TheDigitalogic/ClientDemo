using System;
using System.Collections.Generic;
using TravelNinjaz.B2B.WebAPI.Models.Entity;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IPackageCheckPriceRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Feb 04, 2023</created_date>
        /// <summary>
        ///  Get All PackageList
        /// </summary>
        /// <returns> Gets List of all Package</returns>
        List<Entity.Package> GetPackageCheckPriceList(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0,String UserId="");
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Jan 31, 2023</created_date>
        /// <summary>
        ///   Save PackageCheckPrice details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String[] SavePackageCheckPrice(string package_check_price_json, string UserId);/// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Jan 31, 2023</created_date>
        /// <summary>
        ///   Update favorite package details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String[] UpdateFavoritePackage(Int64 Package_id, Int64 Package_check_price_id,Boolean IsFavorite, string UserId);/// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Jan 31, 2023</created_date>
        /// <summary>
        ///   Save marginDescripton details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String[] UpdateMarginDescriptionPackage(Int64 Package_id,Int64 Package_check_price_id, decimal Margin, String MarginDescription,  string UserId);
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>March 28, 2023</created_date>
        /// <summary>
        ///   Calculate_checkPrice
        /// </summary>
        /// <returns> Boolean </returns>
        String[] CalculateLatestPrice(string package_check_price_json,string UserId);

    }
}
