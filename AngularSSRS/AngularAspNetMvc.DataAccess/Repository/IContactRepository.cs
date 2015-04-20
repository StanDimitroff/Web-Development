using System.Collections.Generic;
using AngularAspNetMvc.Data.ExtendedModels;
using AngularAspNetMvc.Data.Models;
using AngularAspNetMvc.DataAccess.Core.Repository;

namespace AngularAspNetMvc.DataAccess.Repository
{
    public interface IContactRepository : IWriteRepository
    {
        PagedResult<ContactInfo> GetContacts(int pageNumber, int rowsPerPage, string name);
        Contact GetContact(int contactId);
    }
}