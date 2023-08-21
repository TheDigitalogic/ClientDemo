using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface ITransportRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 2, 2022</created_date>
        /// <summary>
        ///  Get All Transport List
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns> Gets List of all Transport</returns>
        List<Entity.Transport> GetTransportList(int type_id = 0);

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 2,2022</created_date>
        /// <summary>
        ///   Save Transport details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String SaveTransport(String transport_json, String operation, String UserId);

    }
}
