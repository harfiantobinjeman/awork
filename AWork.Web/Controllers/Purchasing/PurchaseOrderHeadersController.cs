using AWork.Contracts.Dto.Purchasing;
using AWork.Domain.Base;
using AWork.Domain.Dto.Purchasing;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services;
using AWork.Services.Abstraction;
using AWork.Services.Purchasing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AWork.Web.Controllers.Purchasing
{

    public class PurchaseOrderHeadersController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IPurchasingServiceManager _serviceContext;
        private readonly IHRRepositoryManager _humanResourceRepository;
        private readonly IPurchasingRepositoryManager _purchasingRepository;
        private readonly UserManager<User> _userManager;
        private readonly IPersonServiceManager _personServiceManager;

        public PurchaseOrderHeadersController(AdventureWorks2019Context context, IPurchasingServiceManager serviceContext, IHRRepositoryManager repositoryManager, IPurchasingRepositoryManager purchasingRepository, UserManager<User> userManager, IPersonServiceManager personServiceManager)
        {
            _context = context;
            _serviceContext = serviceContext;
            _humanResourceRepository = repositoryManager;
            _purchasingRepository = purchasingRepository;
            _userManager = userManager;
            _personServiceManager = personServiceManager;
        }

        // GET: PurchaseOrderHeaders
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? page, int? fetchSize)
        {
           
            var pageIndex = page ?? 1;
            var pageSize = fetchSize ?? 10;
            var purchaseOHDto = await _serviceContext.PurchaseOrderHeaderService.GetPurchaseOHPaged(pageIndex, pageSize, false);
            var purchase = await _serviceContext.PurchaseOrderHeaderService.GetAllPurchaseOH(false);
            /*var result = _purchasingRepository.PurchaseOrderHeaderRepository.GetPurchaseOHAllItem().Count();
            var con = Convert.ToInt16(result);*/
            
            // pake select di rawsql
            var totalRows = purchase.Count();
            var purchasePaged = new StaticPagedList<PurchaseOrderHeaderDto>(purchaseOHDto, pageIndex, pageSize - (pageSize - 1), (totalRows/pageSize)+1);
            return View(purchasePaged);
        }

        // GET: PurchaseOrderHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var purchaseOrderHeader = await _context.PurchaseOrderHeaders
                .Include(p => p.Employee)
                .Include(p => p.ShipMethod)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);*/
            var purchaseOHDto = await _serviceContext.PurchaseOrderHeaderService.GetPurchaseOHById((int)id, false);
            if (purchaseOHDto == null)
            {
                return NotFound();
            }

            return View(purchaseOHDto);
        }
        public async Task<JsonResult> GetData(int shipMethodId)
        {
            var result = await _purchasingRepository.PurchaseOrderHeaderRepository.GetData(shipMethodId);
            return Json(result);
        }
        public JsonResult CartPurchaseItems(int purchaseOrderId)
        {
            var result = _purchasingRepository.PurchaseOrderHeaderRepository.CartPurchasesItem(purchaseOrderId);
            return Json(result);
        }

        [HttpPost]
        public IActionResult PostPurchase([FromBody] CartPurchaseItemDto cartPurchaseItemDto, int purchaseOrderId)
        {
            var purchaseOD = cartPurchaseItemDto.PurchaseOrderDetailsDtoss;
            foreach (var item in purchaseOD)
            {
                item.ModifiedDate = DateTime.Now;
                item.DueDate = DateTime.Now;
            }
            var purchaseOH = cartPurchaseItemDto.PurchaseOrderHeaderDtoss;
            purchaseOH.ModifiedDate = DateTime.Now;
            purchaseOH.OrderDate = DateTime.Now;
            purchaseOH.ShipDate = DateTime.Now.AddDays(3);
            purchaseOH.Status = 2;
            _serviceContext.PurchaseOrderHeaderService.EditPurchaseOH(purchaseOH,purchaseOD);
            /*var purchaseOH = purchaseOrderHeaderDto;
            var purchaseOD = purchaseOrderDetailsDto;*/
            var result = new JsonResult(cartPurchaseItemDto)
            {
                Value = "Succeed"
            };
            return result;

        }

        [HttpPost]
        public async Task<IActionResult> PostReject([FromBody] PurchaseOrderHeaderDto purchaseOrderHeaderDto, int shipMethodId)
        {
            purchaseOrderHeaderDto.Status = 3;
            purchaseOrderHeaderDto.ModifiedDate = DateTime.Now;
            purchaseOrderHeaderDto.OrderDate = DateTime.Now;
            purchaseOrderHeaderDto.ShipDate = DateTime.Now.AddDays(3);
            _serviceContext.PurchaseOrderHeaderService.Edit(purchaseOrderHeaderDto);
            var result = new JsonResult(purchaseOrderHeaderDto)
            {
                Value = "Succeed"
            };
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostShip([FromBody] CartPurchaseItemDto cartPurchaseItemDto, int shipMethodId)
        {
            var purchaseOD = cartPurchaseItemDto.PurchaseOrderDetailsDtoss;
            foreach (var item in purchaseOD)
            {
                item.ModifiedDate = DateTime.Now;
                item.DueDate = DateTime.Now;
            }
            var purchaseOH = cartPurchaseItemDto.PurchaseOrderHeaderDtoss;
            purchaseOH.ModifiedDate = DateTime.Now;
            purchaseOH.OrderDate = DateTime.Now;
            purchaseOH.ShipDate = DateTime.Now.AddDays(3);
            purchaseOH.Status = 4;
            _serviceContext.PurchaseOrderHeaderService.EditPurchaseOH(purchaseOH, purchaseOD);
            /*var purchaseOH = purchaseOrderHeaderDto;
            var purchaseOD = purchaseOrderDetailsDto;*/
            var result = new JsonResult(cartPurchaseItemDto)
            {
                Value = "Succeed"
            };
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCart addToCart)
        {
            if (ModelState.IsValid)
            {
                var myUser = await _userManager.GetUserAsync(User);
                var email = await _personServiceManager.EmailAddressServices.GetEmailAddress(myUser.Email, false);
                int bussinessEntityId = email.BusinessEntityId;
                var empId = Convert.ToString(bussinessEntityId);
                var cekEmp = await _serviceContext.PurchaseOrderHeaderService.FilterEmpId(bussinessEntityId, false);
                if (cekEmp.Count() == 0)
                {
                    var purchase = new PurchaseOrderHeaderForCreateDto
                    {
                        EmployeeId = bussinessEntityId,
                        VendorId = addToCart.BusinessEntityId,
                        ShipDate = DateTime.Now.AddDays(3),
                        Status = 1,
                        ShipMethodId = 1,
                        ModifiedDate = DateTime.Now,
                        RevisionNumber = 1,
                        SubTotal = 100,
                        TaxAmt = 5,
                        Freight = 10,
                        TotalDue = 500,
                        OrderDate = DateTime.Now
                    };
                    var purchaseDetailCreate = new List<PurchaseOrderDetailsForCreateDto>();
                        var purchaseOD = new PurchaseOrderDetailsForCreateDto
                        {
                            ProductId = addToCart.ProductId,
                            OrderQty = 1,
                            UnitPrice = 10,
                            DueDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            LineTotal = 100,
                        };
                        purchaseDetailCreate.Add(purchaseOD);
                    _serviceContext.PurchaseOrderHeaderService.CreatePurchaseHeaderDetail(purchase, purchaseDetailCreate);
                }
                else
                {
                    var purchase = new PurchaseOrderHeaderForCreateDto
                    {
                        EmployeeId = bussinessEntityId,
                        VendorId = addToCart.BusinessEntityId,
                        ShipDate = DateTime.Now.AddDays(3),
                        Status = 1,
                        ShipMethodId = 1,
                        ModifiedDate = DateTime.Now,
                        RevisionNumber = 1,
                        SubTotal = 100,
                        TaxAmt = 5,
                        Freight = 10,
                        TotalDue = 500,
                        OrderDate = DateTime.Now
                    };
                    var purchaseDetailCreate = new List<PurchaseOrderDetailsForCreateDto>();
                        var purchaseOD = new PurchaseOrderDetailsForCreateDto
                        {
                            ProductId = addToCart.ProductId,
                            OrderQty = 1,
                            UnitPrice = 10,
                            DueDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            LineTotal = 100,
                        };
                        purchaseDetailCreate.Add(purchaseOD);
                    _serviceContext.PurchaseOrderHeaderService.CreatePurchaseHeaderDetail(purchase, purchaseDetailCreate);
                }

            }
            return RedirectToAction("Index", "ProductVendors");
        }
        // GET: PurchaseOrderHeaders/Create
        public async Task<IActionResult> Create()
        {
            var personEmployee = await _humanResourceRepository.EmployeeRepository.GetPersonByType(false);
            ViewData["BusinessEntityId"] = new SelectList(from x in personEmployee.ToList() select new { ID = x.BusinessEntityId, Fullname = x.FirstName + " " + x.MiddleName + " " + x.LastName }, "ID", "Fullname");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "BusinessEntityId", "BusinessEntityId");
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "Name");
            ViewData["VendorId"] = new SelectList(_context.Vendors, "BusinessEntityId", "Name");
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrderHeaders, "PurchaseOrderId", "PurchaseOrderId");
            return View();
        }

        // POST: PurchaseOrderHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseOrderId,RevisionNumber,Status,EmployeeId,VendorId,ShipMethodId,OrderDate,ShipDate,SubTotal,TaxAmt,Freight,TotalDue,ModifiedDate")] PurchaseOrderHeaderForCreateDto purchaseOrderHeaderForCreateDto)
        {
            if (ModelState.IsValid)
            {
                purchaseOrderHeaderForCreateDto.ModifiedDate = DateTime.Now;
                purchaseOrderHeaderForCreateDto.OrderDate = DateTime.Now;

                /*_context.Add(purchaseOrderHeader);
                await _context.SaveChangesAsync();*/
                _serviceContext.PurchaseOrderHeaderService.Insert(purchaseOrderHeaderForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            var personEmployee = await _humanResourceRepository.EmployeeRepository.GetPersonByType(false);
            ViewData["BusinessEntityId"] = new SelectList(from x in personEmployee.ToList() select new { ID = x.BusinessEntityId, Fullname = x.FirstName + " " + x.MiddleName + " " + x.LastName }, "ID", "Fullname");
            ViewData["EmployeeId"] = new SelectList(_context.Employees ,"BusinessEntityId", "BusinessEntityId", purchaseOrderHeaderForCreateDto.EmployeeId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "Name", purchaseOrderHeaderForCreateDto.ShipMethodId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "BusinessEntityId", "Name", purchaseOrderHeaderForCreateDto.VendorId);
            ViewData["PurchaseOrderId"] = new SelectList(_context.PurchaseOrderHeaders, "PurchaseOrderId", "PurchaseOrderId");
            return View(purchaseOrderHeaderForCreateDto);
        }

        // GET: PurchaseOrderHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var purchaseOrderHeader = await _context.PurchaseOrderHeaders.FindAsync(id);*/
           var purchaseOrderHeader = await _serviceContext.PurchaseOrderHeaderService.GetPurchaseOHById((int)id, true);
            if (purchaseOrderHeader == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "BusinessEntityId", "BusinessEntityId", purchaseOrderHeader.EmployeeId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "Name", purchaseOrderHeader.ShipMethodId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "BusinessEntityId", "Name", purchaseOrderHeader.VendorId);
            
            return View(purchaseOrderHeader);
        }

        // POST: PurchaseOrderHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PurchaseOrderHeaderDto purchaseOrderHeaderDto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(purchaseOrderHeaderDto);
                    await _context.SaveChangesAsync();*/
                    _serviceContext.PurchaseOrderHeaderService.Edit(purchaseOrderHeaderDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!PurchaseOrderHeaderExists(purchaseOrderHeaderDto.PurchaseOrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                    }*/
                    throw;
                }
                return RedirectToAction(nameof(Edit));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "BusinessEntityId", "BusinessEntityId", purchaseOrderHeaderDto.EmployeeId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "Name", purchaseOrderHeaderDto.ShipMethodId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "BusinessEntityId", "Name", purchaseOrderHeaderDto.VendorId);
            return View("Edit");
        }
        public async Task<IActionResult> EditPurchase(int id, PurchaseOrderHeaderDto purchaseOrderHeaderDto)
        {

            if (ModelState.IsValid)
            {
                var purchaseOH = purchaseOrderHeaderDto;
                var purchaseOD = new List<PurchaseOrderDetailsDto>();
                foreach (var item in purchaseOH.GetPurchaseOD)
                {
                    if(item.Product.Name!= null)
                    {
                        var purchaseODEdit = new PurchaseOrderDetailsDto
                        {
                            PurchaseOrderId = item.PurchaseOrderId,
                            PurchaseOrderDetailId = item.PurchaseOrderDetailId,
                            OrderQty = item.OrderQty,
                            UnitPrice = item.UnitPrice,
                            LineTotal = item.LineTotal,
                            DueDate = item.DueDate,
                            ModifiedDate = item.ModifiedDate,
                            ProductId = item.ProductId
                        };
                        purchaseOD.Add(purchaseODEdit);
                    }

                }
                var purchaseOHEdit = new PurchaseOrderHeaderDto
                {
                    PurchaseOrderId = purchaseOH.PurchaseOrderId,
                    SubTotal = purchaseOH.SubTotal,
                    Status = purchaseOH.Status,
                    Freight = purchaseOH.Freight,
                    EmployeeId = purchaseOH.EmployeeId,
                    VendorId = purchaseOH.VendorId,
                    ShipDate = purchaseOH.ShipDate,
                    ShipMethodId = purchaseOH.ShipMethod.ShipMethodId,
                    TotalDue = purchaseOH.TotalDue,
                    ModifiedDate = purchaseOH.ModifiedDate,
                    OrderDate = purchaseOH.OrderDate,
                    RevisionNumber = purchaseOH.RevisionNumber,
                    TaxAmt = purchaseOH.TaxAmt,
                };
                _serviceContext.PurchaseOrderHeaderService.EditPurchaseOH(purchaseOHEdit, purchaseOD);
                
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", purchaseOrderHeaderDto.EmployeeId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "Name", purchaseOrderHeaderDto.ShipMethodId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "BusinessEntityId", "AccountNumber", purchaseOrderHeaderDto.VendorId);
            return RedirectToAction(nameof(Index));
        }


        // GET: PurchaseOrderHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var purchaseOrderHeader = await _context.PurchaseOrderHeaders
                .Include(p => p.Employee)
                .Include(p => p.ShipMethod)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);*/
            var purchaseOrderHeader = await _serviceContext.PurchaseOrderHeaderService.GetPurchaseOHById((int)id, false);
            if (purchaseOrderHeader == null)
            {
                return NotFound();
            }

            return View(purchaseOrderHeader);
        }

        // POST: PurchaseOrderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var purchaseOrderHeader = await _context.PurchaseOrderHeaders.FindAsync(id);
            _context.PurchaseOrderHeaders.Remove(purchaseOrderHeader);
            await _context.SaveChangesAsync();*/
            var purchaseOHModel = await _serviceContext.PurchaseOrderHeaderService.GetPurchaseOHById((int)id, false);
            _serviceContext.PurchaseOrderHeaderService.Remove(purchaseOHModel);
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderHeaderExists(int id)
        {
            return _context.PurchaseOrderHeaders.Any(e => e.PurchaseOrderId == id);
        }
    }
}
