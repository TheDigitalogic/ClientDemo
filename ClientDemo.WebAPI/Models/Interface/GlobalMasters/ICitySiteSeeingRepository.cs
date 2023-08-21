using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{ 
    public interface ICitySiteSeeingRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 21, 2022</created_date>
        /// <summary>
        ///  Get All City Site Seeing
        /// </summary>
        /// <returns> Gets List of all City Site Seeing</returns>
        List<Entity.CitySiteSeeing> GetCitySiteSeeingList(int type_id = 0);
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 21, 2022</created_date>
        /// <summary>
        ///   Save City Site Seeing details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String SaveCitySiteSeeing(String city_site_seeing_json, String UserId);
    }
}
