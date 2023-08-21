using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using TravelNinjaz.B2B.WebAPI.Models.Repository;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessLogsController : Controller
    {
        private readonly IProcessLogsRepository mProcessLogsRepository;
        private readonly IWebHostEnvironment _env;
        public ProcessLogsController(ProcessLogsRepository repository, IWebHostEnvironment env)
        {
            _env = env;
            mProcessLogsRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>June 30, 2023</created_date>
        /// <summary>
        ///  Get List of Process Logs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<ProcessLogs>>> List(DateTime fromDate,DateTime toDate)
        {
            try
            {
                var ProcessLog = await Task.Run(() => mProcessLogsRepository.GetProcessLogsList(fromDate,toDate));
                //return new OkObjectResult(new { Data = ProcessLog, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = ProcessLog });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<ProcessLogs>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<ProcessLogs>() });
            }
        }
    }
}
