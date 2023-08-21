using TravelNinjaz.B2B.WebAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface ITravellingCompanyRepository
    {

        string ImportFileDataToTable(String Original_file_name, String Temp_file_name, Int32 file_record_count, String xml_file_data, String UserId); //, System.Data.DataTable dsImportTable

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>May 22, 2023</created_date>
        /// <summary>
        ///  Get All Destinations List
        /// </summary>
        /// <param ></param>
        /// <returns> Gets List of all Destinations</returns>
        List<Entity.TravellingCompany> GetTravellingDataList(String status, String state, String city);
        String[] SaveTravellingCompany(string travelling_company_json, string UserId);
    }
   

}
