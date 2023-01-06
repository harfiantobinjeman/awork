using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWork.Domain.Models;
using AWork.Persistence;

namespace AWork.Web.Controllers.Sales
{
    public class SalesOrderDetailsServiceController : Controller
    {
        private readonly AdventureWorks2019Context _context;

        public SalesOrderDetailsServiceController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: SalesOrderDetailsService
        public async Task<IActionResult> Index()
        {
            var adventureWorks2019Context = _context.SalesOrderDetails.Include(s => s.SalesOrder).Include(s => s.SpecialOfferProduct);
            return View(await adventureWorks2019Context.ToListAsync());
        }

        // GET: SalesOrderDetailsService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderDetail = await _context.SalesOrderDetails
                .Include(s => s.SalesOrder)
                .Include(s => s.SpecialOfferProduct)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }

            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetailsService/Create
        public IActionResult Create()
        {
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeaders, "SalesOrderId", "SalesOrderNumber");
            ViewData["SpecialOfferId"] = new SelectList(_context.SpecialOfferProducts, "SpecialOfferId", "SpecialOfferId");
            return View();
        }

        // POST: SalesOrderDetailsService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderId,SalesOrderDetailId,CarrierTrackingNumber,OrderQty,ProductId,SpecialOfferId,UnitPrice,UnitPriceDiscount,LineTotal,Rowguid,ModifiedDate")] SalesOrderDetail salesOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeaders, "SalesOrderId", "SalesOrderNumber", salesOrderDetail.SalesOrderId);
            ViewData["SpecialOfferId"] = new SelectList(_context.SpecialOfferProducts, "SpecialOfferId", "SpecialOfferId", salesOrderDetail.SpecialOfferId);
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetailsService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderDetail = await _context.SalesOrderDetails.FindAsync(id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeaders, "SalesOrderId", "SalesOrderNumber", salesOrderDetail.SalesOrderId);
            ViewData["SpecialOfferId"] = new SelectList(_context.SpecialOfferProducts, "SpecialOfferId", "SpecialOfferId", salesOrderDetail.SpecialOfferId);
            return View(salesOrderDetail);
        }

        // POST: SalesOrderDetailsService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesOrderId,SalesOrderDetailId,CarrierTrackingNumber,OrderQty,ProductId,SpecialOfferId,UnitPrice,UnitPriceDiscount,LineTotal,Rowguid,ModifiedDate")] SalesOrderDetail salesOrderDetail)
        {
            if (id != salesOrderDetail.SalesOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderDetailExists(salesOrderDetail.SalesOrderId))
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
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeaders, "SalesOrderId", "SalesOrderNumber", salesOrderDetail.SalesOrderId);
            ViewData["SpecialOfferId"] = new SelectList(_context.SpecialOfferProducts, "SpecialOfferId", "SpecialOfferId", salesOrderDetail.SpecialOfferId);
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetailsService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderDetail = await _context.SalesOrderDetails
                .Include(s => s.SalesOrder)
                .Include(s => s.SpecialOfferProduct)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }

            return View(salesOrderDetail);
        }

        // POST: SalesOrderDetailsService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrderDetail = await _context.SalesOrderDetails.FindAsync(id);
            _context.SalesOrderDetails.Remove(salesOrderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderDetailExists(int id)
        {
            return _context.SalesOrderDetails.Any(e => e.SalesOrderId == id);
        }
    }
}
