using AWork.Domain.Base;
using AWork.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWork.Web.Controllers
{
    public class SpecialOffersController : Controller
    {
        private readonly ISalesRepositoryManager _context;

        public SpecialOffersController(ISalesRepositoryManager context)
        {
            _context = context;
        }

        // GET: SpecialOffers
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpecialOfferRepository.GetAllSpecialOffer(false));
        }

        // GET: SpecialOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOffer = await _context.SpecialOfferRepository.GetSpecialOfferById((int)id, false);
            if (specialOffer == null)
            {
                return NotFound();
            }

            return View(specialOffer);
        }

        // GET: SpecialOffers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpecialOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecialOfferId,Description,DiscountPct,Type,Category,StartDate,EndDate,MinQty,MaxQty,Rowguid,ModifiedDate")] SpecialOffer specialOffer)
        {
            if (ModelState.IsValid)
            {
                _context.SpecialOfferRepository.Insert(specialOffer);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialOffer);
        }

        // GET: SpecialOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOffer = await _context.SpecialOfferRepository.GetSpecialOfferById((int)id, true);
            if (specialOffer == null)
            {
                return NotFound();
            }
            return View(specialOffer);
        }

        // POST: SpecialOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialOfferId,Description,DiscountPct,Type,Category,StartDate,EndDate,MinQty,MaxQty,Rowguid,ModifiedDate")] SpecialOffer specialOffer)
        {
            if (id != specialOffer.SpecialOfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.SpecialOfferRepository.Edit(specialOffer);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(specialOffer);
        }

        // GET: SpecialOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOffer = await _context.SpecialOfferRepository.GetSpecialOfferById((int)id, false);
            if (specialOffer == null)
            {
                return NotFound();
            }

            return View(specialOffer);
        }

        // POST: SpecialOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialOffer = await _context.SpecialOfferRepository.GetSpecialOfferById((int)id, false);
            _context.SpecialOfferRepository.Remove(specialOffer);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
