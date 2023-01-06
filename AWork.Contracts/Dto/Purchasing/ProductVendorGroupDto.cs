using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Purchasing
{
    public class ProductVendorGroupDto
    {
        public VendorForCreateDto VendorForCreateDto { get; set; }

        public List<ProductVendorForCreateDto> ProductVendorForCreateDto { get; set; }
    }
}
