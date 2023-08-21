using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class PackageReviewsRepository:IPackageReviewsRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;
        #endregion
        #region "Constructor"
        public PackageReviewsRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion

        #region "Desination CRUD Functions"
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>07 July, 2023</created_date>
        /// <summary>
        /// Get Package Remarks List
        /// </summary>
        /// <returns>list of Package Remarks list</returns>
        public List<Entity.PackageReviews> GetPackageReviewsList(Int64 Package_id)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.PackageReviews> packageRemarksColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();;
                }
                string query = "Select * From [vw_package_reviews] where Package_id=@Package_id";
                sqlCommand = mDB.GetSqlStringCommand(query);
                mDB.AddInParameter(sqlCommand, "@Package_id",DbType.Int64, Package_id);
                sqlCommand.CommandTimeout = 0;
                result = mDB.ExecuteDataSet(sqlCommand);
                if (result != null && result.Tables.Count > 0)
                {
                    packageRemarksColl = new List<Entity.PackageReviews>();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.PackageReviews objPackageRemarks = new Entity.PackageReviews();
                        objPackageRemarks.Key = Convert.ToInt32(dr["key"]);
                        objPackageRemarks.Package_reviews_id = Convert.ToInt64(dr["Package_reviews_id"]);
                        objPackageRemarks.Package_id= Convert.ToInt64(dr["Package_id"]);
                        objPackageRemarks.Reviews = Convert.ToString(dr["Reviews"]);
                        objPackageRemarks.Reviews_rating = Convert.ToInt32(dr["Reviews_rating"]);
                        objPackageRemarks.First_name= Convert.ToString(dr["First_name"]);
                        objPackageRemarks.Last_name=Convert.ToString(dr["Last_name"]);
                        objPackageRemarks.Row_created_by = Convert.ToString(dr["Row_created_by"]);
                        objPackageRemarks.Row_created_date = Convert.ToDateTime(dr["Row_created_date"]);
                        packageRemarksColl.Add(objPackageRemarks);
                        objPackageRemarks = null;
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
            return packageRemarksColl;
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>07 July, 2023</created_date>
        /// <summary>
        ///   Save Package Remarks details. 
        ///   If operation is "A" (Add) then all the package remarks will be added in the databasae 
        /// </summary>
        /// <param name="package_reviews_json"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public String[] SavePackageReviews(string package_reviews_json, string UserId)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            String[] arrResult = new String[2];
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackageReviews]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_package_reviews_json", DbType.String, package_reviews_json);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                mDB.AddOutParameter(sqlCommand, "@pa_out_package_reviews_id", DbType.Int64, 80000);

                mDB.ExecuteNonQuery(sqlCommand);

                arrResult[0] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));
                arrResult[1] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_package_reviews_id"));
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
            return arrResult;
        }
        #endregion
    }
}
