using System;
using System.Collections.Generic;


namespace AngularAspNetMvc.Models.Reports
{
    public class ReportRequest
    {
        public int ReportRequestId { get; set; }

        public int ReportId { get; set; }

        public string ReportFileName { get; set; }

        public string ReportName { get; set; }

        public string ReportDescription { get; set; }

        public Guid UniqueId { get; set; }

        public List<ReportRequestParameter> ReportRequestParameters { get; set; }
    }
}
