using AWork.Domain.Base;
using AWork.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.Sales
{
    public class CurrenciesController : Controller
    {
        //private readonly AdventureWorks2019Context _context;
        private readonly ISalesRepositoryManager _context;
        public CurrenciesController(ISalesRepositoryManager context)
        {
            _context = context;
        }

        // GET: Currencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.CurrencyRepository.GetAllAsync(false));
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.CurrencyCode == id);*/
            var currency = await _context.CurrencyRepository.GetCurrencyByCode((string)id, false);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: Currencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurrencyCode,Name,ModifiedDate")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(currency);
                await _context.SaveChangesAsync();*/
                _context.CurrencyRepository.Insert(currency);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var currency = await _context.Currencies.FindAsync(id);
            var currency = await _context.CurrencyRepository.GetCurrencyByCode((string)id, true);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CurrencyCode,Name,ModifiedDate")] Currency currency)
        {
            if (id != currency.CurrencyCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(currency);
                    await _context.SaveChangesAsync();*/
                    _context.CurrencyRepository.Change(currency);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!CurrencyExists(currency.CurrencyCode))
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
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.CurrencyCode == id);*/
            var currency = await _context.CurrencyRepository.GetCurrencyByCode((string)id, false);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            /*var currency = await _context.Currencies.FindAsync(id);
            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();*/
            var currency = await _context.CurrencyRepository.GetCurrencyByCode((string)id, false);
            _context.CurrencyRepository.Remove(currency);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        /*private bool CurrencyExists(string id)
        {
            return _context.Currencies.Any(e => e.CurrencyCode == id);
        }*/
    }
}
