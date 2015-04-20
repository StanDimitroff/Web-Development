using System;
using System.Collections.Generic;
using System.Linq;
using AngularAspNetMvc.Data.Models;

namespace AngularAspNetMvc.DataAccess.Repository
{
    public class ReportsRepository : Core.Repository, IReportsRepository
    {
        public IEnumerable<Report> GetReports()
        {
            return Context.Reports.OrderBy(one => one.ReportName);
        }

        public Report GetReport(int reportId)
        {
            return Context.Reports.Include("ReportParameters").FirstOrDefault(one => one.ReportId == reportId);
        }

        public bool CreateRequest(ReportRequest reportRequest)
        {
            Insert(reportRequest);
            return true;
        }

        public ReportRequest GetRequest(Guid reportRequestId)
        {
            return Context.ReportRequests.Include("ReportRequestParameters")
                .FirstOrDefault(one => one.UniqueId == reportRequestId);
        }
    }
}
