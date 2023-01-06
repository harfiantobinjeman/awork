using AWork.Contracts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IPhoneNumberTypeServices
    {
        Task<IEnumerable<PhoneNumberTypeDto>> GetAllPhoneNumberType(bool trackChanges);
        Task<PhoneNumberTypeDto> GetPhoneNumberTypeById(int phoneNumberTypeId, bool trackChanges);
        void Insert(PhoneNumberTypeForCreateDto phoneNumberTypeForCreateDto);
        void Edit(PhoneNumberTypeDto phoneNumberTypeDto);
        void Remove(PhoneNumberTypeDto phoneNumberTypeDto);
    }
}
