using TravelNinjaz.B2B.WebAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IHotelRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 09, 2022</created_date>
        /// <summary>
        ///  Get All Hotel List
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns> Gets List of all Hotel</returns>
        List<Entity.Hotel> GetHotelList(int type_id = 0);
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 11, 2022</created_date>
        /// <summary>
        ///   Save Hotel details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        String SaveHotel(String city_json, String operation,String UserId);
    }
}
