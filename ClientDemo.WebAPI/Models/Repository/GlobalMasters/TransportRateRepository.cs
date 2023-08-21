using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Data;
using System;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Entity;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class TransportRateRepository : ITransportRateRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion

        #region "Constructor"

        public TransportRateRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "TransportRate CRUD Functions"   
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>13 Dec, 2022</created_date>
        /// <summary>
        /// Get Transport Rate List Data
        /// </summary>
        /// <returns>list of Transport Rate Object</returns>
        public List<Entity.CityTransportRate> GetCityTransportRateList()
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;
            DataTable dt = null;

            List<Entity.CityTransportRate> objCityTransportRateList = null;
            List<Entity.TransportRate> objTransportRateList = null;

            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_transport_rate]");
                sqlCommand.CommandTimeout = 0;

                ds = mDB.ExecuteDataSet(sqlCommand);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    using (DataView view = new DataView(dt))
                    {
                        /**get distinct parent details*/
                        using (DataTable distinctCity = view.ToTable(true, "City_id", "City_name", "Transport_rate_id","Transport_starting_price", "Transport_rate_name", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
                        {
                            objCityTransportRateList = new List<Entity.CityTransportRate>();
                            Entity.CityTransportRate objCityTransportRate = null;
                            for (int counter = 0; counter < distinctCity.Rows.Count; counter++)
                            {
                                objCityTransportRate = new Entity.CityTransportRate();
                                objCityTransportRate.Key = counter;
                                objCityTransportRate.City_id = Convert.ToInt32(distinctCity.Rows[counter]["City_id"]);
                                objCityTransportRate.City_name = Convert.ToString(distinctCity.Rows[counter]["City_name"]);
                                objCityTransportRate.Destination_id = Convert.ToInt32(distinctCity.Rows[counter]["Destination_id"]);
                                objCityTransportRate.Transport_rate_id = Convert.ToInt32(distinctCity.Rows[counter]["Transport_rate_id"]);
                                objCityTransportRate.Transport_rate_name = Convert.ToString(distinctCity.Rows[counter]["Transport_rate_name"]);
                                objCityTransportRate.Transport_starting_price = Convert.ToDouble(distinctCity.Rows[counter]["Transport_starting_price"]);
                                objCityTransportRate.Destination_name = Convert.ToString(distinctCity.Rows[counter]["Destination_name"]);
                                objCityTransportRate.Destination_type_id = Convert.ToInt32(distinctCity.Rows[counter]["Destination_type_id"]);
                                objCityTransportRate.Is_active = Convert.ToBoolean(distinctCity.Rows[counter]["Is_active"]);
                                objCityTransportRate.Row_created_date = Convert.ToDateTime(distinctCity.Rows[counter]["Row_created_date"]);
                                objCityTransportRate.Row_created_by = Convert.ToString(distinctCity.Rows[counter]["Row_created_by"]);
                                objCityTransportRate.Row_altered_date = Convert.ToDateTime(distinctCity.Rows[counter]["Row_altered_date"]);
                                objCityTransportRate.Row_altered_by = Convert.ToString(distinctCity.Rows[counter]["Row_altered_by"]);
                                dt.DefaultView.RowFilter = "City_id = '" + objCityTransportRate.City_id + "' AND Transport_rate_name = '" + objCityTransportRate.Transport_rate_name + "'" ;
                                DataTable dtTransport_rate = dt.DefaultView.ToTable();

                                objCityTransportRate.TransportRateList = new List<TransportRate>();
                                objTransportRateList = new List<TransportRate>();
                                TransportRate objTransportRate = null;
                                 
                                /**Get multiple child details*/
                                for (int i = 0; i < dtTransport_rate.Rows.Count; i++)
                                {
                                    objTransportRate = new TransportRate();
                                    objTransportRate.Key = Convert.ToInt32(dtTransport_rate.Rows[i]["key"]);
                                    objTransportRate.Transport_id = Convert.ToInt32(dtTransport_rate.Rows[i]["Transport_id"]);
                                    objTransportRate.Is_active = Convert.ToBoolean(1);
                                    objTransportRate.Transport_rate_id = Convert.ToInt32(dtTransport_rate.Rows[i]["Transport_rate_id"]);
                                    objTransportRate.Vehicle_name = Convert.ToString(dtTransport_rate.Rows[i]["Vehicle_name"]);
                                    objTransportRate.Vehicle_price = Convert.ToString(dtTransport_rate.Rows[i]["Vehicle_price"]);
                                    objTransportRateList.Add(objTransportRate);
                                    objTransportRate = null;
                                }
                                objCityTransportRate.TransportRateList = objTransportRateList;

                                objCityTransportRateList.Add(objCityTransportRate);

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
            return objCityTransportRateList;
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>12 Dec, 2022</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the hotels will be added in the databasae
        ///   If operation is "U" (Update) then all the hotels will be updated in the database 
        /// </summary>
        /// <param name="transport_rate_json"></param>
        /// <returns></returns>
        public String SaveTransportRate(String city_transport_rate_json,String UserId)
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
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveTransportRate]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_city_transport_rate_json", DbType.String, city_transport_rate_json);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                // mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, operation);
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
