using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IPackageCheckPriceUserFavoriteRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Feb 10, 2023</created_date>
        /// <summary>
        ///  Get All Package Check Price user favorite list
        /// </summary>
        /// <returns> Gets List of all Package Check Price user favorite list</returns>
        List<Entity.PackageCheckPrice> GetPackageCheckPriceUserFavoriteList(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_check_price_id = 0);
        List<Entity.PackageCheckPrice> GetPackageCheckPriceListById(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_check_price_id = 0);
    }
}
