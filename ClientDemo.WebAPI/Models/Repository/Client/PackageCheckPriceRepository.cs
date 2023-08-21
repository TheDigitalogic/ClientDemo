using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class PackageCheckPriceRepository:IPackageCheckPriceRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;
        #endregion

        #region "Constructor"
        public PackageCheckPriceRepository(IConfiguration configuration)
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
        public List<Entity.Package> GetPackageCheckPriceList(Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, String UserId = "")        {

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
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetPackageCheckPriceList]");
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
                            using (DataTable distinctPackage = view.ToTable(true, "Package_id", "Package_name","Package_price_before_discount", "Valid_from", "Valid_to", "Package_type", "Package_description", "Package_commision", "Package_price", "Is_best_selling", "Is_family_package", "Is_honeymoon_package", "Transport_rate_name", "Transport_rate_id", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
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

                                            else if (TableName == "PACKAGE_CITY_AND_NIGHTS")
                                            {

                                                DataTable dtCityAndNights_temp = dt_package;
                                                dtCityAndNights_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";

                                                DataTable dtCityAndNights = dtCityAndNights_temp.DefaultView.ToTable();

                                                if (dtCityAndNights != null && dtCityAndNights.Rows.Count > 0)
                                                {
                                                    objCityAndNightsList = new List<Entity.CityAndNights>();
                                                    foreach (DataRow dr in dtCityAndNights.Rows)
                                                    {
                                                        Entity.CityAndNights objCityAndNight = new Entity.CityAndNights();
                                                        objCityAndNight.Key = Convert.ToInt32(dr["key"]);
                                                        objCityAndNight.City_id = Convert.ToInt32(dr["City_id"]);
                                                        objCityAndNight.City_name = Convert.ToString(dr["City_name"]);
                                                        objCityAndNight.Nights = Convert.ToInt32(dr["Nights"]);
                                                        objCityAndNight.City_nights_id = Convert.ToInt64(dr["City_nights_id"]);
                                                        objCityAndNight.Order_no = Convert.ToInt32(dr["Order_no"]);
                                                        objCityAndNight.BlankHotelMealPlanList = new String[] { }; // This is for empty arrya hotelmealPlans
                                                        //objCityAndNight.Package_city_id = Convert.ToInt64("Package_city_id");
                                                        //Loop through Each tables
                                                        foreach (DataTable dt_city in ds.Tables)
                                                        {

                                                            if (dt_city != null && dt_city.Rows.Count > 0)
                                                            {
                                                                TableName = dt_city.Rows[0]["TableName"].ToString();
                                                                if (TableName == "PACKAGE_CITY_SITE_SEEINGS")
                                                                {
                                                                    DataTable dtSiteSeeing_temp = dt_city;
                                                                    dtSiteSeeing_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objCityAndNight.City_id + "'";
                                                                    DataTable dtSiteSeeing = dtSiteSeeing_temp.DefaultView.ToTable();

                                                                    if (dtSiteSeeing != null && dtSiteSeeing.Rows.Count > 0)
                                                                    {
                                                                        objCitySiteSeeingList = new List<Entity.SiteSeeing>();
                                                                        foreach (DataRow dsite in dtSiteSeeing.Rows)
                                                                        {
                                                                            Entity.SiteSeeing objCitySiteSeeing = new Entity.SiteSeeing();
                                                                            objCitySiteSeeing.Key = Convert.ToInt32(dsite["key"]);
                                                                            objCitySiteSeeing.City_site_seeing_id = Convert.ToInt64(dsite["City_site_seeing_id"]);
                                                                            //objCitySiteSeeing.Package_check_price_siteseeing_id = Convert.ToInt64(dsite["Package_check_price_siteseeing_id"]);
                                                                            objCitySiteSeeing.Site = Convert.ToString(dsite["Site"]);
                                                                            objCitySiteSeeing.Rate = Convert.ToString(dsite["Rate"]);
                                                                            objCitySiteSeeingList.Add(objCitySiteSeeing);
                                                                            objCitySiteSeeing = null;
                                                                        }
                                                                        objCityAndNight.SiteSeeingList = objCitySiteSeeingList;
                                                                        objCityAndNight.SelectedSiteSeeingList = new String[] { };//This is for empty array purpos                                                 
                                                                    }
                                                                }
                                                                else if (TableName == "PACKAGE_CITY_HOTELS")
                                                                {
                                                                    DataTable dtHotel_temp = dt_city;
                                                                    dtHotel_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objCityAndNight.City_id + "'";
                                                                    DataTable dtHotel = dtHotel_temp.DefaultView.ToTable();
                                                                    if (dtHotel != null && dtHotel.Rows.Count > 0)
                                                                    {
                                                                        objHotelList = new List<Entity.Hotel>();
                                                                        foreach (DataRow dhotel in dtHotel.Rows)
                                                                        {
                                                                            Entity.Hotel objHotel = new Entity.Hotel();                                                                          
                                                                            objHotel.Hotel_id = Convert.ToInt64(dhotel["Hotel_id"]);
                                                                            objHotel.Hotel_name = Convert.ToString(dhotel["Hotel_name"]);
                                                                            objHotel.Hotel_type = Convert.ToInt32(dhotel["Hotel_type"]);
                                                                            objHotelList.Add(objHotel);
                                                                            objHotel = null;
                                                                        }
                                                                        objCityAndNight.HotelList = objHotelList;
                                                                    }
                                                                }
                                                                else if (TableName == "PACKAGE_HOTEL_MEAL_PLANS")
                                                                {
                                                                    DataTable dtHotel_mealplan = dt_city;
                                                                    dtHotel_mealplan.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objCityAndNight.City_id + "'";
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
                                                                        objCityAndNight.MealPlanList = objMealPlanList;
                                                                    }
                                                                }
                                                                else if (TableName == "PACKAGE_CITY_HOTELS_MEALPLANS_SELECTED")
                                                                {
                                                                    DataTable dtHotel_mealplanSelected = dt_city;
                                                                    dtHotel_mealplanSelected.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objCityAndNight.City_id + "'";
                                                                    DataTable dtSelectedHotelMealPlans = dtHotel_mealplanSelected.DefaultView.ToTable();
                                                                    if (dtSelectedHotelMealPlans != null && dtSelectedHotelMealPlans.Rows.Count > 0)
                                                                    {
                                                                        objSelectedHotelMealPlans = new HotelMealPlan();
                                                                        objSelectedHotelMealPlans.Hotel_id = Convert.ToInt32(dtSelectedHotelMealPlans.Rows[0]["Hotel_id"]);
                                                                        objSelectedHotelMealPlans.Meal_plan_code = Convert.ToString(dtSelectedHotelMealPlans.Rows[0]["Meal_plan_code"]);
                                                                        objCityAndNight.SelectedHotelMealPlan = objSelectedHotelMealPlans;
                                                                    }

                                                                }
                                                            }
                                                        
                                                         }

                                                        objCityAndNightsList.Add(objCityAndNight);
                                                        objCityAndNight = null;
                                                    }

                                                    objPackage.CityAndNightsList = objCityAndNightsList;
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

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>19 Dec, 2022</created_date>
        /// <summary>
        /// Get  Package List
        /// </summary>
        /// <returns>list of Transport Rate Object</returns>
        //public List<Entity.PackageCheckPrice> GetPackageCheckPriceList(int type_id)
        //{
        //    System.Data.Common.DbCommand sqlCommand = null;
        //    DataSet ds = null;
        //    DataTable dt = null;

        //    List<Entity.PackageCheckPrice> objPackageCheckPriceList = null;  //Parent
        //    List<Entity.PackageCity> objPackageCheckPriceCityList = null;  //Child
        //    List<Entity.SiteSeeing> objPackageCheckPriceSiteSeeingList = null; //Child
        //    try
        //    {
        //        if (mDB == null)
        //        {
        //            mDB = new SqlDatabase(mConnectionString);
        //            mConnection = mDB.CreateConnection();
        //        }

        //        sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_package_check_price]");
        //        sqlCommand.CommandTimeout = 0;
        //        ds = mDB.ExecuteDataSet(sqlCommand);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            dt = ds.Tables[0];
        //            using (DataView view = new DataView(dt))
        //            {
        //                using (DataTable distinctPackageCheckPrice = view.ToTable(true, "Package_check_price_id", "Package_id", "Destination_id", "Destination_name", "Package_name", "Package_image", "Company_name", "Drop_location_id", "Pickup_location_id", "Email", "Flight_booked", "Lead_pasanger_name", "Number_of_cabs", "Number_of_childrens", "Number_of_persons", "Number_of_rooms", "Transport_id", "Travel_date", "Is_favourite", "Phone", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
        //                {
        //                    objPackageCheckPriceList = new List<PackageCheckPrice>();
        //                    PackageCheckPrice objPackageCheckPrice = null;
        //                    for (int counter = 0; counter < distinctPackageCheckPrice.Rows.Count; counter++)
        //                    {
        //                        objPackageCheckPrice = new PackageCheckPrice();
        //                        objPackageCheckPrice.Package_check_price_id = Convert.ToInt64(distinctPackageCheckPrice.Rows[counter]["Package_check_price_id"]);
        //                        objPackageCheckPrice.Package_id = Convert.ToInt64(distinctPackageCheckPrice.Rows[counter]["Package_id"]);
        //                        objPackageCheckPrice.Destination_id = Convert.ToInt64(distinctPackageCheckPrice.Rows[counter]["Destination_id"]);
        //                        objPackageCheckPrice.Package_name = Convert.ToString(distinctPackageCheckPrice.Rows[counter]["Package_name"]);
        //                        objPackageCheckPrice.Destination_name= Convert.ToString(distinctPackageCheckPrice.Rows[counter]["Destination_name"]);
        //                        objPackageCheckPrice.Package_image = Convert.ToString(distinctPackageCheckPrice.Rows[counter]["Package_image"]);
        //                        objPackageCheckPrice.Company_name = Convert.ToString(distinctPackageCheckPrice.Rows[counter]["Company_name"]);
        //                        objPackageCheckPrice.Drop_location_id = Convert.ToInt64(distinctPackageCheckPrice.Rows[counter]["Drop_location_id"]);
        //                        objPackageCheckPrice.Pickup_location_id = Convert.ToInt64(distinctPackageCheckPrice.Rows[counter]["Pickup_location_id"]);
        //                        objPackageCheckPrice.Email = Convert.ToString(distinctPackageCheckPrice.Rows[counter]["Email"]);
        //                        objPackageCheckPrice.Flight_booked = Convert.ToBoolean(distinctPackageCheckPrice.Rows[counter]["Flight_booked"]);
        //                        objPackageCheckPrice.Lead_pasanger_name = Convert.ToString(distinctPackageCheckPrice.Rows[counter]["Lead_pasanger_name"]);
        //                        objPackageCheckPrice.Number_of_cabs = Convert.ToInt32(distinctPackageCheckPrice.Rows[counter]["Number_of_cabs"]);
        //                        objPackageCheckPrice.Number_of_childrens = Convert.ToInt32(distinctPackageCheckPrice.Rows[counter]["Number_of_childrens"]);
        //                        objPackageCheckPrice.Number_of_persons = Convert.ToInt32(distinctPackageCheckPrice.Rows[counter]["Number_of_persons"]);
        //                        objPackageCheckPrice.Number_of_rooms = Convert.ToInt32(distinctPackageCheckPrice.Rows[counter]["Number_of_rooms"]);
        //                        objPackageCheckPrice.Transport_id = Convert.ToInt64(distinctPackageCheckPrice.Rows[counter]["Transport_id"]);
        //                        objPackageCheckPrice.Travel_date = Convert.ToDateTime(distinctPackageCheckPrice.Rows[counter]["Travel_date"]);
        //                        objPackageCheckPrice.Is_favourite = Convert.ToBoolean(distinctPackageCheckPrice.Rows[counter]["Is_favourite"]);
        //                        objPackageCheckPrice.Phone = Convert.ToString(distinctPackageCheckPrice.Rows[counter]["Phone"]);
        //                        objPackageCheckPrice.Is_active = Convert.ToBoolean(distinctPackageCheckPrice.Rows[counter]["Is_active"]);
        //                        objPackageCheckPrice.Row_created_date = Convert.ToDateTime(distinctPackageCheckPrice.Rows[counter]["Row_created_date"]);
        //                        objPackageCheckPrice.Row_created_by = Convert.ToString(distinctPackageCheckPrice.Rows[counter]["Row_created_by"]);
        //                        objPackageCheckPrice.Row_altered_date = Convert.ToDateTime(distinctPackageCheckPrice.Rows[counter]["Row_altered_date"]);
        //                        objPackageCheckPrice.Row_altered_by = Convert.ToString(distinctPackageCheckPrice.Rows[counter]["Row_altered_by"]);
        //                        dt.DefaultView.RowFilter = "Package_check_price_id = '" + objPackageCheckPrice.Package_check_price_id + "'"; ;
        //                        DataSet dscityhotel = null;
        //                        //DataTable dtcityandnights = null;
        //                        sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_package_check_price_city_hotel_mealplan]");
        //                        sqlCommand.CommandTimeout = 0;
        //                        dscityhotel = mDB.ExecuteDataSet(sqlCommand);
        //                        if (dscityhotel != null && dscityhotel.Tables.Count > 0)
        //                        {
        //                            objPackageCheckPriceCityList = new List<Entity.PackageCity>();
        //                            foreach (DataRow dr in dscityhotel.Tables[0].Rows)
        //                            {
        //                                if (objPackageCheckPrice.Package_check_price_id == Convert.ToInt64(dr["Package_check_price_id"]))
        //                                {
        //                                    Entity.PackageCity objCityAndHotel = new Entity.PackageCity();
        //                                    objCityAndHotel.Package_city_id = Convert.ToInt32(dr["Package_city_id"]);
        //                                    objCityAndHotel.Package_check_price_id = Convert.ToInt32(dr["Package_check_price_id"]);
        //                                    objCityAndHotel.Meal_plan_code = Convert.ToString(dr["Meal_plan_code"]);
        //                                    objCityAndHotel.Hotel_id = Convert.ToInt64(dr["Hotel_id"]);
        //                                    objCityAndHotel.City_id = Convert.ToInt64(dr["City_id"]);
        //                                    objCityAndHotel.Nights = Convert.ToInt32(dr["Nights"]);

        //                                    objPackageCheckPriceCityList.Add(objCityAndHotel);
        //                                    objCityAndHotel = null;
        //                                }
        //                            }
        //                        }
        //                        objPackageCheckPrice.PackageCityList = objPackageCheckPriceCityList;
        //                        //DataSet dssiteseeing = null;
        //                        ////DataTable dtinclusions = null;
        //                        //sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_city_site_seeing]");
        //                        //sqlCommand.CommandTimeout = 0;
        //                        //dssiteseeing = mDB.ExecuteDataSet(sqlCommand);
        //                        //if (dssiteseeing != null && dssiteseeing.Tables.Count > 0)
        //                        //{
        //                        //    objPackageCheckPriceSiteSeeingList = new List<Entity.SiteSeeing>();
        //                        //    foreach (DataRow di in dssiteseeing.Tables[0].Rows)
        //                        //    {
        //                        //        if (objPackageCheckPrice.Package_check_price_id == Convert.ToInt64(di["Package_check_price_id"]))
        //                        //        {
        //                        //            Entity.SiteSeeing objsiteseeing = new Entity.SiteSeeing();
        //                        //            // objinclusion.Id = Convert.ToInt32(di["key"]);
        //                        //            objsiteseeing.Package_check_price_siteseeing_id = Convert.ToInt64(di["Package_check_price_siteseeing_id"]);
        //                        //            objsiteseeing.City_site_seeing_id = Convert.ToInt64(di["City_site_seeing_id"]);
        //                        //            objsiteseeing.Package_city_id = Convert.ToInt64(di["Package_city_id"]);
        //                        //            objPackageCheckPriceSiteSeeingList.Add(objsiteseeing);
        //                        //            objsiteseeing = null;
        //                        //        }
        //                        //    }
        //                        //}
        //                        //objPackageCheckPrice.SiteSeeingList = objPackageCheckPriceSiteSeeingList;
        //                        objPackageCheckPriceList.Add(objPackageCheckPrice);
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
        //    return objPackageCheckPriceList;
        //}
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>31 Jan, 2023</created_date>
        /// <summary>
        ///   Save Destinations details. 
        ///   If operation is "A" (Add) then all the destinations will be added in the databasae
        ///   If operation is "U" (Update) then all the destinations will be updated in the database 
        /// </summary>
        /// <param name="package_check_price_json"></param>
        /// <param name="operation"></param>
        /// <returns></returns>


        public String[] SavePackageCheckPrice(String package_check_price_json, String UserId)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackageCheckPrice]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_package_check_price_json", DbType.String, package_check_price_json);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                //mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, operation);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                mDB.AddOutParameter(sqlCommand, "@pa_out_package_check_price_id", DbType.Int64, 80000);

                mDB.ExecuteNonQuery(sqlCommand);

                arrResult[0] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_status")); 
                arrResult[1] = Convert.ToString(mDB.GetParameterValue(sqlCommand, "@pa_out_package_check_price_id")); 
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
        
        public String[] UpdateFavoritePackage(Int64 Package_id, Int64 Package_check_price_id, Boolean IsFavorite, string UserId)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackageCheckPrice_is_favorite]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.Int64, Package_id);
                mDB.AddInParameter(sqlCommand, "@pa_package_check_price_id", DbType.Int64, Package_check_price_id);
                mDB.AddInParameter(sqlCommand, "@pa_is_favorite", DbType.Boolean, IsFavorite);
                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddInParameter(sqlCommand, "@pa_last_datetime", DbType.DateTime, DateTime.Now);
                //mDB.AddInParameter(sqlCommand, "@pa_operation", DbType.String, operation);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
               // mDB.AddOutParameter(sqlCommand, "@pa_out_package_check_price_id", DbType.Int64, 80000);

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
        public String[] UpdateMarginDescriptionPackage(Int64 Package_id, Int64 Package_check_price_id, decimal Margin, String MarginDescription, string UserId)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackageCheckPrice_margin_details]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.Int64, Package_id);
                mDB.AddInParameter(sqlCommand, "@pa_package_check_price_id", DbType.Int64, Package_check_price_id);
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
        public String[] CalculateLatestPrice(String package_check_price_json, String UserId)
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

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspSavePackageCheckPrice_CalculateLatestPrice]");
                sqlCommand.CommandTimeout = 0;

                mDB.AddInParameter(sqlCommand, "@pa_package_check_price_json", DbType.String, package_check_price_json);
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

