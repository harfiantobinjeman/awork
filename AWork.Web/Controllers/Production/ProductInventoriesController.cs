using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Contracts.Dto.Production;
using AWork.Services.Abstraction;

namespace AWork.Web.Controllers.Production
{
    public class ProductInventoriesController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceManager;

        public ProductInventoriesController(AdventureWorks2019Context context, IProductionServiceManager productionServiceManager = null)
        {
            _context = context;
            _productionServiceManager = productionServiceManager;
        }

        // GET: ProductInventories
        public async Task<IActionResult> Index()
        {
            /*var adventureWorks2019Context = _context.ProductInventories.Include(p => p.Location).Include(p => p.Product);
            return View(await adventureWorks2019Context.ToListAsync());*/

            var inventoryDto = await _productionServiceManager.ProductInventoryService.GetAllInventory(false);
            return View(inventoryDto);
        }

        // GET: ProductInventories/Details/5
        public async Task<IActionResult> Details(int? id, int ? locationId)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var productInventory = await _context.ProductInventories
                .Include(p => p.Location)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);*/
            //var location = await _productionServiceManager.LocationService.GetLocationById((short)id, false);
            var productInventory = await _productionServiceManager.ProductInventoryService.GetInventoryById((int)id, (int) locationId, false);
            if (productInventory == null)
            {
                return NotFound();
            }

            return View(productInventory);
        }

        // GET: ProductInventories/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: ProductInventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,LocationId,Shelf,Bin,Quantity,Rowguid,ModifiedDate")] ProductInventoryDtoForCreate inventoryDtoForCreate)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(productInventoryDto);
                await _context.SaveChangesAsync();*/
                /* _productionServiceManager.LocationService.Insert(locationForCreateDto);
                 return RedirectToAction(nameof(Index));*/
                _productionServiceManager.ProductInventoryService.Insert(inventoryDtoForCreate);
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", inventoryDtoForCreate.LocationId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", inventoryDtoForCreate.ProductId);
            return View(inventoryDtoForCreate);
        }

        // GET: ProductInventories/Edit/5
        public async Task<IActionResult> Edit(int? id, int? locationId)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var location = await _productionServiceManager.LocationService.GetLocationById((short)id, true);
            //var productInventory = await _context.ProductInventories.FindAsync(id);
            var productInventory = await _productionServiceManager.ProductInventoryService.GetInventoryById((int)id, (int) locationId, true);
            if (productInventory == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", productInventory.LocationId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productInventory.ProductId);
            return View(productInventory);
        }

        // POST: ProductInventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,LocationId,Shelf,Bin,Quantity,Rowguid,LocationId")] ProductInventoryDto productInventoryDto)
        {
            if (id != productInventoryDto.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(productInventoryDto);
                    await _context.SaveChangesAsync();*/
                    //_productionServiceManager.ProductInventoryService.Edit(productInventoryDto);
                    var shelf = productInventoryDto.Shelf;
                    var bin = productInventoryDto.Bin;
                    var quantity = productInventoryDto.Quantity;
                    var rowguid = productInventoryDto.Rowguid;
                    var locationid = productInventoryDto.LocationId;
                    var edit = new ProductInventoryDto()
                    {
                        ProductId = id,
                        Shelf = shelf,
                        Bin = bin,
                        Quantity = quantity,
                        Rowguid = rowguid,
                        ModifiedDate = DateTime.Now
                    };
                    _productionServiceManager.ProductInventoryService.Edit(edit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInventoryExists(productInventoryDto.ProductId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", productInventoryDto.LocationId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productInventoryDto.ProductId);
            return View(productInventoryDto);
        }

        // GET: ProductInventories/Delete/5
        public async Task<IActionResult> Delete(int? id, int? locationId)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var productInventory = await _context.ProductInventories
                .Include(p => p.Location)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);*/
            //var locationGet = await _productionServiceManager.LocationService
            //.GetLocationById((short)id, false);
            var productInventory = await _productionServiceManager.ProductInventoryService
            .GetInventoryById((int)id, (int) locationId, false);
            if (productInventory == null)
            {
                return NotFound();
            }

            return View(productInventory);
        }

        // POST: ProductInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int locationId)
        {
            /*var productInventory = await _context.ProductInventories.FindAsync(id);
            _context.ProductInventories.Remove(productInventory);
            await _context.SaveChangesAsync();*/
            /*var locationModel = await _productionServiceManager.LocationService
                .GetLocationById(id, false);
            _productionServiceManager.LocationService.Remove(locationModel);*/

            var productInventory = await _productionServiceManager.ProductInventoryService
                .GetInventoryById(id, locationId, true);
            _productionServiceManager.ProductInventoryService.Remove(productInventory);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInventoryExists(int id)
        {
            return _context.ProductInventories.Any(e => e.ProductId == id);
        }
    }
}
