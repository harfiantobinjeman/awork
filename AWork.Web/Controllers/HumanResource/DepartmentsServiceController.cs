using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction;
using X.PagedList;
using AWork.Contracts.Dto.HumanResources;

namespace AWork.Web.Controllers.HumanResource
{
    public class DepartmentsServiceController : Controller
    {
        private readonly IHumanResourceServiceManager _context;

        public DepartmentsServiceController(IHumanResourceServiceManager context)
        {
            _context = context;
        }


        // GET: DepartmentsService
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

            var departmentDto = await _context.DepartmentService.GetAllDepartment(false);
            IPagedList<DepartmentDto> departmentDtos = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                departmentDto = departmentDto.
                    Where(p => p.DepartmentId.ToString().Contains(searchString.ToLower()) ||
                               p.Name.ToLower().Contains(searchString.ToLower()) ||
                               p.GroupName.ToLower().Contains(searchString.ToLower()));
            }

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() {Value="5", Text= "5"},
                new SelectListItem() {Value="10", Text= "10"},
                new SelectListItem() {Value="15", Text= "15"},
                new SelectListItem() {Value="20", Text= "20"}
            };

            //Sort Data
            ViewBag.DepartmentId = sortOrder == "DepartmentId" ? "" : "DepartmentId";
            ViewBag.CurrentSort = sortOrder;

            switch (sortOrder)
            {
                case "DepartmentId":
                    departmentDto = departmentDto.OrderByDescending(p => p.DepartmentId);
                    break;
                default:
                    departmentDto = departmentDto.OrderBy(p => p.DepartmentId);
                    break;
            }

            departmentDtos = departmentDto.ToPagedList(pageIndex, defaultSize);
            return View(departmentDtos);
        }

        // GET: DepartmentsService/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.DepartmentService.GetDepartmentById((short)id, false);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }


        // GET: DepartmentsService/Create
        public async Task<IActionResult> Create()
        {
            var allDepartment = await _context.DepartmentService.GetAllDepartment(false);
            ViewData["DepartmentId"] = new SelectList(allDepartment, "GroupName", "GroupName");
            return View();
        }

        // POST: DepartmentsService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,Name,GroupName,ModifiedDate")] DepartmentForCreateDto department)
        {
            if (ModelState.IsValid)
            {
                _context.DepartmentService.insert(department);
                return RedirectToAction(nameof(Index));
            }

            var allDepartment = await _context.DepartmentService.GetAllDepartment(false);
            ViewData["DepartmentId"] = new SelectList(allDepartment, "GroupName", "GroupName");
            return View(department);
        }

        // GET: DepartmentsService/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.DepartmentService.GetDepartmentById((short)id, false);

            if (department == null)
            {
                return NotFound();
            }
            var allDepartment = await _context.DepartmentService.GetAllDepartment(false);
            ViewData["DepartmentId"] = new SelectList(allDepartment, "GroupName", "GroupName");
            return View(department);
        }

        // POST: DepartmentsService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("DepartmentId,Name,GroupName,ModifiedDate")] DepartmentDto department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.DepartmentService.edit(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var allDepartment = await _context.DepartmentService.GetAllDepartment(false);
            ViewData["DepartmentId"] = new SelectList(allDepartment, "GroupName", "GroupName");
            return View(department);
        }


        // GET: DepartmentsService/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.DepartmentService.GetDepartmentById((short)id, false);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: DepartmentsService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var department = await _context.DepartmentService.GetDepartmentById((short)id, false);
            _context.DepartmentService.delete(department);
            return RedirectToAction(nameof(Index));
        }
    }
}