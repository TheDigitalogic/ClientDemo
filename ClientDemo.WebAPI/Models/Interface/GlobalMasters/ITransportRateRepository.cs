using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface ITransportRateRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 13, 2022</created_date>
        /// <summary>
        ///  Get All Transport Rate
        /// </summary>
        /// <returns> Gets List of all Transport Rate</returns>
        List<Entity.CityTransportRate> GetCityTransportRateList();
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 13, 2022</created_date>
        /// <summary>
        ///   Save Transport Rate details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String SaveTransportRate(String city_transport_rate_json,String UserId);
    }
}
