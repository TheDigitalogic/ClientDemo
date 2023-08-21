using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{

    public class DestinationRepository : IDestinationRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion

        #region "Constructor"

        public DestinationRepository(IConfiguration configuration)
        {
          
            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
              
        #endregion

        #region "Desination CRUD Functions"


        /// <created_by>Manisha Tripathi</created_by>
        /// <created_date>22 Oct, 2022</created_date>
        /// <summary>
        /// Get Destinations List Data
        /// </summary>
        /// <returns>list of Destination Object</returns>
        public List<Entity.Destination> GetDestinationList(int type_id)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.Destination> destinationColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_destination]");
                sqlCommand.CommandTimeout = 0;

                result = mDB.ExecuteDataSet(sqlCommand);

                if (result != null && result.Tables.Count > 0)
                {
                    destinationColl = new List<Entity.Destination>();

                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.Destination objDestination = new Entity.Destination();
                        objDestination.Key = Convert.ToInt32(dr["key"]);
                        objDestination.Destination_id = Convert.ToInt32(dr["Destination_id"]);
                        objDestination.Destination_name = Convert.ToString(dr["Destination_name"]);
                        objDestination.Destination_type_id = Convert.ToInt32(dr["Destination_type_id"]);
                        objDestination.Destination_type_name = Convert.ToString(dr["Destination_type_name"]);
                        objDestination.Destination_description = Convert.ToString(dr["Destination_description"]);
                        objDestination.Destination_image = Convert.ToString(dr["Destination_image"]);
                        objDestination.Is_best_selling = Convert.ToBoolean(dr["Is_best_selling"]);
                        objDestination.Is_active = Convert.ToBoolean(dr["Is_active"]);
                        objDestination.Row_created_date = Convert.ToDateTime(dr["Row_created_date"]);
                        objDestination.Row_created_by = Convert.ToString(dr["Row_created_by"]);
                        objDestination.Row_altered_date = Convert.ToDateTime(dr["Row_altered_date"]);
                        objDestination.Row_altered_by = Convert.ToString(dr["Row_altered_by"]);

                        destinationColl.Add(objDestination);

                        objDestination = null;
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
            return destinationColl;
        }

        /// <created_by>Manisha Tripathi</created_by>
        /// <created_date>28 Oct, 2022</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the destinations will be added in the databasae
        ///   If operation is "U" (Update) then all the destinations will be updated in the database 
        /// </summary>
        /// <param name="destination_json"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public String SaveDestination(String destination_json, String operation,String UserId)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveDestination]");
                sqlCommand.CommandTimeout = 0;
                             
                mDB.AddInParameter(sqlCommand, "@pa_destination_json", DbType.String, destination_json);
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
