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
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using Microsoft.AspNetCore.Authorization;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : Controller
    {
        private readonly IPackageRepository mPackageRepository;
        private readonly IWebHostEnvironment _env;
        public PackageController(PackageRepository repository,IWebHostEnvironment env)
        {
            _env = env;
            mPackageRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Dec 19, 2022</created_date>
        /// <summary>
        ///  Get List of Package
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<Package>>> List(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "")
        {
            try
            {
                var Package = await Task.Run(() => mPackageRepository.GetPackageList(destination_type_id, destination_id, package_id, UserId));
                //return new OkObjectResult(new { Data = Package, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = Package });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<Package>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<Package>() });
            }
        }


        /// <summary>
        ///  For a particular Package - Get he list of Cities and its Hotels (Along with Hotels MealPlan list)
        /// </summary>
        /// <created_by>Manisha Tripathi</created_date>
        /// <created_date>July 24, 2023</created_date>
        /// <param name="destination_type_id"></param>
        /// <param name="destination_id"></param>
        /// <param name="package_id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PackageCityHotelMealPlanList")]
        public async Task<ActionResult<IEnumerable<PackageCity>>> PackageCityHotelMealPlanList(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "")
        {
            try
            {
                var PakcageCities = await Task.Run(() => mPackageRepository.PackageCityHotelMealPlanList(destination_type_id, destination_id, package_id, UserId));
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = PakcageCities, });


            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<Package>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<Package>() });
            }
        }


        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 19, 2022</created_date>
        /// <summary>
        /// Save Package Details
        /// </summary>
        /// <param name="package_json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<ActionResult> SavePackage(Package Package)
        {
            try
            {             
                string Message = "";
                string package_json = "";
                String UserId = "";
                if (Package != null)
                {

                    package_json = JsonConvert.SerializeObject(Package);
                    UserId = Package.Row_created_by;
                    var result = await Task.Run(() => mPackageRepository.SavePackage(package_json, UserId));

                    if (result != null && result.Length == 2 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "Package" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Package" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Package." });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "Package.", Data = result });
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
        public JsonResult SaveFile( )
        {
            try
            {
                String Package_id  = Request.Form["Package_id"].ToString();
                var package_folder_name = _env.ContentRootPath + "/Images/Package/" + Package_id.ToString();


                //If folder does not exists than create new folder
                if (!System.IO.Directory.Exists(package_folder_name))
                {
                    System.IO.Directory.CreateDirectory(package_folder_name);
                }

                //GEt all files details 
                var httpRequest = Request.Form;

                //Check if file has data then sa
                if (httpRequest != null && httpRequest.Files.Count > 0)
                {
                    for (int i = 0; i < httpRequest.Files.Count; i++)
                    {
                        var postedFile = httpRequest.Files[i];
                        string filename = postedFile.FileName;
                        var physicalPath = package_folder_name + "/" + filename;

                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                            
                        }
                    }
                }
                //return new JsonResult("Images Uploaded Successfully!");
                return new JsonResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Images Uploaded Successfully!", Data = "" });
            }
            catch (Exception)
            {
                return new JsonResult(new { StatusCode = 500, Status = "ERROR", Message = "Images Upload failed!", Data = "" });
                //return new JsonResult("Images Upload failed!");
            }
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Feb 16, 2022</created_date>
        /// <summary>
        /// Delete Photos
        /// </summary>
        /// <param name="imagefiles"></param>
        /// <returns></returns>
        [Route("DeleteFile")]
        [HttpPost]
        public JsonResult DeleteFile(List<PackageImages> PackageImageList)
        {
        
            try
            {
                string package_folder_name = _env.ContentRootPath + "/Images/Package";
                string file_path = "";
                for (int i = 0; i < PackageImageList.Count; i++)
                {
                    file_path= package_folder_name+"/"+ PackageImageList[i].Package_id.ToString()+"/"+ PackageImageList[i].Image_name;
                    if (System.IO.File.Exists(file_path))
                    {
                       System.IO.File.Delete(file_path);
                    }                   
                }
                return new JsonResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Images Deleted Successfully!", Data = "" });
                //return new JsonResult("Images Deleted Successfully!");
            }
            catch (Exception)
            {

                //return new JsonResult("Images Delete failed!");
                return new JsonResult(new { StatusCode = 500, Status = "ERROR", Message = "Images Delete failed!", Data = "" });
            }
        }
    }
}
