using AWork.Contracts.Dto.Sales.CreditCard;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using X.PagedList;
using System.Linq;

namespace AWork.Web.Controllers.Sales
{
    public class CreditCardsController : Controller
    {
        /*        private readonly AdventureWorks2019Context _context;
        */
        private readonly ISalesServiceManager _context;

        public CreditCardsController(ISalesServiceManager context )
        {
            _context = context;
        }

        // GET: CreditCards
        public async Task<IActionResult> Index(string searchString, string currentFilter, string sortOrder, int? page, int? pageSize)
        {
            // set page
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            // default size is 5 otherwise take pageSize value
            int defaultSize = (pageSize ?? 5);
            ViewBag.psize = defaultSize;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var creditCardDto = await _context.CreditCardService.GetAllCreditCard(false);
            //IPagedList<CreditCardDto> creditCardDtos = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                creditCardDto = creditCardDto.Where(c =>
                c.CardType.ToLower().Contains(searchString.ToLower()) ||
                c.CreditCardId.ToString().Contains(searchString.ToLower()) ||
                c.ExpMonth.ToString().Contains(searchString.ToLower()) ||
                c.ExpYear.ToString().Contains(searchString.ToLower())
                ) ; 
              
            }

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" }
            };

            // sort data
            ViewBag.Territory = sortOrder == "CardType" ? "" : "CardType";
            ViewBag.CurrentSort = sortOrder;
            switch (sortOrder)
            {
                case "Territory":
                    creditCardDto = creditCardDto.OrderBy(s => s.CardType);
                    break;
                default:
                    creditCardDto = creditCardDto.OrderByDescending(s => s.CardType);
                    break;
            }

            //creditCardDto = creditCardDto.ToPagedList(pageIndex, defaultSize);
            return View(creditCardDto.ToPagedList(pageIndex, defaultSize));
        }

        // GET: CreditCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*    var creditCard = await _context.CreditCards
                    .FirstOrDefaultAsync(m => m.CreditCardId == id);*/
            var creditCard = await _context.CreditCardService.GetAllCreditCardById((int)id, false);
            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // GET: CreditCards/Create
        public async Task<IActionResult> Create()
        {
            var creditCard = await _context.CreditCardService.GetAllCreditCard(false);
            ViewData["CardType"] = new SelectList(creditCard, "CardType", "CardType");
            return View();
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreditCardId,CardType,CardNumber,ExpMonth,ExpYear,ModifiedDate")] CreditCardForCreateDto creditCard)
        {
            if (ModelState.IsValid)
            {
                /*      _context.Add(creditCard);
                      await _context.SaveChangesAsync();*/
                _context.CreditCardService.Insert(creditCard);

                return RedirectToAction(nameof(Index));
            }
            return View(creditCard);
        }

        // GET: CreditCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*            var creditCard = await _context.CreditCardRepository.FindAsync(id);
            */
            var creditCard = await _context.CreditCardService.GetAllCreditCardById((int)id, true);
            if (creditCard == null)
            {
                return NotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreditCardId,CardType,CardNumber,ExpMonth,ExpYear,ModifiedDate")] CreditCardDto creditCard)
        {
            if (id != creditCard.CreditCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*       _context.Update(creditCard);
                           await _context.SaveChangesAsync();*/
                    _context.CreditCardService.Edit(creditCard);
                }
                catch (DbUpdateConcurrencyException)
                {
                    /* if (!CreditCardExists(creditCard.CreditCardId))
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
            return View(creditCard);
        }

        // GET: CreditCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*        var creditCard = await _context.CreditCards
                        .FirstOrDefaultAsync(m => m.CreditCardId == id);*/

            var creditCard = await _context.CreditCardService.GetAllCreditCardById((int)id, false);
            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*   var creditCard = await _context.CreditCards.FindAsync(id);
               _context.CreditCards.Remove(creditCard);
               await _context.SaveChangesAsync();*/
            var creditCard = await _context.CreditCardService.GetAllCreditCardById((int)id, false);
            _context.CreditCardService.Remove(creditCard);
            return RedirectToAction(nameof(Index));
        }

        /* private bool CreditCardExists(int id)
         {
             return _context.CreditCards.Any(e => e.CreditCardId == id);
         }*/
    public async Task<IActionResult> EditCustomer(int? id)
        {
           /* if (ModelState.IsValid)
            {
                var creditCard = new CreditCardDto
                {
                    CardNumber = creditCardDto.CardNumber,
                    CardType = creditCardDto.CardType,
                    CreditCardId = creditCardDto.CreditCardId,
                    ExpMonth = creditCardDto.ExpMonth,
                    ExpYear = creditCardDto.ExpYear,
                    ModifiedDate = DateTime.Now
                };
                _context.CreditCardService.Edit(creditCardDto);
                return RedirectToAction(nameof(Index));
            }
            return View(creditCardDto);*/
           if(id ==null)
            {
                return NotFound();
            }
            var creditCard = await _context.CreditCardService.GetAllCreditCardById((int)id, true);
            if (creditCard==null)
            {
                return NotFound();
            }
            var cardtype = await _context.CreditCardService.GetAllCreditCard(false);
            ViewData["CardType"]=new SelectList(cardtype, "CardType", "CardType",creditCard.CreditCardId);
            return View(cardtype);

        }
    }

}