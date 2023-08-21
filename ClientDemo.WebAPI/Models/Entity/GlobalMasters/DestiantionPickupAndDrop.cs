using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class DestiantionPickupAndDrop : Destination
    {
        public Int64 Pickup_drop_id { get; set; }
        public List<DestinationPickup> PickupLocation { get; set; }
        public List<DestinationDrop> DropLocation { get; set; }
    }
}
