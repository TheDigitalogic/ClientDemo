using TravelNinjaz.B2B.WebAPI.Models.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using TravelNinjaz.B2B.WebAPI.Models.Entity;
namespace TravelNinjaz.B2B.WebAPI.Models.Repository
{
    public class PackageQuotationUserFavoriteRepository : IPackageQuotationUserFavoriteRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private Microsoft.Practices.EnterpriseLibrary.Data.Database mDB;
        private System.Data.Common.DbConnection mConnection;
        #endregion
        #region "Constructor"
        public PackageQuotationUserFavoriteRepository(IConfiguration configuration)
        {

            mConnectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        #region "Desination CRUD Functions"

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>10 Feb, 2023</created_date>
        /// <summary>
        /// Get Package Check price user favorite List
        /// </summary>
        /// <returns>list of Package Check price userfavorite list</returns>
        public List<Entity.PackageQuotation> GetPackageQuotationUserFavoriteList_OLD(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_quotation_id = 0)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;
            DataTable dt = null;
            String TableName = "";
            List<Entity.PackageQuotation> objPackageList = null;  //Parent
            List<Entity.CityAndNights> objCityAndNightsList = null;  //Child
            List<Entity.SiteSeeing> objCitySiteSeeingList = null; //GrandChild
            List<Entity.Hotel> objHotelList = null;
            List<Entity.Inclusion> objInclusionList = null; //Child
            List<Entity.PackageImages> objPackageImagesList = null; //Child
            List<Entity.PackageItinerary> objPackageItineraryList = null; //Child
            List<Entity.HotelMealPlan> objMealPlanList = null;
            List<Entity.PackageCity> objPackageCityList = null;  //Child

            HotelMealPlan objSelectedHotelMealPlans = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }

                //sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_package]");

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetPackageQuotationList_UserFavorite]");
                sqlCommand.CommandTimeout = 0;
                if (destination_type_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_type_id", DbType.Int64, destination_type_id);

                if (destination_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_id", DbType.Int64, destination_id);

                if (package_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.Int64, package_id);

                if (package_quotation_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_package_quotation_id", DbType.Int64, package_quotation_id);

                mDB.AddInParameter(sqlCommand, "@pa_user_id", DbType.String, UserId);
                mDB.AddOutParameter(sqlCommand, "@pa_out_status", DbType.String, 80000);
                sqlCommand.CommandTimeout = 0;

                ds = mDB.ExecuteDataSet(sqlCommand);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    dt = ds.Tables[0];
                //    using (DataView view = new DataView(dt))
                //    {
                //        using (DataTable distinctPackage = view.ToTable(true, "Package_id", "Package_name", "PackageGuideLines", "MarginDescription", "Valid_from", "Valid_to", "Package_calculate_price", "Margin", "Package_type", "Package_check_price_id", "Package_description", "Is_favourite", "Company_name", "Phone", "Travel_date", "Lead_pasanger_name", "Number_of_cabs", "Number_of_childrens", "Number_of_kids", "Number_of_rooms", "Number_of_persons", "Email", "Flight_booked", "Drop_location_id", "Pickup_location_id", "Package_commision", "Package_price", "Is_best_selling", "Is_family_package", "Is_honeymoon_package", "Transport_rate_name", "Transport_id", "Transport_rate_id", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by"))
                //        {
                //            objPackageList = new List<PackageQuotation>();
                //            PackageQuotation objPackage = null;
                //            for (int counter = 0; counter < distinctPackage.Rows.Count; counter++)
                //            {
                //                objPackage = new PackageQuotation();
                //                objPackage.Key = counter;
                //                objPackage.Package_id = Convert.ToInt32(distinctPackage.Rows[counter]["Package_id"]);
                //                objPackage.Package_quotation_id = Convert.ToInt32(distinctPackage.Rows[counter]["package_quotation_id"]);
                //                objPackage.Package_calculate_price = Convert.ToDecimal(distinctPackage.Rows[counter]["Package_calculate_price"]);
                //                objPackage.Margin = Convert.ToDecimal(distinctPackage.Rows[counter]["Margin"]);
                //                objPackage.MarginDescription = Convert.ToString(distinctPackage.Rows[counter]["MarginDescription"]);
                //                objPackage.PackageGuideLines = Convert.ToString(distinctPackage.Rows[counter]["PackageGuideLines"]);
                //                objPackage.Company_name = Convert.ToString(distinctPackage.Rows[counter]["Company_name"]);
                //                objPackage.Package_description = Convert.ToString(distinctPackage.Rows[counter]["Package_description"]);
                //                objPackage.Drop_location_id = Convert.ToInt32(distinctPackage.Rows[counter]["Drop_location_id"]);
                //                objPackage.Pickup_location_id = Convert.ToInt32(distinctPackage.Rows[counter]["Pickup_location_id"]);
                //                objPackage.Email = Convert.ToString(distinctPackage.Rows[counter]["Email"]);
                //                objPackage.Flight_booked = Convert.ToBoolean(distinctPackage.Rows[counter]["Flight_booked"]);
                //                objPackage.Lead_pasanger_name = Convert.ToString(distinctPackage.Rows[counter]["Lead_pasanger_name"]);
                //                objPackage.Number_of_cabs = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_cabs"]);
                //                objPackage.Number_of_rooms = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_rooms"]);
                //                objPackage.Number_of_persons = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_persons"]);
                //                objPackage.Phone = Convert.ToString(distinctPackage.Rows[counter]["Phone"]);
                //                objPackage.Travel_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Travel_date"]);
                //                objPackage.Number_of_childrens = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_childrens"]);
                //                objPackage.Number_of_kids = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_kids"]);
                //                objPackage.Transport_id = Convert.ToInt32(distinctPackage.Rows[counter]["Transport_id"]);
                //                objPackage.Is_favourite = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_favourite"]);
                //                objPackage.Package_price = Convert.ToInt32(distinctPackage.Rows[counter]["Package_price"]);
                //                objPackage.Package_name = Convert.ToString(distinctPackage.Rows[counter]["Package_name"]);
                //                objPackage.Package_commision = Convert.ToDouble(distinctPackage.Rows[counter]["Package_commision"]);
                //                objPackage.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
                //                objPackage.Is_honeymoon_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_honeymoon_package"]);
                //                objPackage.Is_family_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_family_package"]);
                //                objPackage.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
                //                objPackage.Transport_rate_id = Convert.ToInt64(distinctPackage.Rows[counter]["Transport_rate_id"]);
                //                objPackage.Destination_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_id"]);
                //                objPackage.Transport_rate_name = Convert.ToString(distinctPackage.Rows[counter]["Transport_rate_name"]);
                //                objPackage.Destination_name = Convert.ToString(distinctPackage.Rows[counter]["Destination_name"]);
                //                objPackage.Destination_type_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_type_id"]);
                //                objPackage.Is_active = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_active"]);
                //                objPackage.Row_created_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_created_date"]);
                //                objPackage.Row_created_by = Convert.ToString(distinctPackage.Rows[counter]["Row_created_by"]);
                //                objPackage.Row_altered_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_altered_date"]);
                //                objPackage.Row_altered_by = Convert.ToString(distinctPackage.Rows[counter]["Row_altered_by"]);

                //                //Loop through Each tables
                //                foreach (DataTable dt_package in ds.Tables)
                //                {
                //                    if (dt_package != null && dt_package.Rows.Count > 0)
                //                    {
                //                        TableName = dt_package.Rows[0]["TableName"].ToString();


                //                        if (TableName == "PACKAGE_CITY")
                //                        {
                //                            DataTable dtPackageCity_temp = dt_package;
                //                            dtPackageCity_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                //                            DataTable dtPackageCity = dtPackageCity_temp.DefaultView.ToTable();


                //                            if (dtPackageCity != null && dtPackageCity.Rows.Count > 0)
                //                            {
                //                                objCityAndNightsList = new List<Entity.CityAndNights>();
                //                                foreach (DataRow dr in dtPackageCity.Rows)
                //                                {
                //                                    objPackageCityList = new List<Entity.PackageCity>();
                //                                    Entity.PackageCity objPackageCity = new Entity.PackageCity();
                //                                    objPackageCity.Package_city_id = Convert.ToInt64(dr["Package_city_id"]);
                //                                    objPackageCity.Key = Convert.ToInt32(dr["key"]);
                //                                    objPackageCity.City_id = Convert.ToInt32(dr["City_id"]);
                //                                    objPackageCity.City_name = Convert.ToString(dr["City_name"]);
                //                                    objPackageCity.Order_no = Convert.ToInt32(dr["Order_no"]);

                //                                    //Loop through Each tables
                //                                    foreach (DataTable dt_city in ds.Tables)
                //                                    {
                //                                        if (dt_city != null && dt_city.Rows.Count > 0)
                //                                        {

                //                                            TableName = dt_city.Rows[0]["TableName"].ToString();
                //                                            if (TableName == "PACKAGE_CITY_SITE_SEEINGS")
                //                                            {
                //                                                DataTable dtSiteSeeing_temp = dt_city;
                //                                                dtSiteSeeing_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objCityAndNight.City_id + "' AND City_nights_id = '" + objCityAndNight.City_nights_id + "'";
                //                                                DataTable dtSiteSeeing = dtSiteSeeing_temp.DefaultView.ToTable();
                //                                                if (dtSiteSeeing != null && dtSiteSeeing.Rows.Count > 0)
                //                                                {
                //                                                    objCitySiteSeeingList = new List<Entity.SiteSeeing>();
                //                                                    foreach (DataRow dsite in dtSiteSeeing.Rows)
                //                                                    {
                //                                                        Entity.SiteSeeing objCitySiteSeeing = new Entity.SiteSeeing();
                //                                                        objCitySiteSeeing.Key = Convert.ToInt32(dsite["key"]);
                //                                                        objCitySiteSeeing.City_site_seeing_id = Convert.ToInt64(dsite["City_site_seeing_id"]);
                //                                                        objCitySiteSeeing.Package_quotation_siteseeing_id = Convert.ToInt64(dsite["package_quotation_siteseeing_id"]);
                //                                                        objCitySiteSeeing.Site = Convert.ToString(dsite["Site"]);
                //                                                        objCitySiteSeeing.Rate = Convert.ToString(dsite["Rate"]);
                //                                                        objCitySiteSeeing.Is_selected = Convert.ToBoolean(dsite["Is_selected"]);
                //                                                        objCitySiteSeeingList.Add(objCitySiteSeeing);
                //                                                        objCitySiteSeeing = null;
                //                                                    }
                //                                                    objCityAndNight.SiteSeeingList = objCitySiteSeeingList;
                //                                                    objCityAndNight.SelectedSiteSeeingList = new String[] { };
                //                                                }
                //                                            }

                //                                            else if (TableName == "PACKAGE_CITY_HOTELS")
                //                                            {
                //                                                DataTable dtHotel_temp = dt_city;
                //                                                dtHotel_temp.DefaultView.RowFilter = "package_quotation_id = '" + objPackage.Package_quotation_id + "' AND City_id = '" + objCityAndNight.City_id + "'";
                //                                                DataTable dtHotel = dtHotel_temp.DefaultView.ToTable();
                //                                                if (dtHotel != null && dtHotel.Rows.Count > 0)
                //                                                {
                //                                                    objHotelList = new List<Entity.Hotel>();
                //                                                    foreach (DataRow dhotel in dtHotel.Rows)
                //                                                    {
                //                                                        Entity.Hotel objHotel = new Entity.Hotel();
                //                                                        objHotel.Hotel_id = Convert.ToInt64(dhotel["Hotel_id"]);
                //                                                        objHotel.Hotel_name = Convert.ToString(dhotel["Hotel_name"]);
                //                                                        objHotel.Hotel_type = Convert.ToInt32(dhotel["Hotel_type"]);
                //                                                        objHotelList.Add(objHotel);
                //                                                        objHotel = null;
                //                                                    }
                //                                                    objCityAndNight.HotelList = objHotelList;
                //                                                }
                //                                            }
                //                                            else if (TableName == "PACKAGE_HOTEL_MEAL_PLANS")
                //                                            {
                //                                                DataTable dtHotel_mealplan = dt_city;
                //                                                dtHotel_mealplan.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objCityAndNight.City_id + "'";
                //                                                DataTable dtMealPlans = dtHotel_mealplan.DefaultView.ToTable();
                //                                                if (dtMealPlans != null && dtMealPlans.Rows.Count > 0)
                //                                                {
                //                                                    objMealPlanList = new List<Entity.HotelMealPlan>();
                //                                                    foreach (DataRow dhMealPlan in dtMealPlans.Rows)
                //                                                    {
                //                                                        Entity.HotelMealPlan objMealPlans = new Entity.HotelMealPlan();
                //                                                        objMealPlans.Hotel_id = Convert.ToInt32(dhMealPlan["Hotel_id"]);
                //                                                        objMealPlans.Hotel_meal_plan_id = Convert.ToInt32(dhMealPlan["Hotel_meal_plan_id"]);
                //                                                        objMealPlans.Meal_plan_code = Convert.ToString(dhMealPlan["Meal_plan_code"]);
                //                                                        objMealPlans.Adult_price = Convert.ToString(dhMealPlan["Adult_price"]);
                //                                                        objMealPlans.Child_price = Convert.ToString(dhMealPlan["Child_price"]);
                //                                                        objMealPlans.Child_price_without_bed = Convert.ToString(dhMealPlan["Child_price_without_bed"]);
                //                                                        objMealPlanList.Add(objMealPlans);
                //                                                        objMealPlans = null;
                //                                                    }
                //                                                    objCityAndNight.MealPlanList = objMealPlanList;
                //                                                }
                //                                            }
                //                                            else if (TableName == "PACKAGE_CITY_HOTELS_MEALPLANS_SELECTED")
                //                                            {
                //                                                DataTable dtHotel_mealplanSelected = dt_city;
                //                                                dtHotel_mealplanSelected.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "' AND City_id = '" + objCityAndNight.City_id + "' AND City_nights_id = '" + objCityAndNight.City_nights_id + "'";
                //                                                DataTable dtSelectedHotelMealPlans = dtHotel_mealplanSelected.DefaultView.ToTable();
                //                                                if (dtSelectedHotelMealPlans != null && dtSelectedHotelMealPlans.Rows.Count > 0)
                //                                                {
                //                                                    objSelectedHotelMealPlans = new HotelMealPlan();
                //                                                    objSelectedHotelMealPlans.Hotel_id = Convert.ToInt32(dtSelectedHotelMealPlans.Rows[0]["Hotel_id"]);
                //                                                    objSelectedHotelMealPlans.Meal_plan_code = Convert.ToString(dtSelectedHotelMealPlans.Rows[0]["Meal_plan_code"]);
                //                                                    objSelectedHotelMealPlans.Package_city_id = Convert.ToInt32(dtSelectedHotelMealPlans.Rows[0]["Package_city_id"]);
                //                                                    objCityAndNight.SelectedHotelMealPlan = objSelectedHotelMealPlans;
                //                                                }

                //                                            }

                //                                        }

                //                                    }
                //                                    objCityAndNightsList.Add(objCityAndNight);

                //                                    objCityAndNight = null;
                //                                }
                //                                //objPackage.CityAndNightsList = objCityAndNightsList;
                //                            }
                //                        }
                //                        else if (TableName == "PACKAGE_INCLUSIONS")
                //                        {
                //                            DataTable dtInclusions_temp = dt_package;
                //                            dtInclusions_temp.DefaultView.RowFilter = "package_quotation_id = '" + objPackage.Package_quotation_id + "'";
                //                            DataTable dtInclusions = dtInclusions_temp.DefaultView.ToTable();

                //                            if (dtInclusions != null && dtInclusions.Rows.Count > 0)
                //                            {
                //                                objInclusionList = new List<Entity.Inclusion>();
                //                                foreach (DataRow di in dtInclusions.Rows)
                //                                {
                //                                    // if (objPackage.Package_id == Convert.ToInt64(di["Package_id"]))
                //                                    // {
                //                                    Entity.Inclusion objinclusion = new Entity.Inclusion();
                //                                    objinclusion.Inclusions_id = Convert.ToInt64(di["Inclusions_id"]);
                //                                    objinclusion.Key = Convert.ToInt64(di["key"]);
                //                                    objinclusion.Inclusions = Convert.ToString(di["Inclusions"]);
                //                                    objinclusion.Is_include = Convert.ToBoolean(di["Is_include"]);
                //                                    objInclusionList.Add(objinclusion);
                //                                    objinclusion = null;
                //                                    //}
                //                                }
                //                                objPackage.InclusionsList = objInclusionList;
                //                            }
                //                        }
                //                        else if (TableName == "PACKAGE_ITINERARY")
                //                        {
                //                            DataTable dtItinerary_temp = dt_package;
                //                            dtItinerary_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                //                            DataTable dtItinerary = dtItinerary_temp.DefaultView.ToTable();
                //                            if (dtItinerary != null && dtItinerary.Rows.Count > 0)
                //                            {
                //                                objPackageItineraryList = new List<Entity.PackageItinerary>();
                //                                foreach (DataRow diti in dtItinerary.Rows)
                //                                {
                //                                    // if (objPackage.Package_id == Convert.ToInt64(di["Package_id"]))
                //                                    // {
                //                                    Entity.PackageItinerary objitinerary = new Entity.PackageItinerary();
                //                                    objitinerary.Key = Convert.ToInt64(diti["key"]);
                //                                    objitinerary.Package_itinerary_id = Convert.ToInt64(diti["Package_itinerary_id"]);
                //                                    objitinerary.Day = Convert.ToInt64(diti["Day"]);
                //                                    objitinerary.Itinerary_title = Convert.ToString(diti["Itinerary_title"]);
                //                                    objitinerary.Itinerary_description = Convert.ToString(diti["Itinerary_description"]);
                //                                    objPackageItineraryList.Add(objitinerary);
                //                                    objitinerary = null;
                //                                    //}
                //                                }
                //                                objPackage.Package_itinerary_list = objPackageItineraryList;
                //                            }
                //                        }
                //                        else if (TableName == "PACKAGE_IMAGES")
                //                        {
                //                            DataTable dtImagelist_temp = dt_package;
                //                            dtImagelist_temp.DefaultView.RowFilter = "Package_id = '" + objPackage.Package_id + "'";
                //                            DataTable dtImages = dtImagelist_temp.DefaultView.ToTable();
                //                            if (dtImages != null && dtImages.Rows.Count > 0)
                //                            {
                //                                objPackageImagesList = new List<Entity.PackageImages>();
                //                                foreach (DataRow dimg in dtImages.Rows)
                //                                {
                //                                    // if (objPackage.Package_id == Convert.ToInt64(di["Package_id"]))
                //                                    // {
                //                                    Entity.PackageImages objImages = new Entity.PackageImages();
                //                                    objImages.Package_id = Convert.ToInt32(dimg["Package_id"]);
                //                                    objImages.Package_image_id = Convert.ToInt64(dimg["Package_image_id"]);
                //                                    objImages.Key = Convert.ToInt64(dimg["key"]);
                //                                    objImages.Image_name = Convert.ToString(dimg["Image_name"]);
                //                                    objImages.Is_primary = Convert.ToBoolean(dimg["Is_primary"]);
                //                                    objImages.Is_active = Convert.ToBoolean(dimg["Is_active"]);
                //                                    objImages.Row_created_date = Convert.ToDateTime(dimg["Row_created_date"]);
                //                                    objImages.Row_created_by = Convert.ToString(dimg["Row_created_by"]);
                //                                    objImages.Row_altered_date = Convert.ToDateTime(dimg["Row_altered_date"]);
                //                                    objImages.Row_altered_by = Convert.ToString(dimg["Row_altered_by"]);
                //                                    objPackageImagesList.Add(objImages);
                //                                    objImages = null;
                //                                    //}
                //                                }
                //                                objPackage.ImageList = objPackageImagesList;
                //                            }
                //                        }
                //                    }
                //                }

                //                objPackageList.Add(objPackage);
                //            }

                //        }
                //    }
                //}

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


        /// <created_by>Manisha Tripathi</created_by>
        /// <created_date>10 Jul, 2023</created_date>
        /// <summary>
        ///  Changed Package check Price to Package Quotation
        ///  Get list of all User's Favorite Package Quotation
        /// </summary>
        /// <returns>list of Package Check price userfavorite list</returns>
        public List<Entity.PackageQuotation> GetPackageQuotationUserFavoriteList(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_quotation_id = 0)
        {
            System.Data.Common.DbCommand sqlCommand = null;
            DataSet ds = null;
           
            List<Entity.PackageQuotation> objPackageQuotationList = null;  //Parent
            List<Entity.PackageCity> objPackageCityList = null;  //Child
            List<Entity.PackageCityHotel> objPackageCityHotelList = null;  //Child of PackageCity

            List<Entity.SiteSeeing> objCitySiteSeeingList = null; //GrandChild
            List<Entity.Hotel> objHotelList = null;
            List<Entity.Inclusion> objInclusionList = null; //Child
            List<Entity.PackageImages> objPackageImagesList = null; //Child
            List<Entity.PackageItinerary> objPackageItineraryList = null; //Child
            List<Entity.HotelMealPlan> objMealPlanList = null;

            HotelMealPlan objSelectedHotelMealPlans = null;
            try
            {
                if (mDB == null)
                {
                    mDB = new SqlDatabase(mConnectionString);
                    mConnection = mDB.CreateConnection();
                }
            
                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetPackageQuotationList_UserFavorite]");
                sqlCommand.CommandTimeout = 0;
                if (destination_type_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_type_id", DbType.Int64, destination_type_id);

                if (destination_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_id", DbType.Int64, destination_id);

                if (package_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.Int64, package_id);

                if (package_quotation_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_package_quotation_id", DbType.Int64, package_quotation_id);

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
                        using (DataTable distinctPackage = view.ToTable(true, "Package_id", "Package_name", "package_quotation_id", "quotation_id", "Package_description", "Is_favourite", "PackageGuideLines", "MarginDescription", "Package_calculate_price", "Margin",  "Company_name", "Phone", "Travel_date", "Lead_pasanger_name", "Number_of_cabs", "Number_of_childrens", "Number_of_kids", "Number_of_rooms", "Number_of_persons", "Email", "Flight_booked", "Drop_location_id", "Pickup_location_id", "Package_commision", "Package_price", "Is_best_selling", "Is_family_package", "Is_honeymoon_package", "Transport_rate_name", "Transport_id", "Transport_rate_id", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by", "Package_reviews_rating_average", "Package_reviews_rating_count"))
                        {
                            objPackageQuotationList = new List<PackageQuotation>();
                            PackageQuotation objPackageQuotation = null;

                            //Package Starts ======================

                            for (int counter = 0; counter < distinctPackage.Rows.Count; counter++)
                            {
                                objPackageQuotation = new PackageQuotation();
                                objPackageQuotation.Key = counter;
                                objPackageQuotation.Package_id = Convert.ToInt32(distinctPackage.Rows[counter]["Package_id"]);
                                objPackageQuotation.Package_quotation_id = Convert.ToInt32(distinctPackage.Rows[counter]["package_quotation_id"]);
                                objPackageQuotation.Quotation_id = Convert.ToString(distinctPackage.Rows[counter]["Quotation_id"]);
                                objPackageQuotation.Package_calculate_price = Convert.ToDecimal(distinctPackage.Rows[counter]["Package_calculate_price"]);
                                objPackageQuotation.Margin = Convert.ToDecimal(distinctPackage.Rows[counter]["Margin"]);
                                objPackageQuotation.MarginDescription = Convert.ToString(distinctPackage.Rows[counter]["MarginDescription"]);
                                objPackageQuotation.PackageGuideLines = Convert.ToString(distinctPackage.Rows[counter]["PackageGuideLines"]);
                                objPackageQuotation.Company_name = Convert.ToString(distinctPackage.Rows[counter]["Company_name"]);
                                objPackageQuotation.Package_description = Convert.ToString(distinctPackage.Rows[counter]["Package_description"]);
                                objPackageQuotation.Drop_location_id = Convert.ToInt32(distinctPackage.Rows[counter]["Drop_location_id"]);
                                objPackageQuotation.Pickup_location_id = Convert.ToInt32(distinctPackage.Rows[counter]["Pickup_location_id"]);
                                objPackageQuotation.Email = Convert.ToString(distinctPackage.Rows[counter]["Email"]);
                                objPackageQuotation.Flight_booked = Convert.ToBoolean(distinctPackage.Rows[counter]["Flight_booked"]);
                                objPackageQuotation.Lead_pasanger_name = Convert.ToString(distinctPackage.Rows[counter]["Lead_pasanger_name"]);
                                objPackageQuotation.Number_of_cabs = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_cabs"]);
                                objPackageQuotation.Number_of_rooms = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_rooms"]);
                                objPackageQuotation.Number_of_persons = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_persons"]);
                                objPackageQuotation.Phone = Convert.ToString(distinctPackage.Rows[counter]["Phone"]);
                                objPackageQuotation.Travel_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Travel_date"]);
                                objPackageQuotation.Number_of_childrens = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_childrens"]);
                                objPackageQuotation.Number_of_kids = Convert.ToInt32(distinctPackage.Rows[counter]["Number_of_kids"]);
                                objPackageQuotation.Transport_id = Convert.ToInt32(distinctPackage.Rows[counter]["Transport_id"]);
                                objPackageQuotation.Is_favourite = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_favourite"]);
                                objPackageQuotation.Package_price = Convert.ToInt32(distinctPackage.Rows[counter]["Package_price"]);
                                objPackageQuotation.Package_name = Convert.ToString(distinctPackage.Rows[counter]["Package_name"]);
                                objPackageQuotation.Package_commision = Convert.ToDouble(distinctPackage.Rows[counter]["Package_commision"]);
                                objPackageQuotation.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
                                objPackageQuotation.Is_honeymoon_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_honeymoon_package"]);
                                objPackageQuotation.Is_family_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_family_package"]);
                                objPackageQuotation.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
                                objPackageQuotation.Transport_rate_id = Convert.ToInt64(distinctPackage.Rows[counter]["Transport_rate_id"]);
                                objPackageQuotation.Destination_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_id"]);
                                objPackageQuotation.Transport_rate_name = Convert.ToString(distinctPackage.Rows[counter]["Transport_rate_name"]);
                                objPackageQuotation.Destination_name = Convert.ToString(distinctPackage.Rows[counter]["Destination_name"]);
                                objPackageQuotation.Destination_type_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_type_id"]);
                                objPackageQuotation.Is_active = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_active"]);
                                objPackageQuotation.Row_created_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_created_date"]);
                                objPackageQuotation.Row_created_by = Convert.ToString(distinctPackage.Rows[counter]["Row_created_by"]);
                                objPackageQuotation.Row_altered_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_altered_date"]);
                                objPackageQuotation.Row_altered_by = Convert.ToString(distinctPackage.Rows[counter]["Row_altered_by"]);

                                objPackageQuotation.Package_reviews_rating_average = Convert.ToDecimal(dt_package.Rows[counter]["Package_reviews_rating_average"]);
                                objPackageQuotation.Package_reviews_rating_count = Convert.ToInt64(dt_package.Rows[counter]["Package_reviews_rating_count"]);


                                //Package city Starts ======================
                                DataTable dt_package_city_temp = ds.Tables["PACKAGE_QUOTATION_CITY"];

                                if (dt_package_city_temp != null && dt_package_city_temp.Rows.Count > 0)
                                {
                                    dt_package_city_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "' AND Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "'";
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
                                            DataTable dt_package_city_hotel_temp = ds.Tables["PACKAGE_QUOTATION_CITY_HOTELS"];

                                            if (dt_package_city_hotel_temp != null && dt_package_city_hotel_temp.Rows.Count > 0)
                                            {

                                                //dt_package_city_hotel_temp.DefaultView.RowFilter = "City_id = '" + objPackageCity.City_id + "'";

                                                dt_package_city_hotel_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "' AND Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "' AND City_id = '" + objPackageCity.City_id + "'";
                                                DataTable dt_package_city_hotel = dt_package_city_hotel_temp.DefaultView.ToTable();

                                                if (dt_package_city_hotel != null && dt_package_city_hotel.Rows.Count > 0)
                                                {
                                                    objHotelList = new List<Entity.Hotel>();
                                                    objPackageCityHotelList = new List<PackageCityHotel>();
                                                    foreach (DataRow dhotel in dt_package_city_hotel.Rows)
                                                    {

                                                        Entity.PackageCityHotel objPackageCityHotel = new Entity.PackageCityHotel();
                                                        objPackageCityHotel.Hotel = new Hotel();
                                                        objPackageCityHotel.Hotel.Hotel_id = Convert.ToInt64(dhotel["Hotel_id"]);
                                                        objPackageCityHotel.Hotel.Hotel_name = Convert.ToString(dhotel["Hotel_name"]);
                                                        objPackageCityHotel.Hotel.Hotel_type = Convert.ToInt32(dhotel["Hotel_type"]);


                                                        //Package City Hotel and MealPlans Selected Starts ================================================
                                                        DataTable dt_package_city_hotel_mealplan_selected_temp = ds.Tables["PACKAGE_QUOTATION_CITY_HOTEL_MEAL_PLAN_SELECTED"];

                                                        if (dt_package_city_hotel_mealplan_selected_temp != null && dt_package_city_hotel_mealplan_selected_temp.Rows.Count > 0)
                                                        {
                                                            dt_package_city_hotel_mealplan_selected_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "' AND City_id = '" + objPackageCity.City_id + "' AND Hotel_id = '" + objPackageCityHotel.Hotel.Hotel_id + "'";
                                                            DataTable dt_package_city_hotel_mealplanSelected = dt_package_city_hotel_mealplan_selected_temp.DefaultView.ToTable();
                                                            if (dt_package_city_hotel_mealplanSelected != null && dt_package_city_hotel_mealplanSelected.Rows.Count > 0)
                                                            {
                                                                objPackageCityHotel.Nights = Convert.ToInt16(dt_package_city_hotel_mealplanSelected.Rows[0]["Nights"]);
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
                                                    }
                                                    objPackageCity.PackageCityHotelList = objPackageCityHotelList;
                                                }

                                            }
                                            //Package City Hotel Ends ================================================

                                            //For each Package City, get the list of Package City Site seeings

                                            //Package City Site Seeing Starts ================================================
                                            DataTable dt_package_city_site_seeing_temp = ds.Tables["PACKAGE_QUOTATION_CITY_SITESEEING"];

                                            if (dt_package_city_site_seeing_temp != null && dt_package_city_site_seeing_temp.Rows.Count > 0)
                                            {
                                                dt_package_city_site_seeing_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "' AND Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "' AND City_id = '" + objPackageCity.City_id + "'";
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
                                //DataTable dt_package_transport_rate_selected_temp = ds.Tables["PACKAGE_TRANSPORT_RATE_SELECTED"];

                                //if (dt_package_transport_rate_selected_temp != null && dt_package_transport_rate_selected_temp.Rows.Count > 0)
                                //{
                                //    dt_package_transport_rate_selected_temp.DefaultView.RowFilter = "Package_id = '" + objPackageQuotation.Package_id + "'";
                                //    DataTable dt_package_transport_rate_selected = dt_package_transport_rate_selected_temp.DefaultView.ToTable();
                                //    if (dt_package_transport_rate_selected != null && dt_package_transport_rate_selected.Rows.Count > 0)
                                //    {
                                //        objSelectedTranpsort = new TransportRate();
                                //        objSelectedTranpsort.Transport_id = Convert.ToInt32(dt_package_transport_rate_selected.Rows[0]["Transport_id"]);
                                //        objPackageQuotation.SelectedTransport = objSelectedTranpsort;
                                //    }

                                //}
                                //Package  Transport Rates selected Ends =================


                                //Package Inclusions Starts =================
                                DataTable dt_package_inclusions_temp = ds.Tables["PACKAGE_QUOTATION_INCLUSIONS"];

                                if (dt_package_inclusions_temp != null && dt_package_inclusions_temp.Rows.Count > 0)
                                {
                                    dt_package_inclusions_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "'";
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

                                DataTable dt_package_images_temp = ds.Tables["PACKAGE_QUOTATION_IMAGES"];

                                if (dt_package_images_temp != null && dt_package_images_temp.Rows.Count > 0)
                                {
                                    dt_package_images_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "'";
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

                                DataTable dt_package_Itinerary_temp = ds.Tables["PACKAGE_QUOTATION_ITINERARY"];

                                if (dt_package_Itinerary_temp != null && dt_package_Itinerary_temp.Rows.Count > 0)
                                {
                                    dt_package_Itinerary_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "'";
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
                            }
                            //Loop through each package

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
            return objPackageQuotationList;
        }

        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>10 Feb, 2023</created_date>
        /// <summary>
        /// Get Package Check price user favorite List
        /// </summary>
        /// <returns>list of Package Check price userfavorite list</returns>
        public List<Entity.PackageQuotation> GetPackageQuotationListById(String UserId = "", Int64 destination_type_id = 0, Int64 destination_id = 0, Int64 package_id = 0, Int64 package_quotation_id = 0)
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

                //sqlCommand = mDB.GetSqlStringCommand("Select * From [vw_package]");

                sqlCommand = mDB.GetStoredProcCommand("[dbo].[uspGetPackageQuotationDetailsById]");
                sqlCommand.CommandTimeout = 0;
                if (destination_type_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_type_id", DbType.Int64, destination_type_id);

                if (destination_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_destination_id", DbType.Int64, destination_id);

                if (package_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_package_id", DbType.Int64, package_id);

                if (package_quotation_id != 0)
                    mDB.AddInParameter(sqlCommand, "@pa_package_quotation_id", DbType.Int64, package_quotation_id);

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

                        using (DataTable distinctPackage = view.ToTable(true, "Package_id", "Package_name", "Quotation_id", "Package_quotation_id","Package_price_before_discount", "Package_description", "Package_commision", "Package_price", "Is_best_selling", "Is_family_package", "Is_honeymoon_package", "Transport_rate_name", "Transport_rate_id", "Destination_id", "Destination_name", "Destination_type_id", "Is_active", "Row_created_date", "Row_created_by", "Row_altered_date", "Row_altered_by", "Package_reviews_rating_average", "Package_reviews_rating_count"))
                        {
                            objPackageQuotationList = new List<PackageQuotation>();
                            PackageQuotation objPackageQuotation = null;

                            //Package Starts ======================
                            for (int counter = 0; counter < distinctPackage.Rows.Count; counter++)
                            {
                                objPackageQuotation = new PackageQuotation();
                                objPackageQuotation.Key = counter;
                                objPackageQuotation.Package_id = Convert.ToInt32(distinctPackage.Rows[counter]["Package_id"]);
                                objPackageQuotation.Package_name = Convert.ToString(distinctPackage.Rows[counter]["Package_name"]);
                                objPackageQuotation.Quotation_id = Convert.ToString(distinctPackage.Rows[counter]["Quotation_id"]);
                                objPackageQuotation.Package_quotation_id = Convert.ToInt32(distinctPackage.Rows[counter]["Package_quotation_id"]);

                                objPackageQuotation.Package_price = Convert.ToInt32(distinctPackage.Rows[counter]["Package_price"]);
                                objPackageQuotation.Package_price_before_discount = Convert.ToDouble(distinctPackage.Rows[counter]["Package_price_before_discount"]);
                                objPackageQuotation.Package_description = Convert.ToString(distinctPackage.Rows[counter]["Package_description"]);
                                objPackageQuotation.Package_commision = Convert.ToDouble(distinctPackage.Rows[counter]["Package_commision"]);
                                objPackageQuotation.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
                                objPackageQuotation.Is_honeymoon_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_honeymoon_package"]);
                                objPackageQuotation.Is_family_package = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_family_package"]);
                                objPackageQuotation.Is_best_selling = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_best_selling"]);
                                objPackageQuotation.Transport_rate_id = Convert.ToInt64(distinctPackage.Rows[counter]["Transport_rate_id"]);
                                objPackageQuotation.Destination_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_id"]);
                                objPackageQuotation.Transport_rate_name = Convert.ToString(distinctPackage.Rows[counter]["Transport_rate_name"]);
                                objPackageQuotation.Destination_name = Convert.ToString(distinctPackage.Rows[counter]["Destination_name"]);
                                objPackageQuotation.Destination_type_id = Convert.ToInt32(distinctPackage.Rows[counter]["Destination_type_id"]);
                                objPackageQuotation.Is_active = Convert.ToBoolean(distinctPackage.Rows[counter]["Is_active"]);
                                objPackageQuotation.Row_created_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_created_date"]);
                                objPackageQuotation.Row_created_by = Convert.ToString(distinctPackage.Rows[counter]["Row_created_by"]);
                                objPackageQuotation.Row_altered_date = Convert.ToDateTime(distinctPackage.Rows[counter]["Row_altered_date"]);
                                objPackageQuotation.Row_altered_by = Convert.ToString(distinctPackage.Rows[counter]["Row_altered_by"]);

                                objPackageQuotation.Package_reviews_rating_average = Convert.ToDecimal(dt_package.Rows[counter]["Package_reviews_rating_average"]);
                                objPackageQuotation.Package_reviews_rating_count = Convert.ToInt64(dt_package.Rows[counter]["Package_reviews_rating_count"]);

                                //Package city Starts ======================
                                DataTable dt_package_city_temp = ds.Tables["PACKAGE_QUATATION_CITY"];

                                if (dt_package_city_temp != null && dt_package_city_temp.Rows.Count > 0)
                                {

                                    dt_package_city_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "'";
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

                                            //For each loop for Hotel
                                            //Package City Hotel and MealPlans Selected Starts ================================================
                                            DataTable dt_package_city_hotel_mealplanSelected_temp = ds.Tables["PACKAGE_QUOTATION_CITY_HOTEL_MEAL_PLAN_SELECTED"];

                                            if (dt_package_city_hotel_mealplanSelected_temp != null && dt_package_city_hotel_mealplanSelected_temp.Rows.Count > 0)
                                            {
                                                dt_package_city_hotel_mealplanSelected_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "' AND City_id = '" + objPackageCity.City_id + "'";
                                                DataTable dt_package_city_hotel_mealplanSelected = dt_package_city_hotel_mealplanSelected_temp.DefaultView.ToTable();
                                                if (dt_package_city_hotel_mealplanSelected != null && dt_package_city_hotel_mealplanSelected.Rows.Count > 0)
                                                {

                                                    objPackageCityHotel.Hotel = new Hotel();
                                                    objPackageCityHotel.Hotel.Hotel_id = Convert.ToInt32(dt_package_city_hotel_mealplanSelected.Rows[0]["Hotel_id"]);

                                                    objPackageCityHotel.Nights = Convert.ToInt16(dt_package_city_hotel_mealplanSelected.Rows[0]["Nights"]);

                                                    //objPackageCityHotel.Hotel.Hotel_name = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Hotel_name"]);

                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan = new HotelMealPlan();
                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan.Hotel_id = Convert.ToInt32(dt_package_city_hotel_mealplanSelected.Rows[0]["Hotel_id"]);
                                                    objPackageCityHotel.Hotel.SelectedHotelMealPlan.Meal_plan_code = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Meal_plan_code"]);
                                                    //objPackageCityHotel.Hotel.SelectedHotelMealPlan.Adult_price = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Adult_price"]);
                                                    //objPackageCityHotel.Hotel.SelectedHotelMealPlan.Child_price = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Child_price"]);
                                                    //objPackageCityHotel.Hotel.SelectedHotelMealPlan.Child_price_without_bed = Convert.ToString(dt_package_city_hotel_mealplanSelected.Rows[0]["Child_price_without_bed"]);
                                                }
                                            }




                                            objPackageCity.PackageCityHotelList.Add(objPackageCityHotel);

                                            //Package City Site Seeing Starts ================================================
                                            DataTable dt_package_city_site_seeing_temp = ds.Tables["PACKAGE_QUOTATION_CITY_SITESEEING"];

                                            if (dt_package_city_site_seeing_temp != null && dt_package_city_site_seeing_temp.Rows.Count > 0)
                                            {
                                                dt_package_city_site_seeing_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "' AND City_id = '" + objPackageCity.City_id + "'";
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
                                    dt_package_transport_rate_selected_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "'";
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
                                DataTable dt_package_inclusions_temp = ds.Tables["PACKAGE_QUATATION_INCLUSIONS"];

                                if (dt_package_inclusions_temp != null && dt_package_inclusions_temp.Rows.Count > 0)
                                {
                                    dt_package_inclusions_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "'";
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

                                DataTable dt_package_images_temp = ds.Tables["PACKAGE_QUOTATION_IMAGES"];

                                if (dt_package_images_temp != null && dt_package_images_temp.Rows.Count > 0)
                                {
                                    dt_package_images_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "'";
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

                                DataTable dt_package_Itinerary_temp = ds.Tables["PACKAGE_QUOTATION_ITINERARY"];

                                if (dt_package_Itinerary_temp != null && dt_package_Itinerary_temp.Rows.Count > 0)
                                {
                                    dt_package_Itinerary_temp.DefaultView.RowFilter = "Package_quotation_id = '" + objPackageQuotation.Package_quotation_id + "'";
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
            return objPackageQuotationList;
        }
        #endregion
    }
}
