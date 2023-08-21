using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class Package_check_price_save_margin:EntityBase
    {
        public long Package_check_price_id { get; set; }
        public Int64 Package_id { get; set; }
        public Decimal Margin { get; set; }
        public String MarginDescription { get; set; }
    }
}
