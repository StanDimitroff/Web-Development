
namespace AngularAspNetMvc.Data.Models
{
    public class ReportRequestParameter
    {

        public int ReportRequestParameterId { get; set; }

        public int ReportRequestId { get; set; }

        public string ParameterValue { get; set; }

        public string ReportParameterName { get; set; }

        public virtual ReportRequest ReportRequest { get; set; }
    }
}
