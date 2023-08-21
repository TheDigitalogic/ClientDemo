using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using Newtonsoft.Json;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using Microsoft.AspNetCore.Authorization;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationPickupAndDropController : Controller
    {
        private readonly IDestinationPickupAndDropRepository mPickupAndDropRepository;
        public DestinationPickupAndDropController(DestinationPickupAndDropRepository repository)
        {
            this.mPickupAndDropRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Jan 24, 2023</created_date>
        /// <summary>
        ///  Get List of PickupAndDrop
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<DestiantionPickupAndDrop>>> List(int type_id = 0)
        {
            try
            {
                var PickupAndDrop = await Task.Run(() => mPickupAndDropRepository.GetPickupAndDropList(type_id));
                //return new OkObjectResult(new { Data = PickupAndDrop, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = PickupAndDrop });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<DestiantionPickupAndDrop>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Jan 24, 2023</created_date>
        /// <summary>
        /// Save PickupAndDrop Details
        /// </summary>
        /// <param name="pickupanddrop_json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<ActionResult> SavePickupAndDrop(DestiantionPickupAndDrop DestiantionPickupAndDrop)
        {
            try
            {
                string Message = "";
                string pickupanddrop_json = "";
                string UserId = "";
                if (DestiantionPickupAndDrop != null)
                {

                    pickupanddrop_json = JsonConvert.SerializeObject(DestiantionPickupAndDrop);
                    UserId = DestiantionPickupAndDrop.Row_created_by;
                    var result = await Task.Run(() => mPickupAndDropRepository.SavePickupAndDrop(pickupanddrop_json, UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "PickupDrop" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "PickupDrop" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "PickupAndDrop." });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "PickupAndDrop.", Data = result });
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
