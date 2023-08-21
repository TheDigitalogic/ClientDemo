using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using TravelNinjaz.B2B.WebAPI.Models.Entity;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class  DashboardRepository:IDashboardRepository
    {
    #region "Variables Declaration"
    private readonly string mConnectionString;
    private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
    private System.Data.Common.DbConnection mConnection;

    #endregion

    #region "Constructor"
    public DashboardRepository(IConfiguration configuration)
    {

        mConnectionString = configuration.GetConnectionString("DatabaseConnection");
    }
        #endregion
        #region "City CRUD Functions"
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>11 July, 2023</created_date>
        /// <summary>
        /// Get List Dashboard
        /// </summary>
        /// <returns>List Dashboard </returns>
        public List<Entity.DestinationDashboard> GetDashboardDestination(string monthYear, string userId)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.DestinationDashboard> DashboardColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }
                
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspDashboard_GetDesinationQuotation]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_month_year", DbType.String, monthYear);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, monthYear);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                result = mDB.ExecuteDataSet(sqlCommand);
                if (result != null && result.Tables.Count > 0)
                {
                    DashboardColl = new List<DestinationDashboard>();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.DestinationDashboard objDashboardLogs = new Entity.DestinationDashboard();
                      
                        objDashboardLogs.Destination_id = Convert.ToInt64(dr["Destination_id"]);
                        objDashboardLogs.Destination_name = Convert.ToString(dr["Destination_name"]);
                        objDashboardLogs.Number_of_quotation = Convert.ToInt32(dr["Number_of_quotation"]);

                        DashboardColl.Add(objDashboardLogs);
                        objDashboardLogs = null;
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
            return DashboardColl;
        }
        //public DataTable GetDashbaord(string monthYear,string userId)
        //{
        //    System.Data.Common.DbCommand sqlCommand = null;
        //    DataSet result = null;

        //    try
        //    {
        //        if (mDB == null)
        //        {
        //            mDB = new SqlDatabase(mConnectionString);
        //            mConnection = mDB.CreateConnection();
        //        }

        //        sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspDashboard_GetDesinationQuotation]");
        //        sqlCommand.CommandTimeout = 0;
        //        mDB.AddInParameter(sqlCommand, "@pa_month_year", DbType.String, monthYear);
        //        mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, monthYear);
        //        mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
        //        result = mDB.ExecuteDataSet(sqlCommand);

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
        //    return result.Tables[0];

        //}

        #endregion

    }
}
