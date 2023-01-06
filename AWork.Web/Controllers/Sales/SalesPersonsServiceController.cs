using AutoMapper;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Contracts.Dto.Sales.Store;
using AWork.Domain.Base;
using AWork.Persistence;
using AWork.Persistence.Base;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using X.PagedList;

namespace AWork.Web.Controllers.Sales
{
    public class SalesPersonsServiceController : Controller
    {
        private readonly ISalesRepositoryManager _repositoryManager;
        private readonly ISalesServiceManager _serviceContext;
        private readonly IHumanResourceServiceManager _HRContext;
        private readonly IPersonServiceManager _personContext;
        private readonly IMapper _mapper;

        public SalesPersonsServiceController(ISalesServiceManager serviceContext, IHumanResourceServiceManager HRContext, IPersonServiceManager personContext, ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _serviceContext = serviceContext;
            _HRContext = HRContext;
            _personContext = personContext;
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // GET: SalesPersonsService
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

            var salesPersonDto = await _serviceContext.SalesPersonService.GetAllSalesPerson(false);
            IPagedList<SalesPersonDto> salesPersonDtos = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                salesPersonDto = salesPersonDto.Where(s => s.BusinessEntity.BusinessEntity.FirstName.ToLower().Contains(searchString.ToLower()) ||
                s.BusinessEntity.JobTitle.ToLower().Contains(searchString.ToLower()));
            }

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" }
            };

            // sort data
            ViewBag.Territory = sortOrder == "Territory" ? "" : "Territory";
            ViewBag.CurrentSort = sortOrder;
            switch (sortOrder)
            {
                case "Territory":
                    salesPersonDto = salesPersonDto.OrderBy(s => s.TerritoryId);
                    break;
                default:
                    salesPersonDto = salesPersonDto.OrderByDescending(s => s.TerritoryId);
                    break;
            }

            salesPersonDtos = salesPersonDto.ToPagedList(pageIndex, defaultSize);
            return View(salesPersonDtos);
        }

        // GET: SalesPersonsService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var salesPerson = await _serviceContext.SalesPersonService.GetSalesPersonById((int)id, false);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // GET: SalesPersonsService/Create
        public async Task<IActionResult> Create()
        {
            var cekData = await _serviceContext.SalesPersonService.GetSalesPersonNotExistsEmployee(false);
            var allTerritory = await _serviceContext.SalesTerritoryService.GetAllSalesTerritory(false);
            ViewData["BusinessEntityId"] = new SelectList(from q in cekData.ToList() select new { ID = q.BusinessEntityId, Fullname = q.FirstName + " " + q.MiddleName + " " + q.LastName }, "ID", "Fullname");
            ViewData["TerritoryId"] = new SelectList(allTerritory, "TerritoryId", "TerritoryId");
            return View();
        }

        public async Task<JsonResult> GetDataEmployeeTypeSP()
        {
            var result = await _repositoryManager.SalesPersonRepository.GetDataEmployeeTypeSP();
            return Json(result);
        }

        public async Task<JsonResult> GetDataTerritory()
        {
            var result = await _repositoryManager.SalesPersonRepository.GetDataTerritoryByName();
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalesPerson(SalesPersonGroupDto salesPersonGroupDto)
        {
            if (ModelState.IsValid)
            {
                var salesPerson = salesPersonGroupDto;
                var store = new List<StoreForCreateDto>();
                foreach (var item in salesPerson.storeForCreateDto)
                {
                    if (item.Name != null)
                    {
                        var businessEntityId = _personContext.BusinessEntityServices.CreateBusinessEntity();
                        var storeCreate = new StoreForCreateDto
                        {
                            BusinessEntityId = businessEntityId.BusinessEntityId,
                            Name = item.Name,
                            SalesPersonId = salesPerson.salesPersonForCreateDto.BusinessEntityId,
                            Rowguid = Guid.NewGuid(),
                            ModifiedDate = DateTime.Now
                        };
                        store.Add(storeCreate);
                    }
                }
                var salesPersonCreate = new SalesPersonForCreateDto
                {
                    BusinessEntityId = salesPerson.salesPersonForCreateDto.BusinessEntityId,
                    TerritoryId = salesPerson.salesPersonForCreateDto.TerritoryId,
                    Bonus = 0,
                    CommissionPct = 0,
                    SalesYtd = 0,
                    SalesLastYear = 0,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                _serviceContext.SalesPersonService.CreateSalesPersonStore(salesPersonCreate, store);
                return RedirectToAction("Create");
            }
            return View("Index");
        }

        // GET: SalesPersonsService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _serviceContext.SalesPersonService.GetSalesPersonById((int)id, true);
            if (salesPerson == null)
            {
                return NotFound();
            }
            var allTerritory = await _serviceContext.SalesTerritoryService.GetAllSalesTerritory(false);
            //ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", salesPerson.BusinessEntityId);
            ViewData["TerritoryId"] = new SelectList(allTerritory, "TerritoryId", "TerritoryId", salesPerson.TerritoryId);
            return View(salesPerson);
        }

        [HttpPost]
        public async Task<IActionResult> EditSalesPerson(SalesPersonDto salesPersonDto)
        {
            if (ModelState.IsValid)
            {
                var salesPerson = salesPersonDto;
                var store = new List<StoreDto>();
                foreach (var item in salesPerson.StoreDtos)
                {
                    if (item.Name != null)
                    {
                        var storeEdit = new StoreDto
                        {
                            BusinessEntityId = item.BusinessEntityId,
                            Name = item.Name,
                            SalesPersonId = salesPerson.BusinessEntityId,
                            Rowguid = Guid.NewGuid(),
                            ModifiedDate = DateTime.Now
                        };
                        store.Add(storeEdit);
                    }
                }

                var salesPersonEdit = new SalesPersonDto
                {
                    BusinessEntityId = salesPerson.BusinessEntityId,
                    TerritoryId = salesPerson.TerritoryId,
                    Bonus = salesPerson.Bonus,
                    CommissionPct = salesPerson.CommissionPct,
                    SalesYtd = salesPerson.SalesYtd,
                    SalesLastYear = salesPerson.SalesLastYear,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                _serviceContext.SalesPersonService.EditSalesPersonStore(salesPersonEdit, store);
                return RedirectToAction("Index", "SalesPersonsService");
            }
            return View("Index");
        }

        // GET: SalesPersonsService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var salesPerson = await _serviceContext.SalesPersonService.GetSalesPersonById((int)id, false);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // POST: SalesPersonsService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesPersonDto = await _serviceContext.SalesPersonService.GetSalesPersonById(id, false);
            foreach (var item in salesPersonDto.StoreDtos)
            {
                var store = new StoreDto
                {
                    BusinessEntityId = item.BusinessEntityId
                };
                _serviceContext.StoreService.Remove(store);
            }

            var salesPerson = new SalesPersonDto
            {
                BusinessEntityId = salesPersonDto.BusinessEntityId
            };
            _serviceContext.SalesPersonService.Remove(salesPerson);

            return RedirectToAction(nameof(Index));
        }
    }
}
