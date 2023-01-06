using AWork.Contracts.Dto;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IEmailAddressServices
    {
        Task<IEnumerable<EmailAddressDto>> GetAllEmailAddress(bool trackChanges);
        Task<EmailAddressDto> GetEmailAddress(string emailAddressId, bool trackChanges);
        Task<EmailAddressDto> GetEmailId(int businessEntity, bool trackChanges);
        Task<EmailAddressDto>GetEmailAddressId(string emailAddress, int businessEntity, bool trackChanges);
        Task<EmailAddressDto> CreateEmail(EmailAddressForCreateDto emailAddressForCreateDto);


        void Insert(EmailAddressForCreateDto emailAddressForCreateDto);
        void Edit(EmailAddressDto emailAddressDto);
        void Delete(EmailAddressDto emailAddressDto);
    }
}
