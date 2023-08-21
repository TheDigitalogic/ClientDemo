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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitySiteSeeingController : Controller
    {
        private readonly ICitySiteSeeingRepository mCitySiteSeeingRepository;
        public CitySiteSeeingController(CitySiteSeeingRepository repository)
        {
            this.mCitySiteSeeingRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Dec 21, 2022</created_date>
        /// <summary>
        ///  Get List of City Site Seeing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<CitySiteSeeing>>> List(int type_id = 0)
        {
            try
            {
                var CitySiteSeeing = await Task.Run(() => mCitySiteSeeingRepository.GetCitySiteSeeingList(type_id));
                //return new OkObjectResult(new { Data = CitySiteSeeing, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = CitySiteSeeing });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<CitySiteSeeing>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<CitySiteSeeing>() });
            }
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 21, 2022</created_date>
        /// <summary>
        /// Save City Site Seeing Details
        /// </summary>
        /// <param name="city_site_seeing_json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<ActionResult> Save(CitySiteSeeing objCitySiteSeeing)
        {
            try
            {
                String Message = "";
                String city_site_seeing_json = "";
                String UserId = "";
                if (objCitySiteSeeing != null)
                {

                    city_site_seeing_json = JsonConvert.SerializeObject(objCitySiteSeeing);
                    UserId = objCitySiteSeeing.Row_created_by;
                    var result = await Task.Run(() => mCitySiteSeeingRepository.SaveCitySiteSeeing(city_site_seeing_json, UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "City Site Seeing" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "City Site Seeing" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "City Site Seeing." });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "City Site Seeing.", Data = result });
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
