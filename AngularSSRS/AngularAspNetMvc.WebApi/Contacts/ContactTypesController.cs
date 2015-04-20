using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AngularAspNetMvc.DataAccess.Repository;
using AngularAspNetMvc.Models.Contacts;
using AngularAspNetMvc.Models.Core;
using AngularAspNetMvc.WebApi.Core;
using AutoMapper;

namespace AngularAspNetMvc.WebApi.Contacts
{
    public class ContactTypesController : RelayController
    {
        private readonly IContactTypeRepository _contactTypeRepository;

        public ContactTypesController(IContactTypeRepository contactTypeRepository)
        {
            _contactTypeRepository = contactTypeRepository;
        }

        public ApiResult<IEnumerable<ContactType>> Get()
        {
            return Execute(() => _contactTypeRepository.GetAllContactTypes().Select(Mapper.Map<ContactType>));
        }

        public ApiResult<IEnumerable<ContactType>> GetForSelect()
        {
            return Execute(() =>
            {
                var data = _contactTypeRepository.GetAllContactTypes().Select(Mapper.Map<ContactType>).ToList();
                data.Insert(0, new ContactType
                {
                    ContactTypeId = 0,
                    Name = "- Select -"
                });
                return data.AsEnumerable();

            });
        }

        [HttpPost]
        public ApiResult<ContactType> Save(ContactType contactType)
        {
            return Execute(
                () =>
                {
                    var dataModel = Mapper.Map<Data.Models.ContactType>(contactType);
                    return Mapper.Map<ContactType>(
                        contactType.ContactTypeId == 0
                            ? _contactTypeRepository.Insert(dataModel)
                            : _contactTypeRepository.Update(dataModel));
                });
        }

        public ApiResult<ContactType> GetById(int contactTypeId)
        {
            return Execute(() => Mapper.Map<ContactType>(_contactTypeRepository.GetContactType(contactTypeId)));
        }

        [HttpPost]
        public ApiResult<ContactType> Delete(ContactType contactType)
        {
            return Execute(() =>
            {
                var dataModel = Mapper.Map<Data.Models.ContactType>(contactType);
                return Mapper.Map<ContactType>(_contactTypeRepository.Delete(dataModel));
            });
        }
    }
}
