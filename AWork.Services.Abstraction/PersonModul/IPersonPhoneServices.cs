using AWork.Contracts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IPersonPhoneServices
    {
        Task<IEnumerable<PersonPhoneDto>> GetAllPersonPhone(bool trackChanges);
        Task<IEnumerable<PersonPhoneDto>> GetPersonPhoneById(int busineessEntity, bool trackChanges);
        void Insert(PersonPhoneForCreateDto personPhoneForCreateDto);
        void Edit(PersonPhoneDto personPhoneDto);
        void Remove(PersonPhoneDto personPhoneDto);
    }
}
