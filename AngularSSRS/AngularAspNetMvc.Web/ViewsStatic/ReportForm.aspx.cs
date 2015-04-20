using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AngularAspNetMvc.Data.Models;
using AngularAspNetMvc.DataAccess.Repository;
using Microsoft.Reporting.WebForms;
using WebGrease.Css.Extensions;
using ReportParameter = Microsoft.Reporting.WebForms.ReportParameter;


namespace AngularAspNetMvc.Web.ViewsStatic
{
    public partial class ReportForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Guid requestID = new Guid(Request.QueryString["r"]);

                ReportRequest request;
                using (var repository = new ReportsRepository())
                {
                    request = repository.GetRequest(requestID);
                }

                mainReportViewer.ServerReport.ReportServerUrl =
                    new Uri(ConfigurationManager.AppSettings["ReportServerUrl"]);
                mainReportViewer.ServerReport.ReportPath =
                    string.Format(ConfigurationManager.AppSettings["ReportPath"], request.ReportFileName);
                mainReportViewer.ProcessingMode = ProcessingMode.Remote;
                mainReportViewer.ShowParameterPrompts = false;
                mainReportViewer.ShowRefreshButton = false;
                mainReportViewer.ShowWaitControlCancelLink = false;
                mainReportViewer.ShowBackButton = false;
                mainReportViewer.ShowCredentialPrompts = false;
                var parametersCollection = new List<ReportParameter>();
                foreach (var parameter in request.ReportRequestParameters)
                {
                    var parameterName = parameter.ReportParameterName;
                    if (parameterName.StartsWith("@"))
                    {
                        parameterName = parameterName.Substring(1);
                    }
                    parametersCollection.Add(new ReportParameter(parameterName, parameter.ParameterValue, false));
                }
                mainReportViewer.ServerReport.SetParameters(parametersCollection);
                using (var repository = new ReportsRepository())
                {
                    request.ReportRequestParameters.ToList().ForEach(one => repository.Delete(one));
                    repository.Delete(request);
                }
                mainReportViewer.ServerReport.Refresh();
            }
        }
    }
}