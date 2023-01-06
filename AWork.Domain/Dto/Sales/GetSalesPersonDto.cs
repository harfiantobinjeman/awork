using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Dto.Sales
{
    public class GetSalesPersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BusinessEntityId { get; set; }
        public int TerritoryId { get; set; }
    }
}
