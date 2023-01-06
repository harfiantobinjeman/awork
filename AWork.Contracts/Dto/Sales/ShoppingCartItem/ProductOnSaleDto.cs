using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Sales.SalesOrderDetail;
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.ShoppingCartItem
{
    public class ProductOnSaleDto
    {
        public ProductDto product { get; set; }
        public ShoppingCartItemDto shoppingCartItem { get; set; }
        public ShoppingCartItemForCreateDto shoppingCartItemCreate { get; set; }
        public SalesOrderHeaderDto salesOrderHeader { get; set; }
        public SalesOrderHeaderForCreateDto salesOrderHeaderCreate { get; set; }
        public SalesOrderDetailForCreateDto salesOrderDetail { get; set; }
    }
}
