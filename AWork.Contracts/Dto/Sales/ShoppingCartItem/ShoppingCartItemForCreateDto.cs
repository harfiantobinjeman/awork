using AWork.Contracts.Dto.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.ShoppingCartItem
{
    public class ShoppingCartItemForCreateDto
    {
        public string ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ProductDto Product { get; set; }

    }
}
