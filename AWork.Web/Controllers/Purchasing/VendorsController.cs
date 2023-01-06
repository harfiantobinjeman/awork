using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Purchasing;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services.Abstraction;
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
    public class VendorsController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IPurchasingServiceManager _serviceContext;
        private readonly IProductionServiceManager _serviceProduction;
        private readonly IPurchasingRepositoryManager _purchasingRepositoryManager;

        public VendorsController(AdventureWorks2019Context context, IPurchasingServiceManager serviceContext, IProductionServiceManager serviceProduction, IPurchasingRepositoryManager purchasingRepositoryManager)
        {
            _context = context;
            _serviceContext = serviceContext;
            _serviceProduction = serviceProduction;
            _purchasingRepositoryManager = purchasingRepositoryManager;
        }

        // GET: Vendors
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? page, int? fetchSize)
        {
            var pageIndex = page ?? 1;
            var pageSize = fetchSize ?? 5;
            ViewBag.pageSize = pageSize;
            //keep state searching value
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var vendor = await _serviceContext.VendorService.GetVendorPaged(pageIndex, pageSize, false);

            var allVendors = await _serviceContext.VendorService.GetAllVendor(false);
            var totalVendor = allVendors.Count();
            var totalRows = Math.Ceiling((double)totalVendor / pageSize);

            if (!String.IsNullOrEmpty(searchString))
            {
                vendor = allVendors.Where(p => p.AccountNumber.ToLower().Contains(searchString.ToLower()) || p.Name.ToLower().Contains(searchString.ToLower()));
                if (vendor.Count() != null)
                {
                    totalRows = 1;
                }
            }
            /*     ViewBag.currentFilter = searchString;*/
            var vendorDtoPaged = new StaticPagedList<VendorDto>(vendor, pageIndex, pageSize - (pageSize - 1), (int)totalRows);

            ViewBag.PagedList = new SelectList(new List<int> { 8, 15, 20 });

            return View(vendorDtoPaged);

            /*var vendorDto = await _serviceContext.VendorService.GetAllVendor(false);
            return View(vendorDto);*/
        }

        // GET: Vendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var vendor = await _context.Vendors
                .Include(v => v.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);*/
            var vendorDto = await _serviceContext.VendorService.GetVendorById((int)id, false);
            if (vendorDto == null)
            {
                return NotFound();
            }

            return View(vendorDto);
        }

        // GET: Vendors/Create
        public async Task<IActionResult> Create()
        {
            var unitMeansure = await _serviceProduction.UnitMeasureservice.GetAllUnitMeasure(false);
            ViewData["UnitMeasureCode"] = new SelectList(unitMeansure, "UnitMeasureCode", "UnitMeasureCode");
            /*var product = await _serviceContext.ProductVendorService.GetAllProductVendor(false);*/
            var product = await _serviceProduction.ProductService.GetAllProduct(false);
            ViewData["ProductId"] = new SelectList(product, "ProductId", "Name");

            VendorForCreateDto vendor = new VendorForCreateDto();
            vendor.ProductVendorForCreateDtos.Add(new ProductVendorForCreateDto() { ProductId = 1});
            return View(vendor);
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendorForCreateDto vendorForCreateDto)
        {
            if (ModelState.IsValid)
            {
                _serviceContext.VendorService.Insert(vendorForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", productVendorGroupDto.BusinessEntityId);
            return View(vendorForCreateDto);
        }

        // GET: Vendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var vendor = await _context.Vendors.FindAsync(id);*/
            var vendor = await _serviceContext.VendorService.GetVendorById((int)id, true);
            if (vendor == null)
            {
                return NotFound();
            }
            var unitMeansure = await _serviceProduction.UnitMeasureservice.GetAllUnitMeasure(false);
            ViewData["UnitMeasureCode"] = new SelectList(unitMeansure, "UnitMeasureCode", "UnitMeasureCode");
            var product = await _serviceProduction.ProductService.GetAllProduct(false);
            ViewData["ProductId"] = new SelectList(product, "ProductId", "Name");

            vendor.ProductVendorForCreateDtos.Add(new ProductVendorForCreateDto() { ProductId = 1 });

            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VendorDto vendorDto)
        {
            if (id != vendorDto.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(vendor);
                    await _context.SaveChangesAsync();*/
                    _serviceContext.VendorService.Edit(vendorDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendorDto.BusinessEntityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", vendorDto.BusinessEntityId);
            return View(vendorDto);
        }

        // GET: Vendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var vendor = await _context.Vendors
                .Include(v => v.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);*/
            var vendor = await _serviceContext.VendorService.GetVendorById((int)id, false);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var vendor = await _context.Vendors.FindAsync(id);
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();*/
            var vendorModel = await _serviceContext.VendorService.GetVendorById((int)id, false);
            _serviceContext.VendorService.Remove(vendorModel);
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.BusinessEntityId == id);
        }
        [HttpPost]
        public async Task<JsonResult> GetVendor(string name)
        {
            var vendor = await _purchasingRepositoryManager.VendorRepository.FindVendorByName(name, false);
            return Json(vendor);
        }
    }
}
