using System;

/// <summary>
///  Entity Base: It will be inherited by each Entity
/// </summary>
namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public abstract class EntityBase
    {
        #region properties

        /// <summary>
        ///  Date on which row is created
        /// </summary>
        public DateTime? Row_created_date { get; set; }

        /// <summary>
        ///  User name by whome row is created
        /// </summary>
        public String Row_created_by { get; set; }

        /// <summary>
        /// Date on which row is altered
        /// </summary>
        public DateTime? Row_altered_date { get; set; }

        /// <summary>
        /// User name by whome row is altered
        /// </summary>
        public String Row_altered_by { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public Boolean isEditing { get; set; }

        public Boolean Is_active { get; set; }


        /// <summary>
        /// Is Active Booean value
        /// </summary>
        //public String Is_active_str
        //{
        //    get
        //    {
        //        return Is_active_str;
        //    }
        //    set
        //    {
        //        Is_active_str = System.Convert.ToString(Is_active == true ? "Y" : "N");
        //    }
        //}

        /// <summary>
        ///  Add/Edit/[Active|Inactive] Operations
        /// </summary>
        public String Operation { get; set; }

        //public Int32? Company_code { get; set; }
        //public String Company_name { get; set; }

        public Int64 Key { get; set; }

        #endregion

    }

}
