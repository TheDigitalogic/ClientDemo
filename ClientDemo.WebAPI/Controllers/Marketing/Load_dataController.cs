using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Diagnostics;
using System.Xml;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TravelNinjaz.B2B.WebAPI.Configuration;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.EmailService;
using DocumentFormat.OpenXml.Wordprocessing;
using WebSettings = TravelNinjaz.B2B.WebAPI.Configuration.WebSettings;
using Microsoft.AspNetCore.SignalR;
using DocumentFormat.OpenXml.Office2010.Ink;

namespace TravelNinjaz.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Load_dataController : Controller
    {
        private readonly ILoad_dataRepository mload_DataRepository;
        private readonly IWebHostEnvironment _env;
        private readonly WebSettings webSettings;
        private readonly IMailService mMailService;
        public Load_dataController(Load_dataRepository repository, IOptions<WebSettings> webOptions, IWebHostEnvironment env, IMailService mailService)
        {
            //this.mDestinationRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _env = env;
            this.mload_DataRepository = repository;
            this.webSettings = webOptions.Value;
            mMailService = mailService;
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>May 22, 2023</created_date>
        /// <summary>
        ///  Get List of TravellingData
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<TravellingCompany>>> List()
        {
            try
            {
                var travellingData = await Task.Run(() => mload_DataRepository.GetTravellingDataList());
                //return new OkObjectResult(new { Data = travellingData, Message = "SUCCESS" });

                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = travellingData });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<TravellingCompany>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<TravellingCompany>() });
            }
        }

        //This is for sent email 
        [HttpPost]
        [Route("sentMail")]
        public async Task<IActionResult> sentMail(List<TravellingCompany> EmailList)
        {
           
            try
            {
                if (EmailList != null && EmailList.Count > 0) {
                    var pathToFile = "Templates"
                                      + Path.DirectorySeparatorChar.ToString()
                                      + "EmailTemplate"
                                      + Path.DirectorySeparatorChar.ToString()
                                      + "Pricepackage"
                                      + Path.DirectorySeparatorChar.ToString()
                                      + "pricepackageTemplate.html";
                    var builder = new MimeKit.BodyBuilder();
                    
                    for (int i = 0; i < EmailList.Count; i++)
                    {
                        if (EmailList[i].Email_id_1 != null)
                        {
                            MailRequest objRequest = new MailRequest();
                            objRequest.ToEmail = EmailList[i].Email_id_1.ToLower();

                            objRequest.Subject = webSettings.ApplicationName + " - Package Rate ";
                            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                            {
                                builder.HtmlBody = SourceReader.ReadToEnd();
                            }
                            string messageBody;
                            messageBody = builder.HtmlBody;
                            objRequest.Body = messageBody;
                            await mMailService.SendEmailAsync(objRequest);
                        }
                         if (EmailList[i].Email_id_2 != null)
                        {
                            MailRequest objRequest = new MailRequest();
                            objRequest.ToEmail = EmailList[i].Email_id_2.ToLower();

                            objRequest.Subject = webSettings.ApplicationName + " - Package Rate ";
                            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                            {
                                builder.HtmlBody = SourceReader.ReadToEnd();
                            }
                            string messageBody;
                            messageBody = builder.HtmlBody;
                            objRequest.Body = messageBody;
                            await mMailService.SendEmailAsync(objRequest);
                        }
                     
                    }
                    //return Ok(new { Status = "Success", Message = "Email sent successfully!" });
                    return Ok(new { StatusCode = 200, Status = "SUCCESS", Message = "Email sent successfully!", Data = "" });
                }
                else
                    //return new BadRequestObjectResult(new { Message = "Data is Empty" });
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Data is Empty", Data = "" });
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }
        }

    }
}

