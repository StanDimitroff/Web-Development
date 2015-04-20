
using AngularAspNetMvc.Models.Core;

namespace AngularAspNetMvc.Models.Contacts
{
    public class ContactInfo : ReadOnlyModel
    {

        public int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
