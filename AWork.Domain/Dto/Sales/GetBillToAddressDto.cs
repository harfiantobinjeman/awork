using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Dto.Sales
{
    public class GetBillToAddressDto
    {
        public string AddressLine1 { get; set; }
        public int AddressID { get; set; }

        public int BillToAddressId { get; set; }
    }
}
