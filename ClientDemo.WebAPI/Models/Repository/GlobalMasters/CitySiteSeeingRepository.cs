using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Data;
using System;
using TravelNinjaz.B2B.WebAPI.Models.Interface;
using TravelNinjaz.B2B.WebAPI.Models.Entity;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class CitySiteSeeingRepository : ICitySiteSeeingRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion

        #region "Constructor"

        public CitySiteSeeingRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "TransportRate CRUD Functions"   
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>21 Dec, 2022</created_date>
        /// <summary>
        /// Get City Site Seeing List Data
        /// </summary>
        /// <returns>list of City Site Seeing  Object</returns>
        public List<Entity.CitySiteSeeing> GetCitySiteSeeingList(int type_id)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;
            DataTable dt = null;

            List<Entity.CitySiteSeeing> objCitySiteSeeingList = null;
            List<Entity.SiteSeeing> objSiteSeeingList = null;

            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_city_site_seeing]");
                sqlCommand.CommandTimeout = 0;

                ds = mDB.ExecuteDataSet(sqlCommand);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    using (DataView view = new DataView(dt))
                    {
                        using (DataTable distinctCitySiteSeeing = view.ToTable(true,  "City_id", "City_name","Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
                        {
                            objCitySiteSeeingList = new List<Entity.CitySiteSeeing>();
                            Entity.CitySiteSeeing objCitySiteSeeing = null;
                            for (int counter = 0; counter < distinctCitySiteSeeing.Rows.Count; counter++)
                            {
                                objCitySiteSeeing = new Entity.CitySiteSeeing();
                                objCitySiteSeeing.Key = counter;
                                //objCitySiteSeeing.City_site_seeing_id = Convert.ToInt32(distinctCitySiteSeeing.Rows[counter]["City_site_seeing_id"]);
                                objCitySiteSeeing.City_id = Convert.ToInt32(distinctCitySiteSeeing.Rows[counter]["City_id"]);
                                objCitySiteSeeing.City_name = Convert.ToString(distinctCitySiteSeeing.Rows[counter]["City_name"]);
                                objCitySiteSeeing.Destination_id = Convert.ToInt32(distinctCitySiteSeeing.Rows[counter]["Destination_id"]);
                                objCitySiteSeeing.Destination_name = Convert.ToString(distinctCitySiteSeeing.Rows[counter]["Destination_name"]);
                                objCitySiteSeeing.Destination_type_id = Convert.ToInt32(distinctCitySiteSeeing.Rows[counter]["Destination_type_id"]);
                                objCitySiteSeeing.Is_active = Convert.ToBoolean(distinctCitySiteSeeing.Rows[counter]["Is_active"]);
                                objCitySiteSeeing.Row_created_date = Convert.ToDateTime(distinctCitySiteSeeing.Rows[counter]["Row_created_date"]);
                                objCitySiteSeeing.Row_created_by = Convert.ToString(distinctCitySiteSeeing.Rows[counter]["Row_created_by"]);
                                objCitySiteSeeing.Row_altered_date = Convert.ToDateTime(distinctCitySiteSeeing.Rows[counter]["Row_altered_date"]);
                                objCitySiteSeeing.Row_altered_by = Convert.ToString(distinctCitySiteSeeing.Rows[counter]["Row_altered_by"]);
                                dt.DefaultView.RowFilter = "City_id = '" + objCitySiteSeeing.City_id + "'";
                                DataTable dtCitySiteSeeing = dt.DefaultView.ToTable();

                                objCitySiteSeeing.SiteSeeingList = new List<SiteSeeing>();
                                objSiteSeeingList = new List<SiteSeeing>();
                                SiteSeeing objSiteSeeing = null;

                                for (int i = 0; i < dtCitySiteSeeing.Rows.Count; i++)
                                {
                                    objSiteSeeing = new SiteSeeing();
                                    objSiteSeeing.Key = i+1;
                                    objSiteSeeing.City_site_seeing_id = Convert.ToInt64(dtCitySiteSeeing.Rows[i]["City_site_seeing_id"]);
                                    objSiteSeeing.Site = Convert.ToString(dtCitySiteSeeing.Rows[i]["Site"]);
                                    objSiteSeeing.Rate = Convert.ToString(dtCitySiteSeeing.Rows[i]["Rate"]);
                                    objSiteSeeingList.Add(objSiteSeeing);
                                    objSiteSeeing = null;
                                }
                                objCitySiteSeeing.SiteSeeingList = objSiteSeeingList;

                                objCitySiteSeeingList.Add(objCitySiteSeeing);

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
            return objCitySiteSeeingList;
        }
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>12 Dec, 2022</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the hotels will be added in the databasae
        ///   If operation is "U" (Update) then all the hotels will be updated in the database 
        /// </summary>
        /// <param name="city_site_seeing_json"></param>
        /// <returns></returns>
        public String SaveCitySiteSeeing(String city_site_seeing_json, String UserId)
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
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveCitySiteSeeing]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_city_site_seeing_json", DbType.String, city_site_seeing_json);
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

