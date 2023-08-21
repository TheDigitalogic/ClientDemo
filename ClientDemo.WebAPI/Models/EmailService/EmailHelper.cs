using System;

namespace TravelNinjaz.B2B.WebAPI.Models.EmailService
{
    public class EmailHelper
    {

        //Will format later on
        public static String Header()
        {
            System.Text.StringBuilder header = new System.Text.StringBuilder();

            header.AppendFormat(@"
            <!doctype html>
            <html>
                    <head>    
                        <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
                        <title>
                        </title>
                        <style type=""text/css"">
		                </style>
                    </head>
                  <body style=""background-color: #EEE;"">
                     <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px;"">
                        <div style = ""border: 0px solid transparent;padding: 15px;"">
                                <div class=""img-container left fixedwidth"" style=""padding-right: 0px; padding-left: 0px; margin-left: 0px;"" align=""left"">
                                    <img class=""left fixedwidth"" style=""text-decoration: none; -ms-interpolation-mode: bicubic; height: auto; border: 0; width: auto; display: block;"" title=""Mendability"" src=""https://www.mendability.com/wp-content/uploads/logo-200.png"" alt=""Mendability"" width=""189"" border=""0""> 
                                </div>
                        </div>                       
                    </div>
            ");


            return header.ToString();
        }

        public static String Footer()
        {
            System.Text.StringBuilder footer = new System.Text.StringBuilder();
            footer.AppendFormat(@"
                        <p style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '> 
                                If you have any questions during this process or at any time as you implement Mendability's enrichment program, don't hesitate to:
                        </p>
                        <p style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '> 
                           <ul>                              
                             <li style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; '>call: <a href=""tel:+18016926830"">+1 801-692-6830</a>, or </li>
                             <li style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; '>email: <a href=""mailto:support@mendability.com"">support@mendability.com</a></li>
                           </ul>
                        </p>
                        <p style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
                            <br > --The Mendability Team
                        </p> 
                         <br >
                ");

            return footer.ToString();
        }

        public static String Footer2()
        {
            System.Text.StringBuilder footer = new System.Text.StringBuilder();
            //footer.AppendFormat(@"
            //            <div style=""clear: both; margin-top: 20px; margin-bottom:50px;max-width: 580px; text-align: center; background-color: transparent;"">
            //                    <p style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 13px; color: #999999; line-height: 1.6; margin: 20px; '>
            //                        This is an important notification email about your Mendability account.
            //                        <br > 
            //                        Mendability, 3651 N 100 E, Ste 375, Provo UT 84604
            //                    </p> 
            //            </div>
            //        </body>
            //    </html>
            //    ");

            footer.AppendFormat(@"
                        <div style=""clear: both; margin-top: 20px; margin-bottom:50px; box - sizing: border - box; display: lock; margin: 0 auto; padding: 0px 0px; max - width: 580px; "">
                               <p style = 'font-family: ""Trebuchet MS"", arial, sans-serif;font-size: 13px;color: #999999;line-height: 1.6;margin: 20px;width: 580;text-align: center;' >
                                            This is an important notification email about your Mendability account.
                                    <br/>
                                    Mendability, 3651 N 100 E, Ste 375, Provo UT 84604
                                </p>
                        </div>
                        <br/>
                    </body>
                </html>
                ");


            return footer.ToString();
        }
        public static String ForgotPaasswordBodyWithOTP(String userFirstName, String otpCode)
        {

            System.Text.StringBuilder body = new System.Text.StringBuilder();

            body.AppendFormat(@"                
                        {0}
                        <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px; border: solid 1px #ddd; background-color: #FFFFFF"">                 
                                    <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>Hello {3},</p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>You recently requested to reset your password for your Mendability for Schools account. </p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>To complete the process, please use the code below:</p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '></p>
                                                <div style = 'text-align: center; width:100%; font-size:30px; font-weight:bold' >
                                                   {4}
                                                </div>
                            {1}
                      </div>
                        {2}
                </body>
        </html>
", Header(), Footer(), Footer2(), userFirstName, otpCode);

            return body.ToString();
        }
        public static String ForgotPaasswordBody(String userFirstName, String callback)
        {

            System.Text.StringBuilder body = new System.Text.StringBuilder();

            //            body.AppendFormat(@"                
            //                        {0}
            //                        <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px; border: solid 1px #ddd; background-color: #FFFFFF"">
            //                              <div class=""divTable main"">
            //                                    <div style='display: table-row; width: 100%; font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '
            //                                        <div style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
            //                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>Hello {3},</p>
            //                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>You recently requested to Reset your password of Mendability for Schools account. </p>
            //                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>In order to reset your password, please click on the button below:</p>
            //                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '></p>
            //                                                <div style = 'text-align: center; width:100%;' >
            //                                                    <a style='display: inline-block;color: #ffffff;background-color: #00A8E2;border: solid 1px #00A8E2;border-radius: 50px;box-sizing: border-box;cursor: pointer;text-decoration: none;font-size: 16px;margin: 10px 0;padding: 13px 25px;border-color: #00A8E2;' 
            //                                                    href = '{4}' target = '_blank' > Reset your Mendability password </a>
            //                                                </div>
            //                                        </div>
            //                                    </div>  
            //                                    {1}
            //                               </div>                             
            //                        </div>
            //                        {2}
            //                </body>
            //        </html>

            //", Header(), Footer(), Footer2(), userFirstName, callback.ToString());


            body.AppendFormat(@"                
                        {0}
                        <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px; border: solid 1px #ddd; background-color: #FFFFFF"">                 
                                    <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>Hello {3},</p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>You recently requested to reset your password of Mendability for Schools account. </p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>In order to reset your password, please click on the button below:</p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '></p>
                                                <div style = 'text-align: center; width:100%;' >
                                                    <a style='font-family: ""Trebuchet MS"", arial, sans-serif; display: inline-block;color: #ffffff;background-color: #00A8E2;border: solid 1px #00A8E2;border-radius: 50px;box-sizing: border-box;cursor: pointer;text-decoration: none;font-size: 16px;margin: 10px 0;padding: 13px 25px;border-color: #00A8E2;' 
                                                    href = '{4}' target = '_blank' > Reset your Mendability password </a>
                                                </div>
                            {1}
                      </div>
                        {2}
                </body>
        </html>
", Header(), Footer(), Footer2(), userFirstName, callback);

            return body.ToString();
        }

        public static String FooterWorksheetEndingReportToSchoolAdmin()
        {
            System.Text.StringBuilder footer = new System.Text.StringBuilder();
            footer.AppendFormat(@"
                        <p style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '> 
                           <ul>                              
                             <li style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; '>call: <a href=""tel:+18016926830"">+1 801-692-6830</a>, or </li>
                             <li style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; '>email: <a href=""mailto:support@mendability.com"">support@mendability.com</a></li>
                           </ul>
                        </p>
                        <p style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
                            <br > --The Mendability Team
                        </p> 
                ");

            return footer.ToString();
        }

        public static String WorksheetEndingReportToSchoolAdmin(String userFirstName, String enrichmentgroupName, String schoolAdminFullName, String className, String callback)
        {

            System.Text.StringBuilder body = new System.Text.StringBuilder();

            body.AppendFormat(@"                
                        {0}
                        <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px; border: solid 1px #ddd; background-color: #FFFFFF"">                 
                                    <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>Hello {2},</p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; font-weight: bold;'>You just activated the last worksheet that Mendability programmed for the {3} ({5}). </p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>If you would like more Sensory Enrichment activities for this group, please discuss this with {4}, supervisor of the Sensory Enrichment program.</p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>As always, if you have any questions during this process or at any time as you implement Mendability's enrichment program, don't hesitate to:</p>
                            {1}
                      </div>
                        {6}
                    </body>
                </html>", Header(), FooterWorksheetEndingReportToSchoolAdmin(), userFirstName, enrichmentgroupName, schoolAdminFullName, className, Footer2(), callback);

            return body.ToString();
        }

        public static String WorksheetEndingReportToMendabilityAdmin(String staffFullName, String schoolName, String enrichmentGroupName, String schoolAdminFullName, String schoolAdminEmail, String staffAdminFullName, String staffAdminEmail, String className, String callback)
        {

            System.Text.StringBuilder body = new System.Text.StringBuilder();

            body.AppendFormat(@"                
                        {0}
                        <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px; border: solid 1px #ddd; background-color: #FFFFFF"">                 
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>Hello team,</p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>{1} at {2} just activated the last worksheet for the {3} ({8}). </p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>If you would like to reach out to them about this, here are some contact details:</p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>- {4} - {5}</p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>- {6} - {7}</p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>--The School Program Computer</p>
                        </div>
                        {9}
                    </body>
                </html>", Header(), staffFullName, schoolName, enrichmentGroupName, schoolAdminFullName, schoolAdminEmail, staffAdminFullName, staffAdminEmail, className, Footer2(), callback);

            return body.ToString();
        }

        public static String newWorksheetToMendabilityTeam(String staffFullName, String schoolName, String enrichmentGroupName, String schoolAdminFullName, String schoolAdminEmail, String staffAdminFullName, String staffAdminEmail, String className, String callback)
        {

            System.Text.StringBuilder body = new System.Text.StringBuilder();

            body.AppendFormat(@"                
                        {0}
                        <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px; border: solid 1px #ddd; background-color: #FFFFFF"">                 
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>Hello team,</p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>{1} at {2}  just completed the baseline profile(s) for {3} ({8}). </p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>Please create an enrichment program for them at your earliest convenience.</p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>If you would like to reach out to them about this, here are some contact details:</p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>- {4} - {5}</p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>- {6} - {7}</p>
                          <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>--The School Program Computer</p>
                        </div>
                        {9}
                    </body>
                </html>", Header(), staffFullName, schoolName, enrichmentGroupName, schoolAdminFullName, schoolAdminEmail, staffAdminFullName, staffAdminEmail, className, Footer2(), callback);

            return body.ToString();
        }

        //public static String SetPaasswordBody(Entity.User userToCreate, Entity.User userData, String callback)
        public static String SetPasswordBody(string inviteeFullName, string userFirstName, String callback, string appURL, string userName, string password)
        {

            System.Text.StringBuilder body = new System.Text.StringBuilder();
            string text1 = string.Empty;
            string text2 = string.Empty;
            string image = string.Empty;

            //Old
            if (password == "" || password == null)
            {
                text1 = "You may now install the app on your iPad and set up a password for your account.";
                text2 = "Your password is not yet created. Please use the “New Password” link on the login page to create it.";
                image = "<div style = 'text-align: center; width:100%;'><img style = 'width: 500px;' src = 'https://app2.mendability.com/images/new-password.png'></div>";
            }
            else
            {
                text1 = "You may now install the app on your iPad and log in, using the password that they created for you.";
                text2 = "- Your password is: " + password;
            }

            body.AppendFormat(@"                
                                    {0}
                                    <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px; border: solid 1px #ddd; background-color: #FFFFFF"">                 
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
                                                    Hello {3},
                                                </p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin:20px; '> 
                                                   As you may be aware, your school is implementing the Mendability enrichment program.
                                                </p><p></p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin:20px; '> 
                                                    {4} has set up an account for you on Mendability for Schools.</p><p></p>
                                                <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin:20px; '> 
                                                    {9}</p>
                                                  <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin:20px; '> 
                                                    1) About your login credentials.</p>
                                                   <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin:20px; '> 
                                                   - Your username is your email address: {7}</p>
                                                   <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin:20px; '> 
                                                   {10}</p>
                                                   {11}
                                                  <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin:20px; '> 
                                                   2) About installing the App</p>
                                                   <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin:20px; '> 
                                                  You can look it up in the App Store under Mendability or click on the button below to access the App Store directly.</p>
                                                   <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin:20px; '> 
                                                <div style = 'text-align: center; width:100%;' >
                                                    <a href = '{6}' target = '_blank'>    
                                                      <img style=""width: 200px;"" src=""https://app2.mendability.com/images/app_store_badge.png"" > 
                                                   </a>                                 
                                                </div>
                                        {1}
                                  </div>
                                    {2}
                            </body>
                    </html>
            ", Header(), Footer(), Footer2(), userFirstName, inviteeFullName, callback, appURL, userName, password, text1, text2, image);
            return body.ToString();
        }


        public static String SetNotifyBody(string FullName, string ClassName, string TeacherName)
        {
            System.Text.StringBuilder body = new System.Text.StringBuilder();
            body.AppendFormat(@"                
                                        {0}
                                        <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px; border: solid 1px #ddd; background-color: #FFFFFF"">                 
                                                    <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
                                                       Hello,
                                                    </p>
                                                    <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
                                                       {3} released new activities in {4}. You can review them now on the activities page in the Mendability app.
                                                    </p>
    
                                            {1}
                                      </div>
                                        {2}
                                </body>
                        </html>
                ", Header(), Footer(), Footer2(), FullName, ClassName, TeacherName);
            return body.ToString();
        }

        public static String SetNotifyStudent(string FullName, string ClassName, string SchoolDistrict , string School , string TemporaryClass , string StudentCodeName)
        {
            System.Text.StringBuilder body = new System.Text.StringBuilder();
            body.AppendFormat(@"                
                                        {0}
                                        <div style=""box-sizing: border-box; display: lock; margin: 0 auto; padding:0px 0px;  max-width: 580px; border: solid 1px #ddd; background-color: #FFFFFF"">                 
                                                    <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
                                                       Hello team,
                                                    </p>
                                                    <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
                                                       {3} has just added a new student to {4}. Due to the current limitations of this app, we need to complete the process for them. Please complete the process for them.
                                                    </p>
                                                      <p style='font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
                                                       Info:
                                                           <ul> 
                                                            <li> School District: {5}  </li>
                                                            <li> School : {6} </li>
                                                            <li> Class : {4}  </li>
                                                            <li> Temporary Class : {7}  </li>
                                                            <li> Student : {8} </li>
                                                            
                                                            </ul>
                                                    </p>
                                                  <p style = 'font-family: ""Trebuchet MS"", arial, sans-serif;   font-size: 12pt; line-height: 1.6; background-color: #FFFFFF; margin: 20px; '>
                                                        <br > --The Mendability System
                                                    </p> 
                                                     <br >
                                            
                                      </div>
                                        {2}
                                </body>
                        </html>
                ", Header(), Footer(), Footer2(), FullName, ClassName, SchoolDistrict, School , TemporaryClass, StudentCodeName);
            return body.ToString();
        }
    }
}
