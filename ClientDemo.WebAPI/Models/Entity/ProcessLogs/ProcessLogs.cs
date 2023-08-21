using System;
using System.Collections.Generic;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public class ProcessLogs : EntityBase
    {
        public long ProcessLogId { get; set; }
        public string Severity { get; set; }
        public string Source { get; set; }
        public string Parameters { get; set; }
        public string Message { get; set; }

        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public string Username { get; set; }
        public DateTime Log_time { get; set; }
        public int Elapsed_secs { get; set; }
        public long ErrorLogId { get; set; }
        public long ErrorNumber { get; set; }
        public long Rows_affected { get; set; }
        public long ErrorLine { get; set; }
        public string ErrorProcedure { get; set; }    
        public string ErrorMessage { get; set; }
        public DateTime ErrorDateTime { get; set; }

    }
}
