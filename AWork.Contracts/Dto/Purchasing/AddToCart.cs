using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Purchasing
{
    public class AddToCart
    {
        public int ProductId { get; set; }
        public int BusinessEntityId { get; set; }

        public PurchaseOrderHeaderForCreateDto PurchaseOrderHeaderDtoss { get; set; }
        public ICollection<PurchaseOrderDetailsForCreateDto> PurchaseOrderDetailsDtoss { get; set; }
    }
}
