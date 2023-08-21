using TravelNinjaz.B2B.WebAPI.Models.Interface;
using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class TravellingCompanyRepository : ITravellingCompanyRepository
    {
        #region "Variables Declaration"

        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion


        #region "Constructor"
        public TravellingCompanyRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion

        public String[] SaveTravellingCompany(String travelling_company_json, String UserId)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            String[] arrResult = new String[2];
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveTravellingCompany]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_travelling_company_json", DbType.String, travelling_company_json);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                mDB.AddOutParameter(sqlCommand, "@pa_out_travelling_company_id", DbType.Int64, 80000);
                mDB.ExecuteNonQuery(sqlCommand);
                arrResult[0] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));
                arrResult[1] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_travelling_company_id"));
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
            return arrResult;
        }
        public String ImportFileDataToTable(String Original_file_name, String Temp_file_name, Int32 file_record_count, String xml_file_data, String UserId) // System.Data.DataTable dsImportTable
        {
            System.Data.Common.DbCommand sqlCommand = null;
            String result = "";
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspImport_File_data_TravellingCompany]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_original_file_name", DbType.String, Original_file_name);
                mDB.AddInParameter(sqlCommand, "@pa_temp_file_name", DbType.String, Temp_file_name);
                mDB.AddInParameter(sqlCommand, "@pa_file_record_count", DbType.String, file_record_count);
                mDB.AddInParameter(sqlCommand, "@pa_xml_file_data", DbType.String, xml_file_data);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                mDB.ExecuteNonQuery(sqlCommand);
                result = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));
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

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>22 May, 2023</created_date>
        /// <summary>
        /// Get Travelling List Data
        /// </summary>
        /// <returns>list of Travelling Object</returns>
        public List<Entity.TravellingCompany> GetTravellingDataList(String status, String state,String city)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;  
            List<Entity.TravellingCompany> travellingCompanyCol = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                String query = @"Select * from 
                                    (Select * from  vw_travelling_company )  AS List
                                    WHERE                                 
                             ([status] In (select * from dbo.fn_SplitIntoRow(@pa_status,',')) OR ISNULL(@pa_status, '') = '' )
                            AND  ([city] In (select * from dbo.fn_SplitIntoRow(@pa_city,',')) OR ISNULL(@pa_city, '') = '')   
                            AND ([state] In(select * from dbo.fn_SplitIntoRow(@pa_state, ',')) OR ISNULL(@pa_state, '') = '')";
                
                // [state] In(select * from dbo.fn_SplitIntoRow(@c_State, ','))

                sqlCommand = mDB.GetSqlStringCommand(query);
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_status", DbType.String, status);
                mDB.AddInParameter(sqlCommand, "@pa_state", DbType.String, state);
                mDB.AddInParameter(sqlCommand, "@pa_city", DbType.String, city);

                result = mDB.ExecuteDataSet(sqlCommand);

                if (result != null && result.Tables.Count > 0)
                {
                    travellingCompanyCol = new List<Entity.TravellingCompany>();

                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.TravellingCompany objTravelling = new Entity.TravellingCompany();
                        objTravelling.Key = Convert.ToInt32(dr["key"]);
                        objTravelling.Travelling_company_id = Convert.ToInt32(dr["Travelling_company_id"]);
                        objTravelling.Company_name = Convert.ToString(dr["Company_name"]);
                        objTravelling.Mobile_1 = Convert.ToString(dr["Mobile_1"]);
                        objTravelling.Mobile_2 = Convert.ToString(dr["Mobile_2"]);
                        objTravelling.Email_id_1 = Convert.ToString(dr["Email_id_1"]);
                        objTravelling.Email_id_2 = Convert.ToString(dr["Email_id_2"]);
                        objTravelling.Website = Convert.ToString(dr["Website"]);
                        objTravelling.Address = Convert.ToString(dr["Address"]);
                        objTravelling.Remarks = Convert.ToString(dr["Remarks"]);
                        objTravelling.Landline = Convert.ToString(dr["Landline"]);
                        objTravelling.City = Convert.ToString(dr["City"]);
                        objTravelling.State = Convert.ToString(dr["State"]);
                        objTravelling.Status = Convert.ToString(dr["Status"]);
                        objTravelling.Is_active = Convert.ToBoolean(dr["Is_active"]);
                        objTravelling.Row_created_date = Convert.ToDateTime(dr["Row_created_date"]);
                        objTravelling.Row_created_by = Convert.ToString(dr["Row_created_by"]);
                        objTravelling.Row_altered_date = Convert.ToDateTime(dr["Row_altered_date"]);
                        objTravelling.Row_altered_by = Convert.ToString(dr["Row_altered_by"]);

                        travellingCompanyCol.Add(objTravelling);

                        objTravelling = null;
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
            return travellingCompanyCol;
        }
    }
}
