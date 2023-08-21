using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IPackageQuotationUserFavoriteRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Feb 10, 2023</created_date>
        /// <summary>
        ///  Get All Package Check Price user favorite list
        /// </summary>
        /// <returns> Gets List of all Package Check Price user favorite list</returns>
        List<Entity.PackageQuotation> GetPackageQuotationUserFavoriteList(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_quotation_id = 0);
        List<Entity.PackageQuotation> GetPackageQuotationListById(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_quotation_id = 0);
    }
}
