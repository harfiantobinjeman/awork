using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Contracts.Dto.Sales.Store;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SalesCustomer
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public int? PersonId { get; set; }
        public int? StoreId { get; set; }
        public int? TerritoryId { get; set; }
        public string AccountNumber { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual PersonDto Person { get; set; }
        public virtual StoreDto Store { get; set; }
        public virtual SalesTerritoryDto Territory { get; set; }
        //public virtual ICollection<SalesOrderHeaderDto> SalesOrderHeaders { get; set; }
    }
}
