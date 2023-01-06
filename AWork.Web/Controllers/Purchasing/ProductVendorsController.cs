using AWork.Contracts.Dto.Purchasing;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Contracts.Dto.Sales.ShoppingCartItem;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AWork.Web.Controllers.Purchasing
{
    public class ProductVendorsController : Controller
    {
        private readonly IPurchasingServiceManager _serviceContext;
        private readonly UserManager<User> _userManager;
        private readonly IPersonServiceManager _personServiceManager;
        private readonly ISalesServiceManager _salesServiceManager;

        public ProductVendorsController(IPurchasingServiceManager serviceContext, IPurchasingRepositoryManager purchasingRepository, UserManager<User> userManager, IPersonServiceManager personServiceManager, ISalesServiceManager salesServiceManager)
        {
            _serviceContext = serviceContext;
            _userManager = userManager;
            _personServiceManager = personServiceManager;
            _salesServiceManager = salesServiceManager;
        }

        public async Task<IActionResult> Index(string searchString, string currentFilter, string sortOrder, int? page, int? pageSize)
        {
            // set page
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            // default size is 5 otherwise take pageSize value
            int defaultSize = (pageSize ?? 5);
            ViewBag.psize = defaultSize;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var productVendor = await _serviceContext.ProductVendorService.GetAllProductVendor(false);
            IPagedList<ProductVendorDto> productVendorDtos = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                productVendor = productVendor.Where(p => p.Product.Name.ToLower().Contains(searchString.ToLower()) ||
                p.BusinessEntity.AccountNumber.ToLower().Contains(searchString.ToLower()) || 
                p.BusinessEntity.Name.ToLower().Contains(searchString.ToLower()));
            }

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" }
            };

            /*var productVendor = await _serviceContext.ProductVendorService.GetAllProductVendor(false);*/
            productVendorDtos = productVendor.ToPagedList(pageIndex, defaultSize);
            return View(productVendorDtos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _serviceContext.ProductVendorService.GetProductVendorById((int)id, false);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> CartItem(PurchaseOrderHeaderDto purchaseOrderHeaderDto)
        {
            var myUser = await _userManager.GetUserAsync(User);
            var email = await _personServiceManager.EmailAddressServices.GetEmailAddress(myUser.Email, false);
            int bussinessEntityId = email.BusinessEntityId;
            var empId = Convert.ToString(bussinessEntityId);
            var itemCartt = await _serviceContext.PurchaseOrderHeaderService.GetCartItemEmpId(bussinessEntityId, false);
            return View(itemCartt);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(ProductVendorDto productVendorDto)
        {
            if (ModelState.IsValid)
            {
                var myUser = await _userManager.GetUserAsync(User);
                var email = await _personServiceManager.EmailAddressServices.GetEmailAddress(myUser.Email, false);
                int bussinessEntityId = email.BusinessEntityId;
                var empId = Convert.ToString(bussinessEntityId);
                var productsV = productVendorDto;
                var cekEmp = await _serviceContext.ProductVendorService.FilterEmpId(bussinessEntityId, false);
                if(cekEmp.Count() == 0)
                {
                    var cartItem = new ShoppingCartItemForCreateDto
                    {
                        ProductId = productsV.ProductId,
                        ModifiedDate = DateTime.Now,
                        Quantity = 1,
                        DateCreated = DateTime.Now,
                        ShoppingCartId = empId
                    };

                    _salesServiceManager.ShoppingCartItemService.Insert(cartItem);
                }

                else
                {
                    ShoppingCartItemDto purchase = new ShoppingCartItemDto();
                    ShoppingCartItemForCreateDto purchaseCreate = new ShoppingCartItemForCreateDto();
                    foreach (var item in cekEmp)
                    {
                        if(item.ProductId == purchase.ProductId)
                        {
                            var quantity = Convert.ToInt32(item.OnOrderQty);
                            purchase.ShoppingCartId = empId;
                            purchase.Quantity = quantity + 1;
                            purchase.ShoppingCartItemId = item.BusinessEntityId+1;
                            purchase.DateCreated = DateTime.Now;
                            purchase.ModifiedDate = DateTime.Now;
                            purchase.ProductId = productsV.ProductId;
                            _salesServiceManager.ShoppingCartItemService.Edit(purchase);
                        }
                        else
                        {
                            purchaseCreate.ProductId = productsV.ProductId;
                            purchaseCreate.Quantity = 1;
                            purchaseCreate.ShoppingCartId = empId;
                            purchaseCreate.DateCreated = DateTime.Now.AddMinutes(1);
                            purchase.ModifiedDate = DateTime.Now.AddMinutes(1);
                        }
                    }
                    if(purchase.ProductId != purchaseCreate.ProductId)
                    {
                        _salesServiceManager.ShoppingCartItemService.Insert(purchaseCreate);
                    }
                }
                /*_serviceContext.ProductVendorService.Insert(productVendorForCreateDto);*/
                return RedirectToAction(nameof(Index));
            }
            
            return View(productVendorDto);
        }

    }
}
