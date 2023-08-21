using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using Microsoft.AspNetCore.Authorization;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PackageCheckPriceUserFavoriteController : Controller
    {
        private readonly IPackageCheckPriceUserFavoriteRepository mPackageCheckPirceUserFavoriteRepository;
        public PackageCheckPriceUserFavoriteController(PackageCheckPriceUserFavoriteRepository repository)
        {
            this.mPackageCheckPirceUserFavoriteRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Feb 10, 2022</created_date>
        /// <summary>
        ///  Get List of Package check price User favorite
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<PackageCheckPrice>>> List(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_check_price_id = 0)
        {
            try
            {
                var PackageCheckPrice = await Task.Run(() => mPackageCheckPirceUserFavoriteRepository.GetPackageCheckPriceUserFavoriteList(UserId, destination_type_id, destination_id, package_id, package_check_price_id));
                return new OkObjectResult(new { Data = PackageCheckPrice, Message = "SUCCESS" });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Data = new List<PackageCheckPrice>(),
                    Message = ex.Message.ToString()
                });
            }
        }        
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Feb 10, 2022</created_date>
        /// <summary>
        ///  Get List of Package check price User favorite
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("PacakgeCheckPriceById")]
        public async Task<ActionResult<IEnumerable<PackageCheckPrice>>> PacakgeCheckPriceById(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_check_price_id = 0)
        {
            try
            {
                var PackageCheckPrice = await Task.Run(() => mPackageCheckPirceUserFavoriteRepository.GetPackageCheckPriceListById(UserId, destination_type_id, destination_id, package_id, package_check_price_id));
                return new OkObjectResult(new { Data = PackageCheckPrice, Message = "SUCCESS" });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Data = new List<PackageCheckPrice>(),
                    Message = ex.Message.ToString()
                });
            }
        }
    }
}
