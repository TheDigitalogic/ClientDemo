using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Authorization;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : Controller
    {

        private readonly ITransportRepository mTransportRepository;

        public TransportController(TransportRepository repository)
        {
            //this.mTransportRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mTransportRepository = repository;
        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 2, 2022</created_date>
        /// <summary>
        ///  Get List of Transport
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]

        public async Task<ActionResult<IEnumerable<Transport>>> List(int type_id = 0)
        {
            try
            {
                var transport = await Task.Run(() => mTransportRepository.GetTransportList(type_id));
                //return new OkObjectResult(new { Data = transport, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = transport });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<Transport>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<Transport>() });
            }
        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 2, 2022</created_date>
        /// <summary>
        ///  Add Transport Details
        /// </summary>
        /// <param name="Transport_json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddTransport(List<Transport> TransportList)
        {
            try
            {
                String Operation = Common.Operation.ADD.ToString();
                String Message = "";
                String Transport_json = "";
                String UserId = "";
                if (TransportList != null && TransportList.Count > 0)
                {

                    Transport_json = JsonConvert.SerializeObject(TransportList);
                    UserId = TransportList[0].Row_created_by;
                    var result = await Task.Run(() => mTransportRepository.SaveTransport(Transport_json, Operation,UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "Transport" + Common.GetMessage(Common.MessageType.InsertSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Transport" + Common.GetMessage(Common.MessageType.InsertSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Transport." });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed), Data = result });
                    }

                }
                ////Data is Empty
                else
                    //return new BadRequestObjectResult(new { Message = "Data is Empty" });
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Data is Empty", Data = "" });

            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }
        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 2, 2022</created_date>
        /// <summary>
        /// Update Transport Details
        /// </summary>
        /// <param name="Transport_json"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> UpdateTransport(List<Transport> TransportList)
        {
            try
            {
                String Operation = Common.Operation.UPDATE.ToString(); ;
                String Message = "";
                String Transport_json = "";
                String UserId = "";
                if (TransportList != null && TransportList.Count > 0)
                {

                    Transport_json = JsonConvert.SerializeObject(TransportList);
                    UserId = TransportList[0].Row_created_by;
                    var result = await Task.Run(() => mTransportRepository.SaveTransport(Transport_json, Operation, UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Transport" + Common.GetMessage(Common.MessageType.UpdateSuccess), Data = result });
                        //return new OkObjectResult(new { Data = result, Message = "Transport" + Common.GetMessage(Common.MessageType.UpdateSuccess) });
                    }
                    else
                    {
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Transport.", Data = "" });
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Transport." });
                    }

                }
                ////Data is Empty
                else
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Data is Empty", Data = "" });
                //return new BadRequestObjectResult(new { Message = "Data is Empty" });

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
                //return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
            }
        }
    }
}
