namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class TravellingCompany:EntityBase
    {
        public long Travelling_company_id { get; set; }
        public string Company_name { get; set; }
        public string Mobile_1 { get; set; }
        public string Mobile_2 { get; set; }
        public string Email_id_1 { get; set; }
        public string Email_id_2 { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Landline { get;set ;}

    }
}
