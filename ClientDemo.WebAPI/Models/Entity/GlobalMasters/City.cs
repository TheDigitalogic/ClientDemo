using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{

    /// <summary>
    ///  City Master entity
    /// </summary>
    public class City : EntityBase
    {
        /// <summary>
        /// City Id - PK
        /// </summary>
        public long City_id { get; set; }      
        
        /// <summary>
        /// Name of City
        /// </summary>
        public string City_name { get; set; }
              
        /// <summary>
        /// Destination Type Id that is associated with this City (FK)
        /// </summary>
        public int Destination_type_id { get; set; }

        /// <summary>
        /// Destination Id that is associated with this City (FK)
        /// </summary>
        public long Destination_id { get; set; }

        /// <summary>
        /// Destination Name that is associated with this City (FK)
        /// </summary>
        public string Destination_name { get; set; }
    }
}
