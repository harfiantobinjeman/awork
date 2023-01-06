using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction;
using X.PagedList;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.HumanResources;
using AWork.Domain.Base;
using System.Reflection;
using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using AWork.Contracts.Dto.PersonModule;
using static System.Formats.Asn1.AsnWriter;

namespace AWork.Web.Controllers.HumanResource
{
    public class EmployeePagedController : Controller
    {
        private readonly IHumanResourceServiceManager _context;
        private readonly IHRRepositoryManager _repositoryContext;
        private readonly IPersonServiceManager _personContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public EmployeePagedController(IHumanResourceServiceManager context, IHRRepositoryManager repositoryContext, IPersonServiceManager personContext, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _repositoryContext = repositoryContext;
            _personContext = personContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: EmployeePagedController
        public async Task<IActionResult> Index(string searchString,
            string currentFilter, string sortOrder, int? page, int? pageSize)
        {
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
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

            var employeeDtos = await _context.EmployeeService.GetAllEmployee(false);
            IPagedList<EmployeeDto> employeeDto = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                employeeDtos = employeeDtos.
                     Where(p => p.BusinessEntity.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                p.BusinessEntity.LastName.ToLower().Contains(searchString.ToLower()) ||
                                p.NationalIdnumber.ToLower().Contains(searchString.ToLower()) ||
                                p.JobTitle.ToLower().Contains(searchString.ToLower()) ||
                                p.HireDate.ToString().Contains(searchString.ToString()));

            }

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() {Value="5", Text= "5"},
                new SelectListItem() {Value="10", Text= "10"},
                new SelectListItem() {Value="20", Text= "20"},
                new SelectListItem() {Value="30", Text= "30"},
                new SelectListItem() {Value="40", Text= "40"},
                new SelectListItem() {Value="50", Text= "50"},
                new SelectListItem() {Value="100", Text= "100"},
            };

            //Sort Data
            ViewBag.BusinessEntityId = sortOrder == "BusinessEntityId" ? "" : "BusinessEntityId";
            ViewBag.CurrentSort = sortOrder;

            switch (sortOrder)
            {
                case "BusinessEntityId":
                    employeeDtos = employeeDtos.OrderByDescending(p => p.BusinessEntityId);
                    break;
                default:
                    employeeDtos = employeeDtos.OrderBy(p => p.BusinessEntityId);
                    break;
            }

            employeeDto = employeeDtos.ToPagedList(pageIndex, defaultSize);

            return View(employeeDto);

        }

        // GET: EmployeePagedController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.EmployeeService.GetEmployeeDtoById((int)id, false);


            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        // GET: EmployeePagedController/Create
        public async Task<ActionResult> Create()
        {
            /*var personEmployee = await _repositoryContext.EmployeeRepository.GetPersonByType(false);
            ViewData["BusinessEntityId"] = new SelectList(from x in personEmployee.ToList() select new {ID = x.BusinessEntityId, Fullname = x.FirstName + " " + x.MiddleName + " " + x.LastName}, "ID", "Fullname");
*/
            var TitleJob = await _repositoryContext.EmployeeRepository.GetJobTitle(false);
            ViewData["JobTitle"] = new SelectList(TitleJob, "JobTitle", "JobTitle");

            var hari = await _context.ShiftService.GetAllShift(false);
            ViewData["ShiftoId"] = new SelectList(from s in hari.ToList() select new {IDS = s.ShiftId, ShiftTime = s.Name}, "IDS", "ShiftTime");

            var departemon = await _context.DepartmentService.GetAllDepartment(false);
            ViewData["DepartementId"] = new SelectList(from d in departemon.ToList() select new {IDD = d.DepartmentId, Dname = d.Name}, "IDD", "Dname");

            return View();

            //ViewData["BusinessEntityId"] = new SelectList((from q in SintaxJoin.ToList() select new { ID = q.BusinessEntityId, Fullname = q.FirstName + " " + q.LastName }), "ID", "Fullname");

            /*
            ViewData["ShiftId"] = new SelectList(hari, "ShiftId", "Name");
            ViewData["JobTitle"] = new SelectList(TitleJob, "JobTitle", "JobTitle");*/

            /*var allTerritory = await _serviceContext.SalesTerritoryService.GetAllSalesTerritory(false);
            ViewData["TerritoryId"] = new SelectList(allTerritory, "TerritoryId", "CountryRegionCode");*/

        }

        // POST: EmployeePagedController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeGroupDto employeeGroupDto)
        {

            if (ModelState.IsValid)
            {
                var employeeGroup = employeeGroupDto;
                // var dataPerson = _personContext.PersonServices.GetAllPersonById(employeeGroup.employeeForCreateDto.BusinessEntityId, false);

                // get email
                var emailDto = await _personContext.EmailAddressServices.GetEmailId(employeeGroup.employeeForCreateDto.BusinessEntityId, false);

                
                var employeeCreate = new EmployeeForCreateDto
                {
                    BusinessEntityId = employeeGroup.employeeForCreateDto.BusinessEntityId,
                    NationalIdnumber = employeeGroup.employeeForCreateDto.NationalIdnumber,
                    LoginId = emailDto.EmailAddress1,
                    JobTitle = employeeGroup.employeeForCreateDto.JobTitle,
                    BirthDate = employeeGroup.employeeForCreateDto.BirthDate,
                    MaritalStatus = employeeGroup.employeeForCreateDto.MaritalStatus,
                    Gender = employeeGroup.employeeForCreateDto.Gender,
                    HireDate = employeeGroup.employeeForCreateDto.HireDate,
                    SalariedFlag = true,
                    VacationHours = employeeGroup.employeeForCreateDto.VacationHours,
                    SickLeaveHours = employeeGroup.employeeForCreateDto.SickLeaveHours,
                    CurrentFlag = true,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now,
                };

                var departmentCreate = new EmployeeDepartmentHistoryForCreateDto
                {
                    BusinessEntityId = employeeGroup.employeeForCreateDto.BusinessEntityId,
                    DepartmentId = employeeGroup.employeeDepartmentForCreateDto.DepartmentId,
                    ShiftId = employeeGroup.employeeDepartmentForCreateDto.ShiftId,
                    StartDate = employeeGroup.employeeForCreateDto.HireDate,
                    ModifiedDate = DateTime.Now
                };

                var employeePHCreate = new EmployeePHForCreateDto
                {
                    BusinessEntityId = employeeGroup.employeeForCreateDto.BusinessEntityId,
                    RateChangeDate = DateTime.Now,
                    Rate = employeeGroup.employeePHForCreateDto.Rate,
                    PayFrequency = employeeGroup.employeePHForCreateDto.PayFrequency,
                    ModifiedDate = DateTime.Now
                };

                _context.EmployeeService.create3table(employeeCreate,departmentCreate,employeePHCreate);

                return RedirectToAction(nameof(Index));
                /*var listPhoto = new List<ProductPhotoForCreateDto>();
                foreach (var itemPhoto in productPhotoGroup.AllPhoto)
                {
                    var fileName = _utilityService.UploadSingleFile(itemPhoto);
                    var photo = new ProductPhotoForCreateDto
                    {
                        PhotoFilename = fileName,
                        PhotoFileSize = (short?)itemPhoto.Length,
                        PhotoFileType = itemPhoto.ContentType
                    };
                    listPhoto.Add(photo);
                }

                _context.ProductService.CreateProductManyPhoto(productPhotoGroupDto.ProductForCreateDto, listPhoto);
*/
            }
            return View("Index");
        }

        // GET: EmployeePagedController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeess = await _repositoryContext.EmployeeRepository.GetEmployeeById((int)id, true);
            var mapEmployeeDto = _mapper.Map<EmployeeDto>(employeess);

            var employeePH = await _repositoryContext.EmployeePayHistoryRepository.GetEmployeePayHistoryById((int)id, true);
            var mapEmployeePHDto = _mapper.Map<EmployeePayHistoryDto>(employeePH);

            var employeeDH = await _repositoryContext.EmployeeDepartmentHistoryRepository.GetEmployeeDepartmentHistoryById((int)id, true);
            var mapEmployeeDHDto = _mapper.Map<EmployeeDepartmentHistoryDto>(employeeDH);

            var emplooyEditGroup = new EmployeeEditGroupDto
            {
                employeeDtos = mapEmployeeDto,
                employeePHDtos = mapEmployeePHDto,
                employeeDepartmentDtos = mapEmployeeDHDto
            };

            if (emplooyEditGroup == null)
            {
                return NotFound();
            }

            var TitleJob = await _repositoryContext.EmployeeRepository.GetJobTitle(true);
            ViewData["JobTitle"] = new SelectList(TitleJob, "JobTitle", "JobTitle");

            var hari = await _context.ShiftService.GetAllShift(true);
            ViewData["ShiftoId"] = new SelectList(from s in hari.ToList() select new { IDS = s.ShiftId, ShiftTime = s.Name }, "IDS", "ShiftTime");

            var departemon = await _context.DepartmentService.GetAllDepartment(true);
            ViewData["DepartementId"] = new SelectList(from d in departemon.ToList() select new { IDD = d.DepartmentId, Dname = d.Name }, "IDD", "Dname");

            return View(emplooyEditGroup);
        }

        // POST: EmployeePagedController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeEditGroupDto employeeEditGroupDto)
        {
            if (ModelState.IsValid)
            {
                var employeeEdit = new EmployeeDto
                {
                    BusinessEntityId = employeeEditGroupDto.employeeDtos.BusinessEntityId,
                    NationalIdnumber = employeeEditGroupDto.employeeDtos.NationalIdnumber,
                    LoginId = employeeEditGroupDto.employeeDtos.LoginId,
                    JobTitle = employeeEditGroupDto.employeeDtos.JobTitle,
                    BirthDate = employeeEditGroupDto.employeeDtos.BirthDate,
                    MaritalStatus = employeeEditGroupDto.employeeDtos.MaritalStatus,
                    Gender = employeeEditGroupDto.employeeDtos.Gender,
                    HireDate = employeeEditGroupDto.employeeDtos.HireDate,
                    SalariedFlag = employeeEditGroupDto.employeeDtos.SalariedFlag,
                    VacationHours = employeeEditGroupDto.employeeDtos.VacationHours,
                    SickLeaveHours = employeeEditGroupDto.employeeDtos.SickLeaveHours,
                    CurrentFlag = employeeEditGroupDto.employeeDtos.CurrentFlag,
                    Rowguid = employeeEditGroupDto.employeeDtos.Rowguid,
                    ModifiedDate = DateTime.Now,
                };

                var departmentEdit = new EmployeeDepartmentHistoryDto
                {
                    BusinessEntityId = employeeEditGroupDto.employeeDepartmentDtos.BusinessEntityId,
                    DepartmentId = employeeEditGroupDto.employeeDepartmentDtos.DepartmentId,
                    ShiftId = employeeEditGroupDto.employeeDepartmentDtos.ShiftId,
                    StartDate = employeeEditGroupDto.employeeDtos.HireDate,
                    ModifiedDate = DateTime.Now
                };

                var employeePHEdit = new EmployeePayHistoryDto
                {
                    BusinessEntityId = employeeEditGroupDto.employeePHDtos.BusinessEntityId,
                    RateChangeDate = employeeEditGroupDto.employeePHDtos.RateChangeDate,
                    Rate = employeeEditGroupDto.employeePHDtos.Rate,
                    PayFrequency = employeeEditGroupDto.employeePHDtos.PayFrequency,
                    ModifiedDate = DateTime.Now
                };
                _context.EmployeeService.edit3table(employeeEdit, departmentEdit, employeePHEdit);
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        // GET: EmployeePagedController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.EmployeeService.GetEmployeeDtoById((int)id, false);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: EmployeePagedController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.EmployeeService.GetEmployeeDtoById((int)id, false);
            _context.EmployeeService.Remove(employee);
            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> GetPersonByType(string name)
        {
            var persons = await _repositoryContext.EmployeeRepository.GetPersonByType(false);
            persons.Where(p => p.FirstName.Contains(name));
            return Json(persons);
        }

    }
}
