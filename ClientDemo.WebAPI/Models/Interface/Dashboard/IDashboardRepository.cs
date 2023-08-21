
using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IDashboardRepository
    {
        //DataTable GetDashbaord(string monthYear,string userId);
        List<Entity.DestinationDashboard> GetDashboardDestination(string monthYear, string userId);
    }
}
