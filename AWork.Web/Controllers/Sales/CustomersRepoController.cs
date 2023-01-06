using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using X.PagedList;
using AWork.Services.Abstraction;
using System.Linq;
using AWork.Contracts.Dto.Sales.SalesCustomer;
using AWork.Contracts.Dto.Sales.SalesPerson;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using AWork.Domain.Base;

namespace AWork.Web.Controllers.Sales
{
    public class CustomersRepoController : Controller
    {
        //private readonly AdventureWorks2019Context _context;
        private readonly ISalesServiceManager _contextSalesService;
        private readonly IPersonServiceManager _contextPersonService;
        private readonly ISalesRepositoryManager _salesRepositoryManager;

        public CustomersRepoController(ISalesServiceManager contextSalesService, IPersonServiceManager contextPersonService, ISalesRepositoryManager salesRepositoryManager)
        {
            _contextSalesService = contextSalesService;
            _contextPersonService = contextPersonService;
            _salesRepositoryManager = salesRepositoryManager;
        }



        // GET: CustomersRepo
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? page, int? pageSize, int sortB)
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
            var customersDtos = await _contextSalesService.CustomerService.GetAllCustomer(false);

            /*IPagedList<CustomerDto> customerDtos = null;*/
            var totalRows = customersDtos.Count();
            // search page by product name and company name
            if (!String.IsNullOrEmpty(searchString))
            {
                customersDtos = customersDtos.Where(p =>
                p.CustomerId.ToString().Contains(searchString)||
                p.Person.FirstName.ToLower().Contains(searchString.ToLower()) ||
                p.Person.LastName.ToLower().Contains(searchString.ToLower()) ||
                p.Territory.Name.ToLower().Contains(searchString.ToLower())
                /*p.Store.Name.ToLower().Contains(searchString.ToLower()) */
                );


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


            //Alloting nos. of records as per pagesize and page index.  

            return View(customersDtos.ToPagedList(pageIndex, defaSize));
        }

        // GET: CustomersRepo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var customer = await _context.Customers
                .Include(c => c.Person)
                .Include(c => c.Store)
                .Include(c => c.Territory)
                .FirstOrDefaultAsync(m => m.CustomerId == id);*/
            var customer = await _contextSalesService.CustomerService.GetCustomerById((int)id, false);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: CustomersRepo/Create
        public async Task<IActionResult> Create()
        {
            var cekData = await _contextSalesService.CustomerService.GetPersonCustomer(false);
            ViewData["PersonId"] = new SelectList(from q in cekData.ToList() select new 
            {ID = q.BusinessEntityId,FullName= q.FirstName + " " + q.LastName}, "ID", "FullName");
            ViewData["StoreId"] = new SelectList(await _contextSalesService.StoreService.GetAllAsync(false), "BusinessEntityId", "Name");
            ViewData["TerritoryId"] = new SelectList(await _contextSalesService.SalesTerritoryService.GetAllSalesTerritory(false), "TerritoryId", "TerritoryId");
            return View();
        }

        public async Task<JsonResult> GetData(int businessEntityId)
        {
            var result = await _salesRepositoryManager.CustomerRepository.GetData(businessEntityId);
            return Json(result);
        }

        public async Task<JsonResult> GetDataTerritory(int territoryId)
        {
            var result = await _salesRepositoryManager.CustomerRepository.GetDataTerritories(territoryId);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerForCreateDto customerForCreateDto)
        {
            if (ModelState.IsValid)
            {
                _contextSalesService.CustomerService.Insert(customerForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            return View(customerForCreateDto);
        }

        // POST: CustomersRepo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,PersonId,StoreId,TerritoryId,AccountNumber,Rowguid,ModifiedDate")] CustomerForCreateDto customerForCreateDto)
        {
            if (ModelState.IsValid)
            {
                _contextSalesService.CustomerService.Insert(customerForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName", customer.PersonId);
            //ViewData["StoreId"] = new SelectList(_context.Stores, "BusinessEntityId", "Name", customer.StoreId);
            //ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", customer.TerritoryId);
            return View(customerForCreateDto);
        }

        // GET: CustomersRepo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _contextSalesService.CustomerService.GetCustomerById((int)id, true);
            if (customer == null)
            {
                return NotFound();
            }
            /*ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName", customer.PersonId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "BusinessEntityId", "Name", customer.StoreId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", customer.TerritoryId);*/
            //ViewData["PersonId"] = new SelectList(await _contextPersonService.PersonServices.GetAllPerson(false), "BusinessEntityId", "FirstName",customer.PersonId);
            ViewData["PersonId"] = new SelectList(await _salesRepositoryManager.CustomerRepository.GetPersonCustomer(true), "BusinessEntityId", "FirstName",customer.PersonId);
            ViewData["StoreId"] = new SelectList(await _contextSalesService.StoreService.GetAllAsync(false), "BusinessEntityId", "Name",customer.StoreId);
            ViewData["TerritoryId"] = new SelectList(await _contextSalesService.SalesTerritoryService.GetAllSalesTerritory(false), "TerritoryId", "Name",customer.TerritoryId);
            return View(customer);
        }

        // POST: CustomersRepo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        public async Task<IActionResult> EditCustomer(CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var customer = new CustomerDto
                {
                    CustomerId = customerDto.CustomerId,
                    PersonId = customerDto.PersonId,
                    TerritoryId = customerDto.TerritoryId,
                    StoreId = customerDto.StoreId,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now

                };
                _contextSalesService.CustomerService.Change(customer);   
                return RedirectToAction(nameof(Index));
            }
            return View(customerDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,PersonId,StoreId,TerritoryId,AccountNumber,Rowguid,ModifiedDate")] CustomerDto customerDto)
        {
            if (id != customerDto.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(customer);
                    await _context.SaveChangesAsync();*/
                    _contextSalesService.CustomerService.Change(customerDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!CustomerExists(customer.CustomerId))
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
            /*ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName", customer.PersonId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "BusinessEntityId", "Name", customer.StoreId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", customer.TerritoryId);*/
            return View(customerDto);
        }

        // GET: CustomersRepo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var customer = await _context.Customers
                .Include(c => c.Person)
                .Include(c => c.Store)
                .Include(c => c.Territory)
                .FirstOrDefaultAsync(m => m.CustomerId == id);*/
            var customer = await _contextSalesService.CustomerService.GetCustomerById((int)id, false);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: CustomersRepo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();*/
            var customer = await _contextSalesService.CustomerService.GetCustomerById((int)id, false);
            _contextSalesService.CustomerService.Remove(customer);
            return RedirectToAction(nameof(Index));
        }

        /*private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }*/
    }
}
