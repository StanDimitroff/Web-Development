using System.Data.Entity.ModelConfiguration;
using AngularAspNetMvc.Data.Models;

namespace AngularAspNetMvc.DataAccess.Config
{
    public class ContactConfig : EntityTypeConfiguration<Contact>
    {
        public ContactConfig()
        {
            // Primary Key
            HasKey(t => t.ContactId);

            // Properties
            Property(t => t.FirstName).HasMaxLength(50).IsRequired();
            Property(t => t.LastName).HasMaxLength(50).IsRequired();

            HasRequired(t => t.ContactType)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.ContactTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
