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
using AWork.Contracts.Dto.Sales.SpecialOffer;
using X.PagedList;
using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using Microsoft.CodeAnalysis;
using AWork.Domain.Base;

namespace AWork.Web.Controllers.Sales
{
    public class SpecialOffersServiceController : Controller
    {
        private readonly ISalesRepositoryManager _repositoryManager;
        private readonly ISalesServiceManager _serviceContext;
        private readonly IProductionServiceManager _productServiceContext;

        public SpecialOffersServiceController(ISalesServiceManager serviceContext, IProductionServiceManager productServiceContext, ISalesRepositoryManager repositoryManager)
        {
            _serviceContext = serviceContext;
            _productServiceContext = productServiceContext;
            _repositoryManager = repositoryManager;
        }


        // GET: SpecialOffersService
        public async Task<IActionResult> Index(string searchString,
            string currentFilter, string sortOrder, int? page, int? pageSize)
        {
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaultSize = (pageSize ?? 5);
            ViewBag.psize = defaultSize;

            //keep state searching value
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var specialOfferDtos = await _serviceContext.SpecialOfferService.GetAllSpecialOffer(false);
            IPagedList<SpecialOfferDto> specialOfferDto = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                specialOfferDtos = specialOfferDtos.
                    Where(s => s.Description.ToLower().Contains(searchString.ToLower()) ||
                               s.Type.ToLower().Contains(searchString.ToLower()) ||
                               s.Category.ToLower().Contains(searchString.ToLower()) ||
                               s.DiscountPct.ToString().Contains(searchString.ToLower()) ||
                               s.StartDate.ToString().Contains(searchString.ToLower()) ||
                               s.EndDate.ToString().Contains(searchString.ToLower()) ||
                               s.MaxQty.ToString().Contains(searchString.ToLower()) ||
                               s.MinQty.ToString().Contains(searchString.ToLower()));
            }

            /*ViewBag.PageSize = new SelectList(new List<int> { 10, 20, 30 });*/

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() {Value="5", Text= "5"},
                new SelectListItem() {Value="10", Text= "10"},
                new SelectListItem() {Value="15", Text= "15"},
                new SelectListItem() {Value="20", Text= "20"},
                new SelectListItem() {Value="20", Text= "30"}
            };

            //sorting
            ViewBag.DiscountPct = sortOrder == "DiscountPct" ? "" : "DiscountPct";
            ViewBag.CurrentSort = sortOrder;

            switch (sortOrder)
            {
                case "DiscountPct":
                    specialOfferDtos = specialOfferDtos.OrderByDescending(s => s.DiscountPct);
                    break;
                default:
                    specialOfferDtos = specialOfferDtos.OrderBy(s => s.DiscountPct);
                    break;
            }

            specialOfferDto = specialOfferDtos.ToPagedList(pageIndex, defaultSize);

            return View(specialOfferDto);
        }

        // GET: SpecialOffersService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOffer = await _serviceContext.SpecialOfferService.GetSpecialOfferById((int)id, false);
            
            if (specialOffer == null)
            {
                return NotFound();
            }

            return View(specialOffer);
        }

        // GET: SpecialOffersService/Create
        public async Task<IActionResult> Create()
        {
            /*var product = await _productServiceContext.ProductService.GetAllProduct(false);
            ViewData["ProductId"] = new SelectList(product, "ProductId", "Name");*/

            SpecialOfferForCreateDto special = new SpecialOfferForCreateDto();
            special.SpecialOfferProductForCreateDto.Add(new SpecialOfferProductForCreateDto() { ProductId = 1 });
            
            //ViewData["ProductId"] = new SelectList(await _productServiceContext.ProductService.GetAllProduct(false), "ProductId", "Name");

            return View();
        }

        public async Task<JsonResult> GetFilterProduct(string name)
        {
            var products= await _repositoryManager.SpecialOfferRepository.GetProductByName(name,false);
            return Json(products);
        }

        // POST: SpecialOffersService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialOfferForCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                _serviceContext.SpecialOfferService.Insert(dto);
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        // GET: SpecialOffersService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOffer = await _serviceContext.SpecialOfferService.GetSpecialOfferById((int)id, false);
            
            if (specialOffer == null)
            {
                return NotFound();
            }
            return View(specialOffer);
        }

        // POST: SpecialOffersService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialOfferId,Description,DiscountPct,Type,Category,StartDate,EndDate,MinQty,MaxQty,Rowguid,ModifiedDate")] SpecialOfferDto specialOffer)
        {
            if (id != specialOffer.SpecialOfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(specialOffer);
                    await _context.SaveChangesAsync();*/
                    _serviceContext.SpecialOfferService.Edit(specialOffer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!SpecialOfferExists(specialOffer.SpecialOfferId))
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
            return View(specialOffer);
        }

        // GET: SpecialOffersService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var specialOffer = await _context.SpecialOffers
                .FirstOrDefaultAsync(m => m.SpecialOfferId == id);*/
            var specialOffer = await _serviceContext.SpecialOfferService.GetSpecialOfferById((int)id, false);
            if (specialOffer == null)
            {
                return NotFound();
            }

            return View(specialOffer);
        }

        // POST: SpecialOffersService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var specialOffer = await _context.SpecialOffers.FindAsync(id);
            _context.SpecialOffers.Remove(specialOffer);
            await _context.SaveChangesAsync();*/
            var specialOffer = await _serviceContext.SpecialOfferService.GetSpecialOfferById((int)id, false);
            _serviceContext.SpecialOfferService.Remove(specialOffer);
            return RedirectToAction(nameof(Index));
        }

        /*private bool SpecialOfferExists(int id)
        {
            return _context.SpecialOffers.Any(e => e.SpecialOfferId == id);
        }*/
    }
}
