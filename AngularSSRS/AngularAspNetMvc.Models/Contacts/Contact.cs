using System.ComponentModel.DataAnnotations;
using AngularAspNetMvc.Models.Core;
using AngularAspNetMvc.Web.Core.Validation;

namespace AngularAspNetMvc.Models.Contacts
{
    public class Contact : EditableModel
    {
        public int ContactId { get; set; }

        [CustomRequired(ErrorMessage = "First name is required")]
        [CustomMaxLength(50, ErrorMessage = "First name cannot be longer than 50.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [CustomRequired(ErrorMessage = "Last name is required")]
        [CustomMaxLength(50, ErrorMessage = "Last name cannot be longer than 50.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Active Contact")]
        public bool IsActive { get; set; }

        [Display(Name = "Contact Type")]
        [DropdownRequired(ErrorMessage = "Contact type is required")]
        public int ContactTypeId { get; set; }

    }
}
