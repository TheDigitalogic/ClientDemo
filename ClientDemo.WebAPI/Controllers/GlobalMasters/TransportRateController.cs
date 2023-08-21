using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransportRateController : Controller
    {
        private readonly ITransportRateRepository mTransportRateRepository;
        public TransportRateController(TransportRateRepository repository)
        {
            this.mTransportRateRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Dec 13, 2022</created_date>
        /// <summary>
        ///  Get List of Transport Rate
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<CityTransportRate>>> List()
        {
            try
            {
                var transportrate = await Task.Run(() => mTransportRateRepository.GetCityTransportRateList());
                //return new OkObjectResult(new { Data = transportrate, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = transportrate });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<TransportRate>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<TransportRate>() });
            }
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 13, 2022</created_date>
        /// <summary>
        /// Save Transport Rate Details
        /// </summary>
        /// <param name="transport_rate_json"></param>
        /// <returns></returns>
        /// 
        /// This method for save parent child
        //[HttpPost]
        //[Route("Save")]
        //public async Task<ActionResult> Save(TransportRate TransportRate)
        //{
        //    try
        //    {
        //        String Message = "";
        //        String transport_rate_json = "";

        //        if (TransportRate != null)
        //        {

        //            transport_rate_json = JsonConvert.SerializeObject(TransportRate);

        //            var result = await Task.Run(() => mTransportRateRepository.SaveTransportRate(transport_rate_json));

        //            if (Convert.ToString(result).ToUpper() == "SUCCESS")
        //            {
        //                return new OkObjectResult(new { Data = result, Message = "Transport Rate" + Common.GetMessage(Common.MessageType.SaveSuccess) });
        //            }
        //            else
        //            {
        //                return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Transport Rate." });
        //            }

        //        }
        //        ////Data is Empty
        //        else
        //            return new BadRequestObjectResult(new { Message = "Data is Empty" });

        //    }
        //    catch (Exception ex)
        //    {
        //        return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
        //    }
        //}
        [HttpPost]
        [Route("Save")]
        //public async Task<ActionResult> Save(List<TransportRate> TransportRateList)
        public async Task<ActionResult> Save(CityTransportRate CityTransportRate)
        {
            try
            {
                String Message = "";
                String city_transport_rate_json = "";
                String UserId = "";
                if (CityTransportRate != null)
                {

                    city_transport_rate_json = JsonConvert.SerializeObject(CityTransportRate);
                    UserId = CityTransportRate.Row_created_by;
                    var result = await Task.Run(() => mTransportRateRepository.SaveTransportRate(city_transport_rate_json,UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "Transport Rate" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Transport Rate" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });

                    }
                    else
                    {

                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Transport Rate.", Data = "" });
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Transport Rate." });
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
    }
}
