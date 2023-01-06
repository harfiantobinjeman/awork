using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Contracts.Dto.Sales.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SalesPerson
{
    public class SalesPersonGroupDto
    {
        public SalesPersonDto salesPersonDto { get; set; }
        public SalesPersonForCreateDto salesPersonForCreateDto { get; set; }
        public List<StoreForCreateDto> storeForCreateDto { get; set; }
    }
}
