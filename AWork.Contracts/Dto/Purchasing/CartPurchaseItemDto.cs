using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Purchasing
{
    public class CartPurchaseItemDto
    {
        public PurchaseOrderHeaderDto PurchaseOrderHeaderDtoss { get; set; }
        public ICollection<PurchaseOrderDetailsDto> PurchaseOrderDetailsDtoss { get; set; }
    }
}
