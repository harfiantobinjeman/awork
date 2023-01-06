using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Domain.Base;
using AWork.Services.Abstraction;
using AWork.Contracts.Dto.Purchasing;
using AWork.Contracts.Dto.Production;
using X.PagedList;

namespace AWork.Web.Controllers.Purchasing
{
    public class PurchaseOrderDetailsController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IPurchasingServiceManager _serviceContext;
        private readonly IPurchasingRepositoryManager _purchasingRepositoryManager;

        public PurchaseOrderDetailsController(AdventureWorks2019Context context, IPurchasingServiceManager serviceContext, IPurchasingRepositoryManager purchasingRepositoryManager)
        {
            _context = context;
            _serviceContext = serviceContext;
            _purchasingRepositoryManager = purchasingRepositoryManager;
        }
        /*public JsonResult GetShipMethod(int ShipMethodID)
        {
            var result = _purchasingRepositoryManager.PurchaseOrderDetailRepository.GetShipMethode(ShipMethodID);
            return Json(result);
        }*/

        // GET: PurchaseOrderDetails
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? page, int? pageSize)
        {
            /*var purchaseOd = await _context.PurchaseOrderDetails.ToListAsync();
            var purchaseODDto = await _serviceContext.PurchaseOrderDetailService.GetAllPurchaseOD(false);
            return View(purchaseODDto);*/
            var pageIndex = 1;
            //var pageSize = fetchSize ?? 5;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 5);
            ViewBag.psize = defaSize;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var purchasesOD = await _context.PurchaseOrderDetails.ToListAsync();
            var purchasesDODto = await _serviceContext.PurchaseOrderDetailService.GetAllPurchaseOD(false);
            IPagedList<PurchaseOrderDetailsDto> purchaseOrderDetailsDtos = null;
            if (!String.IsNullOrEmpty(searchString))
            {
                purchasesDODto = purchasesDODto.Where(p => p.Product.Name.ToLower().Contains(searchString.ToLower()) ||
                p.PurchaseOrder.Vendor.Name.ToLower().Contains(searchString.ToLower()) ||
                p.PurchaseOrder.ShipMethod.Name.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text="5"},
                new SelectListItem() { Value="10", Text="10"},
                new SelectListItem() { Value="15", Text="15"},
                new SelectListItem() { Value="20", Text="20"}
            };
            switch (sortOrder)
            {
                case "productName":
                    purchasesDODto = purchasesDODto.OrderBy(s => s.Product.Name);
                    break;
                case "vendorName":
                    purchasesDODto = purchasesDODto.OrderBy(s => s.PurchaseOrder.Vendor.Name);
                    break;
                case "shipMethodName":
                    purchasesDODto = purchasesDODto.OrderBy(s => s.PurchaseOrder.ShipMethod.Name);
                    break;
                default:
                    purchasesDODto = purchasesDODto.OrderBy(s => s.Product.Name);
                    break;
            }
            /*var totalRows = purchasesDODto.Count();
            if (!String.IsNullOrEmpty(searchString))
            {
                purchasesDODto = purchasesDODto.Where(p => p.Product.Name.ToLower().Contains(searchString.ToLower()));
            }*/

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ProductSortParm"] = String.IsNullOrEmpty(sortOrder) ? "productName" : "";
            ViewData["VendorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "vendorName" : "";
            ViewData["shipSortParm"] = String.IsNullOrEmpty(sortOrder) ? "shipMethodName" : "";
            ViewData["DateSortParm"] = sortOrder == "productName" ? "vendorName" : "productName";
            //var productDtoPaged = new StaticPagedList<PurchaseOrderDetailsDto>(purchasesDODto, pageIndex, pageSize - (pageSize - 1), totalRows);

            /*ViewBag.PageList = new SelectList(new List<int> { 8, 15, 20 });

            return View(productDtoPaged);*/
            purchaseOrderDetailsDtos = purchasesDODto.ToPagedList(pageIndex, defaSize);
            return View(purchasesDODto);
            /*return View(purchaseOrderDetailsDtos);*/

        }

        // GET: PurchaseOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var purchaseOrderDetail = await _context.PurchaseOrderDetails
                .Include(p => p.Product)
                .Include(p => p.PurchaseOrder)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);*/
            var purchaseOrderDetail = await _serviceContext.PurchaseOrderDetailService.GetPurchaseODById((int)id, false);
            if (purchaseOrderDetail == null)
            {
                return NotFound();
            }

            return View(purchaseOrderDetail);
        }

        // GET: PurchaseOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrderHeaders, "PurchaseOrderId", "PurchaseOrderId");
            return View();
        }

        // POST: PurchaseOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseOrderId,PurchaseOrderDetailId,DueDate,OrderQty,ProductId,UnitPrice,LineTotal,ReceivedQty,RejectedQty,StockedQty,ModifiedDate")] PurchaseOrderDetailsForCreateDto purchaseOrderDetailsForCreateDto)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(purchaseOrderDetail);
                await _context.SaveChangesAsync();*/
                /*_context.PurchaseOrderDetailRepository.Insert(purchaseOrderDetail);
                await _context.SaveAsync();*/
                _serviceContext.PurchaseOrderDetailService.Insert(purchaseOrderDetailsForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", purchaseOrderDetailsForCreateDto.ProductId);
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrderHeaders, "PurchaseOrderId", "PurchaseOrderId", purchaseOrderDetailsForCreateDto.PurchaseOrderId);
            return View(purchaseOrderDetailsForCreateDto);
        }

        // GET: PurchaseOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var purchaseOrderDetail = await _serviceContext.PurchaseOrderDetailService.GetPurchaseODById((int)id, true);*/
            var purchaseOrderDetail = await _serviceContext.PurchaseOrderDetailService.GetPurchaseODByIdd((int)id, true);
            if (purchaseOrderDetail == null)
            {
                return NotFound();
            }
            var ship = await _serviceContext.ShipMethodService.GetAllShipMethod(false);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", purchaseOrderDetail.FirstOrDefault().ProductId);
            ViewData["ShipMethodId"] = new SelectList(ship, "ShipMethodId", "Name", ship.FirstOrDefault().ShipMethodId);
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrderHeaders, "PurchaseOrderId", "PurchaseOrderId", purchaseOrderDetail.FirstOrDefault().PurchaseOrderId);
            return View(purchaseOrderDetail);
        }

        // POST: PurchaseOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseOrderId,PurchaseOrderDetailId,DueDate,OrderQty,ProductId,UnitPrice,LineTotal,ReceivedQty,RejectedQty,StockedQty,ModifiedDate")] PurchaseOrderDetailsDto purchaseOrderDetailsDto,PurchaseOrderHeaderDto purchaseOrderHeaderDto, ShipMethodDto shipMethodDto)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    _serviceContext.PurchaseOrderDetailService.Edit(purchaseOrderDetailsDto);
                    /*_serviceContext.PurchaseOrderDetailService.Edit(purchaseOrderDetailsDto);*/
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!PurchaseOrderDetailExists(purchaseOrderDetail.PurchaseOrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }*/
                    throw;
                }
                return RedirectToAction(nameof(Edit));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", purchaseOrderDetailsDto.ProductId);
            ViewData["ShipMethodId"] = new SelectList(_context.PurchaseOrderHeaders.Include(s => s.ShipMethod), "ShipMethodId", "Name", purchaseOrderDetailsDto.PurchaseOrder.ShipMethod.Name);
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrderHeaders, "PurchaseOrderId", "PurchaseOrderId", purchaseOrderDetailsDto.PurchaseOrderId);
            return View("Edit");
        }
        public async Task<IActionResult> EditPurchase(int id, PurchaseOrderDetailsDto purchaseOrderDetailsDto, PurchaseOrderHeaderDto purchaseOrderHeaderDto, ShipMethodDto shipMethodDto)
        {
            if (id != purchaseOrderDetailsDto.PurchaseOrderId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var purchaseOrderDetail = await _serviceContext.PurchaseOrderDetailService.GetPurchaseODById((int)id, true);
                var purchaseOrderHeader = await _serviceContext.PurchaseOrderHeaderService.GetPurchaseOHById((int)id, true);
                var shipMethod = await _serviceContext.ShipMethodService.GetShipMethodById((int)id, true);
                _serviceContext.PurchaseOrderDetailService.Edit(purchaseOrderDetail);
                return RedirectToAction(nameof(Edit));
            }
            var ship = await _serviceContext.ShipMethodService.GetAllShipMethod(false);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", purchaseOrderDetailsDto.ProductId);
            ViewData["ShipMethodId"] = new SelectList(ship, "ShipMethodId", "Name", ship.FirstOrDefault().ShipMethodId);
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrderHeaders, "PurchaseOrderId", "PurchaseOrderId", purchaseOrderDetailsDto.PurchaseOrderId);
            return View("Edit");
        }

        // GET: PurchaseOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var purchaseOrderDetail = await _context.PurchaseOrderDetails
                .Include(p => p.Product)
                .Include(p => p.PurchaseOrder)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);*/
            var purchaseOrderDetail = await _serviceContext.PurchaseOrderDetailService.GetPurchaseODById((int)id, false);
            if (purchaseOrderDetail == null)
            {
                return NotFound();
            }

            return View(purchaseOrderDetail);
        }

        // POST: PurchaseOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrderDetail = await _serviceContext.PurchaseOrderDetailService.GetPurchaseODById((int)id, false);
            _serviceContext.PurchaseOrderDetailService.Remove(purchaseOrderDetail);
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderDetailExists(int id)
        {
            return _context.PurchaseOrderDetails.Any(e => e.PurchaseOrderId == id);
        }
    }
}
