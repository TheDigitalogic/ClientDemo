using ClientDemo.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using ClientDemo.WebAPI.Models.Entity;

namespace ClientDemo.WebAPI.Models.Repository
{
    public class OrganizationRepository:IOrganizationRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;
        #endregion

        #region "Constructor"
        public OrganizationRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "ProcessLogs CRUD Functions"
        public List<Entity.Organization> GetOrganizationList()
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet result = null;
            List<Entity.Organization> organizationColl = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_organization]");
                sqlCommand.CommandTimeout = 0;
                result = mDB.ExecuteDataSet(sqlCommand);
                if (result != null && result.Tables.Count > 0)
                {
                    organizationColl = new List<Organization>();
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        Entity.Organization objOrganization = new Entity.Organization();
                        objOrganization.OrganizationId = Convert.ToInt32(dr["OrganizationId"]);
                        objOrganization.OrganizationCode = Convert.ToString(dr["OrganizationCode"]);
                        objOrganization.OrganizationName = Convert.ToString(dr["OrganizationName"]);

                        organizationColl.Add(objOrganization);
                        objOrganization = null;
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
            return organizationColl;
        }
        public String SaveOrganization(Organization organization)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSaveOrganization]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_organization_id", DbType.Int64, organization.OrganizationId);
                mDB.AddInParameter(sqlCommand, "@pa_organization_code", DbType.String, organization.OrganizationCode);
                mDB.AddInParameter(sqlCommand, "@pa_organization_name", DbType.String, organization.OrganizationName);
                mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, organization.Operation);
                mDB.AddInParameter(sqlCommand, "@pa_is_active", DbType.Boolean, true);
                mDB.AddInParameter(sqlCommand, "@pa_row_created_by", DbType.String, organization.Row_created_by);
                mDB.AddInParameter(sqlCommand, "@pa_row_created_date", DbType.DateTime, DateTime.Now);
                mDB.AddInParameter(sqlCommand, "@pa_row_altered_by", DbType.String, organization.Row_altered_by);
                mDB.AddInParameter(sqlCommand, "@pa_row_altered_date", DbType.DateTime, DateTime.Now);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 2000);
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
