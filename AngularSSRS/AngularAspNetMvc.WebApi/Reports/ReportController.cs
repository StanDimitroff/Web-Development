
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AngularAspNetMvc.DataAccess.Repository;
using AngularAspNetMvc.Models.Core;
using AngularAspNetMvc.Models.Reports;
using AngularAspNetMvc.WebApi.Core;
using AutoMapper;

namespace AngularAspNetMvc.WebApi.Reports
{
    public class ReportController : RelayController
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportController(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        [HttpGet]
        public ApiResult<IEnumerable<Report>> Reports()
        {
            return Execute(() => _reportsRepository.GetReports().Select(Mapper.Map<Report>));
        }

        [HttpGet]
        public ApiResult<ReportRequest> Report(int reportId)
        {
            return Execute(() =>
            {
                var request = Mapper.Map<ReportRequest>(_reportsRepository.GetReport(reportId));
                request.ReportRequestParameters.ForEach(one =>
                {
                    if (one.ParameterViewName == "ActiveFlagDropDown")
                    {
                        one.ParameterValue = "1";
                    }
                });
                return request;
            });
        }


        [HttpPost]
        public ApiResult<string> CreateRequest(ReportRequest reportRequest)
        {
            return Execute(() =>
            {
                var result = Guid.NewGuid();
                reportRequest.UniqueId = result;
                _reportsRepository.CreateRequest(Mapper.Map<Data.Models.ReportRequest>(reportRequest));
                return result.ToString();
            });
        }
    }
}
