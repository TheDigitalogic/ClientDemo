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
using DocumentFormat.OpenXml.ExtendedProperties;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        private readonly IHotelRepository mHotelRepository;
        public HotelController(HotelRepository repository)
        {
            this.mHotelRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Nov 09, 2022</created_date>
        /// <summary>
        ///  Get List of Hotel
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<Hotel>>> List(int type_id = 0)
        {
            try
            {
                var hotels = await Task.Run(() => mHotelRepository.GetHotelList(type_id));
                //return new OkObjectResult(new { Data = hotels, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = hotels });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<Hotel>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<Hotel>() });
            }
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 10, 2022</created_date>
        /// <update-1_date>Nov 30, 2022</created_date>
        /// <summary>
        /// Save Hotel Details
        /// </summary>
        /// <param name="hotel_json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddHotel(List<Hotel> HotelList)
        {
            try
            {
                String Operation = Common.Operation.ADD.ToString();
                String Message = "";
                String Hotel_json = "";
                String UserId = "";
                if (HotelList != null && HotelList.Count > 0)
                {

                    Hotel_json = JsonConvert.SerializeObject(HotelList);
                    UserId = HotelList[0].Row_created_by;
                    var result = await Task.Run(() => mHotelRepository.SaveHotel(Hotel_json, Operation, UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "Hotel" + Common.GetMessage(Common.MessageType.InsertSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Hotel" + Common.GetMessage(Common.MessageType.InsertSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Hotel." });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Hotel.", Data = result });
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
        /// <created_date>Nov 30, 2022</created_date>
        /// <summary>
        /// Update Hotel Details
        /// </summary>
        /// <param name="hotel_json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> UpdateHotel(List<Hotel> HotelList)
        {
            try
            {
                String Operation = Common.Operation.UPDATE.ToString(); ;
                String Message = "";
                String Hotel_json = "";
                String UserId = "";
                if (HotelList != null && HotelList.Count > 0)
                {

                    Hotel_json = JsonConvert.SerializeObject(HotelList);
                    UserId = HotelList[0].Row_created_by;
                    var result = await Task.Run(() => mHotelRepository.SaveHotel(Hotel_json, Operation, UserId));
                    
                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "Hotel" + Common.GetMessage(Common.MessageType.UpdateSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Hotel" + Common.GetMessage(Common.MessageType.UpdateSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Hotel." });
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

    }
}
