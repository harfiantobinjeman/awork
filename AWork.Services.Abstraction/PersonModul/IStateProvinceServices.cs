using AWork.Contracts.Dto.PersonModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IStateProvinceServices
    {
        Task<IEnumerable<StateProvinceDto>> GetAllStateprovince(bool trackChanges);
        Task<StateProvinceDto> GetAllStateProvinceById(int stateId, bool trackChanges);
        void Insert(StateProvinceForCreateDto stateProvinceForCreateDto);
        void Edit(StateProvinceDto stateProvinceDto);
        void Remove(StateProvinceDto stateProvinceDto);
    }
}
