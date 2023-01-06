using AWork.Domain.Base;
using AWork.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.Sales
{
    public class CurrencyRatesController : Controller
    {
        //private readonly AdventureWorks2019Context _context;
        private readonly ISalesRepositoryManager _context;

        public CurrencyRatesController(ISalesRepositoryManager context)
        {
            _context = context;
        }

        // GET: CurrencyRates
        public async Task<IActionResult> Index()
        {
            return View(await _context.CurrencyRateRepository.GetAllAsync(false));
        }

        // GET: CurrencyRates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var currencyRate = await _context.C
                .Include(c => c.FromCurrencyCodeNavigation)
                .Include(c => c.ToCurrencyCodeNavigation)
                .FirstOrDefaultAsync(m => m.CurrencyRateId == id);*/
            var currencyRate = await _context.CurrencyRateRepository.GetByIdAsync((int)id, false);
            if (currencyRate == null)
            {
                return NotFound();
            }

            return View(currencyRate);
        }

        // GET: CurrencyRates/Create
        public IActionResult Create()
        {
            /*ViewData["FromCurrencyCode"] = new SelectList(_context.Currencies, "CurrencyCode", "CurrencyCode");
            ViewData["ToCurrencyCode"] = new SelectList(_context.Currencies, "CurrencyCode", "CurrencyCode");*/
            return View();
        }

        // POST: CurrencyRates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurrencyRateId,CurrencyRateDate,FromCurrencyCode,ToCurrencyCode,AverageRate,EndOfDayRate,ModifiedDate")] CurrencyRate currencyRate)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(currencyRate);
                await _context.SaveChangesAsync();*/
                _context.CurrencyRateRepository.Insert(currencyRate);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }/*
            ViewData["FromCurrencyCode"] = new SelectList(_context.Currencies, "CurrencyCode", "CurrencyCode", currencyRate.FromCurrencyCode);
            ViewData["ToCurrencyCode"] = new SelectList(_context.Currencies, "CurrencyCode", "CurrencyCode", currencyRate.ToCurrencyCode);*/
            return View(currencyRate);
        }

        // GET: CurrencyRates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var currencyRate = await _context.CurrencyRates.FindAsync(id);*/
            var currencyRate = await _context.CurrencyRateRepository.GetByIdAsync((int)id, true);
            if (currencyRate == null)
            {
                return NotFound();
            }
            /*ViewData["FromCurrencyCode"] = new SelectList(_context.Currencies, "CurrencyCode", "CurrencyCode", currencyRate.FromCurrencyCode);
            ViewData["ToCurrencyCode"] = new SelectList(_context.Currencies, "CurrencyCode", "CurrencyCode", currencyRate.ToCurrencyCode);*/
            return View(currencyRate);
        }

        // POST: CurrencyRates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurrencyRateId,CurrencyRateDate,FromCurrencyCode,ToCurrencyCode,AverageRate,EndOfDayRate,ModifiedDate")] CurrencyRate currencyRate)
        {
            if (id != currencyRate.CurrencyRateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(currencyRate);
                    await _context.SaveChangesAsync();*/
                    _context.CurrencyRateRepository.Change(currencyRate);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!CurrencyRateExists(currencyRate.CurrencyRateId))
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
            /*ViewData["FromCurrencyCode"] = new SelectList(_context.Currencies, "CurrencyCode", "CurrencyCode", currencyRate.FromCurrencyCode);
            ViewData["ToCurrencyCode"] = new SelectList(_context.Currencies, "CurrencyCode", "CurrencyCode", currencyRate.ToCurrencyCode);*/
            return View(currencyRate);
        }

        // GET: CurrencyRates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var currencyRate = await _context.CurrencyRates
                .Include(c => c.FromCurrencyCodeNavigation)
                .Include(c => c.ToCurrencyCodeNavigation)
                .FirstOrDefaultAsync(m => m.CurrencyRateId == id);*/
            var currencyRate = await _context.CurrencyRateRepository.GetByIdAsync((int)id, false);
            if (currencyRate == null)
            {
                return NotFound();
            }

            return View(currencyRate);
        }

        // POST: CurrencyRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var currencyRate = await _context.CurrencyRates.FindAsync(id);
            _context.CurrencyRates.Remove(currencyRate);
            await _context.SaveChangesAsync();*/
            var currencyRate = await _context.CurrencyRateRepository.GetByIdAsync((int)id, false);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        /*private bool CurrencyRateExists(int id)
        {
            return _context.CurrencyRates.Any(e => e.CurrencyRateId == id);
        }*/
    }
}
