using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class Transport : EntityBase
    {
        public long Transport_id { get; set; }
        public string Vehicle_name { get; set; }
        public string Vehicle_type { get; set; }
        public  Int16 No_of_seats { get; set; }      
    }
}
