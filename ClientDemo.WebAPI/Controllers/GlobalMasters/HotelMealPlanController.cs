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
    public class HotelMealPlanController : Controller
    {
        private readonly IHotelMealPlanRepository mHotelMealPlanRepository;
        public HotelMealPlanController(HotelMealPlanRepository repository)
        {
            this.mHotelMealPlanRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Nov 22, 2022</created_date>
        /// <summary>
        ///  Get List of Hotel Meal plan
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<Hotel>>> List(Int64 city_id = 0, Int64 hotel_id = 0)
        {
            try
            {
                var hotelmealplan = await Task.Run(() => mHotelMealPlanRepository.GetHotelMealPlanList(city_id,hotel_id));
                //return new OkObjectResult(new { Data = hotelmealplan, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = hotelmealplan });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<HotelMealPlan>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }
        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Nov 28, 2022</created_date>
        /// <summary>
        /// Save Hotel Meal Plan Details
        /// </summary>
        /// <param name="hotel_meal_plan_json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<ActionResult> Save(Hotel Hotel)
        {
            try
            {
                String Message = "";
                String hotel_meal_plan_json = "";
                String UserId = "";
                if (Hotel != null)
                {

                    hotel_meal_plan_json = JsonConvert.SerializeObject(Hotel);
                    UserId = Hotel.Row_created_by;
                    var result = await Task.Run(() => mHotelMealPlanRepository.SaveHotelMealPlan(hotel_meal_plan_json,UserId));

                    if (Convert.ToString(result).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "Hotel Meal Plan" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Hotel Meal Plan" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Hotel Meal Plan." });
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

        ///// <created_by>Sunil Kumar Bais</created_by>
        ///// <created_date>Nov 28, 2022</created_date>
        ///// <summary>
        ///// Save Hotel Meal Plan Details
        ///// </summary>
        ///// <param name="hotel_meal_plan_json"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("Save2")]
        //public async Task<ActionResult> Save2(List<HotelMealPlan> HotelMealPlanList)
        //{
        //    try
        //    {
        //        String Message = "";
        //        String hotel_meal_plan_json = "";

        //        if (HotelMealPlanList != null)
        //        {

        //            hotel_meal_plan_json = JsonConvert.SerializeObject(HotelMealPlanList);

        //            var result = await Task.Run(() => mHotelMealPlanRepository.SaveHotelMealPlan(hotel_meal_plan_json));

        //            if (Convert.ToString(result).ToUpper() == "SUCCESS")
        //            {
        //                return new OkObjectResult(new { Data = result, Message = "Hotel Meal Plan" + Common.GetMessage(Common.MessageType.SaveSuccess) });
        //            }
        //            else
        //            {
        //                return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Hotel Meal Plan." });
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

    }
}
