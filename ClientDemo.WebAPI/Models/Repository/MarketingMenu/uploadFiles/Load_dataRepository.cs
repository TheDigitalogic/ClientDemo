using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Data;
using System;
using TravelNinjaz.B2B.WebAPI.Models.Interface;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class Load_dataRepository: ILoad_dataRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;
        #endregion

        #region "Constructor"
        public Load_dataRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion

        #region "Desination CRUD Functions"
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>22 May, 2023</created_date>
        /// <summary>
        /// Get Travelling List Data
        /// </summary>
        /// <returns>list of Travelling Object</returns>
        public List<Entity.TravellingCompany> GetTravellingDataList()
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.TravellingCompany> travellingCompanyCol = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_travelling_company]");
                sqlCommand.CommandTimeout = 0;

                result = mDB.ExecuteDataSet(sqlCommand);

                if (result != null && result.Tables.Count > 0)
                {
                    travellingCompanyCol = new List<Entity.TravellingCompany>();

                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.TravellingCompany objTravelling = new Entity.TravellingCompany();
                        objTravelling.Key = Convert.ToInt32(dr["key"]);
                        objTravelling.Travelling_company_id = Convert.ToInt32(dr["Travelling_company_id"]);
                        objTravelling.Company_name = Convert.ToString(dr["Company_name"]);
                        objTravelling.Mobile_1 = Convert.ToString(dr["Mobile_1"]);
                        objTravelling.Mobile_2 = Convert.ToString(dr["Mobile_2"]);
                        objTravelling.Email_id_1 = Convert.ToString(dr["Email_id_1"]);
                        objTravelling.Email_id_2 = Convert.ToString(dr["Email_id_2"]);
                        objTravelling.Website = Convert.ToString(dr["Website"]);
                        objTravelling.Address = Convert.ToString(dr["Address"]);
                        objTravelling.City = Convert.ToString(dr["City"]);
                        objTravelling.State = Convert.ToString(dr["State"]);
                        objTravelling.Status = Convert.ToString(dr["Status"]);
                        objTravelling.Is_active = Convert.ToBoolean(dr["Is_active"]);
                        objTravelling.Row_created_date = Convert.ToDateTime(dr["Row_created_date"]);
                        objTravelling.Row_created_by = Convert.ToString(dr["Row_created_by"]);
                        objTravelling.Row_altered_date = Convert.ToDateTime(dr["Row_altered_date"]);
                        objTravelling.Row_altered_by = Convert.ToString(dr["Row_altered_by"]);

                        travellingCompanyCol.Add(objTravelling);

                        objTravelling = null;
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
            return travellingCompanyCol;
        }
        #endregion
    }
}
