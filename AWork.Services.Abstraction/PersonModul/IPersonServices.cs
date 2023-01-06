using AWork.Contracts.Dto;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.PersonModule.Profile;
using AWork.Contracts.Dto.Sales.Store;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IPersonServices
    {
        Task<IEnumerable<PersonDto>> GetAllPerson(bool trackChanges);
        Task<PersonDto> GetAllPersonById(int personId, bool trackChanges);
        Task<ProfileDto> GetProfileById(int businessEntityId, bool trackChanges);
        Task<IEnumerable<PersonDto>> GetPersonPage(int pageIndex, int pageSize, bool trackChanges);
        Task<PersonDto> GetPerson(string firstName, string lastName, bool trackChanges);
        void personProfileEdit(PersonDto personDto,List<EmailAddressForCreateDto> emailAddressForCreateDtos, List<PersonPhoneForCreateDto> personPhoneForCreateDtos, AddressForCreateDto addressForCreateDto);

        Task<PersonDto> CreatePerson(PersonForCreateDto personForCreateDto);

        void Insert(PersonForCreateDto personForCreateDto);


        void Edit(PersonDto personDto);
        void Delete(PersonDto personDto);
    }
}
