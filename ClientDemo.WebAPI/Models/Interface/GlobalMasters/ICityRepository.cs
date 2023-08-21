using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface ICityRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 05, 2022</created_date>
        /// <summary>
        ///  Get All City List
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns> Gets List of all City</returns>
        List<Entity.City> GetCityList(int type_id = 0);
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 10, 2022</created_date>
        /// <summary>
        ///   Save City details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String SaveCity(String city_json, String operation,String UserId);
    }
}
