using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction;
using AWork.Contracts.Dto.Production;
using X.PagedList;

namespace AWork.Web.Controllers.Production
{
    public class BillOfMaterialsController : Controller
    {
        private readonly AdventureWorks2019Context _context;

        private readonly IProductionServiceManager _productionServiceContext;

        public BillOfMaterialsController(AdventureWorks2019Context context, IProductionServiceManager productionServiceContext = null)
        {
            _context = context;
            _productionServiceContext = productionServiceContext;
        }

        // GET: BillOfMaterials
        public async Task<IActionResult> Index(
            string sortOrder, int searchInt,
            int currentFilter, int? page, int? pagesize)
        {
            // set page
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            // default size is 5 otherwise take pageSize value  
            int defaSize = (pagesize ?? 5);
            ViewBag.psize = defaSize;

            if (searchInt != null)
            {
                page = 1;
            }
            else
            {
                searchInt = currentFilter;
            }
            ViewBag.currentFilter = searchInt;


            //var billOfMaterials = await _context.BillOfMaterials.ToListAsync();
            var billOfMaterialsDto = await _productionServiceContext.BillOfMaterialService.GetAllBillOfMaterial(false);
            
           /* if (!int(searchInt))
            {
                billOfMaterialsDto = billOfMaterialsDto.Where(p => p.BillOfMaterialsId.Contains(searchInt)); 
                
            }*/

            //Dropdownlist code for PageSize selection  
            //In View Attach this  
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" }
            };

            return View(billOfMaterialsDto.ToPagedList(pageIndex, defaSize));
        }

        // GET: BillOfMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billOfMaterial = await _productionServiceContext.BillOfMaterialService.GetBillOfMaterialById((int)id, false);
            /*var billOfMaterial = await _context.BillOfMaterials
                .Include(b => b.Component)
                .Include(b => b.ProductAssembly)
                .Include(b => b.UnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.BillOfMaterialsId == id);*/
            if (billOfMaterial == null)
            {
                return NotFound();
            }

            return View(billOfMaterial);
        }

        // GET: BillOfMaterials/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Products, "ProductId", "Name");
            ViewData["ProductAssemblyId"] = new SelectList(_context.Products, "ProductId", "Name");
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode");
            return View();
        }

        // POST: BillOfMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillOfMaterialsId, ProductAssemblyId,ComponentId,EndDate,UnitMeasureCode,Bomlevel")] BillOfMaterialForCreateDto billOfMaterialForCreateDto)
        {
            if (ModelState.IsValid)
            {
                _productionServiceContext.BillOfMaterialService.Insert(billOfMaterialForCreateDto);
                /*_context.Add(billOfMaterial);*/
                /*await _context.SaveChangesAsync();*/
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Products, "ProductId", "Name", billOfMaterialForCreateDto.ComponentId);
            ViewData["ProductAssemblyId"] = new SelectList(_context.Products, "ProductId", "Name", billOfMaterialForCreateDto.ProductAssemblyId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", billOfMaterialForCreateDto.UnitMeasureCode);
            return View(billOfMaterialForCreateDto);
        }

        // GET: BillOfMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var billOfMaterial = await _productionServiceContext.BillOfMaterialService.GetBillOfMaterialById((int)id, true);
            //var billOfMaterial = await _context.BillOfMaterials.FindAsync(id);
            if (billOfMaterial == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Products, "ProductId", "Name", billOfMaterial.ComponentId);
            ViewData["ProductAssemblyId"] = new SelectList(_context.Products, "ProductId", "Name", billOfMaterial.ProductAssemblyId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", billOfMaterial.UnitMeasureCode);
            return View(billOfMaterial);
        }

        // POST: BillOfMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillOfMaterialsId,ProductAssemblyId,ComponentId,StartDate,EndDate,UnitMeasureCode,Bomlevel,PerAssemblyQty,ModifiedDate")] BillOfMaterialDto billOfMaterialDto)
        {
            if (id != billOfMaterialDto.BillOfMaterialsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(billOfMaterial);
                    await _context.SaveChangesAsync();*/
                    _productionServiceContext.BillOfMaterialService.Edit(billOfMaterialDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillOfMaterialExists(billOfMaterialDto.BillOfMaterialsId))
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
            ViewData["ComponentId"] = new SelectList(_context.Products, "ProductId", "Name", billOfMaterialDto.ComponentId);
            ViewData["ProductAssemblyId"] = new SelectList(_context.Products, "ProductId", "Name", billOfMaterialDto.ProductAssemblyId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", billOfMaterialDto.UnitMeasureCode);
            return View(billOfMaterialDto);
        }

        // GET: BillOfMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var billOfMaterial = await _productionServiceContext.BillOfMaterialService.GetBillOfMaterialById((int)id, false);
          /*  var billOfMaterial = await _context.BillOfMaterials
                .Include(b => b.Component)
                .Include(b => b.ProductAssembly)
                .Include(b => b.UnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.BillOfMaterialsId == id);*/
            if (billOfMaterial == null)
            {
                return NotFound();
            }

            return View(billOfMaterial);
        }

        // POST: BillOfMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billOfMaterialModel = await _productionServiceContext.BillOfMaterialService.GetBillOfMaterialById((int)id, false);
            _productionServiceContext.BillOfMaterialService.Remove(billOfMaterialModel);
            /*_context.BillOfMaterials.Remove(billOfMaterial);
            await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool BillOfMaterialExists(int id)
        {
            return _context.BillOfMaterials.Any(e => e.BillOfMaterialsId == id);
        }
    }
}
