using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{

    public class CityRepository : ICityRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion

        #region "Constructor"

        public CityRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "City CRUD Functions"
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>05 Nov, 2022</created_date>
        /// <summary>
        /// Get City List Data
        /// </summary>
        /// <returns>list of City Object</returns>
        public List<Entity.City> GetCityList(int type_id)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.City> cityColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_city]");
                sqlCommand.CommandTimeout = 0;

                result = mDB.ExecuteDataSet(sqlCommand);

                if (result != null && result.Tables.Count > 0)
                {
                    cityColl = new List<Entity.City>();

                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.City objCity = new Entity.City();
                        objCity.Key = Convert.ToInt32(dr["key"]);
                        objCity.City_id = Convert.ToInt32(dr["City_id"]);
                        objCity.Destination_id = Convert.ToInt32(dr["Destination_id"]);
                        objCity.Destination_type_id= Convert.ToInt32(dr["Destination_type_id"]);
                        objCity.Destination_name = Convert.ToString(dr["Destination_name"]);
                        objCity.City_name = Convert.ToString(dr["City_name"]);
                        objCity.Is_active = Convert.ToBoolean(dr["Is_active"]);
                        objCity.Row_created_date = Convert.ToDateTime(dr["Row_created_date"]);
                        objCity.Row_created_by = Convert.ToString(dr["Row_created_by"]);
                        objCity.Row_altered_date = Convert.ToDateTime(dr["Row_altered_date"]);
                        objCity.Row_altered_by = Convert.ToString(dr["Row_altered_by"]);
                        cityColl.Add (objCity);
                        objCity = null;
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
            return cityColl;
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>10 Nov, 2022</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the citiess will be added in the databasae
        ///   If operation is "U" (Update) then all the citiess will be updated in the database 
        /// </summary>
        /// <param name="city_json"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public String SaveCity(String city_json, String operation, String UserId)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveCity]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_city_json", DbType.String, city_json);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, operation);
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
        #endregion


    }
}

