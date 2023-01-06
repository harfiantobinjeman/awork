using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesCustomer;
using AWork.Contracts.Dto.Sales.SalesPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.Store
{
    public class StoreDto
    {
        public int BusinessEntityId { get; set; }
        public string Name { get; set; }
        public int? SalesPersonId { get; set; }
        public string Demographics { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntityDto BusinessEntity { get; set; }
        public virtual SalesPersonDto SalesPerson { get; set; }
        public virtual ICollection<CustomerDto> Customers { get; set; }
    }
}
