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
    public class PackageQuotationController : Controller
    {
        private readonly IPackageQuotationRepository mPackageQuotationRepository;
        public PackageQuotationController(PackageQuotationRepository repository)
        {
            this.mPackageQuotationRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Feb 04, 2022</created_date>
        /// <summary>
        ///  Get List of Package check price
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<Package>>> List(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId="")
        {
            try
            {
                var PackageQuotation = await Task.Run(() => mPackageQuotationRepository.GetPackageQuotationList(destination_type_id, destination_id, package_id,UserId));
                //return new OkObjectResult(new { Data = PackageQuotation, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = PackageQuotation });
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
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 31, 2023</created_date>
        /// <summary>
        ///  Add PackageQuotation
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<ActionResult> SavePackageQuotation(PackageQuotation PackageQuotation)
        {
            try
            {
                string Message = "";
                string package_quotation_json = "";
                String UserId = "";
                if (PackageQuotation != null)
                {
                    package_quotation_json = JsonConvert.SerializeObject(PackageQuotation);
                    UserId = PackageQuotation.Row_created_by;
                    var result = await Task.Run(() => mPackageQuotationRepository.SavePackageQuotation(package_quotation_json, UserId));
                    if (result!=null && result.Length==2 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //var resultCaluclatePrice = await Task.Run(() => mPackageQuotationRepository.CalculateLatestPrice(package_quotation_json));
         
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "PackageQuotation" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });
                    }
                    else
                    {
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "PackageQuotation.", Data = result });
                       
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
            }

        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>April 19, 2023</created_date>
        /// <summary>
        ///  Update favorite PackageQuotation
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateFavorite")]
        public async Task<ActionResult> UpdateFavoritePackage(PackageQuotationIsFavorite favoritePackage)
        {
            try
            {
                string Message = "";
                string favorite_package_json = "";
                Int64 Package_id = 0;
                Int64 Package_quotation_id = 0;
                Boolean IsFavorite=false;
                String UserId = "";
                if (favoritePackage!=null)
                {
                    favorite_package_json= JsonConvert.SerializeObject(favoritePackage);
                    UserId= favoritePackage.Row_created_by;
                    Package_id=favoritePackage.Package_id;
                    Package_quotation_id=favoritePackage.Package_quotation_id;
                    IsFavorite = favoritePackage.Is_favourite;
                    var result = await Task.Run(() => mPackageQuotationRepository.UpdateFavoritePackage(Package_id, Package_quotation_id, IsFavorite, UserId));
                    if (result != null && result.Length == 1 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //var resultCaluclatePrice = await Task.Run(() => mPackageQuotationRepository.CalculateLatestPrice(package_quotation_json));
                        //return new OkObjectResult(new { Data = result, Message = "UpdateFavoritePackage" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "UpdateFavoritePackage" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "UpdateFavoritePackage." });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "UpdateFavoritePackage.", Data = result });
                    }
                }
                ////Data is Empty
                else
                    //return new BadRequestObjectResult(new { Message = "Data is Empty" });
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Data is Empty", Data = "" });
            }
            catch(Exception ex)
            {
                //return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = ""});
            }
        }/// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>April 19, 2023</created_date>
        /// <summary>
        ///  Update margin description PackageQuotation
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateMarginDescription")]
        public async Task<ActionResult> UpdateMarginDescriptionPackage(PackageQuotationSaveMargin marginDescriptionPackage)
        {
            try
            {
                string Message = "";
                string margin_description_package_json = "";
                String UserId = "";
                Int64 Package_id=0;
                Int64 Package_quotation_id = 0;
                Decimal Margin = 0;
                String MarginDescription = "";
                if (marginDescriptionPackage != null)
                {
                    margin_description_package_json = JsonConvert.SerializeObject(marginDescriptionPackage);
                    UserId= marginDescriptionPackage.Row_created_by;
                    Package_id = marginDescriptionPackage.Package_id;
                    Package_quotation_id=marginDescriptionPackage.Package_quotation_id;
                    Margin = marginDescriptionPackage.Margin;
                    MarginDescription= marginDescriptionPackage.MarginDescription;
                    var result = await Task.Run(() => mPackageQuotationRepository.UpdateMarginDescriptionPackage(Package_id, Package_quotation_id, Margin, MarginDescription, UserId));
                    if (result != null && result.Length == 1 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //var resultCaluclatePrice = await Task.Run(() => mPackageQuotationRepository.CalculateLatestPrice(package_quotation_json));
                        //return new OkObjectResult(new { Data = result, Message = "UpdateMarginDescriptionPackage" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "UpdateMarginDescriptionPackage" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "UpdateMarginDescriptionPackage." });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "UpdateMarginDescriptionPackage.", Data = result });
                    }
                }
                ////Data is Empty
                else
                    //return new BadRequestObjectResult(new { Message = "Data is Empty" });
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Data is Empty", Data = "" });

            }
            catch(Exception ex)
            {
                //return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }
        }

        [HttpPost]
        [Route("calculatePrice")]
        public async Task<ActionResult> CalculateLatestPrice(PackageQuotation PackageQuotation)
        {
            try
            {
                string Message = "";
                string package_quotation_json = "";
                String UserId = "";
                if (PackageQuotation != null)
                {
                    package_quotation_json = JsonConvert.SerializeObject(PackageQuotation);
                    UserId = PackageQuotation.Row_created_by;
                    var result = await Task.Run(() => mPackageQuotationRepository.CalculateLatestPrice(package_quotation_json,UserId));                  
                    if (result != null && result.Length == 2 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //var resultCaluclatePrice = await Task.Run(() => mPackageQuotationRepository.CalculateLatestPrice(package_quotation_json));
                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS",  Message = "CalculatePrice" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result, });

                    }
                    else
                    {
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed), Data = "" });
             
                    }
                }
                ////Data is Empty
                else
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Data is Empty", Data = "" });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });

            }

        }
    }
}
