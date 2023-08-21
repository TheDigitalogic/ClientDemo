using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class UserCompany:EntityBase
    {
        public Int32 UserCompanyId { get; set; }
        public string AspNetUserId { get; set; }
        public string Company_name { get; set; }
        public string Company_phone { get; set; }
        public string Company_email { get; set; }
        public string Company_gst_no { get; set; }
        public string Company_website { get; set; }
        public string Company_city { get; set; }
        public string Company_state { get; set; }
        public string Company_country { get; set; }
        public string CurrencyUnit { get; set; }
        public Int32 Company_zipcode
        {
            get; set;
        }
        public string Company_description { get; set; }
    }
}
