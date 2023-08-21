using System;
using System.Collections.Generic;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class PackageQuotationSaveMargin : EntityBase
    {
        public long Package_quotation_id { get; set; }
        public Int64 Package_id { get; set; }
        public Decimal Margin { get; set; }
        public String MarginDescription { get; set; }
    }
}
