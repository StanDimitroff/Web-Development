using System.Collections.Generic;
using System.Linq;
using AngularAspNetMvc.Data.Models;


namespace AngularAspNetMvc.DataAccess.Repository
{
    public class ContactTypeRepository : Core.Repository, IContactTypeRepository
    {
        public IEnumerable<ContactType> GetAllContactTypes()
        {
            
            return Context.ContactTypes.OrderBy(one => one.Name).ToList();
        }

        public ContactType GetContactType(int contactTypeId)
        {
            return GetByPrimaryKey<ContactType>(contactTypeId);
        }
    }
}
