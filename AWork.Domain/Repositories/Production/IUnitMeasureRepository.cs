using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IUnitMeasureRepository
    {
        Task<IEnumerable<UnitMeasure>> GetAllUnitMeasure(bool trackChanges);

        Task<UnitMeasure> GetUnitMeasureById(string UnitMeasureCode, bool trackChanges);

        void Insert(UnitMeasure unitMeasure);
        void Edit(UnitMeasure unitMeasure);
        void Remove(UnitMeasure unitMeasure);
    }
}
