using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class PackageQuotationIsFavorite : EntityBase
    {
        public long Package_quotation_id { get; set; }
        public Int64 Package_id { get; set; }

        public Boolean Is_favourite { get; set; }
    }
}
