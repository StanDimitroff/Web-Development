using System.Data.Entity.ModelConfiguration;
using AngularAspNetMvc.Data.Models;

namespace AngularAspNetMvc.DataAccess.Config
{
    public class ReportRequestParameterConfig : EntityTypeConfiguration<ReportRequestParameter>
    {
        public ReportRequestParameterConfig()
        {
            // Primary Key
            HasKey(t => t.ReportRequestParameterId);

            // Properties
            Property(t => t.ReportParameterName).HasMaxLength(50).IsRequired();
            Property(t => t.ParameterValue).HasMaxLength(int.MaxValue);

        }
    }
}
