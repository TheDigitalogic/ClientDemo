using System;
using System.Collections.Generic;
using TravelNinjaz.B2B.WebAPI.Models.Entity;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IPackageReviewsRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>July 07, 2023</created_date>
        /// <summary>
        ///  Get All Package Remarks List
        /// </summary>
        /// <returns> Gets List of all Package remarks</returns>
        List<Entity.PackageReviews> GetPackageReviewsList(Int64 Package_id);
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>July 07, 2023</created_date>
        /// <summary>
        ///  Add PackageRemarks
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        String[] SavePackageReviews(string package_remarks_json, string UserId);
    }
}
