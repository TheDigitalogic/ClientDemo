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
    public class CityController : ControllerBase
    {
        private readonly ICityRepository mCityRepository;
        public CityController(CityRepository repository)
        {
            this.mCityRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Nov 05, 2022</created_date>
        /// <summary>
        ///  Get List of City
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<City>>> List(int type_id = 0)
        {
            try
            {
                var cities = await Task.Run(() => mCityRepository.GetCityList(type_id));
                //return new OkObjectResult(new { Data = cities, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = cities });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<City>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<City>() });
            }
        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 10, 2022</created_date>
        /// <update-1_date>Nov 29, 2022</created_date>
        /// <summary>
        /// Save City Details
        /// </summary>
        /// <param name="city_json"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddCity(List<City> CityList)
        {
            try
            {
                String Operation = Common.Operation.ADD.ToString();
                String Message = "";
                String City_json = "";
                String UserId = "";
                if (CityList != null && CityList.Count > 0)
                {

                    City_json = JsonConvert.SerializeObject(CityList);
                    UserId = CityList[0].Row_created_by;
                    var result = await Task.Run(() => mCityRepository.SaveCity(City_json, Operation, UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "City" + Common.GetMessage(Common.MessageType.InsertSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "City" + Common.GetMessage(Common.MessageType.InsertSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "City." });
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
        /// <created_date>Nov 29, 2022</created_date>
        /// <summary>
        /// Update City Details
        /// </summary>
        /// <param name="city_json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> UpdateCity(List<City> CityList)
        {
            try
            {
                String Operation = Common.Operation.UPDATE.ToString(); ;
                String Message = "";
                String City_json = "";
                String UserId = "";
                if (CityList != null && CityList.Count > 0)
                {

                    City_json = JsonConvert.SerializeObject(CityList);
                    UserId = CityList[0].Row_created_by;
                    var result = await Task.Run(() => mCityRepository.SaveCity(City_json, Operation, UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "City" + Common.GetMessage(Common.MessageType.UpdateSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "City" + Common.GetMessage(Common.MessageType.UpdateSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "City." });

                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "City.", Data = result });

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
