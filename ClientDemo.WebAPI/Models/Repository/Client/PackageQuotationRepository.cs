using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class PackageQuotationRepository : IPackageQuotationRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;
        #endregion

        #region "Constructor"
        public PackageQuotationRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion

        #region "Desination CRUD Functions"

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>09 Feb, 2023</created_date>
        /// <modified_by>Manisha Tripathi</modified_by>
        /// <modified_date>12 Apr, 2023</modified_date>
        /// <param name="destination_type_id"></param>
        /// <param name="destination_id"></param>
        /// <param name="package_id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Entity.Package> GetPackageQuotationList_OLD(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "")
        {

            System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;
            DataTable dt_parent = null;
            String TableName = "";

            List<Entity.Package> objPackageList = null;  //Parent
            List<Entity.CityAndNights> objCityAndNightsList = null;  //Child
            List<Entity.SiteSeeing> objCitySiteSeeingList = null; //GrandChild
            List<Entity.Hotel> objHotelList = null;
            List<Entity.Inclusion> objInclusionList = null; //Child
            List<Entity.PackageImages> objPackageImagesList = null;
            List<Entity.PackageItinerary> objPackageItineraryList = null;
            List<Entity.HotelMealPlan> objMealPlanList = null;
            HotelMealPlan objSelectedHotelMealPlans = null;
            TransportRate objSelectedTranpsort = null;
            List<Entity.PackageCityHotel> objPackageCityHotelList = null;  //Child of PackageCity

            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetPackageQuotationList]");
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
                    if (ds != null && ds.Tables.Count > 0)
                    {

                        dt_parent = ds.Tables[0];
                        using (DataView view = new DataView(dt_parent))
                        {
                            using (DataTable distinctPackage = view.ToTable(true, "Package_id", "Package_name", "Package_price_before_discount", "Valid_from", "Valid_to", "Package_type", "Package_description", "Package_commision", "Package_price", "Is_best_selling", "Is_family_package", "Is_honeymoon_package", "Transport_rate_name", "Transport_rate_id", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
                            {
                                objPackageList = new List<Package>();
                                Package objPackage = null;
                                for (int counter = 0; counter < distinctPackage.Rows.Count; counter++)
                                {
                                    objPackage = new Package();
                                    objPackage.Key = counter;
                                    objPackage.Package_id = Convert.ToInt32(distinctPackage.Rows[counter]["Package_id"]);
                                    objPackage.Package_price = Convert.ToInt32(distinctPackage.Rows[counter]["Package_price"]);
                                    objPackage.Package_price_before_discount = Convert.ToDouble(distinctPackage.Rows[counter]["Package_price_before_discount"]);
                                    objPackage.Package_name = Convert.ToString(distinctPackage.Rows[counter]["Package_name"]);
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

                                    //Loop through Each tables
                                    foreach (DataTable dt_package in ds.Tables)
                                    {

                                        if (dt_package != null && dt_package.Rows.Count > 0)
                                        {

                                            TableName = dt_package.Rows[0]["TableName"].ToString();
                                            if (TableName == "PACKAGE_TRANSPORT_RATE_SELECTED")
                                            {
                                                DataTable dtSelectedTransport_temp = dt_package;
                                                dtSelectedTransport_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                                                DataTable dtSelectedTransport = dtSelectedTransport_temp.DefaultView.ToTable();
                                                if (dtSelectedTransport != null && dtSelectedTransport.Rows.Count > 0)
                                                {
                                                    objSelectedTranpsort = new TransportRate();
                                                    objSelectedTranpsort.Transport_id = Convert.ToInt32(dtSelectedTransport.Rows[0]["Transport_id"]);
                                                    objPackage.SelectedTransport = objSelectedTranpsort;
                                                }

                                            }

                                            else if (TableName == "PACKAGE_CITY")
                                            {

                                                DataTable dtPackageCity_temp = dt_package;
                                                dtPackageCity_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                                                DataTable dtPackageCity = dtPackageCity_temp.DefaultView.ToTable();

                                                if (dtPackageCity != null && dtPackageCity.Rows.Count > 0)
                                                {
                                                    foreach (DataRow dr in dtPackageCity.Rows)
                                                    {
                                                        Entity.PackageCity objPackageCity = new Entity.PackageCity();
                                                        objPackageCity.Package_city_id = Convert.ToInt64(dr["Package_city_id"]);
                                                        objPackageCity.Key = Convert.ToInt32(dr["key"]);
                                                        objPackageCity.City_id = Convert.ToInt32(dr["City_id"]);
                                                        objPackageCity.City_name = Convert.ToString(dr["City_name"]);
                                                        objPackageCity.Order_no = Convert.ToInt32(dr["Order_no"]);

                                                        //Loop through Each tables
                                                        foreach (DataTable dtCity in ds.Tables)
                                                        {

                                                            if (dtCity != null && dtCity.Rows.Count > 0)
                                                            {
                                                                TableName = dtCity.Rows[0]["TableName"].ToString();
                                                                if (TableName == "PACKAGE_CITY_HOTEL")
                                                                {

                                                                    DataTable dtPackageCityHotel_temp = dtCity;
                                                                    dtPackageCityHotel_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND Package_city_id = '" + objPackageCity.Package_city_id + "'";
                                                                    DataTable dtHotel = dtPackageCityHotel_temp.DefaultView.ToTable();

                                                                    if (dtHotel != null && dtHotel.Rows.Count > 0)
                                                                    {
                                                                        objHotelList = new List<Entity.Hotel>();
                                                                        foreach (DataRow dhotel in dtHotel.Rows)
                                                                        {

                                                                            Entity.PackageCityHotel objPackageCityHotel = new Entity.PackageCityHotel();
                                                                            objPackageCityHotel.Hotel = new Hotel();
                                                                            objPackageCityHotel.Hotel.Hotel_id = Convert.ToInt64(dhotel["Hotel_id"]);
                                                                            objPackageCityHotel.Hotel.Hotel_name = Convert.ToString(dhotel["Hotel_name"]);
                                                                            objPackageCityHotel.Hotel.Hotel_type = Convert.ToInt32(dhotel["Hotel_type"]);
                                                                            objPackageCityHotelList.Add(objPackageCityHotel);
                                                                            objPackageCityHotel = null;
                                                                        }
                                                                        objPackageCity.PackageCityHotelList = objPackageCityHotelList;
                                                                    }
                                                                }

                                                                else if (TableName == "PACKAGE_CITY_SITE_SEEINGS")
                                                                {
                                                                    DataTable dtSiteSeeing_temp = dtCity;
                                                                    dtSiteSeeing_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objPackageCity.City_id + "'";
                                                                    DataTable dtSiteSeeing = dtSiteSeeing_temp.DefaultView.ToTable();

                                                                    if (dtSiteSeeing != null && dtSiteSeeing.Rows.Count > 0)
                                                                    {
                                                                        objCitySiteSeeingList = new List<Entity.SiteSeeing>();
                                                                        foreach (DataRow dsite in dtSiteSeeing.Rows)
                                                                        {
                                                                            Entity.SiteSeeing objCitySiteSeeing = new Entity.SiteSeeing();
                                                                            objCitySiteSeeing.Key = Convert.ToInt32(dsite["key"]);
                                                                            objCitySiteSeeing.City_site_seeing_id = Convert.ToInt64(dsite["City_site_seeing_id"]);
                                                                            //objCitySiteSeeing.PPackage_quotation_siteseeing_id = Convert.ToInt64(dsite["PPackage_quotation_siteseeing_id"]);
                                                                            objCitySiteSeeing.Site = Convert.ToString(dsite["Site"]);
                                                                            objCitySiteSeeing.Rate = Convert.ToString(dsite["Rate"]);
                                                                            objCitySiteSeeingList.Add(objCitySiteSeeing);
                                                                            objCitySiteSeeing = null;
                                                                        }
                                                                        objPackageCity.SiteSeeingList = objCitySiteSeeingList;
                                                                        objPackageCity.SelectedSiteSeeingList = new List<SiteSeeing>();//This is for empty array purpos                                                 
                                                                    }
                                                                }
                                                                else if (TableName == "PACKAGE_CITY_HOTEL_MEAL_PLANS")
                                                                {
                                                                    DataTable dtHotel_mealplan = dtCity;
                                                                    dtHotel_mealplan.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objPackageCity.City_id + "'";
                                                                    DataTable dtMealPlans = dtHotel_mealplan.DefaultView.ToTable();
                                                                    if (dtMealPlans != null && dtMealPlans.Rows.Count > 0)
                                                                    {
                                                                        objMealPlanList = new List<Entity.HotelMealPlan>();
                                                                        foreach (DataRow dhMealPlan in dtMealPlans.Rows)
                                                                        {
                                                                            Entity.HotelMealPlan objMealPlans = new Entity.HotelMealPlan();
                                                                            objMealPlans.Hotel_id = Convert.ToInt32(dhMealPlan["Hotel_id"]);
                                                                            objMealPlans.Hotel_meal_plan_id = Convert.ToInt32(dhMealPlan["Hotel_meal_plan_id"]);
                                                                            objMealPlans.Meal_plan_code = Convert.ToString(dhMealPlan["Meal_plan_code"]);
                                                                            objMealPlans.Adult_price = Convert.ToString(dhMealPlan["Adult_price"]);
                                                                            objMealPlans.Child_price = Convert.ToString(dhMealPlan["Child_price"]);
                                                                            objMealPlans.Child_price_without_bed = Convert.ToString(dhMealPlan["Child_price_without_bed"]);
                                                                            objMealPlanList.Add(objMealPlans);
                                                                            objMealPlans = null;
                                                                        }

                                                                        // objPackageCity.HotelMealPlanList = objMealPlanList;
                                                                    }
                                                                }
                                                                else if (TableName == "PACKAGE_CITY_HOTELS_MEALPLANS_SELECTED")
                                                                {
                                                                    DataTable dtHotel_mealplanSelected = dtCity;
                                                                    dtHotel_mealplanSelected.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objPackageCity.City_id + "'";
                                                                    DataTable dtSelectedHotelMealPlans = dtHotel_mealplanSelected.DefaultView.ToTable();
                                                                    if (dtSelectedHotelMealPlans != null && dtSelectedHotelMealPlans.Rows.Count > 0)
                                                                    {
                                                                        objSelectedHotelMealPlans = new HotelMealPlan();
                                                                        objSelectedHotelMealPlans.Hotel_id = Convert.ToInt32(dtSelectedHotelMealPlans.Rows[0]["Hotel_id"]);
                                                                        objSelectedHotelMealPlans.Meal_plan_code = Convert.ToString(dtSelectedHotelMealPlans.Rows[0]["Meal_plan_code"]);

                                                                        // objCityAndNight.SelectedHotelMealPlan = objSelectedHotelMealPlans;
                                                                    }
                                                                }
                                                            }

                                                        }

                                                        //objCityAndNightsList.Add(objCityAndNight);
                                                        //objCityAndNight = null;
                                                    }

                                                    //objPackage.CityAndNightsList = objCityAndNightsList;
                                                }
                                            }
                                            else if (TableName == "PACKAGE_INCLUSIONS")
                                            {
                                                DataTable dtInclusions_temp = dt_package;
                                                dtInclusions_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                                                DataTable dtInclusions = dtInclusions_temp.DefaultView.ToTable();

                                                if (dtInclusions != null && dtInclusions.Rows.Count > 0)
                                                {
                                                    objInclusionList = new List<Entity.Inclusion>();
                                                    foreach (DataRow di in dtInclusions.Rows)
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
                                            else if (TableName == "PACKAGE_ITINERARY")
                                            {
                                                DataTable dtItinerary_temp = dt_package;
                                                dtItinerary_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                                                DataTable dtItinerary = dtItinerary_temp.DefaultView.ToTable();
                                                if (dtItinerary != null && dtItinerary.Rows.Count > 0)
                                                {
                                                    objPackageItineraryList = new List<Entity.PackageItinerary>();
                                                    foreach (DataRow diti in dtItinerary.Rows)
                                                    {
                                                        // if (objPackage.Package_id == Convert.ToInt64(di["Package_id"]))
                                                        // {
                                                        Entity.PackageItinerary objitinerary = new Entity.PackageItinerary();
                                                        objitinerary.Key = Convert.ToInt64(diti["key"]);
                                                        objitinerary.Package_itinerary_id = Convert.ToInt64(diti["Package_itinerary_id"]);
                                                        objitinerary.Day = Convert.ToInt64(diti["Day"]);
                                                        objitinerary.Itinerary_title = Convert.ToString(diti["Itinerary_title"]);
                                                        objitinerary.Itinerary_description = Convert.ToString(diti["Itinerary_description"]);
                                                        objPackageItineraryList.Add(objitinerary);
                                                        objitinerary = null;
                                                        //}
                                                    }
                                                    objPackage.Package_itinerary_list = objPackageItineraryList;
                                                }
                                            }
                                            else if (TableName == "PACKAGE_IMAGES")
                                            {
                                                DataTable dtImagelist_temp = dt_package;
                                                dtImagelist_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                                                DataTable dtImages = dtImagelist_temp.DefaultView.ToTable();
                                                if (dtImages != null && dtImages.Rows.Count > 0)
                                                {
                                                    objPackageImagesList = new List<Entity.PackageImages>();
                                                    foreach (DataRow dimg in dtImages.Rows)
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

                                        }
                                    }


                                    objPackageList.Add(objPackage);
                                }


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
            return objPackageList;
        }


        public List<Entity.PackageQuotation> GetPackageQuotationList(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "")
        {

            System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;

            List<Entity.PackageQuotation> objPackageQuotationList = null;  //Parent
            List<Entity.PackageCity> objPackageCityList = null;  //Child
            List<Entity.SiteSeeing> objCitySiteSeeingList = null; //Child of PackageCity       
            List<Entity.Inclusion> objInclusionList = null; //Child
            List<Entity.PackageImages> objPackageImagesList = null;
            List<Entity.PackageItinerary> objPackageItineraryList = null;
            TransportRate objSelectedTranpsort = null;


            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetPackageQuotationList]");
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

                        //using (DataTable distinctPackage = view.ToTable(true, "Package_id", "Package_name", "Package_price_before_discount", "Valid_from", "Valid_to", "Package_type", "Package_description", "Package_commision", "Package_price", "Is_best_selling", "Is_family_package", "Is_honeymoon_package", "Transport_rate_name", "Transport_rate_id", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
                        //{
                            objPackageQuotationList = new List<PackageQuotation>();
                            PackageQuotation objPackageQuotation = null;

                            //Package Starts ======================
                            for (int counter = 0; counter < dt_package.Rows.Count; counter++)
                            {
                                objPackageQuotation = new PackageQuotation();
                                objPackageQuotation.Key = counter;
                                objPackageQuotation.Package_id = Convert.ToInt32(dt_package.Rows[counter]["Package_id"]);
                                objPackageQuotation.Quotation_id = Convert.ToString(dt_package.Rows[counter]["Quotation_id"]);
                                objPackageQuotation.Package_price = Convert.ToInt32(dt_package.Rows[counter]["Package_price"]);
                                objPackageQuotation.Package_price_before_discount = Convert.ToDouble(dt_package.Rows[counter]["Package_price_before_discount"]);
                                objPackageQuotation.Package_name = Convert.ToString(dt_package.Rows[counter]["Package_name"]);
                                objPackageQuotation.Package_description = Convert.ToString(dt_package.Rows[counter]["Package_description"]);
                                objPackageQuotation.Package_commision = Convert.ToDouble(dt_package.Rows[counter]["Package_commision"]);
                                objPackageQuotation.Is_best_selling = Convert.ToBoolean(dt_package.Rows[counter]["Is_best_selling"]);
                                objPackageQuotation.Is_honeymoon_package = Convert.ToBoolean(dt_package.Rows[counter]["Is_honeymoon_package"]);
                                objPackageQuotation.Is_family_package = Convert.ToBoolean(dt_package.Rows[counter]["Is_family_package"]);
                                objPackageQuotation.Is_best_selling = Convert.ToBoolean(dt_package.Rows[counter]["Is_best_selling"]);
                                objPackageQuotation.Transport_rate_id = Convert.ToInt64(dt_package.Rows[counter]["Transport_rate_id"]);
                                objPackageQuotation.Destination_id = Convert.ToInt32(dt_package.Rows[counter]["Destination_id"]);
                                objPackageQuotation.Transport_rate_name = Convert.ToString(dt_package.Rows[counter]["Transport_rate_name"]);
                                objPackageQuotation.Destination_name = Convert.ToString(dt_package.Rows[counter]["Destination_name"]);
                                objPackageQuotation.Destination_type_id = Convert.ToInt32(dt_package.Rows[counter]["Destination_type_id"]);
                                objPackageQuotation.Is_active = Convert.ToBoolean(dt_package.Rows[counter]["Is_active"]);
                                objPackageQuotation.Row_created_date = Convert.ToDateTime(dt_package.Rows[counter]["Row_created_date"]);
                                objPackageQuotation.Row_created_by = Convert.ToString(dt_package.Rows[counter]["Row_created_by"]);
                                objPackageQuotation.Row_altered_date = Convert.ToDateTime(dt_package.Rows[counter]["Row_altered_date"]);
                                objPackageQuotation.Row_altered_by = Convert.ToString(dt_package.Rows[counter]["Row_altered_by"]);

                                objPackageQuotation.Package_reviews_rating_average = Convert.ToDecimal(dt_package.Rows[counter]["Package_reviews_rating_average"]);
                                objPackageQuotation.Package_reviews_rating_count = Convert.ToInt64(dt_package.Rows[counter]["Package_reviews_rating_count"]);
                            objPackageQuotation.PackageGuideLines = Convert.ToString(dt_package.Rows[counter]["PackageGuideLines"]);


                            //Package city Starts ======================
                            DataTable dt_package_city_temp = ds.Tables["PACKAGE_CITY_HOTEL"];

                                if (dt_package_city_temp != null && dt_package_city_temp.Rows.Count > 0)
                                {

                                    dt_package_city_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "'";
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

                                            objPackageCity.PackageCityHotelList = new List<PackageCityHotel>();
                                            PackageCityHotel objPackageCityHotel = new PackageCityHotel();
                                            objPackageCityHotel.Nights = Convert.ToInt16(dr["Nights"]);


                                            //For each loop for HOtel
                                            //Package City Hotel and MealPlans Selected Starts ================================================
                                            DataTable dt_package_city_hotel_mealplanSelected_temp = ds.Tables["PACKAGE_CITY_HOTELS_MEALPLANS_SELECTED"];

                                            if (dt_package_city_hotel_mealplanSelected_temp != null && dt_package_city_hotel_mealplanSelected_temp.Rows.Count > 0)
                                            {
                                                dt_package_city_hotel_mealplanSelected_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "' AND City_id = '" + objPackageCity.City_id + "'";
                                                DataTable dt_package_city_hotel_mealplanSelected = dt_package_city_hotel_mealplanSelected_temp.DefaultView.ToTable();
                                                if (dt_package_city_hotel_mealplanSelected != null && dt_package_city_hotel_mealplanSelected.Rows.Count > 0)
                                                {

                                                    objPackageCityHotel.Hotel = new Hotel();
                                                    objPackageCityHotel.Hotel.Hotel_id = Convert.ToInt32(dt_package_city_hotel_mealplanSelected.Rows[0]["Hotel_id"]);
                                                    objPackageCityHotel.Hotel.Hotel_name = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Hotel_name"]);

                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan = new HotelMealPlan();
                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan.Hotel_id = Convert.ToInt32(dt_package_city_hotel_mealplanSelected.Rows[0]["Hotel_id"]);
                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan.Meal_plan_code = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Meal_plan_code"]);
                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan.Meal_plan_desc = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Meal_plan_desc"]);
                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan.Adult_price = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Adult_price"]);
                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan.Child_price = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Child_price"]);
                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan.Child_price_without_bed = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Child_price_without_bed"]);
                                                }
                                            }




                                            objPackageCity.PackageCityHotelList.Add(objPackageCityHotel);

                                           //Package City Site Seeing Starts ================================================
                                           DataTable dt_package_city_site_seeing_temp = ds.Tables["PACKAGE_CITY_SITE_SEEINGS"];

                                            if (dt_package_city_site_seeing_temp != null && dt_package_city_site_seeing_temp.Rows.Count > 0)
                                            {
                                                dt_package_city_site_seeing_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "' AND City_id = '" + objPackageCity.City_id + "'";
                                                DataTable dt_package_city_site_seeing = dt_package_city_site_seeing_temp.DefaultView.ToTable();

                                                if (dt_package_city_site_seeing != null && dt_package_city_site_seeing.Rows.Count > 0)
                                                {
                                                    objCitySiteSeeingList = new List<Entity.SiteSeeing>();
                                                    foreach (DataRow dsite in dt_package_city_site_seeing.Rows)
                                                    {
                                                        Entity.SiteSeeing objCitySiteSeeing = new Entity.SiteSeeing();
                                                        objCitySiteSeeing.Key = Convert.ToInt32(dsite["key"]);
                                                        objCitySiteSeeing.City_site_seeing_id = Convert.ToInt64(dsite["City_site_seeing_id"]);
                                                        //objCitySiteSeeing.PPackage_quotation_siteseeing_id = Convert.ToInt64(dsite["PPackage_quotation_siteseeing_id"]);
                                                        objCitySiteSeeing.Site = Convert.ToString(dsite["Site"]);
                                                        objCitySiteSeeing.Rate = Convert.ToString(dsite["Rate"]);
                                                        objCitySiteSeeingList.Add(objCitySiteSeeing);
                                                        objCitySiteSeeing = null;
                                                    }
                                                    objPackageCity.SiteSeeingList = objCitySiteSeeingList;
                                                    objPackageCity.SelectedSiteSeeingList = new List<SiteSeeing>();//This is for empty array purpos                                                 
                                                }
                                            }
                                            //Package City Site Seeing Ends ================================================

                                            objPackageCityList.Add(objPackageCity);
                                        }


                                        objPackageQuotation.PackageCityList = objPackageCityList;
                                    }
                                }
                                //Package city Ends ======================


                                //Package  Transport Rates selected Starts =================
                                DataTable dt_package_transport_rate_selected_temp = ds.Tables["PACKAGE_TRANSPORT_RATE_SELECTED"];

                                if (dt_package_transport_rate_selected_temp != null && dt_package_transport_rate_selected_temp.Rows.Count > 0)
                                {
                                    dt_package_transport_rate_selected_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "'";
                                    DataTable dt_package_transport_rate_selected = dt_package_transport_rate_selected_temp.DefaultView.ToTable();
                                    if (dt_package_transport_rate_selected != null && dt_package_transport_rate_selected.Rows.Count > 0)
                                    {
                                        objSelectedTranpsort = new TransportRate();
                                        objSelectedTranpsort.Transport_id = Convert.ToInt32(dt_package_transport_rate_selected.Rows[0]["Transport_id"]);
                                        objPackageQuotation.SelectedTransport = objSelectedTranpsort;
                                    }

                                }
                                //Package  Transport Rates selected Ends =================


                                //Package Inclusions Starts =================
                                DataTable dt_package_inclusions_temp = ds.Tables["PACKAGE_INCLUSIONS"];

                                if (dt_package_inclusions_temp != null && dt_package_inclusions_temp.Rows.Count > 0)
                                {
                                    dt_package_inclusions_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "'";
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
                                        objPackageQuotation.InclusionsList = objInclusionList;
                                    }
                                }
                                //Package Inclusions End ===================


                                //Package Images Starts =================

                                DataTable dt_package_images_temp = ds.Tables["PACKAGE_IMAGES"];

                                if (dt_package_images_temp != null && dt_package_images_temp.Rows.Count > 0)
                                {
                                    dt_package_images_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "'";
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
                                        objPackageQuotation.ImageList = objPackageImagesList;
                                    }
                                }
                                //Package Images End ===================

                                //Package Itinerary Starts =================

                                DataTable dt_package_Itinerary_temp = ds.Tables["PACKAGE_ITINERARY"];

                                if (dt_package_Itinerary_temp != null && dt_package_Itinerary_temp.Rows.Count > 0)
                                {
                                    dt_package_Itinerary_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "'";
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

                                        objPackageQuotation.Package_itinerary_list = objPackageItineraryList;
                                    }
                                }
                                //Package Itinerary Ends =================


                                objPackageQuotationList.Add(objPackageQuotation);
                            }  //Loop through each package

                            //Package Ends ======================

                      //  } // End of distinctPackage

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
            return objPackageQuotationList;
        }


        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>31 Jan, 2023</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the destinations will be added in the databasae
        ///   If operation is "U" (Update) then all the destinations will be updated in the database 
        /// </summary>
        /// <param name="package_quotation_json"></param>
        /// <param name="operation"></param>
        /// <returns></returns>


        public String[] SavePackageQuotation(String package_quotation_json, String UserId)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackageQuotation]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_package_quotation_json", DbType.String, package_quotation_json);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                //mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, operation);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                mDB.AddOutParameter(sqlCommand, "@pa_out_package_quotation_id", DbType.Int64, 80000);

                mDB.ExecuteNonQuery(sqlCommand);

                arrResult[0] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));
                arrResult[1] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_package_quotation_id"));
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

        public String[] UpdateFavoritePackage(Int64 Package_id, Int64 Package_quotation_id, Boolean IsFavorite, string UserId)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            String[] arrResult = new String[1];

            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackageQuotation_is_favorite]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.Int64, Package_id);
                mDB.AddInParameter(sqlCommand, "@pa_package_quotation_id", DbType.Int64, Package_quotation_id);
                mDB.AddInParameter(sqlCommand, "@pa_is_favorite", DbType.Boolean, IsFavorite);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);

                mDB.ExecuteNonQuery(sqlCommand);

                arrResult[0] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));
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
        public String[] UpdateMarginDescriptionPackage(Int64 Package_id, Int64 Package_quotation_id, decimal Margin, String MarginDescription, string UserId)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            String[] arrResult = new String[1];
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackageQuotation_margin_details]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.Int64, Package_id);
                mDB.AddInParameter(sqlCommand, "@pa_package_quotation_id", DbType.Int64, Package_quotation_id);
                mDB.AddInParameter(sqlCommand, "@pa_margin", DbType.Decimal, Margin);
                mDB.AddInParameter(sqlCommand, "@pa_margin_description", DbType.String, MarginDescription);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                //mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, operation);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);

                mDB.ExecuteNonQuery(sqlCommand);

                arrResult[0] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));
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
        public String[] CalculateLatestPrice(String package_quotation_json, String UserId)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackageQuotation_CalculateLatestQuotation]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_package_quotation_json", DbType.String, package_quotation_json);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                //mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, operation);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                mDB.AddOutParameter(sqlCommand, "@pa_out_package_final_calculated_price", DbType.Decimal, 80000);
                mDB.ExecuteNonQuery(sqlCommand);
                arrResult[0] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status"));
                arrResult[1] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_package_final_calculated_price"));
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

