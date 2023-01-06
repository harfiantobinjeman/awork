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
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using AWork.Contracts.Dto.Sales.PersonCreditCard;
using X.PagedList;
using AWork.Contracts.Dto.Sales.CreditCard;

namespace AWork.Web.Controllers.Sales
{
    public class SalesOrderHeadersServiceController : Controller
    {
        /* private readonly AdventureWorks2019Context _context;*/
        private readonly ISalesServiceManager _serviceContext;
        private readonly IPersonServiceManager _person;
        private readonly AdventureWorks2019Context _dbContext;

        public SalesOrderHeadersServiceController(ISalesServiceManager serviceContext, IPersonServiceManager person, AdventureWorks2019Context dbContext)
        {
            _serviceContext = serviceContext;
            _person = person;
            _dbContext = dbContext;
        }


        // GET: SalesOrderHeadersService
        /*   public async Task<IActionResult> Index()
           {
              *//* var adventureWorks2019Context = _context.SalesOrderHeaders.Include(s => s.BillToAddress).Include(s => s.CreditCard).Include(s => s.CurrencyRate).Include(s => s.Customer).Include(s => s.SalesPerson).Include(s => s.ShipMethod).Include(s => s.ShipToAddress).Include(s => s.Territory);
               return View(await adventureWorks2019Context.ToListAsync());*//*
              var saleOrderHeaderDto = await _serviceContext.SalesOrderHeaderService.GetAllSalesOrderHeader(false);
               return View(saleOrderHeaderDto);
           }*/

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

            var salesorderheaderDto = await _serviceContext.SalesOrderHeaderService.GetAllSalesOrderHeader(false);
            //IPagedList<CreditCardDto> creditCardDtos = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                salesorderheaderDto = salesorderheaderDto.Where(c =>
                c.SalesOrderNumber.ToLower().Contains(searchString.ToLower()) ||
                c.Status.ToString().Contains(searchString.ToLower()) 
               
                );

            }

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" }
            };

            // sort data
            ViewBag.Territory = sortOrder == "Status" ? "" : "Status";
            ViewBag.CurrentSort = sortOrder;
            switch (sortOrder)
            {
                case "Territory":
                    salesorderheaderDto = salesorderheaderDto.OrderBy(s => s.Status);
                    break;
                default:
                    salesorderheaderDto = salesorderheaderDto.OrderByDescending(s => s.Status);
                    break;
            }

            //creditCardDto = creditCardDto.ToPagedList(pageIndex, defaultSize);
            return View(salesorderheaderDto.ToPagedList(pageIndex, defaultSize));
        }


        // GET: SalesOrderHeadersService/Details/5
        [HttpPost]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

     /*       var salesOrderHeader = await _serviceContext.SalesOrderHeaderService
                .Include(s => s.BillToAddress)
                .Include(s => s.CreditCard)
                .Include(s => s.CurrencyRate)
                .Include(s => s.Customer)
                .Include(s => s.SalesPerson)
                .Include(s => s.ShipMethod)
                .Include(s => s.ShipToAddress)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);*/
            var salesOrderHeader = await _serviceContext.SalesOrderHeaderService.GetSalesOrderHeaderById((int)id, false);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return View(salesOrderHeader);
        }

        // GET: SalesOrderHeadersService/Create
        public IActionResult Create()
        {
           /* ViewData["BillToAddressId"] = new SelectList(_serviceContext.Addresses, "AddressId", "AddressLine1");
            ViewData["CreditCardId"] = new SelectList(_serviceContext.CreditCards, "CreditCardId", "CardNumber");
            ViewData["CurrencyRateId"] = new SelectList(_serviceContext.CurrencyRates, "CurrencyRateId", "FromCurrencyCode");
            ViewData["CustomerId"] = new SelectList(_serviceContext.Customers, "CustomerId", "AccountNumber");
            ViewData["SalesPersonId"] = new SelectList(_serviceContext.SalesPeople, "BusinessEntityId", "BusinessEntityId");
            ViewData["ShipMethodId"] = new SelectList(_serviceContext.ShipMethods, "ShipMethodId", "Name");
            ViewData["ShipToAddressId"] = new SelectList(_serviceContext.Addresses, "AddressId", "AddressLine1");
            ViewData["TerritoryId"] = new SelectList(_serviceContext.SalesTerritories, "TerritoryId", "CountryRegionCode");*/
            return View();
        }

        // POST: SalesOrderHeadersService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderId,RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,SalesOrderNumber,PurchaseOrderNumber,AccountNumber,CustomerId,SalesPersonId,TerritoryId,BillToAddressId,ShipToAddressId,ShipMethodId,CreditCardId,CreditCardApprovalCode,CurrencyRateId,SubTotal,TaxAmt,Freight,TotalDue,Comment,Rowguid,ModifiedDate")] SalesOrderHeaderForCreateDto salesOrderHeader)
        {
            if (ModelState.IsValid)
            {
                /*                _context.Add(salesOrderHeader);
                                await _context.SaveChangesAsync();*/
                _serviceContext.SalesOrderHeaderService.Insert(salesOrderHeader);
                return RedirectToAction(nameof(Index));
            }
         /*   ViewData["BillToAddressId"] = new SelectList(_serviceContext.Addresses, "AddressId", "AddressLine1", salesOrderHeader.BillToAddressId);
            ViewData["CreditCardId"] = new SelectList(_serviceContext.CreditCards, "CreditCardId", "CardNumber", salesOrderHeader.CreditCardId);
            ViewData["CurrencyRateId"] = new SelectList(_serviceContext.CurrencyRates, "CurrencyRateId", "FromCurrencyCode", salesOrderHeader.CurrencyRateId);
            ViewData["CustomerId"] = new SelectList(_serviceContext.Customers, "CustomerId", "AccountNumber", salesOrderHeader.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_serviceContext.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesOrderHeader.SalesPersonId);
            ViewData["ShipMethodId"] = new SelectList(_serviceContext.ShipMethods, "ShipMethodId", "Name", salesOrderHeader.ShipMethodId);
            ViewData["ShipToAddressId"] = new SelectList(_serviceContext.Addresses, "AddressId", "AddressLine1", salesOrderHeader.ShipToAddressId);
            ViewData["TerritoryId"] = new SelectList(_serviceContext.SalesTerritories, "TerritoryId", "CountryRegionCode", salesOrderHeader.TerritoryId);*/
            return View(salesOrderHeader);
        }

        // GET: SalesOrderHeadersService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //   var salesOrderHeader = await _context.SalesOrderHeaders.FindAsync(id);
            var salesOrderHeader = await _serviceContext.SalesOrderHeaderService.GetSalesOrderHeaderById((int)id, false);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }
          /*  ViewData["BillToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressLine1", salesOrderHeader.BillToAddressId);
            ViewData["CreditCardId"] = new SelectList(_context.CreditCards, "CreditCardId", "CardNumber", salesOrderHeader.CreditCardId);
            ViewData["CurrencyRateId"] = new SelectList(_context.CurrencyRates, "CurrencyRateId", "FromCurrencyCode", salesOrderHeader.CurrencyRateId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "AccountNumber", salesOrderHeader.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesOrderHeader.SalesPersonId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "Name", salesOrderHeader.ShipMethodId);
            ViewData["ShipToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressLine1", salesOrderHeader.ShipToAddressId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", salesOrderHeader.TerritoryId);*/
            return View(salesOrderHeader);
        }

        // POST: SalesOrderHeadersService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesOrderId,RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,SalesOrderNumber,PurchaseOrderNumber,AccountNumber,CustomerId,SalesPersonId,TerritoryId,BillToAddressId,ShipToAddressId,ShipMethodId,CreditCardId,CreditCardApprovalCode,CurrencyRateId,SubTotal,TaxAmt,Freight,TotalDue,Comment,Rowguid,ModifiedDate")] SalesOrderHeaderDto salesOrderHeaderDto)
        {
            if (id != salesOrderHeaderDto.SalesOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /* _serviceContext.Update(salesOrderHeader);
                     await _serviceContext.SaveChangesAsync();*/
                    _serviceContext.SalesOrderHeaderService.Edit(salesOrderHeaderDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    /* if (!SalesOrderHeaderExists(salesOrderHeader.SalesOrderId))
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
           /* ViewData["BillToAddressId"] = new SelectList(_serviceContext.Addresses, "AddressId", "AddressLine1", salesOrderHeader.BillToAddressId);
            ViewData["CreditCardId"] = new SelectList(_serviceContext.CreditCards, "CreditCardId", "CardNumber", salesOrderHeader.CreditCardId);
            ViewData["CurrencyRateId"] = new SelectList(_serviceContext.CurrencyRates, "CurrencyRateId", "FromCurrencyCode", salesOrderHeader.CurrencyRateId);
            ViewData["CustomerId"] = new SelectList(_serviceContext.Customers, "CustomerId", "AccountNumber", salesOrderHeader.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_serviceContext.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesOrderHeader.SalesPersonId);
            ViewData["ShipMethodId"] = new SelectList(_serviceContext.ShipMethods, "ShipMethodId", "Name", salesOrderHeader.ShipMethodId);
            ViewData["ShipToAddressId"] = new SelectList(_serviceContext.Addresses, "AddressId", "AddressLine1", salesOrderHeader.ShipToAddressId);
            ViewData["TerritoryId"] = new SelectList(_serviceContext.SalesTerritories, "TerritoryId", "CountryRegionCode", salesOrderHeader.TerritoryId);*/
            return View(salesOrderHeaderDto);

        }

        // GET: SalesOrderHeadersService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
                        var salesOrderHeader = await _serviceContext.SalesOrderHeaders
                            .Include(s => s.BillToAddress)
                            .Include(s => s.CreditCard)
                            .Include(s => s.CurrencyRate)
                            .Include(s => s.Customer)
                            .Include(s => s.SalesPerson)
                            .Include(s => s.ShipMethod)
                            .Include(s => s.ShipToAddress)
                            .Include(s => s.Territory)
                            .FirstOrDefaultAsync(m => m.SalesOrderId == id);*/
            var salesOrderHeader = await _serviceContext.SalesOrderHeaderService.GetSalesOrderHeaderById((int)id, false);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return View(salesOrderHeader);
        }

        // POST: SalesOrderHeadersService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*  var salesOrderHeader = await _context.SalesOrderHeaders.FindAsync(id);
              _serviceContext.SalesOrderHeaderService.Remove(salesOrderHeader);
              await _serviceContext.SaveChangesAsync();*/
            var salesOrderHeader = await _serviceContext.SalesOrderHeaderService.GetSalesOrderHeaderById((int)id, false);
            return RedirectToAction(nameof(Index));
        }
        public async Task<JsonResult> GetTerritory(string name)
        {
            var territory = await _serviceContext.SalesTerritoryService.GetAllSalesTerritory(false);
            var filter = territory.Where(t => t.Name.ToLower().Contains(name.ToLower())).Select(t => new
            {
                TerritoryId = t.TerritoryId,
                Name = t.Name,
                Group = t.Group
            });
            return Json(filter);
        }
        public async Task<JsonResult> GetCreditCardById(string name)
        {
            var dit = await _serviceContext.CreditCardService.GetAllCreditCard(false);
            var creditCard =  dit.Where(p => p.CardType.ToLower().Contains(name.ToLower()));
            return Json(creditCard);
        }
        public async Task<JsonResult> GetSalesPerson(string name)
        {
            var salesPerson = _dbContext.GetSalesPersonSQL.FromSqlRaw("select p.FirstName, p.LastName, s.BusinessEntityID, s.TerritoryID " +
                                "from Sales.SalesPerson s " +
                                "join HumanResources.Employee e on s.BusinessEntityID = e.BusinessEntityID " +
                                "join Person.Person p on e.BusinessEntityID = p.BusinessEntityID " +
                                "where p.Firstname like '"+name+"%'");

            //var person = await _serviceContext.SalesPersonService.GetAllSalesPerson(false);
            /*var person1 = person.Where(p => p.BusinessEntityId.ToString().Contains(name.ToLower()));
            return Json(person);*/
            return Json(salesPerson);
        }
        public async Task<JsonResult> GetCustomerId(string name)
        {
            /*   var customer = await _serviceContext.CustomerService.GetAllCustomer(false);
               var customerdt = customer.Where(p => p.CustomerId.ToString().Contains(name.ToLower()));
               return Json(customer);*/
            var customerId = _dbContext.GetSalesCustomerSQL.FromSqlRaw("select p.FirstName, p.LastName, s.CustomerID, s.TerritoryID " +
                                                                       "from Sales.Customer s " +
                                                                       "join Person.Person p on s.PersonID = p.BusinessEntityID " +
                                                                       "where p.Firstname  like '"+name+"%'");
            return Json(customerId);

        }
        public async Task<JsonResult> GetShipToAddress(string name)
        {
            /*   var customer = await _serviceContext.CustomerService.GetAllCustomer(false);
               var customerdt = customer.Where(p => p.CustomerId.ToString().Contains(name.ToLower()));
               return Json(customer);*/
            var shiptoaddres = _dbContext.GetShipToAddresSQL.FromSqlRaw("select p.AddressLine1, p.AddressID, s.ShipToAddressID " +
                                                                        "from Sales.SalesOrderHeader s " +
                                                                        "join Person.Address p on s.ShipToAddressID = p.AddressID " +
                                                                        "where p.AddressID like '"+name+"%'");
                                                                                    return Json(shiptoaddres);

        }
        public async Task<JsonResult> GetBillToAddres(string name)
        {
            /*   var customer = await _serviceContext.CustomerService.GetAllCustomer(false);
               var customerdt = customer.Where(p => p.CustomerId.ToString().Contains(name.ToLower()));
               return Json(customer);*/
            var shiptoaddres = _dbContext.GetBillToAddressSQL.FromSqlRaw("select p.AddressLine1, p.AddressID, s.BillToAddressID " +
                                                                        "from Sales.SalesOrderHeader s " +
                                                                        "join Person.Address p on s.BillToAddressID = p.AddressID " +
                                                                        "where p.AddressID like '" + name + "%'");
            return Json(shiptoaddres);

        }
        public async Task<JsonResult> GetShipMetthod(string name)
        {
            var shipmethod =_dbContext.GetShipMettodSQL.FromSqlRaw("select p.Name, p.ShipMethodID " +"from Sales.SalesOrderHeader s " + "join Purchasing.ShipMethod p on s.ShipMethodID = p.ShipMethodID " +
                "where p.Name like '" + name + "%'");
            return Json(shipmethod);
        }


        /*   private bool SalesOrderHeaderExists(int id)
           {
               return _serviceContext.SalesOrderHeaderService.Any(e => e.SalesOrderId == id);
           }*/
    }
}
