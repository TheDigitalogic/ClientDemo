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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PackageCheckPriceController : Controller
    {
        private readonly IPackageCheckPriceRepository mPackageCheckPirceRepository;
        public PackageCheckPriceController(PackageCheckPriceRepository repository)
        {
            this.mPackageCheckPirceRepository = repository;
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
                var PackageCheckPrice = await Task.Run(() => mPackageCheckPirceRepository.GetPackageCheckPriceList(destination_type_id, destination_id, package_id,UserId));
                return new OkObjectResult(new { Data = PackageCheckPrice, Message = "SUCCESS" });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Data = new List<Package>(),
                    Message = ex.Message.ToString()
                });
            }
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 31, 2023</created_date>
        /// <summary>
        ///  Add PackageCheckPrice
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<ActionResult> SavePackageCheckPrice(PackageCheckPrice PackageCheckPrice)
        {
            try
            {
                string Message = "";
                string package_check_price_json = "";
                String UserId = "";
                if (PackageCheckPrice != null)
                {
                    package_check_price_json = JsonConvert.SerializeObject(PackageCheckPrice);
                    UserId = PackageCheckPrice.Row_created_by;
                    var result = await Task.Run(() => mPackageCheckPirceRepository.SavePackageCheckPrice(package_check_price_json, UserId));
                    if (result!=null && result.Length==2 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //var resultCaluclatePrice = await Task.Run(() => mPackageCheckPirceRepository.CalculateLatestPrice(package_check_price_json));
                        return new OkObjectResult(new { Data = result, Message = "PackageCheckPrice" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                    }
                    else
                    {
                        return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "PackageCheckPrice." });
                    }
                }
                ////Data is Empty
                else
                    return new BadRequestObjectResult(new { Message = "Data is Empty" });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
            }

        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>April 19, 2023</created_date>
        /// <summary>
        ///  Update favorite packagecheckpirce
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateFavorite")]
        public async Task<ActionResult> UpdateFavoritePackage(PackageCheckPriceIsFavorite favoritePackage)
        {
            try
            {
                string Message = "";
                string favorite_package_json = "";
                Int64 Package_id = 0;
                Int64 Package_check_price_id = 0;
                Boolean IsFavorite=false;
                String UserId = "";
                if (favoritePackage!=null)
                {
                    favorite_package_json= JsonConvert.SerializeObject(favoritePackage);
                    UserId= favoritePackage.Row_created_by;
                    Package_id=favoritePackage.Package_id;
                    Package_check_price_id=favoritePackage.Package_check_price_id;
                    IsFavorite = favoritePackage.Is_favourite;
                    var result = await Task.Run(() => mPackageCheckPirceRepository.UpdateFavoritePackage(Package_id, Package_check_price_id, IsFavorite, UserId));
                    if (result != null && result.Length == 1 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //var resultCaluclatePrice = await Task.Run(() => mPackageCheckPirceRepository.CalculateLatestPrice(package_check_price_json));
                        return new OkObjectResult(new { Data = result, Message = "UpdateFavoritePackage" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                    }
                    else
                    {
                        return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "UpdateFavoritePackage." });
                    }
                }
                ////Data is Empty
                else
                    return new BadRequestObjectResult(new { Message = "Data is Empty" });
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
            }
        }/// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>April 19, 2023</created_date>
        /// <summary>
        ///  Update margin description packagecheckpirce
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateMarginDescription")]
        public async Task<ActionResult> UpdateMarginDescriptionPackage(Package_check_price_save_margin marginDescriptionPackage)
        {
            try
            {
                string Message = "";
                string margin_description_package_json = "";
                String UserId = "";
                Int64 Package_id=0;
                Int64 Package_check_price_id = 0;
                Decimal Margin = 0;
                String MarginDescription = "";
                if (marginDescriptionPackage != null)
                {
                    margin_description_package_json = JsonConvert.SerializeObject(marginDescriptionPackage);
                    UserId= marginDescriptionPackage.Row_created_by;
                    Package_id = marginDescriptionPackage.Package_id;
                    Package_check_price_id=marginDescriptionPackage.Package_check_price_id;
                    Margin = marginDescriptionPackage.Margin;
                    MarginDescription= marginDescriptionPackage.MarginDescription;
                    var result = await Task.Run(() => mPackageCheckPirceRepository.UpdateMarginDescriptionPackage(Package_id, Package_check_price_id, Margin, MarginDescription, UserId));
                    if (result != null && result.Length == 1 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //var resultCaluclatePrice = await Task.Run(() => mPackageCheckPirceRepository.CalculateLatestPrice(package_check_price_json));
                        return new OkObjectResult(new { Data = result, Message = "UpdateMarginDescriptionPackage" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                    }
                    else
                    {
                        return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "UpdateMarginDescriptionPackage." });
                    }
                }
                ////Data is Empty
                else
                    return new BadRequestObjectResult(new { Message = "Data is Empty" });
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("calculatePrice")]
        public async Task<ActionResult> CalculateLatestPrice(PackageCheckPrice PackageCheckPrice)
        {
            try
            {
                string Message = "";
                string package_check_price_json = "";
                String UserId = "";
                if (PackageCheckPrice != null)
                {
                    package_check_price_json = JsonConvert.SerializeObject(PackageCheckPrice);
                    UserId = PackageCheckPrice.Row_created_by;
                    var result = await Task.Run(() => mPackageCheckPirceRepository.CalculateLatestPrice(package_check_price_json,UserId));                  
                    if (result != null && result.Length == 2 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //var resultCaluclatePrice = await Task.Run(() => mPackageCheckPirceRepository.CalculateLatestPrice(package_check_price_json));
                        return new OkObjectResult(new { Data = result, Message = "CalculatePrice" + Common.GetMessage(Common.MessageType.SaveSuccess) });
                    }
                    else
                    {
                        return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "CalculatePrice." });
                    }
                }
                ////Data is Empty
                else
                    return new BadRequestObjectResult(new { Message = "Data is Empty" });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
            }

        }
    }
}
