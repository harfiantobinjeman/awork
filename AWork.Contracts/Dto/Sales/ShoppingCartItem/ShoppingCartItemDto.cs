using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.ShoppingCartItem
{
    public class ShoppingCartItemDto
    {
        public int ShoppingCartItemId { get; set; }
        public string ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ProductDto Product { get; set; }
    }
}
