using System.Collections.Generic;
using AngularAspNetMvc.Data.Models;
using AngularAspNetMvc.DataAccess.Core.Repository;

namespace AngularAspNetMvc.DataAccess.Repository
{
    public interface IContactTypeRepository : IWriteRepository
    {
        IEnumerable<ContactType> GetAllContactTypes();

        ContactType GetContactType(int contactTypeId);
    }
}