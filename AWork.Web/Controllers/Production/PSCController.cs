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

namespace AWork.Web.Controllers.Production
{
    public class PSCController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceContext;

        public PSCController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: PSC
        public async Task<IActionResult> Index()
        {
            var adventureWorks2019Context = _context.ProductSubcategories.Include(p => p.ProductCategory);
            return View(await adventureWorks2019Context.ToListAsync());
        }

        // GET: PSC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSubcategory = await _context.ProductSubcategories
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.ProductSubcategoryId == id);
            if (productSubcategory == null)
            {
                return NotFound();
            }

            return View(productSubcategory);
        }

        // GET: PSC/Create
        public async Task<IActionResult> Create()
        {
            var allCate = await _productionServiceContext.ProductCategoryService
                .GetAllProdcCategory(false);

            ViewData["ProductCategoryId"] = new SelectList(allCate, "ProductCategoryId", "ProductCategoryId");
            return View();
           /* ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name");
            return View();*/
        }

        // POST: PSC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductSubcategoryId,ProductCategoryId,Name,Rowguid,ModifiedDate")] ProductSubCategoryForCreateDto subcate)
        {
            if (ModelState.IsValid)
            {
                
                 _productionServiceContext.ProductSubCategoryService.Insert(subcate);
                return RedirectToAction(nameof(Index));

            }
            var allCate = await _productionServiceContext.ProductCategoryService.GetAllProdcCategory(false);
            ViewData["ProductCategoryId"] = new SelectList(allCate, "ProductCategoryId","Name");
           
            return View();
        }

        // GET: PSC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSubcategory = await _context.ProductSubcategories.FindAsync(id);
            if (productSubcategory == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", productSubcategory.ProductCategoryId);
            return View(productSubcategory);
        }

        // POST: PSC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductSubcategoryId,ProductCategoryId,Name,Rowguid,ModifiedDate")] ProductSubcategory productSubcategory)
        {
            if (id != productSubcategory.ProductSubcategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSubcategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSubcategoryExists(productSubcategory.ProductSubcategoryId))
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
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", productSubcategory.ProductCategoryId);
            return View(productSubcategory);
        }

        // GET: PSC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSubcategory = await _context.ProductSubcategories
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.ProductSubcategoryId == id);
            if (productSubcategory == null)
            {
                return NotFound();
            }

            return View(productSubcategory);
        }

        // POST: PSC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSubcategory = await _context.ProductSubcategories.FindAsync(id);
            _context.ProductSubcategories.Remove(productSubcategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSubcategoryExists(int id)
        {
            return _context.ProductSubcategories.Any(e => e.ProductSubcategoryId == id);
        }
    }
}
