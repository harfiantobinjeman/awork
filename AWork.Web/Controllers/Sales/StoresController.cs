using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using AWork.Services.Abstraction;
using X.PagedList;
using AWork.Contracts.Dto.Sales.Store;
using AWork.Contracts.Dto.Sales.SalesCustomer;

namespace AWork.Web.Controllers.Sales
{
    public class StoresController : Controller
    {
        //private readonly AdventureWorks2019Context _context;
        private readonly ISalesServiceManager _salesServiceManager;
        private readonly IPersonServiceManager _personServiceManager;

        public StoresController(ISalesServiceManager salesServiceManager, IPersonServiceManager personServiceManager)
        {
            _salesServiceManager = salesServiceManager;
            _personServiceManager = personServiceManager;
        }


        // GET: Stores
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? page, int? pageSize,string sortOrder)
        {
            // set page
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            // default size is 5 otherwise take pageSize value  
            int defaSize = (pageSize ?? 10);
            ViewBag.psize = defaSize;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            //Create a instance of our DataContext  
            var storesDtos = await _salesServiceManager.StoreService.GetAllAsync(false);

            var totalRows = storesDtos.Count();

            // search page by store name
            if (!String.IsNullOrEmpty(searchString))
            {
                storesDtos = storesDtos.Where(p =>
                p.BusinessEntityId.ToString().Contains(searchString) ||
                p.Name.ToLower().Contains(searchString.ToLower())
                );
            }

            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "store_name" : " ";

            var storeForSort = from s in storesDtos select s;

            switch (sortOrder)
            {
                case "store_name":
                    storeForSort = storeForSort.OrderBy(s => s.Name);
                    break;
                default:
                    storeForSort = storesDtos;
                    break;
            }

            //Dropdownlist code for PageSize selection  
            //In View Attach this  
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value="100", Text= "100" }
            };


            storesDtos = storeForSort.ToPagedList(pageIndex, defaSize);
            //Alloting nos. of records as per pagesize and page index.  
            return View(storesDtos);
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var store = await _context.Stores
                .Include(s => s.BusinessEntity)
                .Include(s => s.SalesPerson)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);*/
            var store = await _salesServiceManager.StoreService.GetCurrencyByCode((int)id, false);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create

        public async Task<IActionResult> Create()
        {
            /*ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId");
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId");*/
            ViewData["BusinessEntityId"] = new SelectList(await _salesServiceManager.StoreService.GetAllAsync(false), "BusinessEntityId", "BusinessEntityId");
            ViewData["SalesPersonId"] = new SelectList(await _salesServiceManager.SalesPersonService.GetAllSalesPerson(false), "BusinessEntityId", "BusinessEntityId");

            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public async Task<IActionResult> CreateStore(StoreForCreateDto storeForCreateDto)
        {
            if (ModelState.IsValid)
            {
                var businessEntity = _personServiceManager.BusinessEntityServices.CreateBusinessEntity();
                storeForCreateDto.BusinessEntityId = businessEntity.BusinessEntityId;
                _salesServiceManager.StoreService.Insert(storeForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            return View(storeForCreateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,Name,SalesPersonId,Demographics,Rowguid,ModifiedDate")] StoreForCreateDto storeForCreateDto)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(store);
                await _context.SaveChangesAsync();*/
                _salesServiceManager.StoreService.Insert(storeForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            /*ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", store.BusinessEntityId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", store.SalesPersonId);*/
            return View(storeForCreateDto);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var store = await _context.Stores.FindAsync(id);
            var store = await _salesServiceManager.StoreService.GetCurrencyByCode((int)id, true);
            if (store == null)
            {
                return NotFound();
            }
            /*ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", store.BusinessEntityId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", store.SalesPersonId);*/
            ViewData["BusinessEntityId"] = new SelectList(await _salesServiceManager.StoreService.GetAllAsync(false), "BusinessEntityId", "BusinessEntityId");
            ViewData["SalesPersonId"] = new SelectList(await _salesServiceManager.SalesPersonService.GetAllSalesPerson(false), "BusinessEntityId", "BusinessEntityId");
            return View(store);
        }

        [HttpPost]
        public async Task<IActionResult> EditStore(StoreDto storeDto)
        {
            if (ModelState.IsValid)
            {
                var store = new StoreDto
                {
                    BusinessEntityId = storeDto.BusinessEntityId,
                    Name = storeDto.Name,   
                    SalesPersonId = storeDto.SalesPersonId,
                    Demographics = null,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                    
                };
                _salesServiceManager.StoreService.Change(store);
                return RedirectToAction(nameof(Index));
            }
            return View(storeDto);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,Name,SalesPersonId,Demographics,Rowguid,ModifiedDate")] Store store)
        {
            if (id != store.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
            }
            /*ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", store.BusinessEntityId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", store.SalesPersonId);*/
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var store = await _salesServiceManager.StoreService.GetCurrencyByCode((int)id, false);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();*/
            var store = await _salesServiceManager.StoreService.GetCurrencyByCode((int)id, false);
            _salesServiceManager.StoreService.Remove(store);
            return RedirectToAction(nameof(Index));
        }

        /*private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.BusinessEntityId == id);
        }*/
    }
}
