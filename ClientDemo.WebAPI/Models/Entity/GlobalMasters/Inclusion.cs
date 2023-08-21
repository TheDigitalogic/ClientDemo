using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class Inclusion : EntityBase
    {
        public Int64 Inclusions_id { get; set; }
        public String Inclusions { get; set; }
        public Boolean Is_include { get; set; }
    }
}
