using System.Collections.Generic;

namespace AngularAspNetMvc.Data.Models
{
    public class ReportParameter
    {

        public int ReportParameterId { get; set; }

        public int ReportId { get; set; }

        public string ReportParameterName { get; set; }

        public string ParameterName { get; set; }

        public string ParameterViewName { get; set; }

        public virtual Report Report { get; set; }

    }
}
