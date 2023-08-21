using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class Package: Destination //: City
    {
        /**This Entity for Package**/
        public Int64 Package_id { get; set; }  //PK
        public Int32 Package_price { get; set; }
        public string Package_name { get; set; }
        public string Package_description { get; set; }
        public Boolean Is_honeymoon_package { get; set; }
        public Boolean Is_family_package { get; set; }
        public Int64 Transport_rate_id { get; set; }
        public string Transport_rate_name { get; set; }
        public double Package_commision { get; set; }
        public string PackageGuideLines { get;set; }
        public double Package_price_before_discount { get; set; }
        public string Package_type { get; set; }
        public DateTime Valid_from { get; set; }
        public DateTime Valid_to { get; set; }

        public List<Inclusion> InclusionsList { get; set; }
        public List<PackageImages> ImageList { get; set; }
        public List<PackageItinerary> Package_itinerary_list { get; set; }
        public TransportRate SelectedTransport { get; set; }

        /*Remove below list*/
       // public List<CityAndNights> CityAndNightsList { get; set; }

        /*New List to be included */
        public List<PackageCity> PackageCityList { get; set; }

    }
}

