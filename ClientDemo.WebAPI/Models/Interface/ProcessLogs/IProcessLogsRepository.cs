
using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Interface
{
    public interface IProcessLogsRepository
    {
        /// <created_by>Sunil Kumar Bais</created_by>
        /// <created_date>June 30, 2023</created_date>
        /// <summary>
        ///  Get All ProcessLogList
        /// </summary>
        /// <returns> Gets List of all ProcessLogs</returns>
        List<Entity.ProcessLogs> GetProcessLogsList(DateTime fromDate,DateTime toDate);
    }
}
