using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Data;
using System;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.Interface;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class ProcessLogsRepository:IProcessLogsRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;
        #endregion

        #region "Constructor"
        public ProcessLogsRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "ProcessLogs CRUD Functions"
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>30 JUne, 2023</created_date>
        /// <summary>
        /// Get  ProcessLogs List
        /// </summary>
        /// <returns>list of ProcessLogs Object</returns>
        public List<Entity.ProcessLogs> GetProcessLogsList(DateTime fromDate,DateTime toDate)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.ProcessLogs> processLogsColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }
                String query = @"Select * from vw_ProcessLogs where cast(log_time as date) between cast(@start_date as date) and cast(@end_date as date)";
                sqlCommand = mDB.GetSqlStringCommand(query);
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@start_date", DbType.DateTime, fromDate);
                mDB.AddInParameter(sqlCommand, "@end_date", DbType.DateTime, toDate);
                result = mDB.ExecuteDataSet(sqlCommand);
                if (result != null && result.Tables.Count > 0)
                {
                    processLogsColl = new List<ProcessLogs>();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.ProcessLogs objProcessLogs = new Entity.ProcessLogs();
                        objProcessLogs.Key = Convert.ToInt32(dr["key"]);
                        objProcessLogs.ProcessLogId= Convert.ToInt64(dr["ProcessLogId"]);
                        objProcessLogs.Severity = Convert.ToString(dr["Severity"]);
                        objProcessLogs.Source= Convert.ToString(dr["Source"]);
                        objProcessLogs.Parameters = Convert.ToString(dr["Parameters"]);
                        objProcessLogs.Message = Convert.ToString(dr["Message"]);
                        objProcessLogs.Start_time = Convert.ToDateTime(dr["Start_time"]);
                        objProcessLogs.End_time = Convert.ToDateTime(dr["End_time"]);
                        objProcessLogs.Username = Convert.ToString(dr["Username"]);
                        objProcessLogs.Log_time = Convert.ToDateTime(dr["Log_time"]);
                        objProcessLogs.Elapsed_secs = Convert.ToInt32(dr["Elapsed_secs"]);
                        objProcessLogs.ErrorNumber = Convert.ToInt64(dr["ErrorNumber"]);
                        objProcessLogs.Rows_affected = Convert.ToInt64(dr["Rows_affected"]);
                        objProcessLogs.ErrorLine = Convert.ToInt64(dr["ErrorLine"]);
                        objProcessLogs.ErrorProcedure = Convert.ToString(dr["ErrorProcedure"]);
                        objProcessLogs.ErrorMessage = Convert.ToString(dr["ErrorMessage"]);
                        objProcessLogs.ErrorDateTime= Convert.ToDateTime(dr["ErrorDateTime"]);
                        processLogsColl.Add(objProcessLogs);
                        objProcessLogs = null;
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
            return processLogsColl;
        }
        #endregion
    }
}
