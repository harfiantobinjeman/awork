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
using X.PagedList;
using Newtonsoft.Json;
using AWork.Domain.Base;

namespace AWork.Web.Controllers.Production
{
    public class WorkOrdersController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceManager;
        private readonly IProductionRepositoryManager _productionRepositoryManager;


        public WorkOrdersController(AdventureWorks2019Context context, IProductionServiceManager productionServiceManager, IProductionRepositoryManager productionRepositoryManager = null)
        {
            _context = context;
            _productionServiceManager = productionServiceManager;
            _productionRepositoryManager = productionRepositoryManager;
        }


        //get include
        /*public async Task<IActionResult> GetIncludeWOR([FromBody] int ? id)
        {
            var a = await _productionServiceManager.WorkOrderService.GetIncludeID((int)id);
            return Json(a);
        }*/
        public async Task<JsonResult>GetPencarian(int? workId, int? productId)
        {
            var resultCari = await _productionRepositoryManager.WorkOrderRepository.GerPencarian((int)workId,(int)productId, false);
            return Json(resultCari);
        }

        public async Task<JsonResult> GetIncludeWOR(int? id)
        {
            var result = await _productionRepositoryManager.WorkOrderRoutingRepository.GetIncludeID((int)id, false);
            return Json(result);
        }

        public async Task<JsonResult>GetWorkOrder(int? id)
        {
            var result = await _productionRepositoryManager.WorkOrderRepository.GetWorkOrder((int)id, false);
            return Json(result);
        }
        /*[HttpPost]
        public async Task<IActionResult> GetIncludeWOR(int? id)
        {
            var result = await _productionServiceManager.WorkOrderService.GetIncludeID((int)id, false);
            return View(result);
        }*/

        // GET: WorkOrders
        public async Task<IActionResult> Index(string searchString, string currentFilter,
            string sortOrder, int? page, int? fetchSize, int id)
        
        
        {
            /*var adventureWorks2019Context = _context.WorkOrders.Include(w => w.Product).Include(w => w.ScrapReason);
            return View(await adventureWorks2019Context.ToListAsync());*/
            //var orderWorkModel = await _productionServiceManager.WorkOrderService.GetAllWorkOrder(false);
            //return View(orderWorkModel);
            var pageIndex = page ?? 1;
            var pageSize = fetchSize ?? 5;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;


            var workForSearch = await _productionServiceManager.WorkOrderService.GetWorkrderPaged(pageIndex, pageSize, false,(int) id);
            /*var includeWorkOrder = await _productionServiceManager.WorkOrderService.GetIncludeID((int)id);

            

            ViewBag.Include =includeWorkOrder;*/

            var totalRows = workForSearch.Count();


            if (!String.IsNullOrEmpty(searchString))
            {
                workForSearch = workForSearch.Where(p => p.Product.Name.ToLower().Contains(searchString.ToLower()));
            }


            ViewBag.ProductNameSort = String.IsNullOrEmpty(sortOrder) ? "product_name" : "";
            ViewBag.LocationsView = workForSearch;
            ViewBag.UnitPriceSort = sortOrder == "date" ? "location" : "date";

            var routingForSort = from p in workForSearch
                                 select p;

            /*var includeWo = from p in includeWorkOrder join q in includeWorkOrder on p.WorkOrderId equals q.WorkOrderId select p;*/
            var product = await _productionRepositoryManager.ProductRepository.GetProductById(id,false);

            ViewBag.Id = product.ProductId;
            ViewBag.Name = product.Name;
            ViewBag.Number = product.ProductNumber;
            ViewBag.Date = product.DaysToManufacture;
            ViewBag.ListPrice = product.ListPrice;
            ViewBag.SafetyStockLevel = product.SafetyStockLevel;
            ViewBag.ReorderPoint = product.ReorderPoint;
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

            switch (sortOrder)
            {
                case "product_name":
                    routingForSort = routingForSort.OrderByDescending(p => p.Product.Name);
                    break;
                case "date":
                    routingForSort = routingForSort.OrderBy(p => p.ModifiedDate);
                    break;
                case "location":
                    routingForSort = routingForSort.OrderByDescending(p => p.DueDate);
                    break;
                default:
                    routingForSort = routingForSort.OrderBy(p => p.OrderQty);
                    break;
            }

            var routingDtoPaged = new StaticPagedList<WorkOrderDto>(routingForSort, pageIndex, pageSize - (pageSize - 1), totalRows);
            ViewBag.PagedList = new SelectList(new List<int> { 8, 15, 20 });
            return View(routingDtoPaged);
        }

        public IActionResult Error()
        {
            return View();
        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders
                .Include(w => w.Product)
                .Include(w => w.ScrapReason)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public async Task<IActionResult> Create(int? id)
        {
            /*var sR = awa _productionServiceManager.ScrapReasonService.GetAllScrapReason(false);
            var scrapReasonName = from j in sR select j.Name;
            ViewBag.NameSc = scrapReasonName;

            var scrapReasonId = from j in sR select j.ScrapReasonId;
            ViewBag.IdSc = scrapReasonId;*/
            /*var scrap = await _productionRepositoryManager.ScrapReasonRepository.GetAllScrapReason(false);
            ViewBag.ScrapReason = scrap;
            ViewBag.ScrapId = scrap.FirstOrDefault().ScrapReasonId;
            ViewBag.ScrapName = scrap.FirstOrDefault().Name;*/

            var product = await _productionRepositoryManager.ProductRepository.GetProductById((int)id, false);
            ViewBag.Id = id;
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            ViewData["ScrapReasonId"] = new SelectList(_context.ScrapReasons, "ScrapReasonId", "Name");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("WorkOrderId,ProductId,OrderQty,StockedQty,ScrappedQty,StartDate,EndDate,DueDate,ScrapReasonId")] WorkOrderForCreateDto workOrderForCreateDto)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(workOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));*/
                var dueDate = workOrderForCreateDto.DueDate;
                var stockedQty = workOrderForCreateDto.StockedQty;
                var orderqty = workOrderForCreateDto.OrderQty;
                var scrappedqty = workOrderForCreateDto.ScrappedQty;
                var stardate = workOrderForCreateDto.StartDate;
                var enddate = workOrderForCreateDto.EndDate;
                var scrapresonId = workOrderForCreateDto.ScrapReasonId;
                var insert = new WorkOrderForCreateDto
                {
                    ProductId = id,
                    ScrapReasonId = scrapresonId,
                    StockedQty =stockedQty,
                    OrderQty = orderqty,
                    StartDate = stardate,
                    EndDate = enddate,
                    ScrappedQty = scrappedqty,
                    ModifiedDate = DateTime.Now,
                    DueDate = dueDate
                };
                _productionServiceManager.WorkOrderService.Insert(insert);
                return RedirectToAction("Index", "WorkOrders", new { id = id});
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", workOrderForCreateDto.ProductId);
            ViewData["ScrapReasonId"] = new SelectList(_context.ScrapReasons, "ScrapReasonId", "Name", workOrderForCreateDto.ScrapReasonId);
            return View(workOrderForCreateDto);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", workOrder.ProductId);
            ViewData["ScrapReasonId"] = new SelectList(_context.ScrapReasons, "ScrapReasonId", "Name", workOrder.ScrapReasonId);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkOrderId,ProductId,OrderQty,StockedQty,ScrappedQty,StartDate,EndDate,DueDate,ScrapReasonId,ModifiedDate")] WorkOrder workOrder)
        {
            if (id != workOrder.WorkOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(workOrder.WorkOrderId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", workOrder.ProductId);
            ViewData["ScrapReasonId"] = new SelectList(_context.ScrapReasons, "ScrapReasonId", "Name", workOrder.ScrapReasonId);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders
                .Include(w => w.Product)
                .Include(w => w.ScrapReason)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);
            _context.WorkOrders.Remove(workOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrders.Any(e => e.WorkOrderId == id);
        }
    }
}
