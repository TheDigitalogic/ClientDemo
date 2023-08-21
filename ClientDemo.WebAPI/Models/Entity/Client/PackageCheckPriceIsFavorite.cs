using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class PackageCheckPriceIsFavorite:EntityBase
    {
        public long Package_check_price_id { get; set; }
        public Int64 Package_id { get; set; }

        public Boolean Is_favourite { get; set; }
    }
}
