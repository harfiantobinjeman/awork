using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface ISalesOrderHeaderRepository
    {
        Task<IEnumerable<SalesOrderHeader>> GetAllSalesOrderHeader(bool trackChanges);
        Task<SalesOrderHeader> GetByIdSalesOrderHeader(int salesorderHeader, bool trackChanges);
        void Edit(SalesOrderHeader salesorderHeader);
        void Insert(SalesOrderHeader salesorderHeader);
        void Remove(SalesOrderHeader salesorderHeader);


    }
}
