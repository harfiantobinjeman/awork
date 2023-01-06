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
using AWork.Contracts.Dto.Sales.SpecialOfferProduct;

namespace AWork.Web.Controllers.Sales
{
    public class SpecialOfferProductsServiceController : Controller
    {
        private readonly ISalesServiceManager _serviceContext;

        public SpecialOfferProductsServiceController(ISalesServiceManager serviceContext)
        {
            _serviceContext = serviceContext;
        }

        // GET: SpecialOfferProductsService
        public async Task<IActionResult> Index()
        {
            /*var adventureWorks2019Context = _context.SpecialOfferProducts.Include(s => s.Product).Include(s => s.SpecialOffer);
            return View(await adventureWorks2019Context.ToListAsync());*/
            var specialOfferProductDto = await _serviceContext.SpecialOfferProductService.GetAllSpecialOfferProduct(false);
            return View(specialOfferProductDto);
        }

        // GET: SpecialOfferProductsService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var specialOfferProduct = await _context.SpecialOfferProducts
                .Include(s => s.Product)
                .Include(s => s.SpecialOffer)
                .FirstOrDefaultAsync(m => m.SpecialOfferId == id);*/
            var specialOfferProduct = await _serviceContext.SpecialOfferProductService.GetSpecialOfferProductById((int)id, false);
            if (specialOfferProduct == null)
            {
                return NotFound();
            }

            return View(specialOfferProduct);
        }

        // GET: SpecialOfferProductsService/Create
        public async Task<IActionResult> Create()
        {
            //var allProduct = await _serviceContext.ProductService.GetAllSpecialOffer(false);
            var allSpecialOffer = await _serviceContext.SpecialOfferService.GetAllSpecialOffer(false);
            //ViewData["ProductId"] = new SelectList(product, "ProductId", "Name");
            ViewData["SpecialOfferId"] = new SelectList(allSpecialOffer, "SpecialOfferId", "Category");
            var allSpecialOfferProduct = await _serviceContext.SpecialOfferProductService.GetAllSpecialOfferProduct(false);
            ViewData["ProductId"] = new SelectList(allSpecialOfferProduct, "ProductId", "Product");
            return View();
        }

        // POST: SpecialOfferProductsService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecialOfferId,ProductId,Rowguid,ModifiedDate")] SpecialOfferProductForCreateDto specialOfferProduct)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(specialOfferProduct);
                await _context.SaveChangesAsync();*/
                _serviceContext.SpecialOfferProductService.Insert(specialOfferProduct);
                return RedirectToAction(nameof(Index));
            }
            var allSpecialOffer = await _serviceContext.SpecialOfferService.GetAllSpecialOffer(false);
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", specialOfferProduct.ProductId);
            ViewData["SpecialOfferId"] = new SelectList(allSpecialOffer, "SpecialOfferId", "Category", specialOfferProduct.ProductId);
            return View(specialOfferProduct);
        }

        // GET: SpecialOfferProductsService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            //var specialOfferProduct = await _context.SpecialOfferProducts.FindAsync(id);
            var specialOfferProduct = await _serviceContext.SpecialOfferProductService.GetSpecialOfferProductById((int)id, true);
            if (specialOfferProduct == null)
            {
                return NotFound();
            }
            var allSpecialOffer = await _serviceContext.SpecialOfferService.GetAllSpecialOffer(false);
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", specialOfferProduct.ProductId);
            ViewData["SpecialOfferId"] = new SelectList(allSpecialOffer, "SpecialOfferId", "Category", specialOfferProduct.ProductId);
            return View(specialOfferProduct);
        }

        // POST: SpecialOfferProductsService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialOfferId,ProductId,Rowguid,ModifiedDate")] SpecialOfferProductDto specialOfferProduct)
        {
            if (id != specialOfferProduct.SpecialOfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(specialOfferProduct);
                    await _context.SaveChangesAsync();*/
                    _serviceContext.SpecialOfferProductService.Edit(specialOfferProduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!SpecialOfferProductExists(specialOfferProduct.SpecialOfferId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }*/
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var allSpecialOffer = await _serviceContext.SpecialOfferService.GetAllSpecialOffer(false);
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", specialOfferProduct.ProductId);
            ViewData["SpecialOfferId"] = new SelectList(allSpecialOffer, "SpecialOfferId", "Category", specialOfferProduct.SpecialOfferId);
            return View(specialOfferProduct);
        }

        // GET: SpecialOfferProductsService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var specialOfferProduct = await _context.SpecialOfferProducts
                .Include(s => s.Product)
                .Include(s => s.SpecialOffer)
                .FirstOrDefaultAsync(m => m.SpecialOfferId == id);*/
            var specialOfferProduct = await _serviceContext.SpecialOfferProductService.GetSpecialOfferProductById((int)id, false);
            if (specialOfferProduct == null)
            {
                return NotFound();
            }

            return View(specialOfferProduct);
        }

        // POST: SpecialOfferProductsService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var specialOfferProduct = await _context.SpecialOfferProducts.FindAsync(id);
            _context.SpecialOfferProducts.Remove(specialOfferProduct);
            await _context.SaveChangesAsync();*/
            var specialOfferProduct = await _serviceContext.SpecialOfferProductService.GetSpecialOfferProductById((int)id, false);
            _serviceContext.SpecialOfferProductService.Remove(specialOfferProduct);
            return RedirectToAction(nameof(Index));
        }

        /*private bool SpecialOfferProductExists(int id)
        {
            return _context.SpecialOfferProducts.Any(e => e.SpecialOfferId == id);
        }*/
    }
}
