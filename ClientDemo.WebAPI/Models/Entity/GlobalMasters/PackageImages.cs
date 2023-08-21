using System;
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class PackageImages:EntityBase
    {
        public Int64 Package_image_id { get; set; }
        public Boolean Is_primary { get; set; }
        public Int64 Package_id { get; set; }
        public string Image_name { get; set; }
    }
}
