using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using TravelNinjaz.B2B.WebAPI.Models.Entity;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository mDashboardRepository;
        public DashboardController(DashboardRepository repository)
        {
            
           this.mDashboardRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>July 11, 2023</created_date>
        /// <summary>
        ///  Get Json of Charts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<DestinationDashboard>>> List(String monthYear = "042023", String userId = "admin")
        {
            try
            {
                var DashboardDestination = await Task.Run(() => mDashboardRepository.GetDashboardDestination(monthYear, userId));
                //return new OkObjectResult(new { Data = DashboardDestination, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = DashboardDestination });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<DestinationDashboard>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<DestinationDashboard>() });
            }
        }
        //public async Task<ActionResult> List(String monthYear="042023",String userId="admin")
        //{
        //    try
        //    {
        //        DataTable dashboardTable = await Task.Run(() => mDashboardRepository.GetDashbaord(monthYear, userId));
        //        //string jsonString = JsonConvert.SerializeObject(dashboardTable);

        //        //Trial 1 -----
        //        //directly try to pass datatable 
        //        //---------------


        //        //Trial 2--------
        //        //string JSONString = string.Empty;
        //        //JSONString = JsonConvert.SerializeObject(dashboardTable);
        //        //---------------------------

        //        //Trial 3
        //        // var data = dashboardTable.Rows.OfType<DataRow>()
        //        //.Select(row => dashboardTable.Columns.OfType<DataColumn>()
        //        //    .ToDictionary(col => col.ColumnName, c => row[c]));

        //        // string new_json = System.Text.Json.JsonSerializer.Serialize(data);
        //        //------------------------------------

        //        //ChartJS + Examples + which uses .net core API + datatables
        //        return new OkObjectResult(new { Data = jsonString, Message = "SUCCESS" });
        //        //return new OkObjectResult(200);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BadRequestObjectResult(new
        //        {

        //            Message = ex.Message.ToString()
        //        });
        //    }
        //}
    }
}
