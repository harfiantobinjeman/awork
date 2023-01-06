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
    public class ProductProductPhotoesServiceController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceManager;

        public ProductProductPhotoesServiceController(AdventureWorks2019Context context, IProductionServiceManager productionServiceManager)
        {
            _context = context;
            _productionServiceManager = productionServiceManager;
        }

        // GET: ProductProductPhotoesService
        public async Task<IActionResult> Index()
        {
            //var productProductPhoto = await _context.ProductProductPhotos.ToListAsync();
            var productProductPhotoDto = await _productionServiceManager.ProductProductPhotoService.GetAllProductProductPhoto(false);
            return View(productProductPhotoDto);
            //var adventureWorks2019Context = _context.ProductProductPhotos.Include(p => p.Product).Include(p => p.ProductPhoto);
            //return View(await adventureWorks2019Context.ToListAsync());
        }

        // GET: ProductProductPhotoesService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var productProductPhoto = await _context.ProductProductPhotos
            var productProductPhotoDto = await _productionServiceManager.ProductProductPhotoService.GetProductProductPhotoById((int)id, false);
            //.Include(p => p.Product)
            //var locationDto = await _productionServiceManager.LocationService.GetAllLocation(false);
            //var location = await _productionServiceManager.LocationService.GetLocationById((short)id, false);
            //.Include(p => p.ProductPhoto)
            //.FirstOrDefaultAsync(m => m.ProductId == id);
            if (productProductPhotoDto == null)
            {
                return NotFound();
            }

            return View(productProductPhotoDto);
        }

        // GET: ProductProductPhotoesService/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            ViewData["ProductPhotoId"] = new SelectList(_context.ProductPhotos, "ProductPhotoId", "LargePhotoFileName");
            return View();
        }

        // POST: ProductProductPhotoesService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductPhotoId,Primary")] ProductProductPhotoForCreateDto productProductPhoto)
        {
            if (ModelState.IsValid)
            {
                _productionServiceManager.ProductProductPhotoService.Insert(productProductPhoto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productProductPhoto.ProductId);
            ViewData["ProductPhotoId"] = new SelectList(_context.ProductPhotos, "ProductPhotoId", "LargePhotoFileName", productProductPhoto.ProductPhotoId);
            return View(productProductPhoto);
        }
        //aaaaaswppp
        // GET: ProductProductPhotoesService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var productProductPhoto = await _context.ProductProductPhotos.FindAsync(id);
            var productProductPhoto = await _productionServiceManager.ProductProductPhotoService.GetProductProductPhotoById((int)id, true);
            if (productProductPhoto == null)
            {
                return NotFound();
            }
   
            ViewData["ProductPhotoId"] = new SelectList(_context.ProductPhotos, "ProductPhotoId", "LargePhotoFileName", productProductPhoto.ProductPhoto.LargePhotoFileName);
            return View(productProductPhoto);
        }

        // POST: ProductProductPhotoesService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductPhotoId,Primary,ModifiedDate")] ProductProductPhotoDto productProductPhoto)
        {
            if (id != productProductPhoto.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(productProductPhoto);
                    await _context.SaveChangesAsync();*/
                    _productionServiceManager.ProductProductPhotoService.Edit(productProductPhoto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!ProductProductPhotoExists(productProductPhoto.ProductId))
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

            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productProductPhoto.ProductId);
            ViewData["ProductPhotoId"] = new SelectList(_context.ProductPhotos, "ProductPhotoId", "ProductPhotoId", productProductPhoto.ProductPhotoId);
            return View(productProductPhoto);
        }

        // GET: ProductProductPhotoesService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var productProductPhoto = await _context.ProductProductPhotos
            var productProductPhoto = await _productionServiceManager.ProductProductPhotoService.GetProductProductPhotoById((int)id, false);
            /*.Include(p => p.Product)
            .Include(p => p.ProductPhoto)
            .FirstOrDefaultAsync(m => m.ProductId == id);*/
            if (productProductPhoto == null)
            {
                return NotFound();
            }

            return View(productProductPhoto);
        }

        // POST: ProductProductPhotoesService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var productProductPhoto = await _context.ProductProductPhotos.FindAsync(id);
            _context.ProductProductPhotos.Remove(productProductPhoto);
            await _context.SaveChangesAsync();*/
            var productProductPhoto1 = await _productionServiceManager.ProductProductPhotoService.GetProductProductPhotoById((int)id, false);
            _productionServiceManager.ProductProductPhotoService.Remove(productProductPhoto1);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductProductPhotoExists(int id)
        {
            return _context.ProductProductPhotos.Any(e => e.ProductId == id);
        }
    }
}
