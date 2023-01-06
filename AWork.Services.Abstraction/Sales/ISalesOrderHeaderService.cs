using AWork.Contracts.Dto.Sales.SalesOrderDetail;
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using AWork.Contracts.Dto.Sales.SalesPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface ISalesOrderHeaderService
    {
        Task<IEnumerable<SalesOrderHeaderDto>> GetAllSalesOrderHeader(bool trackChanges);

        Task<SalesOrderHeaderDto> GetSalesOrderHeaderById(int bussinessEntityId, bool trackChanges);

        void CreateSalesOrderHeaderDetail(SalesOrderHeaderForCreateDto salesOrderHeader, List<SalesOrderDetailForCreateDto> salesOrderDetail);

        void Insert(SalesOrderHeaderForCreateDto salesOrderHeaderForCreateDto);

        void Edit(SalesOrderHeaderDto salesOrderHeaderForDto);

        void Remove(SalesOrderHeaderDto salesOrderHeaderForDto);
    }
}
