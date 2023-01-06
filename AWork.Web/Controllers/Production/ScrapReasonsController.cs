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
    public class ScrapReasonsController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceManager;
        public ScrapReasonsController(AdventureWorks2019Context context, IProductionServiceManager productionServiceManager = null)
        {
            _context = context;
            _productionServiceManager = productionServiceManager;
        }

        // GET: ScrapReasons
        public async Task<IActionResult> Index()
        {
            //return View(await _context.ScrapReasons.ToListAsync());

            var scrapView = await _productionServiceManager.ScrapReasonService.GetAllScrapReason(false);
            return View(scrapView);
        }

        // GET: ScrapReasons/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var scrapReason = await _context.ScrapReasons
                .FirstOrDefaultAsync(m => m.ScrapReasonId == id);*/
            var scrapReason = await _productionServiceManager.ScrapReasonService.GetLocationById((short)id, false);
            if (scrapReason == null)
            {
                return NotFound();
            }

            return View(scrapReason);
        }

        // GET: ScrapReasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScrapReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScrapReasonId,Name,ModifiedDate")] ScrapReasonForCreateDto scrapReasonForCreateDto)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(scrapReasonForCreateDto);
                await _context.SaveChangesAsync();*/
                _productionServiceManager.ScrapReasonService.Insert(scrapReasonForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            return View(scrapReasonForCreateDto);
        }

        // GET: ScrapReasons/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var scrapReason = await _context.ScrapReasons.FindAsync(id);
            var scrapReason = await _productionServiceManager.ScrapReasonService.GetLocationById((short)id, false);
            if (scrapReason == null)
            {
                return NotFound();
            }
            return View(scrapReason);
        }

        // POST: ScrapReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ScrapReasonId,Name,ModifiedDate")] ScrapReasonDto scrapReasonDto)
        {
            if (id != scrapReasonDto.ScrapReasonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(scrapReason);
                    await _context.SaveChangesAsync();*/
                    _productionServiceManager.ScrapReasonService.Edit(scrapReasonDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScrapReasonExists(scrapReasonDto.ScrapReasonId))
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
            return View(scrapReasonDto);
        }

        // GET: ScrapReasons/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var scrapReason = await _context.ScrapReasons
                .FirstOrDefaultAsync(m => m.ScrapReasonId == id);*/
            var scrapReason = await _productionServiceManager.ScrapReasonService
                .GetLocationById((short)id, false);
            if (scrapReason == null)
            {
                return NotFound();
            }

            return View(scrapReason);
        }

        // POST: ScrapReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            /*var scrapReason = await _context.ScrapReasons.FindAsync(id);
            _context.ScrapReasons.Remove(scrapReason);
            await _context.SaveChangesAsync();*/
            var scrapModel = await _productionServiceManager.ScrapReasonService
                .GetLocationById(id, false);
            _productionServiceManager.ScrapReasonService.Remove(scrapModel);
            return RedirectToAction(nameof(Index));
        }

        private bool ScrapReasonExists(short id)
        {
            return _context.ScrapReasons.Any(e => e.ScrapReasonId == id);
        }
    }
}
