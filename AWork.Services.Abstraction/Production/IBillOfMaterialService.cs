using AWork.Contracts.Dto.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IBillOfMaterialService
    {
        Task<IEnumerable<BillOfMaterialDto>> GetAllBillOfMaterial(bool trackChanges);
        Task<BillOfMaterialDto> GetBillOfMaterialById(int BillOfMaterialsId, bool trackChanges);

        void Insert(BillOfMaterialForCreateDto billOfMaterialForCreateDto);
        void Edit(BillOfMaterialDto billOfMaterialDto);
        void Remove(BillOfMaterialDto billOfMaterialDto);
    }
}
