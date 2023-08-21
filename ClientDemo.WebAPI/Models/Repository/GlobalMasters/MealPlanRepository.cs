using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{

    public class MealPlanRepository : IMealPlanRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion

        #region "Constructor"

        public MealPlanRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "MealPlan CRUD Functions"
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>21 Nov, 2022</created_date>
        /// <summary>
        /// Get MealPlan List Data
        /// </summary>
        /// <returns>list of MealPlan Object</returns>
        public List<Entity.MealPlan> GetMealPlanList()
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.MealPlan> mealPlanColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_meal_plan]");
                sqlCommand.CommandTimeout = 0;

                result = mDB.ExecuteDataSet(sqlCommand);

                if (result != null && result.Tables.Count > 0)
                {
                    mealPlanColl = new List<Entity.MealPlan>();

                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.MealPlan objMealPlan = new Entity.MealPlan();
                        objMealPlan.Key=Convert.ToInt32(dr["key"]);
                        objMealPlan.Meal_plan_desc = Convert.ToString(dr["Meal_plan_desc"]);
                        objMealPlan.Meal_plan_id = Convert.ToInt32(dr["Meal_plan_id"]);
                        objMealPlan.Meal_plan_code = Convert.ToString(dr["Meal_plan_code"]);
                       mealPlanColl.Add(objMealPlan);

                        objMealPlan = null;
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
            return mealPlanColl;
        }
        #endregion
    }
}

