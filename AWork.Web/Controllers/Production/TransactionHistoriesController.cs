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
    public class TransactionHistoriesController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceManager;

        public TransactionHistoriesController(AdventureWorks2019Context context, IProductionServiceManager productionServiceManager = null)
        {
            _context = context;
            _productionServiceManager = productionServiceManager;
        }

        // GET: TransactionHistories
        public async Task<IActionResult> Index()
        {
            /*var adventureWorks2019Context = _context.TransactionHistories.Include(t => t.Product);
            return View(await adventureWorks2019Context.ToListAsync());*/
            //var locationDto = await _productionServiceManager.LocationService.GetAllLocation(false);
            var transaktionDto = await _productionServiceManager.TransactionHistoryService.GetAllTransaction(false);
            return View(transaktionDto);
        }

        // GET: TransactionHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var transactionHistory = await _context.TransactionHistories
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.TransactionId == id);*/
            var transactionHistory = await _productionServiceManager.TransactionHistoryService.GetTransactionHistoryById((int)id, false);
            if (transactionHistory == null)
            {
                return NotFound();
            }

            return View(transactionHistory);
        }

        // GET: TransactionHistories/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: TransactionHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,ProductId,ReferenceOrderId,ReferenceOrderLineId,TransactionDate," +
            "TransactionType,Quantity,ActualCost,ModifiedDate")] TransactionHistoryForCreateDto transactionHistoryForCreateDto)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(transactionHistory);
                await _context.SaveChangesAsync();*/
                //_productionServiceManager.LocationService.Insert(locationForCreateDto);
                _productionServiceManager.TransactionHistoryService.Insert(transactionHistoryForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", transactionHistoryForCreateDto.ProductId);
            return View(transactionHistoryForCreateDto);
        }

        // GET: TransactionHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var transactionHistory = await _context.TransactionHistories.FindAsync(id);
            var transactionHistory = await _productionServiceManager.TransactionHistoryService.GetTransactionHistoryById((int)id, false);
            if (transactionHistory == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", transactionHistory.ProductId);
            return View(transactionHistory);
        }

        // POST: TransactionHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,ProductId,ReferenceOrderId,ReferenceOrderLineId," +
            "TransactionDate,TransactionType,Quantity,ActualCost,ModifiedDate")] TransactionHistoryDto transactionHistoryDto)
        {
            if (id != transactionHistoryDto.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(transactionHistory);
                    await _context.SaveChangesAsync();*/
                    //_productionServiceManager.LocationService.Edit(locationDto);
                    _productionServiceManager.TransactionHistoryService.Edit(transactionHistoryDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionHistoryExists(transactionHistoryDto.TransactionId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", transactionHistoryDto.ProductId);
            return View(transactionHistoryDto);
        }

        // GET: TransactionHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var transactionHistory = await _context.TransactionHistories
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.TransactionId == id);*/
            /*var locationGet = await _productionServiceManager.LocationService
                .GetLocationById((short)id, false);*/
            var transactionHistory = await _productionServiceManager.TransactionHistoryService
                .GetTransactionHistoryById((int)id, false);
            if (transactionHistory == null)
            {
                return NotFound();
            }

            return View(transactionHistory);
        }

        // POST: TransactionHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var transactionHistory = await _context.TransactionHistories.FindAsync(id);
            _context.TransactionHistories.Remove(transactionHistory);
            await _context.SaveChangesAsync();*/
            var transactionModel = await _productionServiceManager.TransactionHistoryService
                .GetTransactionHistoryById(id, false);
            _productionServiceManager.TransactionHistoryService.Remove(transactionModel);
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionHistoryExists(int id)
        {
            return _context.TransactionHistories.Any(e => e.TransactionId == id);
        }
    }
}
