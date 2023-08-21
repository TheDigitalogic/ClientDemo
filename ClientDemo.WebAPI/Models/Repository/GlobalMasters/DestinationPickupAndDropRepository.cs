using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Data;
using System;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class DestinationPickupAndDropRepository : IDestinationPickupAndDropRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;
        #endregion

        #region "Constructor"
        public DestinationPickupAndDropRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "DestinationPickupAndDrop CRUD Functions"   
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>24 Jan, 2023</created_date>
        /// <summary>
        /// Get  PickupAndDropList
        /// </summary>
        /// <returns>list of PickupAndDrop Object</returns>
        public List<Entity.DestiantionPickupAndDrop> GetPickupAndDropList(int type_id)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;
            DataTable dt = null;
            List<Entity.DestiantionPickupAndDrop> objPickupAndDropList = null;  //Parent
            List<Entity.DestinationPickup> objPickupList = null;  //Child
            List<Entity.DestinationDrop> objDropList = null; //Child
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }
                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_pickup_drop_location]");
                sqlCommand.CommandTimeout = 0;
                ds = mDB.ExecuteDataSet(sqlCommand);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    using (DataView view = new DataView(dt))
                    {
                        using (DataTable distinctPickupDrop = view.ToTable(true, "Pickup_drop_id", "Destination_type_id", "Destination_id","Destination_name", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
                        {
                            objPickupAndDropList = new List<DestiantionPickupAndDrop>();
                            DestiantionPickupAndDrop objPickupAndDrop = null;
                            for (int counter = 0; counter < distinctPickupDrop.Rows.Count; counter++)
                            {
                                objPickupAndDrop = new DestiantionPickupAndDrop();
                                objPickupAndDrop.Key= counter;
                                objPickupAndDrop.Pickup_drop_id = Convert.ToInt32(distinctPickupDrop.Rows[counter]["Pickup_drop_id"]);
                                objPickupAndDrop.Destination_id = Convert.ToInt32(distinctPickupDrop.Rows[counter]["Destination_id"]);
                                objPickupAndDrop.Destination_name = Convert.ToString(distinctPickupDrop.Rows[counter]["Destination_name"]);
                                objPickupAndDrop.Destination_type_id = Convert.ToInt32(distinctPickupDrop.Rows[counter]["Destination_type_id"]);
                                objPickupAndDrop.Is_active = Convert.ToBoolean(distinctPickupDrop.Rows[counter]["Is_active"]);
                                objPickupAndDrop.Row_created_date = Convert.ToDateTime(distinctPickupDrop.Rows[counter]["Row_created_date"]);
                                objPickupAndDrop.Row_created_by = Convert.ToString(distinctPickupDrop.Rows[counter]["Row_created_by"]);
                                objPickupAndDrop.Row_altered_date = Convert.ToDateTime(distinctPickupDrop.Rows[counter]["Row_altered_date"]);
                                objPickupAndDrop.Row_altered_by = Convert.ToString(distinctPickupDrop.Rows[counter]["Row_altered_by"]);
                                dt.DefaultView.RowFilter = "Destination_id = '" + objPickupAndDrop.Destination_id + "'";
                                DataSet dspickuplocation = null;
                                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_pickup_location]");
                                sqlCommand.CommandTimeout = 0;
                                dspickuplocation = mDB.ExecuteDataSet(sqlCommand);
                                if (dspickuplocation != null && dspickuplocation.Tables.Count > 0)
                                {
                                    objPickupList = new List<Entity.DestinationPickup>();
                                    foreach (DataRow dr in dspickuplocation.Tables[0].Rows)
                                    {
                                        if(objPickupAndDrop.Pickup_drop_id == Convert.ToInt64(dr["Pickup_drop_id"]))
                                        {
                                            Entity.DestinationPickup objPickup = new Entity.DestinationPickup();
                                            objPickup.Key = Convert.ToInt32(dr["Key"]);
                                            objPickup.Pickup_location_id= Convert.ToInt32(dr["Pickup_location_id"]);
                                            objPickup.Pickup_location_name= Convert.ToString(dr["Pickup_location_name"]);
                                            objPickupList.Add(objPickup);
                                            objPickup = null;
                                        }
                                    }
                                }
                                objPickupAndDrop.PickupLocation = objPickupList;
                                DataSet dsdroplocation = null;
                                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_drop_location]");
                                sqlCommand.CommandTimeout = 0;
                                dsdroplocation = mDB.ExecuteDataSet(sqlCommand);
                                if (dsdroplocation != null && dsdroplocation.Tables.Count > 0)
                                {
                                    objDropList = new List<Entity.DestinationDrop>();
                                    foreach (DataRow di in dsdroplocation.Tables[0].Rows)
                                    {
                                        if (objPickupAndDrop.Pickup_drop_id == Convert.ToInt64(di["Pickup_drop_id"]))
                                        {
                                            Entity.DestinationDrop objDrop= new Entity.DestinationDrop();
                                            objDrop.Key = Convert.ToInt32(di["key"]);
                                            objDrop.Drop_location_id = Convert.ToInt32(di["Drop_location_id"]);
                                            objDrop.Drop_location_name = Convert.ToString(di["Drop_location_name"]);
                                            objDropList.Add(objDrop);
                                            objDrop = null;
                                        }
                                    }
                                }
                                objPickupAndDrop.DropLocation = objDropList;
                                objPickupAndDropList.Add(objPickupAndDrop);
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
            return objPickupAndDropList;
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>24 Jan, 2023</created_date>
        /// <summary>
        ///   Save PickupAndDrop details. 
        ///   If operation is "A" (Add) then all the hotels will be added in the databasae
        ///   If operation is "U" (Update) then all the hotels will be updated in the database 
        /// </summary>
        /// <param name="pickupanddrop_json"></param>
        /// <returns></returns>
        public string SavePickupAndDrop(String pickupanddrop_json, String UserId)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            string result = "";
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePickupAndDrop]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_pickupanddrop_json", DbType.String, pickupanddrop_json);
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
