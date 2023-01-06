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
    public class ProductReviewServiceController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceContext;

        public ProductReviewServiceController(AdventureWorks2019Context context, IProductionServiceManager productionServiceContext)
        {
            _context = context;
            _productionServiceContext = productionServiceContext;
        }

        // GET: ProductReviewService
        public async Task<IActionResult> Index()
        {
            //var adventureWorks2019Context = _context.ProductReviews.Include(p => p.Product);
            var productReviewDto = await _productionServiceContext.ProductReviewService.GetAllProductReview(false);

            return View(productReviewDto);

        }

        // GET: ProductReviewService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var productReview = await _context.ProductReviews
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductReviewId == id);*/
            var productReview = await _productionServiceContext.ProductReviewService.GetProductReviewById((int)id, false);
            if (productReview == null)
            {
                return NotFound();
            }

            return View(productReview);
        }

        // GET: ProductReviewService/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: ProductReviewService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ReviewerName,ReviewDate,EmailAddress,Rating,Comments,ModifiedDate")] ProductReviewForCreateDto productReviewFC)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(productReview);
                await _context.SaveChangesAsync();*/
                _productionServiceContext.ProductReviewService.Insert(productReviewFC);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productReviewFC.ProductId);
            return View(productReviewFC);
        }

        // GET: ProductReviewService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var productReview = await _context.ProductReviews.FindAsync(id);
            var productReview = await _productionServiceContext.ProductReviewService.GetProductReviewById((int)id,true);
            if (productReview == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productReview.ProductId);
            return View(productReview);
        }

        // POST: ProductReviewService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductReviewId,ProductId,ReviewerName,ReviewDate,EmailAddress,Rating,Comments,ModifiedDate")] ProductReviewDto productReview)
        {
            if (id != productReview.ProductReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productionServiceContext.ProductReviewService.Edit(productReview);
                    /*_context.Update(productReview);
                    await _context.SaveChangesAsync();*/
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductReviewExists(productReview.ProductReviewId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productReview.ProductId);
            return View(productReview);
        }

        // GET: ProductReviewService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReview = await _context.ProductReviews
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductReviewId == id);
            if (productReview == null)
            {
                return NotFound();
            }

            return View(productReview);
        }

        // POST: ProductReviewService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productReview = await _context.ProductReviews.FindAsync(id);
            _context.ProductReviews.Remove(productReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductReviewExists(int id)
        {
            return _context.ProductReviews.Any(e => e.ProductReviewId == id);
        }
    }
}
