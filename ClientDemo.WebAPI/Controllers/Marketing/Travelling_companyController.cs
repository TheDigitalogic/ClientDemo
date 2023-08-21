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
using Microsoft.AspNetCore.Authorization;
using TravelNinjaz.CommonUtilities.Import;
using TravelNinjaz.CommonUtilities.Extentions;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using MimeKit;
using TravelNinjaz.B2B.WebAPI.Models.EmailService;

namespace TravelNinjaz.WebUI.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Travelling_companyController : Controller
    {

        #region variables & constructors 

        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IConfiguration mConfiguration;
        private readonly WebSettings webSettings;
        private readonly ITravellingCompanyRepository mTravellingCompanyRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMailService mMailService;

        public Travelling_companyController(IOptions<JwtBearerTokenSettings> jwtTokenOptions,
                IOptions<WebSettings> webOptions,
                UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IConfiguration config,
                TravellingCompanyRepository repository,
                IWebHostEnvironment env,
                IMailService mailService
                )
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.webSettings = webOptions.Value;
            this.userManager = userManager;
            this.roleManager = roleManager;
            mConfiguration = config;
            this.mTravellingCompanyRepository = repository;
            mMailService = mailService;
            _env = env;

        }

        #endregion constructors and variables

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>May 22, 2023</created_date>
        /// <summary>
        ///  Get List of TravellingData
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<TravellingCompany>>> List(String status="", String state="", String city="")
        {
            try
            {
                var travellingData = await Task.Run(() => mTravellingCompanyRepository.GetTravellingDataList(status,state,city));
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
        /**This is for save and update travelling Company*/
        [HttpPost]
        [Route("SaveTravellingCompany")]
        public async Task<ActionResult> SaveTravellingCompany(TravellingCompany TravellingCompany)
        {
            try
            {
                string Message = "";
                string travelling_company_json = "";
                String UserId = "";
                if (TravellingCompany != null)
                {

                    travelling_company_json = JsonConvert.SerializeObject(TravellingCompany);
                    UserId = TravellingCompany.Row_created_by;
                    var result = await Task.Run(() => mTravellingCompanyRepository.SaveTravellingCompany(travelling_company_json, UserId));

                    if (result != null && result.Length == 2 && Convert.ToString(result[0]).ToUpper() == "SUCCESS")
                    {
                        //return new OkObjectResult(new { Data = result, Message = "TravellingCompany" + Common.GetMessage(Common.MessageType.SaveSuccess) });

                        return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "TravellingCompany" + Common.GetMessage(Common.MessageType.SaveSuccess), Data = result });
                    }
                    else
                    {
                        //return new BadRequestObjectResult(new { Data = result, Message = Common.GetMessage(Common.MessageType.SaveFailed) + "TravellingCompany." });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = Common.GetMessage(Common.MessageType.SaveFailed) + "TravellingCompany.", Data = result });
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

        [Route("PreviewImportFile")]
        [HttpPost]
        public async Task<IActionResult> PreviewImportFile()
        {           
            String Status = "SUCCESS";
            String Message = "";
            SetLog("Info", "Import File Starts");

            try
            {
                var httpRequest = Request.Form;
                
                if (httpRequest != null)
                {
                    String UserId = Request.Form["User_id"].ToString();
                    var FileUpload = httpRequest.Files[0];
                    string filename = FileUpload.FileName;
                    string file_extension = ".xlsx";

                    if (ImportUtility.IsExcelFile(FileUpload.ContentType))
                    {
                   
                        string Original_file_name = System.IO.Path.GetFileName(FileUpload.FileName);

                        SetLog("Info", "file_name: " + Original_file_name);

                        if (Original_file_name.EndsWith(file_extension))
                        {

                            //Get Interface Path
                            String InterfaceName = "Travelling_company";
                            var InterfacePath = _env.ContentRootPath + "\\Import_data\\" + InterfaceName;  //  + "/Data/" + UserId.ToString();

                            String temp_file_name = DateTime.Now.ToString("yyyyMMdd_HHmmssfff")  + "_" + Original_file_name ;

                            String import_file_full_path = (InterfacePath + "\\DATA\\") + temp_file_name;
                         

                            //Save file copy to data folder
                            using (var stream = new FileStream(import_file_full_path, FileMode.Create))
                            {
                                FileUpload.CopyTo(stream);
                            }

                            SetLog("Info", "File saved to DATA folder: " + import_file_full_path);

                           
                            //Compare Template file and Importing files header 

                            string template_file_full_path = (InterfacePath + "\\TEMPLATE\\") + InterfaceName + "_template" + file_extension;


                            var  FileMatchinWithTemplate = ImportUtility.IsImportingFileMatchingWithTemplateFile(template_file_full_path, import_file_full_path);

                            if(FileMatchinWithTemplate.Item1.ToUpper() == "SUCCESS")
                            {
                                SetLog(FileMatchinWithTemplate.Item1, FileMatchinWithTemplate.Item2);
                            }
                            else
                            {
                                SetLog(FileMatchinWithTemplate.Item1, FileMatchinWithTemplate.Item2);
                                return await ShowJsonMessage(FileMatchinWithTemplate.Item1, FileMatchinWithTemplate.Item2, Original_file_name, temp_file_name);
                            }

                            Int32 file_record_count = 0;
                            Int32 table_record_count = 0;


                            //Read File data and Store into DataTable 
                            DataTable dtImportData = ImportUtility.ConvertXLSXtoDataTable(import_file_full_path, ref file_record_count, ref table_record_count);

                            if (dtImportData != null && dtImportData.Rows.Count == 0)
                            {
                                Message = "The file being imported is empty";

                                SetLog("Error", Message);
                                return await ShowJsonMessage("Error", Message);
                            }

                            //Convert DataTable to XML
                            String ImportDataXML = ExtentionsUtility.ConvertDatatableToXML(dtImportData);
                            String result = "";
                           // ImportDataXML = ImportDataXML.ToString().Replace("_x0020_", " ");
                            result = mTravellingCompanyRepository.ImportFileDataToTable(Original_file_name, temp_file_name, file_record_count, ImportDataXML, UserId);

                            if (result.ToUpper() == "SUCCESS")
                            {
                                Status = "SUCCESS";
                                Message = "File \"" + Original_file_name + "\"  data is ready to Preview. Total " + dtImportData.Rows.Count + " Record(s) imported successfully.";

                                //Message = "File data is ready to Preview";

                                //Now Delete the temp file
                                System.IO.File.Delete(import_file_full_path);
                            }
                            else
                            {
                                Status = "ERROR";
                                Message = result;
                            }


                            return await ShowJsonMessage(Status, Message, Original_file_name, temp_file_name);



                        }
                        else
                        {
                            Message = "This file is not valid format";
                            SetLog("Error", Message);

                            return await ShowJsonMessage("Error", Message);
                        }

                    }
                    else
                    {
                        Message = "Only " + file_extension + " file format is supported";

                        SetLog("Error", Message);

                        return await ShowJsonMessage("Error", Message);
                    }

                }
                //else
                //{

                //    if (FileUpload == null)
                //    {
                //        return ShowJsonMessage(Status, "Only " + file_extension + " file format is supported");
                //    }

                //}

            }
            catch (Exception ex)
            {

                Status = "Error";
                Message = ex.ToString();
                SetLog("Error", ex.ToString());
            }
            finally
            {
                SetLog("Error", "Import File Complete");
            }

            return await ShowJsonMessage(Status, Message);
        }
               

        #region "Common functions"
        public void SetLog(string MessageStatus, string Message)
        {
            //using (System.IO.StreamWriter sw = System.IO.File.AppendText(Server.MapPath("~/Logs/logfile.txt")))
            //{
            //    sw.WriteLine(System.DateTime.Now.ToString() + ">>" + MessageStatus + ">" + Message);
            //}
        }

        public  async Task<IActionResult> ShowJsonMessage(string MessageStatus, string Message, string Original_file_name = "", string file_name = "")
        {           

            return Ok(new
            {
                Status = MessageStatus,
                Message = Message,
                Original_file_name = Original_file_name,
                File_name = file_name,
            });

        }


        //Get
        [HttpGet]
        [Route("downloadTemplate")]
        public async Task<IActionResult> DownloadTemplateFile(String InterfaceName)
        {

            string TempFilePath = _env.ContentRootPath + "/Import_data";

            string file = (TempFilePath + "/" + InterfaceName + "/TEMPLATE/") + InterfaceName + "_template.xlsx";

            if (!System.IO.File.Exists(file))
            {
                return await ShowJsonMessage("Error", "File path not found");
            }

            //string contentType = "text/xlsx";
            //string contentType = "application/octet-stream";k
            //return File(file, contentType, System.IO.Path.GetFileName(file));
            //return File(file, contentType, System.IO.Path.GetFileName(file));
            //return PhysicalFile(file, MimeTypes.GetMimeType(file), Path.GetFileName(file));
            var memory = new MemoryStream();
            using (var stream = new FileStream(file, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(file).ToLowerInvariant();
            return File(memory, "application/vnd.ms-excel", Path.GetFileName(file));

        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
        {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.ms-word"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            //{".xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
             {".xlsx","application/vnd.ms-excel"},
            {".csv","text/csv" }

        };
        }
        //This is for sent email 
        [HttpPost]
        [Route("sendMail")]
        public async Task<IActionResult> sendMail(List<TravellingCompany> EmailList)
        {

            try
            {
                if (EmailList != null && EmailList.Count > 0)
                {
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
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Please select atleast one email.", Data = "" });
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


        #endregion

    }
}

