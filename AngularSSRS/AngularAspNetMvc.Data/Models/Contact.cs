namespace AngularAspNetMvc.Data.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public int ContactTypeId { get; set; }

        public virtual ContactType ContactType { get; set; }


    }
}
