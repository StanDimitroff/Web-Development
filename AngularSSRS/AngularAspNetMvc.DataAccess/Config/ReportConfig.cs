using System.Data.Entity.ModelConfiguration;
using AngularAspNetMvc.Data.Models;

namespace AngularAspNetMvc.DataAccess.Config
{
    public class ReportConfig : EntityTypeConfiguration<Report>
    {
        public ReportConfig()
        {
            // Primary Key
            HasKey(t => t.ReportId);

            // Properties
            Property(t => t.ReportFileName).HasMaxLength(50).IsRequired();
            Property(t => t.ReportName).HasMaxLength(50).IsRequired();
            Property(t => t.ReportDescription).HasMaxLength(int.MaxValue).IsRequired();

            HasMany(one => one.ReportParameters)
                .WithRequired(t => t.Report)
                .HasForeignKey(t => t.ReportId)
                .WillCascadeOnDelete(false);
        }
    }
}
