using System.Data.Entity.ModelConfiguration;
using AngularAspNetMvc.Data.Models;

namespace AngularAspNetMvc.DataAccess.Config
{
    public class ContactTypeConfig : EntityTypeConfiguration<ContactType>
    {
        public ContactTypeConfig()
        {
            // Primary Key
            HasKey(t => t.ContactTypeId);

            // Properties
            Property(t => t.Name).HasMaxLength(30).IsRequired();

        }
    }
}
