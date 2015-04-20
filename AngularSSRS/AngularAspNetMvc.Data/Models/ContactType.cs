using System.Collections.Generic;

namespace AngularAspNetMvc.Data.Models
{
    public class ContactType
    {
        public int ContactTypeId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
