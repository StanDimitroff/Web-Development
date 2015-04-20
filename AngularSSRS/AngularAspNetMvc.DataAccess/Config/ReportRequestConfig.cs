using System.Data.Entity.ModelConfiguration;
using AngularAspNetMvc.Data.Models;

namespace AngularAspNetMvc.DataAccess.Config
{
    public class ReportRequestConfig : EntityTypeConfiguration<ReportRequest>
    {
        public ReportRequestConfig()
        {
            // Primary Key
            HasKey(t => t.ReportRequestId);

            // Properties
            Property(t => t.ReportFileName).HasMaxLength(50).IsRequired();

            Property(t => t.UniqueId).IsRequired();

            HasRequired(t => t.Report)
                .WithMany(t => t.ReportRequests)
                .HasForeignKey(t => t.ReportId)
                .WillCascadeOnDelete(false);

            HasMany(t => t.ReportRequestParameters)
                .WithRequired(t => t.ReportRequest)
                .HasForeignKey(t => t.ReportRequestId)
                .WillCascadeOnDelete(false);
        }
    }
}
