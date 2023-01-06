using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface ISalesOrderDetailRepository
    {
        Task<IEnumerable<SalesOrderDetail>> GetAllSalesOrderDetail(bool trackChanges);
        Task<SalesOrderDetail> GetSalesOrderDetailById(int specialOfferId, bool trackChanges);
        void Insert(SalesOrderDetail salesOrderDetail);
        void Edit(SalesOrderDetail salesOrderDetail);
        void Remove(SalesOrderDetail salesOrderDetail);
    }
}
