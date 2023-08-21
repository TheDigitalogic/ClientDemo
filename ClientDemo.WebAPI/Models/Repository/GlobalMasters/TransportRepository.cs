using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class TransportRepository : ITransportRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion

        #region "Constructor"

        public TransportRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }

        #endregion

        #region "Desination CRUD Functions"


        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 2, 2022</created_date>
        /// <summary>
        /// Get Transport List Data
        /// </summary>
        /// <returns>list of Transport Object</returns>
        public List<Entity.Transport> GetTransportList(int type_id)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.Transport> transportColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }
                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_transport]");
                sqlCommand.CommandTimeout = 0;

                result = mDB.ExecuteDataSet(sqlCommand);

                if (result != null && result.Tables.Count > 0)
                {
                    transportColl = new List<Entity.Transport>();

                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.Transport objTransport = new Entity.Transport();
                        objTransport.Key = Convert.ToInt32(dr["key"]);
                        objTransport.Transport_id = Convert.ToInt32(dr["Transport_id"]);
                        objTransport.Vehicle_name = Convert.ToString(dr["Vehicle_name"]);
                        objTransport.Vehicle_type = Convert.ToString(dr["Vehicle_type"]);
                        objTransport.No_of_seats = Convert.ToInt16(dr["No_of_seats"]);
                        objTransport.Is_active = Convert.ToBoolean(dr["Is_active"]);
                        objTransport.Row_created_date = Convert.ToDateTime(dr["Row_created_date"]);
                        objTransport.Row_created_by = Convert.ToString(dr["Row_created_by"]);
                        objTransport.Row_altered_date = Convert.ToDateTime(dr["Row_altered_date"]);
                        objTransport.Row_altered_by = Convert.ToString(dr["Row_altered_by"]);
                        transportColl.Add(objTransport);
                        objTransport = null;
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
            return transportColl;
        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>Dec 2, 2022</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the destinations will be added in the databasae
        ///   If operation is "U" (Update) then all the destinations will be updated in the database 
        /// </summary>
        /// <param name="transport_json"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public String SaveTransport(String transport_json, String operation, String UserId)
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
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveTransport]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_transport_json", DbType.String, transport_json);
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
