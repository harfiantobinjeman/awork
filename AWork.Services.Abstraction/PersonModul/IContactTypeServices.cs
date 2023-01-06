using AWork.Contracts.Dto.PersonModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IContactTypeServices
    {
        Task<IEnumerable<ContactTypeDto>> GetAllContactType(bool trackChanges);
        Task<ContactTypeDto> GetAllContactTypeById(int contactId, bool trackChanges);
        void Insert(ContactTypeForCreateDto contactTypeForCreateDto);
        void Edit(ContactTypeDto contactTypeDto);
        void Remove(ContactTypeDto contactTypeDto);
    }
}
