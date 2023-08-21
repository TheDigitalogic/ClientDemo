using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class PackageCity_OLD : EntityBase
    {
        public long City_id { get; set; }        
        public long Hotel_id { get; set; }
        public string Meal_plan_code { get; set; }
        public int Nights{ get; set; }
       public int Package_city_id { get; set; }

        public Int64 City_nights_id { get; set; }

        public int Package_check_price_id { get; set; }
        public List<SiteSeeing> SiteSeeingList { get; set; }


    }
}
