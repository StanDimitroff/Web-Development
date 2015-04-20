using System.ComponentModel.DataAnnotations;
using AngularAspNetMvc.Models.Core;
using AngularAspNetMvc.Web.Core.Validation;

namespace AngularAspNetMvc.Models.Contacts
{
    public class ContactType : EditableModel
    {
        public int ContactTypeId { get; set; }

        [CustomRequired(ErrorMessage = "Type is required")]
        [CustomMaxLength(30, ErrorMessage = "Type name cannot be longer than 30.")]
        [Display(Name = "Contact Type")]
        public string Name { get; set; }

    }
}
