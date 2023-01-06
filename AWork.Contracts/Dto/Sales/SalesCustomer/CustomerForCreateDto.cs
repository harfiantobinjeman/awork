using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesTerritory;
using AWork.Contracts.Dto.Sales.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SalesCustomer
{
    public class CustomerForCreateDto
    {
        public int? PersonId { get; set; }
        public int? StoreId { get; set; }
        public int? TerritoryId { get; set; }
        public string AccountNumber { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
