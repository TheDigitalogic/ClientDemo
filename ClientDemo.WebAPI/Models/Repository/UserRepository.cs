using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using TravelNinjaz.B2B.WebAPI.Configuration;
//using TravelNinjaz.B2B.WebAPI.Models.EmailService;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.Interface;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{

    public class UserRepository : IUserRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        private readonly WebSettings mWebSettings;
        #endregion

        #region "Constructor"

        public UserRepository(IConfiguration configuration)
        {
            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }


        #endregion

        #region "User CRUD Functions"
        //public UserInfo UserCheckAuthentication(string email, string password)
        //{
        //    System.Data.Common.DbCommand sqlCommand = null;
        //    DataSet DS = null;
        //    UserInfo result = null;
        //    try~
        //    {
        //        if (mDB == null)
        //        {
        //            mDB = new SqlDatabase(mConnectionString);
        //            mConnection = mDB.CreateConnection();
        //        }

        //        sqlCommand = mDB.GetStoredProcCommand("[dbo].[spUserCheckAuthentication]");
        //        sqlCommand.CommandTimeout = 0;

        //        mDB.AddInParameter(sqlCommand, "@email", DbType.String, email);
        //        mDB.AddInParameter(sqlCommand, "@password", DbType.String, password);

        //        DS = mDB.ExecuteDataSet(sqlCommand);

        //        if (DS != null && DS.Tables.Count > 0)
        //        {
        //            if (DS.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow dr = DS.Tables[0].Rows[0];

        //                result = new UserInfo();

        //                result.UserId = Convert.ToInt32(dr["user_id"]);
        //                result.FirstName = Convert.ToString(dr["first_name"]);
        //                result.LastName = Convert.ToString(dr["last_name"]);
        //                result.Email = Convert.ToString(dr["email"]);
        //                result.UserName = Convert.ToString(dr["user_name"]);
        //                result.UserStatus = "Online";
        //                result.IsLoggedIn = true;

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (mConnection != null)
        //        {
        //            if (mConnection.State != ConnectionState.Closed)
        //            {
        //                mConnection.Close();
        //            }
        //            mConnection.Dispose();
        //            mConnection = null;
        //            mDB = null;
        //        }
        //    }
        //    return result;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User uspGetUserDetailByUserName(string userName)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet DS = null;
            User result = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetUserDetailByUserName]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_user_name", DbType.String, userName);

                DS = mDB.ExecuteDataSet(sqlCommand);

                if (DS != null && DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = DS.Tables[0].Rows[0];
                        result = new User();
                        result.UserId = Convert.ToInt32(dr["user_id"]);
                        result.AspNetUserId = Convert.ToString(dr["AspNetUserId"]);
                        result.FirstName = Convert.ToString(dr["first_name"]);
                        result.LastName = Convert.ToString(dr["last_name"]);
                        result.UserName = Convert.ToString(dr["UserName"]);
                        result.Email = Convert.ToString(dr["Email"]);
                        result.Is_active = Convert.ToBoolean(dr["is_active"]);
                        result.Phone = Convert.ToString(dr["PhoneNumber"]);
                        result.Role = Convert.ToString(dr["Role"]);
                    }
                }

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    mDB = null;
                }
            }
            return result;
        }
        /**This is for get user*/
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>20 July, 2023</created_date>
        /// <summary>
        /// Get  Users List
        /// </summary>
        /// <returns>list of Users</returns>
        public List<Entity.User> GetUserList()
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.User> processUserColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }
                
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetUser]");
                sqlCommand.CommandTimeout = 0;
                result = mDB.ExecuteDataSet(sqlCommand);
                if (result != null && result.Tables.Count > 0)
                {
                    processUserColl = new List<User>();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.User objUsers = new Entity.User();
                        objUsers.UserId = Convert.ToInt32(dr["user_id"]);
                        objUsers.AspNetUserId= Convert.ToString(dr["AspNetUserId"]);
                        objUsers.FirstName = Convert.ToString(dr["first_name"]);
                        objUsers.LastName= Convert.ToString(dr["last_name"]);
                        objUsers.UserName= Convert.ToString(dr["UserName"]);
                        objUsers.Phone = Convert.ToString(dr["PhoneNumber"]);
                        objUsers.Email= Convert.ToString(dr["Email"]);
                        objUsers.Role=Convert.ToString(dr["Role"]);
                        processUserColl.Add(objUsers);
                        objUsers = null;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    mDB = null;
                }
            }
            return processUserColl;
        }

        /// <created_by>Heta Shah</created_by>
        /// <created_date>July 08, 2020</created_date> 
        /// <summary>
        ///  Create / Update User data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="lastName"></param>
        /// <param name="title"></param>
        /// <param name="score"></param>
        /// <param name="profileImage"></param>
        /// <param name="inactive"></param>
        /// <param name="dataManagedBy"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public string SaveUser(User userToSave)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            int resultId = 0;
            string status = "";
            string aspNetUserId = "";
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveUser]");
                sqlCommand.CommandTimeout = 0;

                if (userToSave.UserId > 0)
                 mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.Int64, userToSave.UserId);
                mDB.AddInParameter(sqlCommand, "@pa_asp_net_user_id", DbType.String, userToSave.AspNetUserId);
                mDB.AddInParameter(sqlCommand, "@pa_phone", DbType.String, userToSave.Phone);
                mDB.AddInParameter(sqlCommand, "@pa_first_name", DbType.String, userToSave.FirstName);
                mDB.AddInParameter(sqlCommand, "@pa_last_name", DbType.String, userToSave.LastName);

                if (userToSave.Operation == "ADD")
                mDB.AddInParameter(sqlCommand, "@pa_is_active", DbType.Boolean, true);
               else
                mDB.AddInParameter(sqlCommand, "@pa_is_active", DbType.Boolean, userToSave.Is_active);


                mDB.AddInParameter(sqlCommand, "@pa_row_created_by", DbType.String, userToSave.UserName); 
                mDB.AddInParameter(sqlCommand, "@pa_row_created_date", DbType.DateTime, DateTime.Now);

                mDB.AddInParameter(sqlCommand, "@pa_row_altered_by", DbType.String,userToSave.UserName);
                mDB.AddInParameter(sqlCommand, "@pa_row_altered_date", DbType.DateTime, DateTime.Now);

                mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, userToSave.Operation);
 
                mDB.AddOutParameter(sqlCommand, "@pa_user_id_out", DbType.Int32, 10);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 2000);


                mDB.ExecuteNonQuery(sqlCommand);

                resultId = Convert.ToInt32(mDB.GetParameterValue(sqlCommand, "@pa_user_id_out"));
                status = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));
                aspNetUserId = userToSave.AspNetUserId;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    mDB = null;
                }
            }
            return aspNetUserId;
        }

        //update user company

        public string UpdateUser(UserInfo updateUser)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            int resultId = 0;
            string status = "";

            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveUser]");
                sqlCommand.CommandTimeout = 0;
                 mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.Int64, updateUser.UserId);
                mDB.AddInParameter(sqlCommand, "@pa_asp_net_user_id", DbType.String, updateUser.AspNetUserId);
                mDB.AddInParameter(sqlCommand, "@pa_phone", DbType.String, updateUser.Phone);
                mDB.AddInParameter(sqlCommand, "@pa_first_name", DbType.String, updateUser.FirstName);
                mDB.AddInParameter(sqlCommand, "@pa_last_name", DbType.String, updateUser.LastName);
                mDB.AddInParameter(sqlCommand, "@pa_profession", DbType.String, updateUser.Profession);
                mDB.AddInParameter(sqlCommand, "@pa_country", DbType.String, updateUser.Country);
                mDB.AddInParameter(sqlCommand, "@pa_state", DbType.String, updateUser.State);
                mDB.AddInParameter(sqlCommand, "@pa_city ", DbType.String, updateUser.City);
                mDB.AddInParameter(sqlCommand, "@pa_zipcode", DbType.String, updateUser.Zipcode);
                mDB.AddInParameter(sqlCommand, "@pa_is_active", DbType.Boolean, updateUser.Is_active);
                mDB.AddInParameter(sqlCommand, "@pa_row_altered_by", DbType.String, updateUser.Email);
                mDB.AddInParameter(sqlCommand, "@pa_row_altered_date", DbType.DateTime, DateTime.Now);
                mDB.AddInParameter(sqlCommand, "@pa_profile_image ", DbType.String, updateUser.Profile_image);
                mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, updateUser.Operation);
                mDB.AddOutParameter(sqlCommand, "@pa_user_id_out", DbType.Int32, 10);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 2000);

                
                mDB.ExecuteNonQuery(sqlCommand);

                resultId = Convert.ToInt32(mDB.GetParameterValue(sqlCommand, "@pa_user_id_out"));
                status = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    mDB = null;
                }
            }
              return "SUCCESS";
        }

        /**update company user*/
        public string SaveUserCompany(UserCompany savecompany)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            int resultId = 0;
            string status = "";
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveUserCompany]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_user_company_id", DbType.Int64, savecompany.UserCompanyId);
                mDB.AddInParameter(sqlCommand, "@pa_asp_net_user_id", DbType.String, savecompany.AspNetUserId);
                mDB.AddInParameter(sqlCommand, "@pa_company_name", DbType.String, savecompany.Company_name);
                mDB.AddInParameter(sqlCommand, "@pa_company_gst_no", DbType.String, savecompany.Company_gst_no);
                mDB.AddInParameter(sqlCommand, "@pa_is_active", DbType.Boolean, savecompany.Is_active);
                mDB.AddInParameter(sqlCommand, "@pa_row_created_by", DbType.String, savecompany.Row_created_by);
                mDB.AddInParameter(sqlCommand, "@pa_row_created_date", DbType.DateTime, DateTime.Now);
                mDB.AddInParameter(sqlCommand, "@pa_row_altered_by", DbType.String, savecompany.Row_altered_by);
                mDB.AddInParameter(sqlCommand, "@pa_row_altered_date", DbType.DateTime, DateTime.Now);
                mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, savecompany.Operation);
                mDB.AddOutParameter(sqlCommand, "@pa_user_company_id_out", DbType.Int32, 10);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 2000);


                mDB.ExecuteNonQuery(sqlCommand);

                resultId = Convert.ToInt32(mDB.GetParameterValue(sqlCommand, "@pa_user_company_id_out"));
                status = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    mDB = null;
                }
            }
            return "SUCCESS";
        }

        /**update company user*/
        public string UpdateUserCompany(UserCompany updatecompany)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            int resultId = 0;
            string status = "";

            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveUserCompany]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_user_company_id", DbType.Int64, updatecompany.UserCompanyId);
                mDB.AddInParameter(sqlCommand, "@pa_asp_net_user_id", DbType.String, updatecompany.AspNetUserId);
                mDB.AddInParameter(sqlCommand, "@pa_company_name", DbType.String, updatecompany.Company_name);
                mDB.AddInParameter(sqlCommand, "@pa_company_phone", DbType.String, updatecompany.Company_phone);
                mDB.AddInParameter(sqlCommand, "@pa_company_email", DbType.String, updatecompany.Company_email);
                mDB.AddInParameter(sqlCommand, "@pa_company_gst_no", DbType.String, updatecompany.Company_gst_no);
                mDB.AddInParameter(sqlCommand, "@pa_company_website", DbType.String, updatecompany.Company_website);
                mDB.AddInParameter(sqlCommand, "@pa_company_zipcode", DbType.Int32, updatecompany.Company_zipcode);
                mDB.AddInParameter(sqlCommand, "@pa_company_country", DbType.String, updatecompany.Company_country);
                mDB.AddInParameter(sqlCommand, "@pa_currenyUnit", DbType.String, updatecompany.CurrencyUnit);
                mDB.AddInParameter(sqlCommand, "@pa_company_state", DbType.String, updatecompany.Company_state);
                mDB.AddInParameter(sqlCommand, "@pa_company_city", DbType.String, updatecompany.Company_city);
                mDB.AddInParameter(sqlCommand, "@pa_company_description", DbType.String, updatecompany.Company_description);
                mDB.AddInParameter(sqlCommand, "@pa_is_active", DbType.Boolean, updatecompany.Is_active);
                mDB.AddInParameter(sqlCommand, "@pa_row_altered_by", DbType.String, updatecompany.Row_altered_by);
                mDB.AddInParameter(sqlCommand, "@pa_row_altered_date", DbType.DateTime, DateTime.Now);

                mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, updatecompany.Operation);
                mDB.AddOutParameter(sqlCommand, "@pa_user_company_id_out", DbType.Int32, 10);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 2000);


                mDB.ExecuteNonQuery(sqlCommand);

                resultId = Convert.ToInt32(mDB.GetParameterValue(sqlCommand, "@pa_user_company_id_out"));
                status = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    mDB = null;
                }
            }
            return "SUCCESS";
        }

        public User getUserCompany(string AspNetUserId)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet DS = null;
            User objUser = null;
            String TableName = "";
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetUserCompany]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_aspnet_user_id", DbType.String, AspNetUserId);

                DS = mDB.ExecuteDataSet(sqlCommand);

                if (DS != null && DS.Tables.Count > 0)
                {
                    foreach (DataTable dt in DS.Tables){
                        if (dt.Rows.Count > 0)
                        {
                            TableName = dt.Rows[0]["TableName"].ToString();
                              
                            if (TableName == "USER")
                            {
                                objUser = new User();
                                DataRow dr = dt.Rows[0];
                                objUser.AspNetUserId= Convert.ToString(dr["AspNetUserId"]);
                                objUser.UserId = Convert.ToInt32(dr["user_id"]);
                                objUser.FirstName = Convert.ToString(dr["first_name"]);
                                objUser.LastName = Convert.ToString(dr["last_name"]);
                                objUser.Phone= Convert.ToString(dr["PhoneNumber"]); 
                                objUser.Profession = Convert.ToString(dr["Profession"]);
                                objUser.City = Convert.ToString(dr["City"]);
                                objUser.State = Convert.ToString(dr["State"]);
                                objUser.Country = Convert.ToString(dr["Country"]);
                                objUser.Zipcode = Convert.ToInt32(dr["Zipcode"]);
                                objUser.Profile_image = Convert.ToString(dr["Profile_image"]);
                                //objUser.Profession = Convert.ToString(dr["Profession"]);
                                //objUser.Email = Convert.ToString(dr["Email"]);
                                //objUser.Is_active = Convert.ToBoolean(dr["is_active"]);
                                //objUser.Phone = Convert.ToString(dr["PhoneNumber"]);

                            }
                            else if (TableName == "USERCOMPANY")
                            {
                                DataRow dr = dt.Rows[0];
                                UserCompany objCompany = new UserCompany();
                                objCompany.UserCompanyId= Convert.ToInt32(dr["user_company_id"]);
                                objCompany.AspNetUserId=Convert.ToString(dr["AspNetUserId"]);
                                objCompany.Company_name = Convert.ToString(dr["Company_name"]);
                                objCompany.Company_phone= Convert.ToString(dr["Company_phone"]);
                                objCompany.Company_email = Convert.ToString(dr["Company_email"]);
                                objCompany.Company_gst_no = Convert.ToString(dr["Company_gst_no"]);
                                objCompany.Company_city = Convert.ToString(dr["Company_city"]);
                                objCompany.Company_state = Convert.ToString(dr["Company_state"]);
                                objCompany.Company_country = Convert.ToString(dr["Company_country"]);
                                objCompany.CurrencyUnit = Convert.ToString(dr["currencyUnit"]);
                                objCompany.Company_zipcode = Convert.ToInt32(dr["Company_zipcode"]);
                                objCompany.Company_website = Convert.ToString(dr["Company_website"]);
                                objCompany.Company_description = Convert.ToString(dr["Company_description"]);
                                objUser.Usercompany=objCompany;
                            }
                        }
                       
                    }
                      

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    mDB = null;
                }
            }
            return objUser;
        }
        #endregion




    }
}
