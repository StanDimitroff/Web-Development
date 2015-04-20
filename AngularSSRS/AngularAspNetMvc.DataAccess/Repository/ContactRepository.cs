using System;
using System.Linq;
using AngularAspNetMvc.Data.ExtendedModels;
using AngularAspNetMvc.Data.Models;


namespace AngularAspNetMvc.DataAccess.Repository
{
    public class ContactRepository : Core.Repository, IContactRepository
    {
        public PagedResult<ContactInfo> GetContacts(int pageNumber, int rowsPerPage, string name)
        {
            var finalName = name ?? string.Empty;
            var query = Context.Contacts
                .Where(one => one.FirstName.Contains(finalName) || one.LastName.Contains(finalName));
            var totalRows = query.Count();
            return new PagedResult<ContactInfo>(
               query
                    .OrderBy(one => one.LastName)
                    .ThenBy(one => one.FirstName)
                    .Skip((pageNumber - 1) * rowsPerPage)
                    .Take(rowsPerPage)
                    .Select(one => new ContactInfo
                    {
                        ContactId = one.ContactId,
                        FirstName = one.FirstName,
                        LastName = one.LastName
                    })
                    .ToList(), totalRows, (int)Math.Ceiling(totalRows / (decimal)rowsPerPage));
        }

        public Contact GetContact(int contactId)
        {
            return GetByPrimaryKey<Contact>(contactId);
        }
    }
}
