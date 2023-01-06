using AWork.Contracts.Dto.Sales.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SalesPerson
{
    public class SalesPersonGroupEditDto
    {
        public SalesPersonDto salesPersonDto { get; set; }

        public List<StoreDto> storeDto { get; set; }
    }
}
