using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IDestinationRepository
    {
        /// <created_by>Manisha Tripathi</created_by>
        /// <created_date>Oct 21, 2022</created_date>
        /// <summary>
        ///  Get All Destinations List
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns> Gets List of all Destinations</returns>
        List<Entity.Destination> GetDestinationList(int type_id = 0);

        /// <created_by>Manisha Tripathi</created_by>
        /// <created_date>Oct 28, 2022</created_date>
        /// <summary>
        ///   Save Destination details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String SaveDestination(String destination_json, String operation, String UserId);

    }
}
