using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Contracts.Dto.Production;
using AWork.Services.Abstraction;
using X.PagedList;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AWork.Web.Controllers.Production
{
    public class WorkOrderRoutingsController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceManager;

        public WorkOrderRoutingsController(AdventureWorks2019Context context, IProductionServiceManager productionServiceManager)
        {
            _context = context;
            _productionServiceManager = productionServiceManager;
        }


        // GET: WorkOrderRoutings
        public async Task<IActionResult> Index(string searchString, string currentFilter,
            string sortOrder, int? page, int? fetchSize, int? id)
        {


            //var adventureWorks2019Context = await _productionServiceManager.WorkOrderRoutingService.GetAllRouting(false);
            //return View(await adventureWorks2019Context.ToListAsync());
            var pageIndex = page ?? 1;
            var pageSize = fetchSize ?? 10;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;



            var routingForSearch = await _productionServiceManager.WorkOrderRoutingService.GetWorkrderRoutingPaged(pageIndex, pageSize, false,(int)id);

            var totalRows = routingForSearch.Count();
            if (!String.IsNullOrEmpty(searchString))
            {
                routingForSearch = routingForSearch.Where(p => p.Location.Name.ToLower().Contains(searchString.ToLower()));
            }


            ViewBag.ProductNameSort = String.IsNullOrEmpty(sortOrder) ? "location_name" : "";
            ViewBag.LocationsView = routingForSearch;
            ViewBag.UnitPriceSort = sortOrder == "date" ? "location" : "date";

            var routingForSort = from p in routingForSearch
                                 select p;
            //viewBag
            ViewBag.Id = routingForSort.FirstOrDefault().WorkOrder.Product.ProductId;
            ViewBag.Name = routingForSort.FirstOrDefault().WorkOrder.Product.Name;
            ViewBag.Number = routingForSort.FirstOrDefault().WorkOrder.Product.ProductNumber;
            ViewBag.Date = routingForSort.FirstOrDefault().WorkOrder.Product.DaysToManufacture;
            ViewBag.ListPrice = routingForSort.FirstOrDefault().WorkOrder.Product.ListPrice;
            ViewBag.OrderQty = routingForSort.FirstOrDefault().WorkOrder.OrderQty;
            ViewBag.StockedQty = routingForSort.FirstOrDefault().WorkOrder.StockedQty;
            ViewBag.StartDate = routingForSort.FirstOrDefault().WorkOrder.StartDate;
            ViewBag.EndDate = routingForSort.FirstOrDefault().WorkOrder.EndDate;
            ViewBag.DueDate = routingForSort.FirstOrDefault().WorkOrder.DueDate;
            //batas viewBag
            var sR = await _productionServiceManager.ScrapReasonService.GetAllScrapReason(false);
            var scrapReasonName = from j in sR select j.Name;
            ViewBag.NameSc = scrapReasonName;

            var scrapReasonId = from j in sR select j.ScrapReasonId;
            ViewBag.IdSc = scrapReasonId;
            if (ViewBag.IdSc is not null)
            {
                var scrapReasonNames = from j in sR select j.Name;
                ViewBag.NamesSc = scrapReasonNames;
            }

            var test = routingForSearch.FirstOrDefault();
            switch (sortOrder)
            {
                case "location_name":
                    routingForSort = routingForSort.OrderByDescending(p => p.WorkOrder.Product.Name);
                    break;
                case "date":
                    routingForSort = routingForSort.OrderBy(p => p.ModifiedDate);
                    break;
                case "location":
                    routingForSort = routingForSort.OrderByDescending(p => p.Location.Name);
                    break;
                default:
                    routingForSort = routingForSort.OrderBy(p => p.WorkOrder.OrderQty);
                    break;
            }

            var routingDtoPaged = new StaticPagedList<WorkOrderRoutingDto>(routingForSort, pageIndex, pageSize - (pageSize - 1), totalRows);
            ViewBag.PagedList = new SelectList(new List<int> { 8, 15, 20 });
            return View(routingDtoPaged);
        }

        // GET: WorkOrderRoutings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderRouting = await _context.WorkOrderRoutings
                .Include(w => w.Location)
                .Include(w => w.WorkOrder)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrderRouting == null)
            {
                return NotFound();
            }

            return View(workOrderRouting);
        }

        // GET: WorkOrderRoutings/Create
        public IActionResult Create(int? id1, int? id2)
        {
            var workOrderId = id1;
            var productId = id2;
            ViewBag.WorkOrderId = workOrderId;
            ViewBag.ProductId = productId;
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
            //ViewData["WorkOrderId"] = new SelectList(_context.WorkOrders, "WorkOrderId", "WorkOrderId");
            return View();
        }

        // POST: WorkOrderRoutings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkOrderId,ProductId,ScheduledStartDate,ScheduledEndDate,ModifiedDate")] WorkOrderRoutingForCreateDto workOrderRoutingForCreateDto)
        {
            if (ModelState.IsValid)
            {

                //var locations = await _context.Locations.Where(c => c.LocationId >= 10 && c.LocationId != 45).ToListAsync();
                var locations1 = await _productionServiceManager.LocationService.GetSetLocation(false);
                foreach (var item in locations1)
                {
                    var wo = new WorkOrderRoutingForCreateDto
                    {
                        WorkOrderId = workOrderRoutingForCreateDto.WorkOrderId,
                        ProductId = workOrderRoutingForCreateDto.ProductId,
                        LocationId = item.LocationId,
                        OperationSequence = (short)(item.LocationId / 10),
                        ScheduledStartDate = workOrderRoutingForCreateDto.ScheduledStartDate,
                        ScheduledEndDate = workOrderRoutingForCreateDto.ScheduledEndDate,
                        ActualStartDate = DateTime.Now.AddDays(3),
                        ActualEndDate = DateTime.Now.AddDays(6),
                        ActualCost = 92,
                        ActualResourceHrs = 4,
                        PlannedCost = item.Availability,
                        ModifiedDate = DateTime.Now
                    };
                    //_context.Add(wo);
                    //_context.SaveChanges();
                    _productionServiceManager.WorkOrderRoutingService.Insert(wo);

                }
                return RedirectToAction("Index", "WorkOrders", new { id = workOrderRoutingForCreateDto.ProductId });
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", workOrderRoutingForCreateDto.LocationId);
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrders, "WorkOrderId", "WorkOrderId", workOrderRoutingForCreateDto.WorkOrderId);
            return View(workOrderRoutingForCreateDto);
        }

        // GET: WorkOrderRoutings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderRouting = await _context.WorkOrderRoutings.FindAsync(id);
            if (workOrderRouting == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", workOrderRouting.LocationId);
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrders, "WorkOrderId", "WorkOrderId", workOrderRouting.WorkOrderId);
            return View(workOrderRouting);
        }

        // POST: WorkOrderRoutings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkOrderId,ProductId,OperationSequence,LocationId,ScheduledStartDate,ScheduledEndDate,ActualStartDate,ActualEndDate,ActualResourceHrs,PlannedCost,ActualCost,ModifiedDate")] WorkOrderRouting workOrderRouting)
        {
            if (id != workOrderRouting.WorkOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrderRouting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderRoutingExists(workOrderRouting.WorkOrderId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", workOrderRouting.LocationId);
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrders, "WorkOrderId", "WorkOrderId", workOrderRouting.WorkOrderId);
            return View(workOrderRouting);
        }

        // GET: WorkOrderRoutings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderRouting = await _context.WorkOrderRoutings
                .Include(w => w.Location)
                .Include(w => w.WorkOrder)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrderRouting == null)
            {
                return NotFound();
            }

            return View(workOrderRouting);
        }

        // POST: WorkOrderRoutings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrderRouting = await _context.WorkOrderRoutings.FindAsync(id);
            _context.WorkOrderRoutings.Remove(workOrderRouting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderRoutingExists(int id)
        {
            return _context.WorkOrderRoutings.Any(e => e.WorkOrderId == id);
        }
    }
}
