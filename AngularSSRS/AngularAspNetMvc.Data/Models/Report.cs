using System.Collections.Generic;

namespace AngularAspNetMvc.Data.Models
{
    public class Report
    {
        public int ReportId { get; set; }

        public string ReportFileName { get; set; }

        public string ReportName { get; set; }

        public string ReportDescription { get; set; }

        public virtual ICollection<ReportParameter> ReportParameters { get; set; }

        public virtual ICollection<ReportRequest> ReportRequests { get; set; }
    }
}
