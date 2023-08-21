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
    public class HotelRepository : IHotelRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion

        #region "Constructor"

        public HotelRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "Hotel CRUD Functions"
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>09 Nov, 2022</created_date>
        /// <summary>
        /// Get Hotel List Data
        /// </summary>
        /// <returns>list of Hotel Object</returns>
        public List<Entity.Hotel> GetHotelList(int type_id)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.Hotel> hotelColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_hotel]");
                sqlCommand.CommandTimeout = 0;

                result = mDB.ExecuteDataSet(sqlCommand);

                if (result != null && result.Tables.Count > 0)
                {
                    hotelColl = new List<Entity.Hotel>();

                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.Hotel objHotel = new Entity.Hotel();
                        objHotel.Key = Convert.ToInt32(dr["key"]);
                        objHotel.City_id = Convert.ToInt32(dr["City_id"]);
                        objHotel.City_name = Convert.ToString(dr["City_name"]);
                        objHotel.Destination_id = Convert.ToInt32(dr["Destination_id"]);
                        objHotel.Destination_name = Convert.ToString(dr["Destination_name"]);
                        objHotel.Destination_type_id= Convert.ToInt32(dr["Destination_type_id"]);
                        objHotel.Hotel_id = Convert.ToInt32(dr["Hotel_id"]);
                        objHotel.Hotel_name = Convert.ToString(dr["Hotel_name"]);
                        objHotel.Hotel_type= Convert.ToInt32(dr["Hotel_type"]);
                        objHotel.Is_active = Convert.ToBoolean(dr["Is_active"]);
                        objHotel.Row_created_date = Convert.ToDateTime(dr["Row_created_date"]);
                        objHotel.Row_created_by = Convert.ToString(dr["Row_created_by"]);
                        objHotel.Row_altered_date = Convert.ToDateTime(dr["Row_altered_date"]);
                        objHotel.Row_altered_by = Convert.ToString(dr["Row_altered_by"]);                 
                        hotelColl.Add(objHotel);
                        objHotel = null;
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
            return hotelColl;
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>11 Nov, 2022</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the hotels will be added in the databasae
        ///   If operation is "U" (Update) then all the hotels will be updated in the database 
        /// </summary>
        /// <param name="hotel_json"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public String SaveHotel(String hotel_json, String operation, String UserId)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveHotel]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_hotel_json", DbType.String, hotel_json);
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
