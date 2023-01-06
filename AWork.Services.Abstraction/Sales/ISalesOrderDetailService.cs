using AWork.Contracts.Dto.Sales.SalesOrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface ISalesOrderDetailService
    {
        Task<IEnumerable<SalesOrderDetailDto>> GetAllSalesOrderDetail(bool trackChanges);
        Task<SalesOrderDetailDto> GetSalesOrderDetailById(int specialOfferId, bool trackChanges);

        void Insert(SalesOrderDetailForCreateDto specialOfferProductForCreateDto);

        void Edit(SalesOrderDetailDto salesOrderDetailDto);

        void Remove(SalesOrderDetailDto salesOrderDetailDto);
    }
}
