using System.Collections.ObjectModel;
using AngularAspNetMvc.Data.Models;

namespace AngularAspNetMvc.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ContactsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ContactsContext context)
        {
            context.ContactTypes.AddOrUpdate(
                p => p.Name,
                new[]
                {
                    new ContactType{Name = "Friend"},
                    new ContactType{Name = "Coworker"},
                    new ContactType{Name = "Family"}
                }
            );

            context.Reports.AddOrUpdate(
                p => p.ReportName,
                new[]
                {
                    new Report{
                        ReportName = "Contact List", 
                        ReportFileName = "ContactsList",
                        ReportDescription = "Print alphabetical contact list",
                        ReportParameters = new Collection<ReportParameter>
                        {
                            new ReportParameter
                            {
                                ReportParameterName = "@ActiveFlag",
                                ParameterViewName = "ActiveFlagDropDown",
                                ParameterName = "Filter by Active"
                                
                            }
                        }
                    }
                }
            );
        }
    }
}
