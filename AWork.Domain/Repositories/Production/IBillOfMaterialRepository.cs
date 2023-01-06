using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IBillOfMaterialRepository
    {
        Task<IEnumerable<BillOfMaterial>> GetAllBillOfMaterial(bool trackChanges);

        Task<BillOfMaterial> GetBillOfMaterialById(int BillOfMaterialsId, bool trackChanges);

        void Insert(BillOfMaterial billOfMaterial);
        void Edit(BillOfMaterial billOfMaterial);
        void Remove(BillOfMaterial billOfMaterial);
    }
}
