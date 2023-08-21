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
    public class PackageReviewsController : Controller
    {
        private readonly IPackageReviewsRepository mPackageRemarksRepository;
        public PackageReviewsController(PackageReviewsRepository repository)
        {
            this.mPackageRemarksRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>July 07, 2023</created_date>
        /// <summary>
        ///  Get List of Package Remarks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<PackageReviews>>> List(Int64 Package_id)
        {
            try
            {
                var PackageRemarks = await Task.Run(() => mPackageRemarksRepository.GetPackageReviewsList(Package_id));
                //return new OkObjectResult(new { Data = PackageRemarks, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = PackageRemarks });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<PackageRemarks>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<PackageReviews>() });
            }
        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>July 07, 2023</created_date>
        /// <summary>
        ///  Add PackageRemarks
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<ActionResult> SavePackageReviews(PackageReviews PackageReviews)
        {
            try
            {
                string Message = "";
                string package_remarks_json = "";
                String UserId = "";
                if (PackageReviews != null)
                {
                    package_remarks_json = JsonConvert.SerializeObject(PackageReviews);
                    UserId = PackageReviews.Row_created_by;
                    var result = await Task.Run(() => mPackageRemarksRepository.SavePackageReviews(package_remarks_json, UserId));
                    if (result != null && result.Length == 2 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "PackageRemarks" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Package Reviews " + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "PackageRemarks." });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Package Reviews .", Data = result });
                    }
                }
                else
                    //return new BadRequestObjectResult(new { Message = "Data is Empty" });
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Data is Empty", Data = "" });

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
                //return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
            }
        }
    }
}
