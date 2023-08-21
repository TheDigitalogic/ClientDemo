using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelNinjaz.B2B.WebAPI.Models.Entity;

namespace ClientDemo.WebAPI.Models.Entity
{
    public class Organization:EntityBase
    {
        public Int32 OrganizationId { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
    }
}
