using System.Data.Entity;
using AngularAspNetMvc.Data.Models;
using AngularAspNetMvc.DataAccess.Config;

namespace AngularAspNetMvc.DataAccess
{
    public class ContactsContext : DbContext
    {
        public ContactsContext()
            :base("Name=ContactsDb")
        {
            
        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<ReportParameter> ReportParameters { get; set; }

        public DbSet<ReportRequest> ReportRequests { get; set; }

        public DbSet<ReportRequestParameter> ReportRequestParameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContactConfig());
            modelBuilder.Configurations.Add(new ContactTypeConfig());
            modelBuilder.Configurations.Add(new ReportConfig());
            modelBuilder.Configurations.Add(new ReportParameterConfig());
        }
    }
}
