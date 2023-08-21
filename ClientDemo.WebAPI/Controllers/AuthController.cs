using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelNinjaz.B2B.WebAPI.Configuration;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.Helper;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Repository;
using TravelNinjaz.B2B.WebAPI.Models.Entity.Authentication;
using Microsoft.AspNetCore.Http;
using TravelNinjaz.B2B.WebAPI.Models.EmailService;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using UserInfo = TravelNinjaz.B2B.WebAPI.Models.Entity.UserInfo;
using System.Diagnostics;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace TravelNinjaz.B2B.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IConfiguration mConfiguration;
        private readonly WebSettings webSettings;
        private readonly IUserRepository mUserRepository;
        private readonly IMailService mMailService;
        private readonly IWebHostEnvironment _env;

        //public AuthController(UserRepository repository)
        //{         
        //    this.mUserRepository = (IUserRepository)(repository ?? throw new ArgumentNullException(nameof(repository))); 
        //}
        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions,
                IOptions<WebSettings> webOptions,
                UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IConfiguration config,
                IMailService mailService,
                UserRepository repository,
                IWebHostEnvironment env
                )
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.webSettings = webOptions.Value;
            this.userManager = userManager;
            this.roleManager = roleManager;         
            mConfiguration = config;
            mMailService = mailService;
            this.mUserRepository = repository; //  (IUserRepository)(repository ?? throw new ArgumentNullException(nameof(repository)));
            _env = env;

        }
        private async Task<IActionResult> Login_common(LoginCredentials credentials)
        {
            IdentityUser identityUser = null;
            User memberProfile;
            string firstName = "";
            string lastName = "";
            string fullName = "";
            int userId = 0;
            String email = "";
            string phoneNumber = "";
            string role = "";
            string token = "";

            try
            {
                //Dot Net Identity Manager check
                identityUser = await ValidateUser(credentials);
                var rolesList = await userManager.GetRolesAsync(identityUser).ConfigureAwait(false);
                //--------------------------
                //JWT         
                token = (string)GenerateToken(identityUser, rolesList);
                //---------------------------

                // Database Check for user's authority
                memberProfile = mUserRepository.uspGetUserDetailByUserName(credentials.Username);
                if (memberProfile != null)
                {
                    credentials.Username = memberProfile.UserName;
                    firstName = memberProfile.FirstName;
                    lastName = memberProfile.LastName;
                    fullName = memberProfile.FullName;
                    userId = memberProfile.UserId;
                    email = memberProfile.Email;
                    phoneNumber = memberProfile.Phone;
                    role = memberProfile.Role;                   

                }
                else
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Incorrect UserName or Password.", Data = "" });


                return Ok(new
                {
                    StatusCode = 200,
                    Status = "SUCCESS",
                    Message = "User Logged In Successfully",
                    Token = token,
                    UserKey = identityUser.Id,
                    UserId = userId,
                    UserName = credentials.Username, 
                    FirstName = firstName,
                    LastName = lastName,
                    FullName = fullName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Role = role,
                    //Roles = rolesList,
                  
                });

            }
            catch (Exception ex)
            {
                return Ok(new { StatusCode = "500", Status = "ERROR", Message = ex.Message, Data = "" });
            }
        }

        /// <created_by>Sunil Kumar Bais<created_by>
        /// <created_date>June 30, 2023</created_date>
        /// <summary>
        ///  Get List of Process Logs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<User>>> List()
        {
            try
            {
                var users = await Task.Run(() => mUserRepository.GetUserList());
                //return new OkObjectResult(new { Data = ProcessLog, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = users });
                //return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Data = new List<ProcessLogs>(),
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = new List<User>() });
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginCredentials credentials)
        {
            IdentityUser identityUser;         

            try
            {               

                //isValidEmail = RegexUtilities.IsValidEmail(credentials.Username);

                //if (!isValidEmail)
                //    return new BadRequestObjectResult(new {StatusCode="500", Status="ERROR", Message = "Invalid Email Id", Data= ""});


                if (!ModelState.IsValid || credentials == null || (identityUser = await ValidateUser(credentials)) == null)
                {
                    return new BadRequestObjectResult(new { StatusCode = "500", Status = "ERROR", Message = "Incorrect UserName or Password.", Data = "" });
                }

                //if (!(await userManager.IsEmailConfirmedAsync(identityUser)))
                //{

                //    return new BadRequestObjectResult(new { StatusCode = "500", Status = "ERROR", Message = "You must have confirmed your email before log in to system.", Data = "" });
                //}

                return await Login_common(credentials);


            }
            catch (Exception ex)
            {
                //return Ok(new
                //{
                //    Status = "Error",
                //    Message = ex.Message,
                //});

                return  Ok(new { StatusCode = "500",  Status = "ERROR", Message = ex.Message, Data = "" });

            } 


        }

        //{
        //    IdentityUser identityUser;
        //    User memberProfile;
        //    string firstName = "";
        //    string lastName = "";
        //    string fullName = "";
        //    int userId = 0;
        //    String email = "";
        //    string phoneNumber = "";
        //    string company_name = "";
        //    string company_gst_no = "";
        //    string social_media_type = "";
        //    string profileImage = "";
        //    bool isValidEmail = false;
        //    string token = "";

        //    try
        //    {

        //        isValidEmail = RegexUtilities.IsValidEmail(credentials.Username);
        //        if (isValidEmail)
        //        {

        //            if (!ModelState.IsValid
        //          || credentials == null
        //          || (identityUser = await ValidateUser(credentials)) == null)
        //            {
        //                return new BadRequestObjectResult(new { Message = "Incorrect UserName or Password." });
        //            }

        //            if (! (await userManager.IsEmailConfirmedAsync(identityUser)))
        //            {

        //                return new BadRequestObjectResult(new { Status = "Error", Message = "You must have confirmed your email before log in to system." });
        //            }

        //            var rolesList = await userManager.GetRolesAsync(identityUser).ConfigureAwait(false);

        //            //JWT
        //            //token = (string) GenerateToken(identityUser);
        //            token = (string)GenerateToken(identityUser, rolesList);

        //            //memberProfile = mUserRepository.UserCheckAuthentication(credentials.Username, credentials.Password);
        //            memberProfile = mUserRepository.GetUserDetailByEmailID(credentials.Username);
        //            if (memberProfile != null) {
        //                credentials.Username = memberProfile.UserName;
        //                firstName = memberProfile.FirstName;
        //                lastName = memberProfile.LastName;
        //                fullName = memberProfile.FullName;
        //                userId = memberProfile.UserId;
        //                email = memberProfile.Email;
        //                phoneNumber = memberProfile.Phone;
        //                company_name = memberProfile.Company_name;
        //                social_media_type = memberProfile.Social_media_type;
        //                company_gst_no = memberProfile.Company_gst_no;

        //            }
        //            else
        //                return new BadRequestObjectResult(new { Status = "Error", Message = "Incorrect UserName or Password." });
        //        }




        //        //var token = "abcd1234";

        //        //var userIdentity = (ClaimsIdentity)User.Identity;
        //        //var claims = userIdentity.Claims;
        //        //var roleClaimType = userIdentity.RoleClaimType;
        //        //var roles = claims.Where(c => c.Type == roleClaimType).ToList();


        //        return Ok(new
        //        {
        //            Token = token,
        //            UserKey = 1, //identityUser.Id,
        //            UserId = userId,
        //            UserName = credentials.Username, //identityUser.Email,
        //            FirstName = firstName,
        //            LastName = lastName,
        //            FullName = fullName,
        //            Email = email,
        //            PhoneNumber = phoneNumber,
        //            Company_name = company_name,
        //            Company_gst_no = company_gst_no,
        //            Social_media_type =social_media_type,
        //            //Roles = rolesList,
        //            Status = "Success",
        //            Message = "Success",
        //        });

        //    }
        //    catch (Exception ex) {
        //        return Ok(new
        //        {
        //            Status = "Error",
        //            Message = ex.Message,
        //        });
        //    }


        //}


        // Login with google

        [HttpPost]
        [Route("googleLogin")]
        public async Task<IActionResult> googleLogin(LoginWithgoogle userView)
        {
            {
                //var userExists = await userManager.FindByNameAsync(user.FullName);
                //if (userExists != null)
                //    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "User already exists!" });

                var userExists = await userManager.FindByNameAsync(userView.Email);
                if (userExists != null && userView.Email_verified)
                {
                    //return new BadRequestObjectResult(new { Message = "User with this Email Id already exist!" });
                    return await Login_common(new LoginCredentials(userView.Email,userView.Social_media_type));
                }


                UserCredentialDetails userData = new UserCredentialDetails();
                IdentityUser identityUser = null;

                try
                {
                    string callbackUrl = string.Empty;
                    string password = "";
                    userData.Email = userView.Email.ToLower();
                    userData.UserName = userView.Email.ToLower();
                    userData.UserName = userView.Email.ToLower();
                    if (password != "")
                    {
                        userData.Password = password;
                    }
                    else
                    {
                        userData.Password = Convert.ToString(Guid.NewGuid()).Substring(0, 8) + "M#";
                    }

                    userData.Role = userView.Role;

                    if (userView.UserId == 0 && userView.Email_verified)
                    {

                        identityUser = new IdentityUser() { UserName = userData.UserName, Email = userData.Email,EmailConfirmed=true };
                        var resultUser = await userManager.CreateAsync(identityUser, userData.Password);

                        userView.AspNetUserId = identityUser.Id;

                        if (!resultUser.Succeeded)
                        {
                            var dictionary = new ModelStateDictionary();
                            foreach (IdentityError error in resultUser.Errors)
                            {
                                dictionary.AddModelError(error.Code, error.Description);
                            }

                            //return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
                            return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "User Registration Failed", Data = "", Errors = dictionary });

                        }
                        else
                        {
                            if (userView.Roles != null && userView.Roles.Count > 0)
                            {
                                var roleresult = await userManager.AddToRolesAsync(identityUser, userView.Roles);
                            }
                            else
                            {

                                // Add the Admin role to the database
                                IdentityResult roleResult;
                                bool adminRoleExists = await roleManager.RoleExistsAsync(userView.Role.ToUpper());
                                if (!adminRoleExists)
                                {

                                    roleResult = await roleManager.CreateAsync(new IdentityRole(userView.Role.ToUpper()));
                                }


                                var roleresult = await userManager.AddToRoleAsync(identityUser, userData.Role.ToUpper());
                            }
                        }
                        User objUser = new User();
                        objUser.Email = userView.Email;
                        objUser.FullName = userView.Name;
                        objUser.AspNetUserId = identityUser.Id;
                        objUser.Role = userView.Role;
                        objUser.UserName = userView.Email;
                        objUser.Operation = userView.Operation;
                        objUser.FromApplication = userView.FromApplication;
                        objUser.FirstName = userView.FirstName;
                        objUser.LastName = userView.LastName;
                        objUser.Social_media_type = userView.Social_media_type;
                        var result = await Task.Run(() => mUserRepository.SaveUser(objUser));
                        string token = await userManager.GeneratePasswordResetTokenAsync(identityUser);
                    }
                    return await Login_common(new LoginCredentials(userView.Email,userView.Social_media_type));
                    //return Ok(new { Status = "Success", Message = "User created successfully!" });
                }

                catch (Exception ex)
                {
                    DeleteUser(identityUser);
                    //return Ok(new
                    //{
                    //    Status = "Error",
                    //    Message = ex.Message.ToString(),
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
                    // return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
                }

            }
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            // Well, What do you want to do here ?
            // Wait for token to get expired OR 
            // Maintain token cache and invalidate the tokens after logout method is called
            //return Ok(new { Token = "", Message = "Logged Out" });

            return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Token = "", Message = "Logged Out", Data = "" });
        }
              

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(User userToCreate)
        {


            var userExists = await userManager.FindByNameAsync(userToCreate.Email);
            if (userExists != null)
                return new BadRequestObjectResult(new { Message = "User with this Email Id already exist!" });

            UserCredentialDetails userData = new UserCredentialDetails();
            IdentityUser identityUser = null;
            string aspnetUserId = "";

            try
            {
                string callbackUrl = string.Empty;

                userData.Email = userToCreate.Email.ToLower();
                userData.UserName = userToCreate.UserName.ToLower();


                if (userToCreate.Password != null || userToCreate.Password != "")
                {
                    userData.Password = userToCreate.Password;
                }
                else
                {
                    userData.Password = Convert.ToString(Guid.NewGuid()).Substring(0, 8) + "M#";
                }

                userData.Role = userToCreate.Role;

                if (userToCreate.UserId == 0)
                {

                    identityUser = new IdentityUser() { UserName = userData.UserName, Email = userData.Email, PhoneNumber = userToCreate.Phone };
                    var resultUser = await userManager.CreateAsync(identityUser, userData.Password);


                    userToCreate.AspNetUserId = identityUser.Id;

                    if (!resultUser.Succeeded)
                    {
                        var dictionary = new ModelStateDictionary();
                        foreach (IdentityError error in resultUser.Errors)
                        {
                            dictionary.AddModelError(error.Code, error.Description);
                        }

                        //return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
                        return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "User Registration Failed", Data = "", Errors = dictionary });

                    }
                    else
                    {
                        if (userToCreate.Roles != null && userToCreate.Roles.Count > 0)
                        {
                            var roleresult = await userManager.AddToRolesAsync(identityUser, userToCreate.Roles);
                        }
                        else
                        {

                            // Add the Admin role to the database
                            IdentityResult roleResult;
                            bool adminRoleExists = await roleManager.RoleExistsAsync(userToCreate.Role.ToUpper());
                            if (!adminRoleExists)
                            {

                                roleResult = await roleManager.CreateAsync(new IdentityRole(userToCreate.Role.ToUpper()));
                            }


                            var roleresult = await userManager.AddToRoleAsync(identityUser, userData.Role.ToUpper());
                        }
                    }


                    var result = await Task.Run(() => mUserRepository.SaveUser(userToCreate));
                    aspnetUserId = result;

                    //Below is the code to send varification email to new user
                    /*
                     string token = await userManager.GenerateEmailConfirmationTokenAsync(identityUser);


                     callbackUrl = webSettings.UniversalURL + "/verifyUserEmail?";
                     callbackUrl = callbackUrl + "t=" + HttpUtility.UrlEncode(token);
                     callbackUrl = callbackUrl + "&e=" + HttpUtility.UrlEncode(identityUser.Email);

                     Random generator = new Random();
                     String otp = generator.Next(0, 1000000).ToString("D6");

                     MailRequest objRequest = new MailRequest();
                     objRequest.ToEmail = userToCreate.Email.ToLower();
                     objRequest.Subject = webSettings.ApplicationName + " - Confirm User Registration ";

                     var pathToFile = "Templates"
                                    + Path.DirectorySeparatorChar.ToString()
                                    + "EmailTemplate"
                                    + Path.DirectorySeparatorChar.ToString()
                                    + "Register_EmailTemplate.html";

                     var builder = new MimeKit.BodyBuilder();
                     using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                     {
                         builder.HtmlBody = SourceReader.ReadToEnd();
                     }


                     string messageBody = builder.HtmlBody;
                     messageBody = ReplacePlaceHolder(messageBody, "{Firstname}", userToCreate.FirstName);
                     messageBody = ReplacePlaceHolder(messageBody, "{CallBackUrl}", callbackUrl);
                     if (userToCreate.FromApplication== "Admin_module")
                     {
                         messageBody = ReplacePlaceHolder(messageBody, "{ApplicationName}", "TravelNinjaz");
                     }
                     else
                     {
                         messageBody = ReplacePlaceHolder(messageBody, "{ApplicationName}", "TravelNinjaz B2B");
                     }
                     objRequest.Body = messageBody;

                     await mMailService.SendEmailAsync(objRequest);

                     */
                }

                return Ok(new { StatusCode = 200, Status = "SUCCESS", aspNetUserId = aspnetUserId, Message = "User created successfully!", Data = "" });
            }

            catch (Exception ex)
            {
                DeleteUser(identityUser);
                return Ok(new
                {
                    Message = ex.Message.ToString(),
                    StatusCode = 500,
                    Status = "ERROR",
                    Data = ""
                });

                // return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
            }

        }

        [HttpPost]
        [Route("UpdateUserclient")]
        public async Task<IActionResult> UpdateUserclient(User user)
        {

            IdentityUser identityUser = new IdentityUser();
            string aspnetUserId = "";

            try
            {
                /****
                //Update ASPNet User Table Details
               // identityUser = new IdentityUser() { UserName = user.UserName, Email = user.Email, PhoneNumber = user.Phone };

                identityUser = await userManager.FindByIdAsync(user.UserId.ToString());
                var resultUser = await userManager.UpdateAsync(identityUser);
                //user.AspNetUserId = identityUser.Id;


                // Updat ASPNet Role Table
                var result1 = await userManager.RemoveFromRoleAsync(identityUser, user.OldRole);
                var result2  =await userManager.AddToRoleAsync(identityUser,  user.Role);
                               
                
                // THIS LINE IS IMPORTANT
                //var oldUser = userManager.FindByIdAsync(user.Id);
                //var oldRoleId = userManager.GetRolesAsync()
                //var oldRoleName = DB.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

                //if (oldRoleName != role)
                //{
                //    userManager.RemoveFromRolesAsync(user.Id, oldRoleName);
                //    Manager.AddToRole(user.Id, role);
                //}


                //Update User table
                var result = await Task.Run(() => mUserRepository.SaveUser(user));
                aspnetUserId = result;

                **/
                return Ok(new { StatusCode = 200, Status = "ERROR", aspNetUserId = aspnetUserId, Message = "User updated successfully!", Data = "" });
            }

            catch (Exception ex)
            {
                DeleteUser(identityUser);
                return Ok(new
                {
                    Message = ex.Message.ToString(),
                    StatusCode = 500,
                    Status = "ERROR",
                    Data = ""
                });

                // return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
            }

        }


        [HttpPost]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(User user)
        {

            IdentityUser identityUser = null;
            string aspnetUserId = "";

            try
            {                                       

                //Delete from User table
                var result = await Task.Run(() => mUserRepository.SaveUser(user));
                aspnetUserId = result;

                //// Delete ASPNet User Role from table
                //await userManager.RemoveFromRoleAsync(identityUser, user.Role);

                ////Delete ASPNet User from table
                //identityUser = new IdentityUser() { UserName = user.UserName, Email = user.Email, PhoneNumber = user.Phone };
                //var resultUser = await userManager.DeleteAsync(identityUser);



                return Ok(new { StatusCode = 200, Status = "SUCCESS", aspNetUserId = aspnetUserId, Message = "User Deleted successfully!", Data = "" });
            }

            catch (Exception ex)
            {
                DeleteUser(identityUser);
                return Ok(new
                {
                    Message = ex.Message.ToString(),
                    StatusCode = 500,
                    Status = "ERROR",
                    Data = ""
                });

                // return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
            }

        }


        //save company details
        [HttpPost]
        [Route("SaveUserCompany")]
        public async Task<IActionResult> SaveUserCompany(UserCompany savecompany)
        {
            try
            {
                var result = await Task.Run(() => mUserRepository.SaveUserCompany(savecompany));
                if (Convert.ToString(result).ToUpper() == "SUCCESS")
                {
                    //return Ok(new { Status = "Success", Message = "UserCompany Saved successfully!" });
                    return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "UserCompany Saved successfully!", Data = result });
                }
                else
                {
                    //return new BadRequestObjectResult(new { Status = "Failed", Message = "UserCompany Saved Failed!" });
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "UserCompany Saved Failed!", Data = "" });
                }

            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }

        }
        //update company details
        [HttpPost]
        [Route("UpdateUserCompany")]
        public async Task<IActionResult> UpdateUserCompany(UserCompany updatecompany)
        {
            try
            {
                var result = await Task.Run(() => mUserRepository.UpdateUserCompany(updatecompany));
                if (Convert.ToString(result).ToUpper() == "SUCCESS")
                {
                    //return Ok(new { Status = "Success", Message = "UserCompany updated  successfully!" });
                    return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "UserCompany updated  successfully!", Data = result });
                }
                else
                {
                    //return new BadRequestObjectResult(new { Status = "Failed", Message = "UserCompany updated Failed!" });

                }
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "UserCompany updated Failed!", Data = "" });

            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }

        }

        // Update user details

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserInfo updateUser)
        {
            try
            {
                var result = await Task.Run(() => mUserRepository.UpdateUser(updateUser));
                if (Convert.ToString(result).ToUpper() == "SUCCESS")
                {
                    //return Ok(new { Status = "Success", Message = "User updated successfully!" });
                    return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "User updated successfully!", Data = result });
                }
                else
                {
                    //return new BadRequestObjectResult(new { Status = "Failed", Message = "User updated Failed!" });
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "User updated Failed!", Data = "" });
                }
                    
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });

            }

        }


        [HttpPost]
        [Route("VerifyUserEmail")]
        public async Task<ActionResult> VerifyUserEmail(VerifyUserEmail model)
        {
            try
            {

                if (!ModelState.IsValid)
                    //return new BadRequestObjectResult(new
                    //{
                    //    Message = "Invalid Email."
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Invalid Email.", Data = "" });


                bool isValidEmail = false;
                isValidEmail = RegexUtilities.IsValidEmail(model.Email);

                if (isValidEmail == false)
                {
                    //return new BadRequestObjectResult(new
                    //{
                    //    Message = "Invalid Email."
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Invalid Email.", Data = "" });
                }

                var identityUser = await userManager.FindByEmailAsync(model.Email);
                if (identityUser == null)
                {
                    //return new BadRequestObjectResult(new
                    //{
                    //    Message = "Sorry, this e-mail address does not exist in our system."
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Sorry, this e-mail address does not exist in our system.", Data = "" });
                }

                //var result = await userManager.ConfirmEmailAsync(identityUser, HttpUtility.UrlDecode(model.Token) );

                var result = await userManager.ConfirmEmailAsync(identityUser, model.Token);

                //return Ok(new { Status = "Success", Message = "Email Verified Successully" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Email Verified Successully", Data = "" });

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

        public String ReplacePlaceHolder(String inputString, String placeHolder, String newValue)
        {
            if (inputString != "" && placeHolder != "")
                return inputString.Replace(placeHolder, newValue);
            else
                return "";
        }


        public void DeleteUser(IdentityUser identityUser)
        {

            userManager.DeleteAsync(identityUser);


        }

        private async Task<IdentityUser> ValidateUser(LoginCredentials credentials)
        {
            try
            {
                var identityUser = await userManager.FindByNameAsync(credentials.Username);
               
                if (identityUser != null)
                {
                    if(credentials.Social_media_type== "APPLICATION" && credentials.Social_media_type!=null)
                    {
                        var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
                        return result == PasswordVerificationResult.Failed ? null : identityUser;
                    }
                    else
                    {
                        return identityUser;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        private object GenerateToken(IdentityUser identityUser, IList<string> roleList)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim(JwtRegisteredClaimNames.Sub, mConfiguration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", identityUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, identityUser.Email),
                    new Claim(ClaimTypes.Role, string.Join(",", roleList.ToArray()))
                }),

                Expires = DateTime.UtcNow.AddMinutes(jwtBearerTokenSettings.ExpiryTimeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPassword model)
        {
            try
            {
                String callbackUrl = "";
                if (!ModelState.IsValid)
                    //return new BadRequestObjectResult(new
                    //{
                    //    Message = "Invalid Email."
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Invalid Email.", Data = "" });

                bool isValidEmail = false;
                isValidEmail = RegexUtilities.IsValidEmail(model.Email);

                if (isValidEmail == false)
                {
                    //return new BadRequestObjectResult(new
                    //{
                    //    Message = "Invalid Email."
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Invalid Email.", Data = "" });
                }

                var identityUser = await userManager.FindByEmailAsync(model.Email);
                if (identityUser == null)
                {
                    //return new BadRequestObjectResult(new
                    //{
                    //    Message = "Sorry, this e-mail address does not exist in our system."
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Sorry, this e-mail address does not exist in our system.", Data = "" });
                }
                else
                {

                    string token = await userManager.GeneratePasswordResetTokenAsync(identityUser);
                    string s = "p";
                    if (webSettings.SystemMode.ToUpper() == "LOCAL")
                        s = "l";
                    else if (webSettings.SystemMode.ToUpper() == "STAGING")
                        s = "s";
                    else
                        s = "p";

                    callbackUrl = webSettings.UniversalURL + "/resetpassword?";
                    // if (s == "l" || s == "s")
                    //     callback = callback + "&a=" + webSettings.ExpoAsset;
                    //callback = callback + "&q=reset-password/" + HttpUtility.UrlEncode(token) + "/" + HttpUtility.UrlEncode(model.Email);
                    // callback = callback + "&q=resetpassword";
                    //callback = callback + "resetpassword";
                    callbackUrl = callbackUrl + "t=" + HttpUtility.UrlEncode(token);
                    callbackUrl = callbackUrl + "&e=" + HttpUtility.UrlEncode(model.Email);

                    Random generator = new Random();
                    String otp = generator.Next(0, 1000000).ToString("D6");
                }

                MailRequest objRequest = new MailRequest();
                objRequest.ToEmail = model.Email;
                objRequest.Subject = webSettings.ApplicationName + " - Forgot Password?";

                var pathToFile = "Templates"
                               + Path.DirectorySeparatorChar.ToString()
                               + "EmailTemplate"
                               + Path.DirectorySeparatorChar.ToString()
                               + "Forgot_Password_EmailTemplate.html";

                var builder = new MimeKit.BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }                               
                          
                string messageBody = builder.HtmlBody;

                messageBody = ReplacePlaceHolder(messageBody, "{CallBackUrl}", callbackUrl);
                if(model.Application_type== "client")
                {
                    messageBody = ReplacePlaceHolder(messageBody, "{ApplicationName}", "TravelNinjaz B2B");
                }
                else
                {
                    messageBody = ReplacePlaceHolder(messageBody, "{ApplicationName}", "TravelNinjaz");
                }
                
                objRequest.Body = messageBody;

                await mMailService.SendEmailAsync(objRequest);

                //return Ok(new { Status = "Success", Message = "Email Sent Successully" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Email Sent Successully", Data = "" });

            }
            catch(Exception ex)
            {
                //return new BadRequestObjectResult(new
                //{
                //    Message = ex.Message.ToString()
                //});
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }

        }
        // This is for token generate for changepassword
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ForgotPassword model)
        {
            string token = "";
            try
            {
                if (!ModelState.IsValid)
                    //return new BadRequestObjectResult(new
                    //{
                    //    Message = "Invalid Email."
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Invalid Email.", Data = "" });


                bool isValidEmail = false;
                isValidEmail = RegexUtilities.IsValidEmail(model.Email);

                if (isValidEmail == false)
                {
                    //return new BadRequestObjectResult(new
                    //{
                    //    Message = "Invalid Email."
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Invalid Email.", Data = "" });
                }

                var identityUser = await userManager.FindByEmailAsync(model.Email);
                if (identityUser == null)
                {
                    //return new BadRequestObjectResult(new
                    //{
                    //    Message = "Sorry, this e-mail address does not exist in our system."
                    //});
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Sorry, this e-mail address does not exist in our system.", Data = "" });
                }
                else
                {

                    token = await userManager.GeneratePasswordResetTokenAsync(identityUser);

                }
                //return Ok(new { Status = "Success", Message = "Email Sent Successully", Token = HttpUtility.UrlEncode(token) });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Email Sent Successully", Data = "" , Token = HttpUtility.UrlEncode(token) });

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

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPasswordModel)
        {

            try
            {

                if (!ModelState.IsValid)
                    //return new BadRequestObjectResult(new { Message = "Invalid Email." });
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Invalid Email.", Data = "" });
                var identityUser = await userManager.FindByEmailAsync(resetPasswordModel.Email);
                //var identityUser = await userManager.FindByEmailAsync(HttpUtility.UrlDecode(resetPasswordModel.Email));

                if (identityUser == null)
                    //return new BadRequestObjectResult(new { Message = "Sorry, this e-mail address does not exist in our system." });
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Sorry, this e-mail address does not exist in our system.", Data = "" });
                var resetPassResult = await userManager.ResetPasswordAsync(identityUser, resetPasswordModel.Token, HttpUtility.UrlDecode(resetPasswordModel.Password));
                //var resetPassResult = await userManager.ResetPasswordAsync(identityUser, HttpUtility.UrlDecode(resetPasswordModel.Token), HttpUtility.UrlDecode(resetPasswordModel.Password));

                if (!resetPassResult.Succeeded)
                {
                    var dictionary = new ModelStateDictionary();
                    foreach (IdentityError error in resetPassResult.Errors)
                    {
                        dictionary.AddModelError(error.Code, error.Description);
                    }

                    //return new BadRequestObjectResult(new { Message = "Reset password failed.", Errors = dictionary });
                    return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = "Reset password failed.", Data = "", Errors = dictionary });
                }

                //return Ok(new { Data = true, Message = "Reset password successfully." }); //this line commented by sunil kumar bais because i want a status success this line given data=true
                //return Ok(new { Status = "Success", Message = "Reset password successfully." });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Reset password successfully.", Data = "" });
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(new { Message = ex.Message.ToString() });
                return new BadRequestObjectResult(new { StatusCode = 500, Status = "ERROR", Message = ex.Message.ToString(), Data = "" });
            }

        }

        /**get user company details*/
        [HttpGet]
        [Route("getUserCompany")]
        public async Task<ActionResult<IEnumerable<User>>> getUserCompany(String AspNetUserId = "")
        {
            try
            {
                var result = await Task.Run(() => mUserRepository.getUserCompany(AspNetUserId));
                //return new OkObjectResult(new { Data = result, Message = "SUCCESS" });
                return new OkObjectResult(new { StatusCode = 200, Status = "SUCCESS", Message = "SUCCESS", Data = result });
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
        /// <created_date>May 17, 2023</created_date>
        /// <summary>
        /// Save Photos
        /// </summary>
        /// <param name="imagefiles"></param>
        /// <returns></returns>
        [Route("SaveProfileImage")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                String User_id = Request.Form["User_id"].ToString();
                var profile_folder_name = _env.ContentRootPath + "/Images/ClientProfileImage/" + User_id.ToString();
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                string filepath=profile_folder_name+"/"+filename;
                if (System.IO.Directory.Exists(profile_folder_name))
                {
                    if (!string.IsNullOrEmpty(profile_folder_name))
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(profile_folder_name);
                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                    }
                }
                
                //If folder does not exists than create new folder
                if (!System.IO.Directory.Exists(profile_folder_name))
                {
                    System.IO.Directory.CreateDirectory(profile_folder_name);
                }
                
                var physicalPath = profile_folder_name +"/"+ filename;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                //return new JsonResult(filename);
                return new JsonResult(new { StatusCode = 200, Status = "SUCCESS", Message = "Profille image Uploaded Successfully!", Data = "" });
            }
            catch (Exception)
            {
                //return new JsonResult("error");
                return new JsonResult(new { StatusCode = 500, Status = "ERROR", Message = "Profille image Uploaded Failed!", Data = "" });
            }
        }
        [Route("PreviewImportFile")]
        [HttpPost]
        public async Task<IActionResult> PreviewImportFile()
        {
            var httpRequest = Request.Form;
            String User_id = Request.Form["User_id"].ToString();
            var FileUpload = httpRequest.Files[0];
            string filename = FileUpload.FileName;

            String Status = "SUCCESS";
            String Message = "";
            //string log_fle = "";
            SetLog("Info", "Import File Starts");

            try
            {

                string file_extension = ".xlsx";
                //string file_extension = ".csv";
                if (FileUpload != null)
                {
                    if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")  // for .dat files : FileUpload.ContentType == "application/octet-stream"
                    //if (FileUpload.ContentType == "text/csv" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {

                        string file_name = System.IO.Path.GetFileName(FileUpload.FileName);
                        string Original_file_name = file_name;

                        SetLog("Info", "file_name: " + file_name);

                        if (file_name.EndsWith(file_extension))
                        {

                            String InterfaceName = "TravellingAgents_template";// Request.Form["Itf"];
                            //String InterfacePath = System.Configuration.ConfigurationManager.AppSettings["InterfacePath"] + "\\" + InterfaceName;

                            var InterfacePath = _env.ContentRootPath + "/Models/Interface/TravellingAgents";

                            //string InterfacePath_file_path = (InterfacePath + "/" + InterfaceName + "/DATA/");
                            string currentDateTimeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                            file_name = currentDateTimeStamp + "_" + file_name;
                            //string import_file_full_path = (InterfacePath + "\\DATA\\") + file_name;

                            // If folder does not exists than create new folder
                            if (!System.IO.Directory.Exists(InterfacePath))
                            {
                                System.IO.Directory.CreateDirectory(InterfacePath);
                            }
                            string import_file_full_path = InterfacePath + "/Data/" + 1.ToString() +"/"+ file_name;
                           
                            using (var stream = new FileStream(import_file_full_path, FileMode.Create))
                            {
                                FileUpload.CopyTo(stream);
                            }
                            //  FileUpload.save(import_file_full_path);

                            SetLog("Info", "File saved to DATA folder: " + import_file_full_path);

                            string template_file_full_path = InterfacePath + "/Template/" + InterfaceName + file_extension;
                            //Get both file headers
                            string TemplateFileHeader = ImportHeaders(template_file_full_path, true); //array1
                            string ImportFileHeaders = ImportHeaders(import_file_full_path, true); //array2
                            SetLog("TemplateFileHeader", TemplateFileHeader);
                            SetLog("ImportFileHeaders", ImportFileHeaders);

                            //Message = "File Imported successfully";
                            SetLog(Status, Message);
                            Char str_ConfigTerminator = Convert.ToChar(',');
                             if (!ImportFileHeaders.SequenceEqual(TemplateFileHeader))
                                {
                                    Status = "Error";
                                    Message = "There is some problem in header of Improting file. Please download Template File and import file exactly matching with template format";


                                    string[] TemplateFileHeaderArray = TemplateFileHeader.Split(str_ConfigTerminator);
                                    string[] ImportFileHeaderArray = ImportFileHeaders.Split(str_ConfigTerminator);

                                    //Check missing columns in Importing file
                                    IEnumerable<string> diff = TemplateFileHeaderArray.Except(ImportFileHeaderArray);
                                    string MissingColumns = string.Join(",", diff);

                                    if (MissingColumns != "")
                                    {
                                        Message = "(" + MissingColumns + ") Column headers are missing in Importing file. Please download Template File and import file exactly matching with template format";

                                        SetLog("Error", Message);

                                        return await ShowJsonMessage("Error", Message);
                                    }

                                    //Extra columns are there
                                    diff = ImportFileHeaderArray.Except(TemplateFileHeaderArray);
                                    string ExtraColumns = string.Join(",", diff);

                                    if (ExtraColumns != "")
                                    {
                                        Message = "(" + ExtraColumns + ") found extra in Importing file. Please download Template File and import file exactly matching with template format";

                                        SetLog("Error", Message);
                                        return await ShowJsonMessage("Error", Message);
                                    }

                                    //No of columns are same but column Order Mismatch
                                    //diff = ImportFileHeaderArray.Except(TemplateFileHeaderArray);
                                    String OrderMismatchColumns = "";
                                    for (int i = 0; i < TemplateFileHeaderArray.Length; i++)
                                    {
                                        bool found = false;
                                        if (TemplateFileHeaderArray[i] == ImportFileHeaderArray[i])
                                        {
                                            found = true;
                                        }
                                        if (!found)
                                        {

                                            OrderMismatchColumns = OrderMismatchColumns + TemplateFileHeaderArray[i] + ",";
                                        }
                                    }

                                    if (OrderMismatchColumns != "")
                                    {
                                        OrderMismatchColumns = OrderMismatchColumns.Substring(0, OrderMismatchColumns.Length - 1);
                                        Message = "(" + OrderMismatchColumns + ") Order is not proper in Importing file. Please download Template File and import file exactly matching with template format";

                                        SetLog("Error", Message);
                                        return await ShowJsonMessage("Error", Message);
                                    }
                                }
                            // return ShowJsonMessage(Status, Message, resultProductJson, resultProductTaJson);
                            return await ShowJsonMessage(Status, Message, Original_file_name, file_name);

                            //}
                            //else
                            //{
                            //    Message = "Import file's delimiter is not matching with the Template. It should be (" + str_ConfigTerminator + ")";
                            //    SetLog("Error", Message);

                            //    return ShowJsonMessage("Error", Message);
                            //}
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

        private String ImportHeaders(string srcFilePath, bool newType = true)
        {
            // Initilization
            string headers = "";

            try
            {
                //Open the Excel file in Read Mode using OpenXml.
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(srcFilePath, false))
                {
                    var workbook = doc.WorkbookPart.Workbook;
                    var sheetList = workbook.Sheets.ToList();

                    Sheet sheet = (Sheet)workbook.Sheets.ToList().FirstOrDefault();

                    //** Read Excel file Start **/
                    //Get the Worksheet instance.
                    Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                    //Fetch all the rows present in the Worksheet.
                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                    ////Loop through the Worksheet rows.
                    //WorkbookPart workbookPart = doc.WorkbookPart;
                    //SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    //SharedStringTable sst = sstpart.SharedStringTable;

                    String value = "";

                    foreach (Row row in rows)
                    {

                        //Read first row as a header
                        if (row.RowIndex.Value == 1)
                        {

                            //Crearte Datatable Columns 
                            foreach (Cell c in row.Elements<Cell>())
                            {
                                value = GetValue(doc, c);
                                headers = headers + value + ",";
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return headers;
        }

        public void SetLog(string MessageStatus, string Message)
        {
            //using (System.IO.StreamWriter sw = System.IO.File.AppendText(Server.MapPath("~/Logs/logfile.txt")))
            //{
            //    sw.WriteLine(System.DateTime.Now.ToString() + ">>" + MessageStatus + ">" + Message);
            //}
        }

        public async Task<IActionResult> ShowJsonMessage(string MessageStatus, string Message, string Original_file_name = "", string file_name = "")
        {

            return Ok(new
            {
                Status = MessageStatus,
                Message = Message,
                Original_file_name = Original_file_name,
                File_name = file_name,
            });

        }
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = "";

            if (cell.CellValue != null)
            {
                value = cell.CellValue.InnerText;

                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }
                else if (cell.DataType == null) // number & dates.
                {
                    if (cell.StyleIndex != null)
                    {
                        int styleIndex = (int)cell.StyleIndex.Value;
                        //var cellFormat = doc.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ChildElements[int.Parse(cell.StyleIndex.InnerText)] as CellFormat;

                        var cellFormat = doc.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ChildElements[int.Parse(cell.StyleIndex.InnerText)] as CellFormat;
                        uint formatId = cellFormat.NumberFormatId.Value;


                        if (cell.StyleIndex != null)
                        {
                            //if (formatId == (uint)Formats.DateShort || formatId == (uint)Formats.DateName || formatId == (uint)Formats.DateFormat2 || formatId == (uint)Formats.DateFormat3 || formatId == (uint)Formats.DateLong || formatId == (uint)164)
                            //{
                            //    double oaDate;
                            //    if (double.TryParse(cell.InnerText, out oaDate))
                            //    {
                            //        value = DateTime.FromOADate(oaDate).ToShortDateString();
                            //    }
                            //    else
                            //    {
                            //        value = cell.CellValue.InnerText;
                            //    }
                            //}

                            var dateFormat = GetDateTimeFormat(cellFormat.NumberFormatId);

                            if (dateFormat != null && dateFormat != "")
                            {
                                value = DateTime.FromOADate(double.Parse(value)).ToString();
                            }
                            else if (formatId == (uint)Formats.Percentage)
                            {
                                double pct = 0;
                                if (double.TryParse(cell.InnerText, out pct))
                                {
                                    //value = String.Format("{0:0.##}", pct * 100);
                                    //value = pct.ToString();
                                    value = (pct * 100).ToString("0.00");
                                }
                                else
                                {
                                    value = cell.CellValue.InnerText;
                                }
                            }
                            //else if (formatId == (uint)Formats.DateLong)
                            //{
                            //    //value = String.Format("{0:0.##}", value );
                            //    double num = 0;
                            //    if (double.TryParse(cell.InnerText, out num))
                            //    {
                            //        value = num.ToString("0.00");
                            //    }
                            //    else
                            //    {
                            //        value = cell.CellValue.InnerText;
                            //    }
                            //}
                            else
                            {
                                value = cell.CellValue.InnerText;
                            }
                        }
                        else
                        {
                            value = cell.CellValue.InnerText;
                        }
                    }

                }
            }
            return value;
        }
        private string GetDateTimeFormat(uint numberFormatId)
        {
            return DateFormatDictionary.ContainsKey(numberFormatId) ? DateFormatDictionary[numberFormatId] : string.Empty;
        }
        private readonly Dictionary<uint, string> DateFormatDictionary = new Dictionary<uint, string>()
        {
            [14] = "dd/MM/yyyy",
            [15] = "d-MMM-yy",
            [16] = "d-MMM",
            [17] = "MMM-yy",
            [18] = "h:mm AM/PM",
            [19] = "h:mm:ss AM/PM",
            [20] = "h:mm",
            [21] = "h:mm:ss",
            [22] = "M/d/yy h:mm",
            [30] = "M/d/yy",
            [34] = "yyyy-MM-dd",
            [45] = "mm:ss",
            [46] = "[h]:mm:ss",
            [47] = "mmss.0",
            [51] = "MM-dd",
            [52] = "yyyy-MM-dd",
            [53] = "yyyy-MM-dd",
            [55] = "yyyy-MM-dd",
            [56] = "yyyy-MM-dd",
            [58] = "MM-dd",
            [165] = "M/d/yy",
            [166] = "dd MMMM yyyy",
            [167] = "dd/MM/yyyy",
            [168] = "dd/MM/yy",
            [169] = "d.M.yy",
            [170] = "yyyy-MM-dd",
            [171] = "dd MMMM yyyy",
            [172] = "d MMMM yyyy",
            [173] = "M/d",
            [174] = "M/d/yy",
            [175] = "MM/dd/yy",
            [176] = "d-MMM",
            [177] = "d-MMM-yy",
            [178] = "dd-MMM-yy",
            [179] = "MMM-yy",
            [180] = "MMMM-yy",
            [181] = "MMMM d, yyyy",
            [182] = "M/d/yy hh:mm t",
            [183] = "M/d/y HH:mm",
            [184] = "MMM",
            [185] = "MMM-dd",
            [186] = "M/d/yyyy",
            [187] = "d-MMM-yyyy"
        };
        private enum Formats
        {
            General = 0,
            Number = 1,
            Decimal = 2,
            Currency = 164,
            Accounting = 44,
            DateShort = 14,
            DateName = 15,
            DateLong = 165,
            DateFormat2 = 176,
            DateFormat3 = 178,
            Time = 166,
            Percentage = 10,
            Fraction = 12,
            Scientific = 11,
            Text = 49
        }
        //[HttpPost]
        //[Route("ForgotPassword")]
        //public async Task<ActionResult> ForgotPassword(ForgotPassword model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return new BadRequestObjectResult(new
        //            {
        //                Message = "Invalid Email."
        //            });

        //        bool isValidEmail = false;

        //        isValidEmail = RegexUtilities.IsValidEmail(model.Email);
        //        if (isValidEmail == false)
        //        {
        //            return new BadRequestObjectResult(new
        //            {
        //                Message = "Invalid Email."
        //            });
        //        }

        //        var identityUser = await userManager.FindByEmailAsync(model.Email);
        //        if (identityUser == null)
        //        {
        //            return new BadRequestObjectResult(new
        //            {
        //                Message = "Sorry, this e-mail address does not exist in our system."
        //            });
        //        }

        //        string token = await userManager.GeneratePasswordResetTokenAsync(identityUser);
        //        string s = "p";
        //        if (webSettings.SystemMode.ToUpper() == "LOCAL")
        //            s = "l";
        //        else if (webSettings.SystemMode.ToUpper() == "STAGING")
        //            s = "s";
        //        else
        //            s = "p";

        //        string callback = webSettings.UniversalURL + "?s=" + s;
        //        if (s == "l" || s == "s")
        //            callback = callback + "&a=" + webSettings.ExpoAsset;
        //        //callback = callback + "&q=reset-password/" + HttpUtility.UrlEncode(token) + "/" + HttpUtility.UrlEncode(model.Email);
        //        callback = callback + "&q=reset-password";
        //        callback = callback + "&t=" + HttpUtility.UrlEncode(token);
        //        callback = callback + "&e=" + HttpUtility.UrlEncode(model.Email);

        //        Random generator = new Random();
        //        String otp = generator.Next(0, 1000000).ToString("D6");

        //        System.Text.StringBuilder body = new System.Text.StringBuilder();

        //        UserInfo Userdata = mUserRepository.GetUserDetailByEmailID(model.Email);

        //        //body.AppendLine(EmailHelper.ForgotPaasswordBody(Userdata.FirstName, callback));
        //        body.AppendLine(EmailHelper.ForgotPaasswordBodyWithOTP(Userdata.FirstName, otp));

        //        mUserRepository.SaveUserResetPasswordOTP(Userdata.UserId, otp, Userdata.UserId);

        //        //var result = await Task.Run(() => mUserRepository.SendForgotPasswordEmail(model.Email, "Forgot Password", body.ToString()));
        //        var result = await Task.Run(() => mUserRepository.SendForgotPasswordEmail(model.Email, "Mendability for Schools Password Reset", body.ToString()));

        //        return Ok(new { Data = true, Message = "Email Sent Successully" });

        //    }
        //    catch (Exception ex)
        //    {
        //        return new BadRequestObjectResult(new
        //        {
        //            Message = ex.Message.ToString()
        //        });
        //    }
        //}

    }
}
