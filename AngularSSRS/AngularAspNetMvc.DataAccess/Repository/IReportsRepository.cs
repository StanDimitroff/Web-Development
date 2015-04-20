using System.Collections.Generic;
using AngularAspNetMvc.Data.Models;
using AngularAspNetMvc.DataAccess.Core.Repository;

namespace AngularAspNetMvc.DataAccess.Repository
{
    public interface IReportsRepository : IWriteRepository
    {
        IEnumerable<Report> GetReports();
        Report GetReport(int reportId);
        bool CreateRequest(ReportRequest reportRequest);
    }
}