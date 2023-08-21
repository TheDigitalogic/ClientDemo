using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Data;
using System;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
using TravelNinjaz.B2B.WebAPI.Models.Interface;

namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class PackageRepository : IPackageRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;

        #endregion

        #region "Constructor"

        public PackageRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "Package CRUD Functions"
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>19 Dec, 2022</created_date>
        /// <summary>
        /// Get  Package List
        /// </summary>
        /// <returns>list of Transport Rate Object</returns>

        //public List<Entity.Package> GetPackageList(int type_id)
        //{
        //    System.Data.Common.DbCommand sqlCommand = null;
        //    DataSet ds = null;
        //    DataTable dt = null;

        //    List<Entity.Package> objPackageList = null;  //Parent
        //    List<Entity.CityAndNights> objCityAndNightsList = null;  //Child
        //    List<Entity.Inclusion> objInclusionList = null; //Child
        //    try
        //    {
        //        if (mDB == null)
        //        {
        //            mDB = new SqlDatabase(mConnectionString);
        //            mConnection = mDB.CreateConnection();
        //        }

        //        sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_package]");
        //        sqlCommand.CommandTimeout = 0;

        //        ds = mDB.ExecuteDataSet(sqlCommand);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            dt = ds.Tables[0];
        //            using (DataView view = new DataView(dt))
        //            {
        //                using (DataTable distinctPackage = view.ToTable(true, "Package_id", "Package_name", "Package_commision", "Package_image", "Is_best_selling", "Is_family_package", "Is_honeymoon_package", "Transport_rate_name", "Transport_rate_id", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
        //                {
        //                    objPackageList = new List<Package>();
        //                    Package objPackage = null;
        //                    for (int counter = 0; counter < distinctPackage.Rows.Count; counter++)
        //                    {
        //                        objPackage = new Package();
        //                        objPackage.Key = counter;
        //                        objPackage.Package_id = Convert.ToInt32(distinctPackage.Rows[counter]["Package_id"]);
        //                        objPackage.Package_name = Convert.ToString(distinctPackage.Rows[counter]["Package_name"]);
        //                        objPackage.Package_commision = Convert.ToDouble(distinctPackage.Rows[counter]["Package_commision"]);
        //                        objPackage.Package_image = Convert.ToString(distinctPackage.Rows[counter]["Package_image"]);
        //                        objPackage.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
        //                        objPackage.Is_honeymoon_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_honeymoon_package"]);
        //                        objPackage.Is_family_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_family_package"]);
        //                        objPackage.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
        //                        objPackage.Transport_rate_id = Convert.ToInt64(distinctPackage.Rows[counter]["Transport_rate_id"]);
        //                        objPackage.Destination_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_id"]);
        //                        objPackage.Transport_rate_name = Convert.ToString(distinctPackage.Rows[counter]["Transport_rate_name"]);
        //                        objPackage.Destination_name = Convert.ToString(distinctPackage.Rows[counter]["Destination_name"]);
        //                        objPackage.Destination_type_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_type_id"]);
        //                        objPackage.Is_active = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_active"]);
        //                        objPackage.Row_created_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_created_date"]);
        //                        objPackage.Row_created_by = Convert.ToString(distinctPackage.Rows[counter]["Row_created_by"]);
        //                        objPackage.Row_altered_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_altered_date"]);
        //                        objPackage.Row_altered_by = Convert.ToString(distinctPackage.Rows[counter]["Row_altered_by"]);
        //                        dt.DefaultView.RowFilter = "Transport_rate_id = '" + objPackage.Transport_rate_id + "' AND Package_name = '" + objPackage.Package_name + "'";
        //                        DataSet dscityandnights = null;
        //                        //DataTable dtcityandnights = null;
        //                        sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_package_city_nights]");
        //                        sqlCommand.CommandTimeout = 0;
        //                        dscityandnights = mDB.ExecuteDataSet(sqlCommand);
        //                        if (dscityandnights != null && dscityandnights.Tables.Count > 0)
        //                        {
        //                            objCityAndNightsList = new List<Entity.CityAndNights>();
        //                            foreach (DataRow dr in dscityandnights.Tables[0].Rows)
        //                            {
        //                                if (objPackage.Package_id == Convert.ToInt64(dr["Package_id"]))
        //                                {
        //                                    Entity.CityAndNights objCityAndNight = new Entity.CityAndNights();
        //                                    objCityAndNight.Key = Convert.ToInt32(dr["key"]);
        //                                    objCityAndNight.City_id = Convert.ToInt32(dr["City_id"]);
        //                                    objCityAndNight.City_name = Convert.ToString(dr["City_name"]);
        //                                    objCityAndNight.Nights = Convert.ToInt32(dr["Nights"]);
        //                                    objCityAndNight.City_nights_id = Convert.ToInt64(dr["City_nights_id"]);
        //                                    objCityAndNightsList.Add(objCityAndNight);
        //                                    objCityAndNight = null;
        //                                }
        //                            }
        //                        }
        //                        objPackage.CityAndNightsList = objCityAndNightsList;
        //                        DataSet dsinclusions = null;
        //                        //DataTable dtinclusions = null;
        //                        sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_package_inclusions]");
        //                        sqlCommand.CommandTimeout = 0;
        //                        dsinclusions = mDB.ExecuteDataSet(sqlCommand);
        //                        if (dsinclusions != null && dsinclusions.Tables.Count > 0)
        //                        {
        //                            objInclusionList = new List<Entity.Inclusion>();
        //                            foreach (DataRow di in dsinclusions.Tables[0].Rows)
        //                            {
        //                                if (objPackage.Package_id == Convert.ToInt64(di["Package_id"]))
        //                                {
        //                                    Entity.Inclusion objinclusion = new Entity.Inclusion();
        //                                    // objinclusion.Id = Convert.ToInt32(di["key"]);
        //                                    objinclusion.Inclusions_id = Convert.ToInt64(di["Inclusions_id"]);
        //                                    objinclusion.Key = Convert.ToInt64(di["key"]);
        //                                    objinclusion.Inclusions = Convert.ToString(di["Inclusions"]);
        //                                    objInclusionList.Add(objinclusion);
        //                                    objinclusion = null;
        //                                }
        //                            }
        //                        }
        //                        objPackage.InclusionsList = objInclusionList;
        //                        objPackageList.Add(objPackage);
        //                    }

        //                }
        //            }
        //        }


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
        //    return objPackageList;
        //}


        //public List<Entity.Package> GetPackageList_v2(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "")
        //{
        //    System.Data.Common.DbCommand sqlCommand = null;
        //    DataSet ds = null;
        //    DataTable dt = null;
        //    String TableName = "";
        //    List<Entity.Package> objPackageList = null;  //Parent
        //    //List<Entity.CityAndNights> objCityAndNightsList = null;  //Child
        //    List<Entity.PackageCity> objPackageCityList = null;  //Child
        //    List<Entity.PackageCityHotel> objPackageCityHotelList = null;  //Child of PackageCity
        //    List<Entity.Inclusion> objInclusionList = null; //Child
        //    List<Entity.PackageImages> objPackageImagesList = null;
        //    List<Entity.PackageItinerary> objPackageItineraryList = null;
        //    try
        //    {
        //        if (mDB == null)
        //        {
        //            mDB = new SqlDatabase(mConnectionString);
        //            mConnection = mDB.CreateConnection();
        //        }

        //        //sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_package]");

        //        sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetPackageList]");
        //        sqlCommand.CommandTimeout = 0;
        //        if (destination_type_id != 0)
        //            mDB.AddInParameter(sqlCommand, "@pa_destination_type_id", DbType.String, destination_type_id);

        //        if (destination_id != 0)
        //            mDB.AddInParameter(sqlCommand, "@pa_destination_id", DbType.String, destination_id);

        //        if (package_id != 0)
        //            mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.String, package_id);

        //        mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
        //        mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);


        //        sqlCommand.CommandTimeout = 0;

        //        ds = mDB.ExecuteDataSet(sqlCommand);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            dt = ds.Tables[0];
        //            using (DataView view = new DataView(dt))
        //            {
        //                using (DataTable distinctPackage = view.ToTable(true, "Package_id", "Package_name", "PackageGuideLines", "Package_description", "Package_commision", "Package_price","Is_best_selling", "Is_family_package", "Is_honeymoon_package", "Transport_rate_name", "Transport_rate_id", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
        //                {
        //                    objPackageList = new List<Package>();
        //                    Package objPackage = null;
        //                    for (int counter = 0; counter < distinctPackage.Rows.Count; counter++)
        //                    {
        //                        objPackage = new Package();
        //                        objPackage.Key = counter;
        //                        objPackage.Package_id = Convert.ToInt32(distinctPackage.Rows[counter]["Package_id"]);
        //                        objPackage.Package_name = Convert.ToString(distinctPackage.Rows[counter]["Package_name"]);
        //                        //objPackage.Package_type = Convert.ToString(distinctPackage.Rows[counter]["Package_type"]);
        //                        //objPackage.Valid_from = Convert.ToDateTime(distinctPackage.Rows[counter]["Valid_from"]);
        //                        //objPackage.Valid_to = Convert.ToDateTime(distinctPackage.Rows[counter]["Valid_to"]);
        //                        objPackage.PackageGuideLines = Convert.ToString(distinctPackage.Rows[counter]["PackageGuideLines"]);
        //                        objPackage.Package_description = Convert.ToString(distinctPackage.Rows[counter]["Package_description"]);
        //                        objPackage.Package_commision = Convert.ToDouble(distinctPackage.Rows[counter]["Package_commision"]);
        //                        objPackage.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
        //                        objPackage.Is_honeymoon_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_honeymoon_package"]);
        //                        objPackage.Is_family_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_family_package"]);
        //                        objPackage.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
        //                        objPackage.Transport_rate_id = Convert.ToInt64(distinctPackage.Rows[counter]["Transport_rate_id"]);
        //                        objPackage.Destination_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_id"]);
        //                        objPackage.Transport_rate_name = Convert.ToString(distinctPackage.Rows[counter]["Transport_rate_name"]);
        //                        objPackage.Destination_name = Convert.ToString(distinctPackage.Rows[counter]["Destination_name"]);
        //                        objPackage.Destination_type_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_type_id"]);

        //                        objPackage.Is_active = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_active"]);
        //                        objPackage.Row_created_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_created_date"]);
        //                        objPackage.Row_created_by = Convert.ToString(distinctPackage.Rows[counter]["Row_created_by"]);
        //                        objPackage.Row_altered_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_altered_date"]);
        //                        objPackage.Row_altered_by = Convert.ToString(distinctPackage.Rows[counter]["Row_altered_by"]);

        //                        //Loop through Each tables
        //                        foreach (DataTable dt_package in ds.Tables)
        //                        {
        //                            TableName = dt_package.Rows[0]["TableName"].ToString();
        //                            if (TableName == "PACKAGE_CITY")
        //                            {
        //                                DataTable dtPackageCity_temp = dt_package;
        //                                dtPackageCity_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
        //                                DataTable dtPackageCity = dtPackageCity_temp.DefaultView.ToTable();
        //                                if (dtPackageCity != null && dtPackageCity.Rows.Count > 0)
        //                                {
        //                                    objPackageCityList = new List<Entity.PackageCity>();
        //                                    foreach (DataRow dr in dtPackageCity.Rows)
        //                                    {

        //                                        Entity.PackageCity objPackageCity = new Entity.PackageCity();
        //                                        objPackageCity.Package_city_id = Convert.ToInt64(dr["Package_city_id"]);
        //                                        objPackageCity.Key = Convert.ToInt32(dr["key"]);
        //                                        objPackageCity.City_id = Convert.ToInt32(dr["City_id"]);
        //                                        objPackageCity.City_name = Convert.ToString(dr["City_name"]);
        //                                        objPackageCity.Order_no = Convert.ToInt32(dr["Order_no"]);



        //                                        foreach (DataTable dtCity in ds.Tables)
        //                                        {

        //                                            if (dtCity != null && dtCity.Rows.Count > 0)
        //                                            {
        //                                                TableName = dtCity.Rows[0]["TableName"].ToString();
        //                                                if (TableName == "PACKAGE_CITY_HOTEL")
        //                                                {
        //                                                    DataTable dtPackageCityHotel_temp = dtCity;
        //                                                    dtPackageCityHotel_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND Package_city_id = '" + objPackageCity.Package_city_id + "'";
        //                                                    DataTable dtHotel = dtPackageCityHotel_temp.DefaultView.ToTable();
        //                                                    if (dtHotel != null && dtHotel.Rows.Count > 0)
        //                                                    {
        //                                                        objPackageCityHotelList = new List<Entity.PackageCityHotel>();
        //                                                        foreach (DataRow dhotel in dtHotel.Rows)
        //                                                        {
        //                                                            Entity.PackageCityHotel objPackageCityHotel = new Entity.PackageCityHotel();
        //                                                            objPackageCityHotel.Hotel = new Hotel();
        //                                                            objPackageCityHotel.Hotel.Hotel_id = Convert.ToInt64(dhotel["Hotel_id"]);
        //                                                            objPackageCityHotel.Hotel.Hotel_name = Convert.ToString(dhotel["Hotel_name"]);
        //                                                            objPackageCityHotel.Hotel.Hotel_type = Convert.ToInt32(dhotel["Hotel_type"]);
        //                                                            objPackageCityHotel.Nights = Convert.ToInt16(dhotel["Nights"]);

        //                                                            objPackageCityHotelList.Add(objPackageCityHotel);
        //                                                            objPackageCityHotel = null;
        //                                                        }
        //                                                        objPackageCity.PackageCityHotelList = objPackageCityHotelList;
        //                                                    }

        //                                                    break;
        //                                                }
        //                                                else
        //                                                    continue;                                                   
        //                                            }
        //                                        }
        //                                        objPackageCityList.Add(objPackageCity);

        //                                        objPackageCity = null;

        //                                    }
        //                                    objPackage.PackageCityList = objPackageCityList;



        //                                }
        //                            }
        //                            else if (TableName == "PACKAGE_INCLUSIONS")
        //                            {
        //                                DataTable dtInclusions_temp = dt_package;
        //                                dtInclusions_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
        //                                DataTable dtInclusions = dtInclusions_temp.DefaultView.ToTable();
        //                                if (dtInclusions != null && dtInclusions.Rows.Count > 0)
        //                                {
        //                                    objInclusionList = new List<Entity.Inclusion>();
        //                                    foreach (DataRow di in dtInclusions.Rows)
        //                                    {
        //                                        // if (objPackage.Package_id == Convert.ToInt64(di["Package_id"]))
        //                                        // {
        //                                        Entity.Inclusion objinclusion = new Entity.Inclusion();
        //                                        objinclusion.Inclusions_id = Convert.ToInt64(di["Inclusions_id"]);
        //                                        objinclusion.Key = Convert.ToInt64(di["key"]);
        //                                        objinclusion.Is_include = Convert.ToBoolean(di["Is_include"]);
        //                                        objinclusion.Inclusions = Convert.ToString(di["Inclusions"]);
        //                                        objInclusionList.Add(objinclusion);
        //                                        objinclusion = null;
        //                                        //}f
        //                                    }
        //                                    objPackage.InclusionsList = objInclusionList;
        //                                }
        //                            }
        //                            else if (TableName == "PACKAGE_ITINERARY")
        //                            {
        //                                DataTable dtItinerary_temp = dt_package;
        //                                dtItinerary_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
        //                                DataTable dtItinerary = dtItinerary_temp.DefaultView.ToTable();
        //                                if (dtItinerary != null && dtItinerary.Rows.Count > 0)
        //                                {
        //                                    objPackageItineraryList = new List<Entity.PackageItinerary>();
        //                                    foreach (DataRow diti in dtItinerary.Rows)
        //                                    {
        //                                        // if (objPackage.Package_id == Convert.ToInt64(di["Package_id"]))
        //                                        // {
        //                                        Entity.PackageItinerary objitinerary = new Entity.PackageItinerary();
        //                                        objitinerary.Key = Convert.ToInt64(diti["key"]);
        //                                        objitinerary.Package_itinerary_id = Convert.ToInt64(diti["Package_itinerary_id"]);
        //                                        objitinerary.Day = Convert.ToInt64(diti["Day"]);
        //                                        objitinerary.Itinerary_title = Convert.ToString(diti["Itinerary_title"]);
        //                                        objitinerary.Itinerary_description = Convert.ToString(diti["Itinerary_description"]);
        //                                        objPackageItineraryList.Add(objitinerary);
        //                                        objitinerary = null;
        //                                        //}
        //                                    }
        //                                    objPackage.Package_itinerary_list = objPackageItineraryList;
        //                                }
        //                            }
        //                            else if (TableName == "PACKAGE_IMAGES")
        //                            {
        //                                DataTable dtImagelist_temp = dt_package;
        //                                dtImagelist_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
        //                                DataTable dtImages = dtImagelist_temp.DefaultView.ToTable();
        //                                if (dtImages != null && dtImages.Rows.Count > 0)
        //                                {
        //                                    objPackageImagesList = new List<Entity.PackageImages>();
        //                                    foreach (DataRow dimg in dtImages.Rows)
        //                                    {
        //                                        // if (objPackage.Package_id == Convert.ToInt64(di["Package_id"]))
        //                                        // {
        //                                        Entity.PackageImages objImages = new Entity.PackageImages();
        //                                        objImages.Package_id = Convert.ToInt32(dimg["Package_id"]);
        //                                        objImages.Package_image_id = Convert.ToInt64(dimg["Package_image_id"]);
        //                                        objImages.Key = Convert.ToInt64(dimg["key"]);
        //                                        objImages.Image_name = Convert.ToString(dimg["Image_name"]);
        //                                        objImages.Is_primary = Convert.ToBoolean(dimg["Is_primary"]);
        //                                        objImages.Is_active = Convert.ToBoolean(dimg["Is_active"]);
        //                                        objImages.Row_created_date = Convert.ToDateTime(dimg["Row_created_date"]);
        //                                        objImages.Row_created_by = Convert.ToString(dimg["Row_created_by"]);
        //                                        objImages.Row_altered_date = Convert.ToDateTime(dimg["Row_altered_date"]);
        //                                        objImages.Row_altered_by = Convert.ToString(dimg["Row_altered_by"]);
        //                                        objPackageImagesList.Add(objImages);
        //                                        objImages = null;
        //                                        //}
        //                                    }

        //                                    objPackage.ImageList = objPackageImagesList;
        //                                }
        //                            }
        //                        }                                                                                                                                                               
        //                        objPackageList.Add(objPackage);
        //                    }

        //                }
        //            }
        //        }

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
        //    return objPackageList;
        //}


        public List<Entity.Package> GetPackageList(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "")
        {

            System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;

            List<Entity.Package> objPackageList = new List<Package>();  //Parent
            List<Entity.PackageCity> objPackageCityList = null;  //Child
            List<Entity.PackageCityHotel> objPackageCityHotelList = null;  //Child of PackageCity

            List<Entity.SiteSeeing> objCitySiteSeeingList = null; //Child of PackageCity
            List<Entity.Hotel> objHotelList = null;
            List<Entity.Inclusion> objInclusionList = null; //Child
            List<Entity.PackageImages> objPackageImagesList = null;
            List<Entity.PackageItinerary> objPackageItineraryList = null;
            List<Entity.HotelMealPlan> objMealPlanList = null;
            HotelMealPlan objSelectedHotelMealPlans = null;
            TransportRate objSelectedTranpsort = null;


            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetPackageList]");
                sqlCommand.CommandTimeout = 0;
                if (destination_type_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_type_id", DbType.String, destination_type_id);

                if (destination_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_id", DbType.String, destination_id);

                if (package_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.String, package_id);

                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                sqlCommand.CommandTimeout = 0;

                ds = mDB.ExecuteDataSet(sqlCommand);

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                            ds.Tables[ds.Tables.IndexOf(dt)].TableName = Convert.ToString(dt.Rows[0]["TableName"]);
                    }
                }

                DataTable dt_package = ds.Tables["PACKAGE"];

                if (dt_package != null && dt_package.Rows.Count > 0)
                {

                    using (DataView view = new DataView(dt_package))
                    {

                        using (DataTable distinctPackage = view.ToTable(true, "Package_id", "Package_name", "PackageGuideLines", "Package_description", "Package_commision", "Package_price", "Is_best_selling", "Is_family_package", "Is_honeymoon_package", "Transport_rate_name", "Transport_rate_id", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
                        {
                            objPackageList = new List<Package>();
                            Package objPackage = null;

                            //Package Starts ======================
                            for (int counter = 0; counter < distinctPackage.Rows.Count; counter++)
                            {
                                objPackage = new Package();
                                objPackage.Key = counter;
                                objPackage.Package_id = Convert.ToInt32(distinctPackage.Rows[counter]["Package_id"]);
                                objPackage.Package_name = Convert.ToString(distinctPackage.Rows[counter]["Package_name"]);
                                //objPackage.Package_type = Convert.ToString(distinctPackage.Rows[counter]["Package_type"]);
                                //objPackage.Valid_from = Convert.ToDateTime(distinctPackage.Rows[counter]["Valid_from"]);
                                //objPackage.Valid_to = Convert.ToDateTime(distinctPackage.Rows[counter]["Valid_to"]);
                                objPackage.PackageGuideLines = Convert.ToString(distinctPackage.Rows[counter]["PackageGuideLines"]);
                                objPackage.Package_description = Convert.ToString(distinctPackage.Rows[counter]["Package_description"]);
                                objPackage.Package_commision = Convert.ToDouble(distinctPackage.Rows[counter]["Package_commision"]);
                                objPackage.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
                                objPackage.Is_honeymoon_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_honeymoon_package"]);
                                objPackage.Is_family_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_family_package"]);
                                objPackage.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
                                objPackage.Transport_rate_id = Convert.ToInt64(distinctPackage.Rows[counter]["Transport_rate_id"]);
                                objPackage.Destination_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_id"]);
                                objPackage.Transport_rate_name = Convert.ToString(distinctPackage.Rows[counter]["Transport_rate_name"]);
                                objPackage.Destination_name = Convert.ToString(distinctPackage.Rows[counter]["Destination_name"]);
                                objPackage.Destination_type_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_type_id"]);

                                objPackage.Is_active = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_active"]);
                                objPackage.Row_created_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_created_date"]);
                                objPackage.Row_created_by = Convert.ToString(distinctPackage.Rows[counter]["Row_created_by"]);
                                objPackage.Row_altered_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_altered_date"]);
                                objPackage.Row_altered_by = Convert.ToString(distinctPackage.Rows[counter]["Row_altered_by"]);


                                //Package city Starts ======================
                                DataTable dt_package_city_temp = ds.Tables["PACKAGE_CITY"];

                                if (dt_package_city_temp != null && dt_package_city_temp.Rows.Count > 0)
                                {

                                    dt_package_city_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                                    DataTable dt_package_city = dt_package_city_temp.DefaultView.ToTable();

                                    //All City childs will come here..


                                    if (dt_package_city != null && dt_package_city.Rows.Count > 0)
                                    {
                                        objPackageCityList = new List<PackageCity>();
                                        foreach (DataRow dr in dt_package_city.Rows)
                                        {
                                            Entity.PackageCity objPackageCity = new Entity.PackageCity();
                                            objPackageCity.Package_city_id = Convert.ToInt64(dr["Package_city_id"]);
                                            objPackageCity.Key = Convert.ToInt32(dr["key"]);
                                            objPackageCity.City_id = Convert.ToInt32(dr["City_id"]);
                                            objPackageCity.City_name = Convert.ToString(dr["City_name"]);
                                            objPackageCity.Order_no = Convert.ToInt32(dr["Order_no"]);


                                            //For each Package City, get the list of Package City Hotels  
                                            //Package City Hotel Starts ================================================
                                            DataTable dt_package_city_hotel_temp = ds.Tables["PACKAGE_CITY_HOTEL"];

                                            if (dt_package_city_hotel_temp != null && dt_package_city_hotel_temp.Rows.Count > 0)
                                            {
                                                dt_package_city_hotel_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objPackageCity.City_id + "'";
                                                DataTable dt_package_city_hotel = dt_package_city_hotel_temp.DefaultView.ToTable();

                                                if (dt_package_city_hotel != null && dt_package_city_hotel.Rows.Count > 0)
                                                {
                                                    objHotelList = new List<Entity.Hotel>();
                                                    foreach (DataRow dhotel in dt_package_city_hotel.Rows)
                                                    {

                                                        objPackageCityHotelList = new List<PackageCityHotel>();
                                                        Entity.PackageCityHotel objPackageCityHotel = new Entity.PackageCityHotel();
                                                        objPackageCityHotel.Hotel = new Hotel();
                                                        objPackageCityHotel.PackageCityHotelId = Convert.ToInt64(dhotel["Package_city_hotel_id"]);
                                                        objPackageCityHotel.Hotel.Hotel_id = Convert.ToInt64(dhotel["Hotel_id"]);
                                                        objPackageCityHotel.Hotel.Hotel_name = Convert.ToString(dhotel["Hotel_name"]);
                                                        objPackageCityHotel.Hotel.Hotel_type = Convert.ToInt32(dhotel["Hotel_type"]);
                                                        objPackageCityHotel.Nights = Convert.ToInt16(dhotel["Nights"]);

                                                        objPackageCityHotelList.Add(objPackageCityHotel);
                                                        objPackageCityHotel = null;
                                                    }
                                                    objPackageCity.PackageCityHotelList = objPackageCityHotelList;
                                                }

                                            }
                                            //Package City Hotel Ends ================================================


                                            objPackageCityList.Add(objPackageCity);
                                        }


                                        objPackage.PackageCityList = objPackageCityList;
                                    }
                                }
                                //Package city Ends ======================


                                //Package Inclusions Starts =================
                                DataTable dt_package_inclusions_temp = ds.Tables["PACKAGE_INCLUSIONS"];

                                if (dt_package_inclusions_temp != null && dt_package_inclusions_temp.Rows.Count > 0)
                                {
                                    dt_package_inclusions_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                                    DataTable dt_package_inclusions = dt_package_inclusions_temp.DefaultView.ToTable();
                                    if (dt_package_inclusions != null && dt_package_inclusions.Rows.Count > 0)
                                    {

                                        objInclusionList = new List<Entity.Inclusion>();
                                        foreach (DataRow di in dt_package_inclusions.Rows)
                                        {
                                            Entity.Inclusion objinclusion = new Entity.Inclusion();
                                            objinclusion.Inclusions_id = Convert.ToInt64(di["Inclusions_id"]);
                                            objinclusion.Key = Convert.ToInt64(di["key"]);
                                            objinclusion.Inclusions = Convert.ToString(di["Inclusions"]);
                                            objinclusion.Is_include = Convert.ToBoolean(di["Is_include"]);
                                            objInclusionList.Add(objinclusion);
                                            objinclusion = null;

                                        }
                                        objPackage.InclusionsList = objInclusionList;
                                    }
                                }
                                //Package Inclusions End ===================


                                //Package Images Starts =================

                                DataTable dt_package_images_temp = ds.Tables["PACKAGE_IMAGES"];

                                if (dt_package_images_temp != null && dt_package_images_temp.Rows.Count > 0)
                                {
                                    dt_package_images_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                                    DataTable dt_package_images = dt_package_images_temp.DefaultView.ToTable();
                                    if (dt_package_images != null && dt_package_images.Rows.Count > 0)
                                    {

                                        objPackageImagesList = new List<Entity.PackageImages>();
                                        foreach (DataRow dimg in dt_package_images.Rows)
                                        {
                                            Entity.PackageImages objImages = new Entity.PackageImages();
                                            objImages.Package_id = Convert.ToInt32(dimg["Package_id"]);
                                            objImages.Package_image_id = Convert.ToInt64(dimg["Package_image_id"]);
                                            objImages.Key = Convert.ToInt64(dimg["key"]);
                                            objImages.Image_name = Convert.ToString(dimg["Image_name"]);
                                            objImages.Is_primary = Convert.ToBoolean(dimg["Is_primary"]);
                                            objImages.Is_active = Convert.ToBoolean(dimg["Is_active"]);
                                            objImages.Row_created_date = Convert.ToDateTime(dimg["Row_created_date"]);
                                            objImages.Row_created_by = Convert.ToString(dimg["Row_created_by"]);
                                            objImages.Row_altered_date = Convert.ToDateTime(dimg["Row_altered_date"]);
                                            objImages.Row_altered_by = Convert.ToString(dimg["Row_altered_by"]);
                                            objPackageImagesList.Add(objImages);
                                            objImages = null;
                                        }
                                        objPackage.ImageList = objPackageImagesList;
                                    }
                                }
                                //Package Images End ===================

                                //Package Itinerary Starts =================

                                DataTable dt_package_Itinerary_temp = ds.Tables["PACKAGE_ITINERARY"];

                                if (dt_package_Itinerary_temp != null && dt_package_Itinerary_temp.Rows.Count > 0)
                                {
                                    dt_package_Itinerary_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                                    DataTable dtItinerary = dt_package_Itinerary_temp.DefaultView.ToTable();
                                    if (dtItinerary != null && dtItinerary.Rows.Count > 0)
                                    {
                                        objPackageItineraryList = new List<Entity.PackageItinerary>();
                                        foreach (DataRow diti in dtItinerary.Rows)
                                        {
                                            Entity.PackageItinerary objitinerary = new Entity.PackageItinerary();
                                            objitinerary.Key = Convert.ToInt64(diti["key"]);
                                            objitinerary.Package_itinerary_id = Convert.ToInt64(diti["Package_itinerary_id"]);
                                            objitinerary.Day = Convert.ToInt64(diti["Day"]);
                                            objitinerary.Itinerary_title = Convert.ToString(diti["Itinerary_title"]);
                                            objitinerary.Itinerary_description = Convert.ToString(diti["Itinerary_description"]);
                                            objPackageItineraryList.Add(objitinerary);
                                            objitinerary = null;
                                        }

                                        objPackage.Package_itinerary_list = objPackageItineraryList;
                                    }
                                }
                                //Package Itinerary Ends =================


                                objPackageList.Add(objPackage);
                            }  //Loop through each package

                            //Package Ends ======================

                        } // End of distinctPackage

                    } // using DataView
                } // End of dt_package condition


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
            return objPackageList;
        }

        /// <summary>
        ///  For a particular Package - Get he list of Cities and its Hotels (Along with Hotels MealPlan list)
        /// </summary>
        /// <created_by>Manisha Tripathi</created_date>
        /// <created_date>July 24, 2023</created_date>
        /// <summary>
        ///  Get All PackageList
        /// </summary>
        /// <returns> Gets List of all Package</returns>
        public List<Entity.PackageCity> PackageCityHotelMealPlanList(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "")
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;

            List<Entity.PackageCity> objPackageCityList = null;  //Child
            List<Entity.PackageCityHotel> objPackageCityHotelList = null;  //Child of PackageCity                   
            List<Entity.Hotel> objHotelList = null;
            List<Entity.HotelMealPlan> objMealPlanList = null;


            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetPackageCityHotelMealPlanList]");
                sqlCommand.CommandTimeout = 0;
                if (destination_type_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_type_id", DbType.String, destination_type_id);

                if (destination_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_id", DbType.String, destination_id);

                if (package_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.String, package_id);

                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                sqlCommand.CommandTimeout = 0;

                ds = mDB.ExecuteDataSet(sqlCommand);

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                            ds.Tables[ds.Tables.IndexOf(dt)].TableName = Convert.ToString(dt.Rows[0]["TableName"]);
                    }
                }



                //Package city Starts ======================
                DataTable dt_package_city = ds.Tables["PACKAGE_CITY"];

                //All City childs will come here..
                if (dt_package_city != null && dt_package_city.Rows.Count > 0)
                {
                    objPackageCityList = new List<PackageCity>();
                    foreach (DataRow dr in dt_package_city.Rows)
                    {
                        Entity.PackageCity objPackageCity = new Entity.PackageCity();
                        objPackageCity.Package_city_id = Convert.ToInt64(dr["Package_city_id"]);
                        objPackageCity.Key = Convert.ToInt32(dr["key"]);
                        objPackageCity.City_id = Convert.ToInt32(dr["City_id"]);
                        objPackageCity.City_name = Convert.ToString(dr["City_name"]);
                        objPackageCity.Order_no = Convert.ToInt32(dr["Order_no"]);
                        objPackageCity.SelectedSiteSeeingList= new List<SiteSeeing>();

                        //For each Package City, get the list of Package City Hotels  
                        //Package City Hotel Starts ================================================
                        DataTable dt_package_city_hotel_temp = ds.Tables["PACKAGE_CITY_HOTEL"];

                        if (dt_package_city_hotel_temp != null && dt_package_city_hotel_temp.Rows.Count > 0)
                        {
                            dt_package_city_hotel_temp.DefaultView.RowFilter = "City_id = '" + objPackageCity.City_id + "'";
                            DataTable dt_package_city_hotel = dt_package_city_hotel_temp.DefaultView.ToTable();

                            if (dt_package_city_hotel != null && dt_package_city_hotel.Rows.Count > 0)
                            {
                                objPackageCityHotelList = new List<PackageCityHotel>();
                                foreach (DataRow dhotel in dt_package_city_hotel.Rows)
                                {

                                 
                                    Entity.PackageCityHotel objPackageCityHotel = new Entity.PackageCityHotel();
                                    objPackageCityHotel.Hotel = new Hotel();
                                    objPackageCityHotel.Hotel.Hotel_id = Convert.ToInt64(dhotel["Hotel_id"]);
                                    objPackageCityHotel.Hotel.Hotel_name = Convert.ToString(dhotel["Hotel_name"]);
                                    objPackageCityHotel.Hotel.Hotel_type = Convert.ToInt32(dhotel["Hotel_type"]);
                                    objPackageCityHotel.Hotel.Hotel_rating = Convert.ToInt64(dhotel["Hotel_rating"]);


                                        //For each  Package City Hotels, get the list of Hotel Meal Plans
                                        DataTable dt_package_city_hotel_mealplan_temp = ds.Tables["PACKAGE_CITY_HOTEL_MEAL_PLANS"];
                                        if (dt_package_city_hotel_mealplan_temp != null && dt_package_city_hotel_mealplan_temp.Rows.Count > 0)
                                        {
                                            dt_package_city_hotel_mealplan_temp.DefaultView.RowFilter = " City_id = '" + objPackageCity.City_id + "' AND Hotel_id = '" + objPackageCityHotel.Hotel.Hotel_id + "'";
                                            DataTable dt_package_city_hotel_mealplan = dt_package_city_hotel_mealplan_temp.DefaultView.ToTable();
                                            if (dt_package_city_hotel_mealplan != null && dt_package_city_hotel_mealplan.Rows.Count > 0)
                                            {
                                                objMealPlanList = new List<Entity.HotelMealPlan>();
                                                objPackageCityHotel.Hotel.HotelMealPlanList = new List<Entity.HotelMealPlan>();
                                                foreach (DataRow drMealPlan in dt_package_city_hotel_mealplan.Rows)
                                                {
                                                    Entity.HotelMealPlan objMealPlans = new Entity.HotelMealPlan();
                                                    objMealPlans.Hotel_id = Convert.ToInt32(drMealPlan["Hotel_id"]);
                                                    objMealPlans.Hotel_meal_plan_id = Convert.ToInt32(drMealPlan["Hotel_meal_plan_id"]);
                                                    objMealPlans.Meal_plan_code = Convert.ToString(drMealPlan["Meal_plan_code"]);
                                                    objMealPlans.Adult_price = Convert.ToString(drMealPlan["Adult_price"]);
                                                    objMealPlans.Child_price = Convert.ToString(drMealPlan["Child_price"]);
                                                    objMealPlans.Child_price_without_bed = Convert.ToString(drMealPlan["Child_price_without_bed"]);
                                                    objMealPlanList.Add(objMealPlans);
                                                    objMealPlans = null;
                                                }

                                                objPackageCityHotel.Hotel.HotelMealPlanList = objMealPlanList;
                                            } // If Meal Plan exists
                                       
                                        } //

                                        //Package City Hotel and MealPlans Selected Starts ================================================
                                        DataTable dt_package_city_hotel_mealplanSelected_temp = ds.Tables["PACKAGE_CITY_HOTELS_MEALPLANS_SELECTED"];

                                        if (dt_package_city_hotel_mealplanSelected_temp != null && dt_package_city_hotel_mealplanSelected_temp.Rows.Count > 0)
                                        {
                                            dt_package_city_hotel_mealplanSelected_temp.DefaultView.RowFilter = " City_id = '" + objPackageCity.City_id + "'";
                                            DataTable dt_package_city_hotel_mealplanSelected = dt_package_city_hotel_mealplanSelected_temp.DefaultView.ToTable();
                                            if (dt_package_city_hotel_mealplanSelected != null && dt_package_city_hotel_mealplanSelected.Rows.Count > 0)
                                            {
                                                objPackageCityHotel.Hotel.SelectedHotelMealPlan = new HotelMealPlan();
                                                objPackageCityHotel.Hotel.SelectedHotelMealPlan.Hotel_id = Convert.ToInt32(dt_package_city_hotel_mealplanSelected.Rows[0]["Hotel_id"]);
                                                objPackageCityHotel.Hotel.SelectedHotelMealPlan.Meal_plan_code = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Meal_plan_code"]);
                                                objPackageCityHotel.Hotel.SelectedHotelMealPlan.Adult_price = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Adult_price"]);
                                                objPackageCityHotel.Hotel.SelectedHotelMealPlan.Child_price = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Child_price"]);
                                                objPackageCityHotel.Hotel.SelectedHotelMealPlan.Child_price_without_bed = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Child_price_without_bed"]);
                                            }
                                        }

                                    objPackageCityHotelList.Add(objPackageCityHotel);
                                    objPackageCityHotel = null;

                                } // Forloop Package City Hotel 
                                objPackageCity.PackageCityHotelList = objPackageCityHotelList;
                            }

                        }
                        //Package City Hotel Ends ================================================



                            objPackageCityList.Add(objPackageCity);
                            objPackageCity = null;
                      
                    } //Forloop Package City

                } //dt_package_city != null


                //Package city Ends ======================                                                


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
            return objPackageCityList;
        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>19 Dec, 2022</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the hotels will be added in the databasae
        ///   If operation is "U" (Update) then all the hotels will be updated in the database 
        /// </summary>
        /// <param name="package_json"></param>
        /// <returns></returns>

        public String[] SavePackage(String package_json, String UserId)
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
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackage]");
                sqlCommand.CommandTimeout = 0;
                mDB.AddInParameter(sqlCommand, "@pa_package_json", DbType.String, package_json);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                // mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, operation);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                mDB.AddOutParameter(sqlCommand, "@pa_out_package_id", DbType.Int64, 80000);
                mDB.ExecuteNonQuery(sqlCommand);
                arrResult[0] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));
                arrResult[1] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_package_id"));
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

