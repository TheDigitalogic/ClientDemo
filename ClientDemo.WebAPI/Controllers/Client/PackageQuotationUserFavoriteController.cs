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
    public class PackageQuotationUserFavoriteController : Controller
    {
        private readonly IPackageQuotationUserFavoriteRepository mPackage_quotationUserFavoriteRepository;
        public PackageQuotationUserFavoriteController(PackageQuotationUserFavoriteRepository repository)
        {
            this.mPackage_quotationUserFavoriteRepository = repository;
        }
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Feb 10, 2022</created_date>
        /// <summary>
        ///  Get List of Package check price User favorite
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<PackageQuotation>>> List(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_quotation_id = 0)
        {
            try
            {
                var PackageQuotation = await Task.Run(() => mPackage_quotationUserFavoriteRepository.GetPackageQuotationUserFavoriteList(UserId, destination_type_id, destination_id, package_id, package_quotation_id));
                //return new OkObjectResult(new { Data = PackageQuotation, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = PackageQuotation });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<PackageQuotation>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }
        }        
        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>Feb 10, 2022</created_date>
        /// <summary>
        ///  Get List of Package check price User favorite
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("PackageQuotationById")]
        public async Task<ActionResult<IEnumerable<PackageQuotation>>> GetPackageQuotationById(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_quotation_id = 0)
        {
            try
            {
                var PackageQuotation = await Task.Run(() => mPackage_quotationUserFavoriteRepository.GetPackageQuotationListById(UserId, destination_type_id, destination_id, package_id, package_quotation_id));
                //return new OkObjectResult(new { Data = PackageQuotation, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = PackageQuotation });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<PackageQuotation>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<PackageQuotation>() });
            }
        }
    }
}
