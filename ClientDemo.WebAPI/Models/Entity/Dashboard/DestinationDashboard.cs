using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class DestinationDashboard
    {
        public long Destination_id { get; set; }
        public string Destination_name { get; set; }
        public int Number_of_quotation { get; set; }
    }
}
