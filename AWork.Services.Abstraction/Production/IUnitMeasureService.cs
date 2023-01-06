using AWork.Contracts.Dto.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IUnitMeasureService
    {
        Task<IEnumerable<UnitMeasureDto>> GetAllUnitMeasure(bool trackChanges);

        Task<UnitMeasureDto> GetUnitAllMeasureById(string UnitMeasureCode, bool trackChanges);

        void Insert(UnitMeasureForCreateDto unitMeasureForCreateDto);
        void Edit(UnitMeasureDto unitMeasureDto);
        void Remove(UnitMeasureDto unitMeasureDto);
    }
}
