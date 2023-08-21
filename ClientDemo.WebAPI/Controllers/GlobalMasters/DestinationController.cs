using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
   //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController : Controller
    {

        private readonly IDestinationRepository mDestinationRepository;
        private readonly IWebHostEnvironment _env;
        public DestinationController(DestinationRepository repository, IWebHostEnvironment env)
        {
            //this.mDestinationRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _env = env;
            this.mDestinationRepository = repository;
        }

        /// <created_by>Manisha Tripathi</created_by>
        /// <created_date>Oct 21, 2022</created_date>
        /// <summary>
        ///  Get List of Destinations
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]

        public async Task<ActionResult<IEnumerable<Destination>>> List(int type_id = 0)
        {
            try
            {
                var destinations = await Task.Run(() => mDestinationRepository.GetDestinationList(type_id));
                //return new OkObjectResult(new { Data = destinations, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = destinations });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<Destination>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<Destination>() });
            }
        }

        /// <created_by>Manisha Tripathi</created_by>
        /// <created_date>Oct 28, 2022</created_date>
        /// <summary>
        ///  Add Destination Details
        /// </summary>
        /// <param name="destination_json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddDestination(List<Destination> DestinationList)
        { 
        try
            {
                String Operation = Common.Operation.ADD.ToString();
                String Message = "";
                String Destination_json = "";
                String UserId = "";

                if (DestinationList != null && DestinationList.Count > 0)
                {

                    Destination_json = JsonConvert.SerializeObject(DestinationList);
                    UserId= DestinationList[0].Row_created_by;
                    var result = await Task.Run(() => mDestinationRepository.SaveDestination(Destination_json, Operation, UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "Destination" + Common.GetMessage(Common.MessageType.InsertSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Destination" + Common.GetMessage(Common.MessageType.InsertSuccess), Data = result });
                    }
                    else
                    {
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed), Data = result });
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Destination." });
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

        /// <created_by>Manisha Tripathi</created_by>
        /// <created_date>Nov 28, 2022</created_date>
        /// <summary>
        /// Update Destination Details
        /// </summary>
        /// <param name="destination_json"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> UpdateDestination(List<Destination> DestinationList)  
        {
            try
            {
                String Operation = Common.Operation.UPDATE.ToString(); ;
                String Message = "";
                String Destination_json = "";
                String UserId = "";
                if (DestinationList != null && DestinationList.Count > 0)
                {

                    Destination_json = JsonConvert.SerializeObject(DestinationList);

                    var result = await Task.Run(() => mDestinationRepository.SaveDestination(Destination_json, Operation, UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "Destination" + Common.GetMessage(Common.MessageType.UpdateSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Destination" + Common.GetMessage(Common.MessageType.UpdateSuccess), Data = result });
                    }
                    else
                    {
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Destination.", Data = result });
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Destination." });
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
        /// <created_date>Dec 28, 2022</created_date>
        /// <summary>
        /// Save Photos
        /// </summary>
        /// <param name="imagefiles"></param>
        /// <returns></returns>
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Images/"+"/Destination/"+ filename;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                //return new JsonResult(filename);
                return new JsonResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Images Uploaded Successfully!", Data = filename });

            }
            catch (Exception)
            {
                //return new JsonResult("error.png");
                return new JsonResult(new { StatusCode = 500, Status = "ERROR", Message = "Images Upload failed!", Data = "" });
            }
        }
    }
}
