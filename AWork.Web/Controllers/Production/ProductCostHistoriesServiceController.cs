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

namespace AWork.Web.Controllers.Production
{
    public class ProductCostHistoriesServiceController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceContext;

        public ProductCostHistoriesServiceController(AdventureWorks2019Context context, IProductionServiceManager productionServiceContext)
        {
            _context = context;
            _productionServiceContext = productionServiceContext;
        }



        // GET: ProductCostHistoriesService
        public async Task<IActionResult> Index()
        {
            /* var adventureWorks2019Context = _context.ProductCostHistories.Include(p => p.Product);
             return View(await adventureWorks2019Context.ToListAsync());*/
            var productCH = await _context.ProductCostHistories.ToListAsync();
            var productCHDto = await _productionServiceContext.ProductCostHistoryService.GetAllProductCostHistory(false);
            return View(productCHDto);


        }

        // GET: ProductCostHistoriesService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCostHistory = await _context.ProductCostHistories
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productCostHistory == null)
            {
                return NotFound();
            }

            return View(productCostHistory);
        }

        // GET: ProductCostHistoriesService/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: ProductCostHistoriesService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,StartDate,EndDate,StandardCost,ModifiedDate")] ProductCostHistory productCostHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCostHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productCostHistory.ProductId);
            return View(productCostHistory);
        }

        // GET: ProductCostHistoriesService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCostHistory = await _context.ProductCostHistories.FindAsync(id);
            if (productCostHistory == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productCostHistory.ProductId);
            return View(productCostHistory);
        }

        // POST: ProductCostHistoriesService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,StartDate,EndDate,StandardCost,ModifiedDate")] ProductCostHistory productCostHistory)
        {
            if (id != productCostHistory.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCostHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCostHistoryExists(productCostHistory.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productCostHistory.ProductId);
            return View(productCostHistory);
        }

        // GET: ProductCostHistoriesService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCostHistory = await _context.ProductCostHistories
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productCostHistory == null)
            {
                return NotFound();
            }

            return View(productCostHistory);
        }

        // POST: ProductCostHistoriesService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCostHistory = await _context.ProductCostHistories.FindAsync(id);
            _context.ProductCostHistories.Remove(productCostHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCostHistoryExists(int id)
        {
            return _context.ProductCostHistories.Any(e => e.ProductId == id);
        }
    }
}
