using AWork.Contracts.Dto.Sales.PersonCreditCard;
using AWork.Contracts.Dto.Sales.ShoppingCartItem;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using X.PagedList;
using AWork.Contracts.Dto.Production;
using Microsoft.AspNetCore.Identity;
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using AWork.Contracts.Dto.Sales.SalesOrderDetail;
using AutoMapper;

namespace AWork.Web.Controllers.Sales
{
    public class ShoppingCartItemsServiceController : Controller
    {
        private readonly ISalesServiceManager _context;
        private readonly IPurchasingServiceManager _purchasingContext;
        private readonly UserManager<User> _userManager;
        private readonly IPersonServiceManager _servisManager;

        public ShoppingCartItemsServiceController(ISalesServiceManager context, UserManager<User> userManager, IPersonServiceManager servisManager, IPurchasingServiceManager purchasingContext)
        {
            _context = context;
            _userManager = userManager;
            _servisManager = servisManager;
            _purchasingContext = purchasingContext;
        }

        public async Task<IActionResult> CartItem()
        {
            var myUser = await _userManager.GetUserAsync(User);
            var email = await _servisManager.EmailAddressServices.GetEmailAddress(myUser.Email, false);
            int businessEntityId = email.BusinessEntityId;
            var customerId = Convert.ToString(businessEntityId);
            var itemCart = await _context.ShoppingCartItemService.GetCartItemByCustId(customerId, false);
            var allShipMethod = await _purchasingContext.ShipMethodService.GetAllShipMethod(false);
            var allCreditCard = await _context.CreditCardService.GetAllCreditCard(false);
            ViewData["ShipMethodId"] = new SelectList(allShipMethod, "ShipMethodId", "Name");
            ViewData["CreditCardId"] = new SelectList(allCreditCard, "CreditCardId", "CardNumber");
            return View(itemCart);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(List<ProductOnSaleDto> shop)
        {
            if (ModelState.IsValid)
            {
                var myUser = await _userManager.GetUserAsync(User);
                var email = await _servisManager.EmailAddressServices.GetEmailAddress(myUser.Email, false);
                int businessEntityId = email.BusinessEntityId;
                var customerId = Convert.ToString(businessEntityId);

                var salesOrderDetailCreate = new List<SalesOrderDetailForCreateDto>();
                var salesOrderHeader = new SalesOrderHeaderForCreateDto();
                salesOrderHeader.CustomerId = Convert.ToInt32(customerId);
                salesOrderHeader.OrderDate = DateTime.Now;
                salesOrderHeader.DueDate = DateTime.Now;
                salesOrderHeader.Status = 0;
                salesOrderHeader.OnlineOrderFlag = false;
                salesOrderHeader.BillToAddressId = 1;
                salesOrderHeader.ShipToAddressId = 1;
                salesOrderHeader.ShipMethodId = 1;
                salesOrderHeader.CreditCardId = 1;
                salesOrderHeader.TaxAmt = 0;
                salesOrderHeader.Freight = 0;
                foreach (var item in shop)
                {
                    

                    var salesOrderDetail = new SalesOrderDetailForCreateDto
                    {
                        OrderQty = Convert.ToInt16(item.shoppingCartItem.Quantity),
                        ProductId = item.shoppingCartItem.ProductId,
                        SpecialOfferId = 1,
                        UnitPrice = item.shoppingCartItem.Product.ListPrice,
                        UnitPriceDiscount = 0,
                        LineTotal = 0,
                        Rowguid = Guid.NewGuid(),
                        ModifiedDate = DateTime.Now,
                    };
                    salesOrderDetailCreate.Add(salesOrderDetail);
                }
                _context.SalesOrderHeaderService.CreateSalesOrderHeaderDetail(salesOrderHeader, salesOrderDetailCreate);

                foreach (var item in shop)
                {
                    var shoppingCartItem = await _context.ShoppingCartItemService.GetShopCartItemById(item.shoppingCartItem.ShoppingCartItemId, false);
                    _context.ShoppingCartItemService.Remove(shoppingCartItem);
                }
                return RedirectToAction("CartItem", "ShoppingCartItemsService");
            }
            return RedirectToAction("CartItem");
        }
    }
}
