using ClientDemo.WebAPI.Models.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using ClientDemo.WebAPI.Models.Interface;
using ClientDemo.WebAPI.Models.Repository;
using Newtonsoft.Json;

namespace ClientDemo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository mOrganizationRepository;
        private readonly IWebHostEnvironment _env;
        public OrganizationController(OrganizationRepository repository, IWebHostEnvironment env)
        {
            _env = env;
            mOrganizationRepository = repository;
        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<Organization>>> List()
        {
            try
            {
                var Organization = await Task.Run(() => mOrganizationRepository.GetOrganizationList());
                //return new OkObjectResult(new { Data = ProcessLog, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = Organization });
               
            }
            catch (Exception ex)
            {
             
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<Organization>() });
            }
        }
        [HttpPost]
        [Route("SaveOrganization")]
        public async Task<ActionResult> SaveOrganization(Organization organization)
        {
            try
            {
                var result = await Task.Run(() => mOrganizationRepository.SaveOrganization(organization));
                if (Convert.ToString(result).ToUpper() == "SUCCESS")
                {
                   
                    return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Organization " + organization.Operation+ " successfully!", Data = result });
                }
                else
                {
                    
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Organization " + organization.Operation + " Failed !", Data = "" });
                }

            }
            catch (Exception ex)
            {
                
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }
        }
    }
}
