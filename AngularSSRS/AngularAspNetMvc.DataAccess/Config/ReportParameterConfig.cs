using System.Data.Entity.ModelConfiguration;
using AngularAspNetMvc.Data.Models;

namespace AngularAspNetMvc.DataAccess.Config
{
    public class ReportParameterConfig : EntityTypeConfiguration<ReportParameter>
    {
        public ReportParameterConfig()
        {
            // Primary Key
            HasKey(t => t.ReportParameterId);

            // Properties
            Property(t => t.ParameterName).HasMaxLength(50).IsRequired();
            Property(t => t.ParameterViewName).HasMaxLength(50).IsRequired();
            Property(t => t.ReportParameterName).HasMaxLength(50).IsRequired();

            HasRequired(one => one.Report)
                .WithMany(one => one.ReportParameters)
                .HasForeignKey(one => one.ReportId)
                .WillCascadeOnDelete(false);
        }
    }
}
