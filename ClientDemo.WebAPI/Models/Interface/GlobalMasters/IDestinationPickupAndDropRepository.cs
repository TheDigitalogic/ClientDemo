using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IDestinationPickupAndDropRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Jan 24, 2023</created_date>
        /// <summary>
        ///  Get All DestinationPickupAndDrop
        /// </summary>
        /// <returns> Gets List of all  DestinationPickupAndDrop</returns>
        List<Entity.DestiantionPickupAndDrop> GetPickupAndDropList(int type_id = 0);
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Jan 24, 2023</created_date>
        /// <summary>
        ///   Save PickupAndDrop details (Add/Update)
        /// </summary>
        /// <returns> Boolean </returns>
        string SavePickupAndDrop(string pickupanddrop_json,string UserId);
    }
}
