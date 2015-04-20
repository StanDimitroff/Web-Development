using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AngularAspNetMvc.DataAccess.Repository;
using AngularAspNetMvc.Models.Common;
using AngularAspNetMvc.Models.Contacts;
using AngularAspNetMvc.Models.Core;
using AngularAspNetMvc.WebApi.Core;
using AutoMapper;

namespace AngularAspNetMvc.WebApi.Contacts
{
    public class ContactsController : RelayController
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactTypeRepository)
        {
            _contactRepository = contactTypeRepository;
        }

        public ApiResult<PagedResult<ContactInfo>> Get([FromUri] PagedCriteria criteria)
        {
            return Execute(() => Mapper.Map<PagedResult<ContactInfo>>(
                _contactRepository.GetContacts(criteria.PageNumber, criteria.PageSize, criteria.Name)));
        }

        [HttpPost]
        public ApiResult<Contact> Save(Contact contact)
        {
            return Execute(
                () =>
                {
                    var dataModel = Mapper.Map<Data.Models.Contact>(contact);
                    return Mapper.Map<Contact>(
                        contact.ContactId == 0
                            ? _contactRepository.Insert(dataModel)
                            : _contactRepository.Update(dataModel));
                });
        }

        public ApiResult<Contact> GetById(int contactId)
        {
            return Execute(() => Mapper.Map<Contact>(_contactRepository.GetContact(contactId)));
        }

        [HttpPost]
        public ApiResult<Contact> Delete(Contact contact)
        {
            return Execute(() =>
            {
                var dataModel = Mapper.Map<Data.Models.Contact>(contact);
                return Mapper.Map<Contact>(_contactRepository.Delete(dataModel));
            });
        }
    }
}
