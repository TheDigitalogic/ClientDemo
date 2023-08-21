using System.Collections.Generic;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface ILoad_dataRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>May 22, 2023</created_date>
        /// <summary>
        ///  Get All Destinations List
        /// </summary>
        /// <param ></param>
        /// <returns> Gets List of all Destinations</returns>
        List<Entity.TravellingCompany> GetTravellingDataList();
    }
}
