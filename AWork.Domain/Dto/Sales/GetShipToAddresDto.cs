using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Dto.Sales
{
    public class GetShipToAddresDto
    {
        public string AddressLine1 { get; set; }
        public int AddressID { get; set; }
       
        public int ShipToAddressId { get; set; }

    }
}
