using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Data;
using System;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Entity;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class HotelMealPlanRepository : IHotelMealPlanRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion

        #region "Constructor"

        public HotelMealPlanRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "HotelMealPlan CRUD Functions"   
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>21 Nov, 2022</created_date>
        /// <summary>
        /// Get HotelMealPlan List Data
        /// </summary>
        /// <returns>list of HotelMealPlan Object</returns>
         public List<Entity.Hotel> GetHotelMealPlanList(Int64 city_id = 0, Int64 hotel_id = 0)
         {
             System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;
            DataTable dt = null;

            List<Entity.Hotel> objHotelList = null;
            List<Entity.HotelMealPlan> objHotelMealPlanList = null;

            try
            {
                 if (mDB == null)
                 {
                     mDB = new SqlDatabase(mConnectionString);
                     mConnection = mDB.CreateConnection();
                 }

                String Query = "Select * From [vw_hotel_meal_plan] WHere ( city_id = @pa_city_id OR @pa_city_id is NULL ) AND ( hotel_id = @pa_hotel_id OR @pa_hotel_id is NULL )";

                sqlCommand = mDB.GetSqlStringCommand(Query);

                if (city_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_city_id", DbType.Int64, city_id);
                else
                    mDB.AddInParameter(sqlCommand, "@pa_city_id", DbType.Int64, DBNull.Value);

                if (hotel_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_hotel_id", DbType.Int64, hotel_id);
                else
                    mDB.AddInParameter(sqlCommand, "@pa_hotel_id", DbType.Int64, DBNull.Value);

                sqlCommand.CommandTimeout = 0;

                ds = mDB.ExecuteDataSet(sqlCommand);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    using (DataView view = new DataView(dt))
                    {
                        using (DataTable distinctHotels = view.ToTable(true, "Hotel_id", "Hotel_name", "City_id", "Hotel_room_starting_price", "Destination_id","Is_active", "Destination_name", "City_name", "Destination_type_id", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
                        {
                            objHotelList = new List<Entity.Hotel>();
                            Entity.Hotel objHotel = null;

                            for (int counter = 0; counter < distinctHotels.Rows.Count; counter++)
                            {
                                objHotel = new Entity.Hotel();
                                objHotel.Key = counter;
                                objHotel.Hotel_id = Convert.ToInt32(distinctHotels.Rows[counter]["Hotel_id"]);
                                objHotel.Hotel_name = Convert.ToString(distinctHotels.Rows[counter]["Hotel_name"]);
                                objHotel.Hotel_room_starting_price = Convert.ToDouble(distinctHotels.Rows[counter]["Hotel_room_starting_price"]);
                                objHotel.City_id = Convert.ToInt32(distinctHotels.Rows[counter]["City_id"]);
                                objHotel.City_name = Convert.ToString(distinctHotels.Rows[counter]["City_name"]);
                                objHotel.Destination_id = Convert.ToInt32(distinctHotels.Rows[counter]["Destination_id"]);
                                objHotel.Destination_name = Convert.ToString(distinctHotels.Rows[counter]["Destination_name"]);
                                objHotel.Destination_type_id = Convert.ToInt32(distinctHotels.Rows[counter]["Destination_type_id"]);
                                objHotel.Is_active= Convert.ToBoolean(distinctHotels.Rows[counter]["Is_active"]);
                                objHotel.Row_created_date = Convert.ToDateTime(distinctHotels.Rows[counter]["Row_created_date"]);
                                objHotel.Row_created_by = Convert.ToString(distinctHotels.Rows[counter]["Row_created_by"]);
                                objHotel.Row_altered_date = Convert.ToDateTime(distinctHotels.Rows[counter]["Row_altered_date"]);
                                objHotel.Row_altered_by = Convert.ToString(distinctHotels.Rows[counter]["Row_altered_by"]);
                                dt.DefaultView.RowFilter = "Hotel_id = '" + objHotel.Hotel_id + "'";
                                DataTable dtHotel_meal_plan = dt.DefaultView.ToTable();

                                objHotel.HotelMealPlanList = new List<HotelMealPlan>();
                                objHotelMealPlanList = new List<HotelMealPlan>();
                                HotelMealPlan objHotelMealPlan = null;

                                for (int i = 0; i < dtHotel_meal_plan.Rows.Count; i++)
                                {
                                    objHotelMealPlan = new HotelMealPlan();
                                    objHotelMealPlan.Key= Convert.ToInt32(dtHotel_meal_plan.Rows[i]["key"]);
                                    objHotelMealPlan.Hotel_meal_plan_id = Convert.ToInt32(dtHotel_meal_plan.Rows[i]["Hotel_meal_plan_id"]);
                                    objHotelMealPlan.Hotel_id = Convert.ToInt32(dtHotel_meal_plan.Rows[i]["Hotel_id"]);
                                    objHotelMealPlan.Is_active = Convert.ToBoolean(1);
                                    objHotelMealPlan.Meal_plan_id = Convert.ToInt32(dtHotel_meal_plan.Rows[i]["Meal_plan_id"]);
                                    objHotelMealPlan.Meal_plan_desc = Convert.ToString(dtHotel_meal_plan.Rows[i]["Meal_plan_desc"]);
                                    objHotelMealPlan.Meal_plan_code = Convert.ToString(dtHotel_meal_plan.Rows[i]["Meal_plan_code"]);
                                    objHotelMealPlan.Adult_price = Convert.ToString(dtHotel_meal_plan.Rows[i]["Adult_price"]); 
                                    objHotelMealPlan.Child_price = Convert.ToString(dtHotel_meal_plan.Rows[i]["Child_price"]);
                                    objHotelMealPlan.Child_price_without_bed = Convert.ToString(dtHotel_meal_plan.Rows[i]["Child_price_without_bed"]);
                                    objHotelMealPlanList.Add(objHotelMealPlan);
                                    objHotelMealPlan = null;
                                }
                                objHotel.HotelMealPlanList = objHotelMealPlanList;

                                objHotelList.Add(objHotel);

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
             return objHotelList;
         }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>28 Nov, 2022</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the hotels will be added in the databasae
        ///   If operation is "U" (Update) then all the hotels will be updated in the database 
        /// </summary>
        /// <param name="hotel_meal_plan_json"></param>
        /// <returns></returns>
        //public String SaveHotelMealPlan(String hotel_meal_plan_json)
        //{
        //    System.Data.Common.DbCommand sqlCommand = null;
        //    String result = "";
        //    try
        //    {
        //        if (mDB == null)
        //        {
        //            mDB = new SqlDatabase(mConnectionString);
        //            mConnection = mDB.CreateConnection();
        //        }
        //        sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveHotelMealPlan]");
        //        sqlCommand.CommandTimeout = 0;
        //        mDB.AddInParameter(sqlCommand, "@pa_hotel_meal_plan_json", DbType.String, hotel_meal_plan_json);
        //        mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, "sunil");
        //        mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
        //       // mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, operation);
        //        mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
        //        mDB.ExecuteNonQuery(sqlCommand);

        //        result = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));

        //    }
        //     catch (Exception ex)
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


        public String SaveHotelMealPlan(String hotel_meal_plan_json, String UserId)
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
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveHotelMealPlan]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_hotel_meal_plan_json", DbType.String, hotel_meal_plan_json);
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
