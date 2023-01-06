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

namespace AWork.Web.Controllers.HumanResource
{
    public class EmployeeDepartmentHistoriesServiceController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IHumanResourceServiceManager _humanResourceServiceContext;

        public EmployeeDepartmentHistoriesServiceController(AdventureWorks2019Context context, IHumanResourceServiceManager humanResourceServiceContext)
        {
            _context = context;
            _humanResourceServiceContext = humanResourceServiceContext;
        }

        // GET: EmployeeDepartmentHistoriesService
        public async Task<IActionResult> Index()
        {
            var employeeDepartmentHistory = await _context.EmployeeDepartmentHistoriesService.ToListAsync();
            var employeeDepartmentHistoryDto = await _humanResourceServiceContext.EmployeeDepartmentHistoryService.GetAllEmployeeDepartmentHistory(false);
            return View(employeeDepartmentHistoryDto);
        }

        // GET: EmployeeDepartmentHistoriesService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDepartmentHistory = await _context.EmployeeDepartmentHistoriesService
                .Include(e => e.BusinessEntity)
                .Include(e => e.Department)
                .Include(e => e.Shift)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (employeeDepartmentHistory == null)
            {
                return NotFound();
            }

            return View(employeeDepartmentHistory);
        }

        // GET: EmployeeDepartmentHistoriesService/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "GroupName");
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "Name");
            return View();
        }

        // POST: EmployeeDepartmentHistoriesService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,DepartmentId,ShiftId,StartDate,EndDate,ModifiedDate")] EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeDepartmentHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", employeeDepartmentHistory.BusinessEntityId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "GroupName", employeeDepartmentHistory.DepartmentId);
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "Name", employeeDepartmentHistory.ShiftId);
            return View(employeeDepartmentHistory);
        }

        // GET: EmployeeDepartmentHistoriesService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDepartmentHistory = await _context.EmployeeDepartmentHistoriesService.FindAsync(id);
            if (employeeDepartmentHistory == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", employeeDepartmentHistory.BusinessEntityId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "GroupName", employeeDepartmentHistory.DepartmentId);
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "Name", employeeDepartmentHistory.ShiftId);
            return View(employeeDepartmentHistory);
        }

        // POST: EmployeeDepartmentHistoriesService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,DepartmentId,ShiftId,StartDate,EndDate,ModifiedDate")] EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            if (id != employeeDepartmentHistory.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeDepartmentHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDepartmentHistoryExists(employeeDepartmentHistory.BusinessEntityId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", employeeDepartmentHistory.BusinessEntityId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "GroupName", employeeDepartmentHistory.DepartmentId);
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "Name", employeeDepartmentHistory.ShiftId);
            return View(employeeDepartmentHistory);
        }

        // GET: EmployeeDepartmentHistoriesService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDepartmentHistory = await _context.EmployeeDepartmentHistoriesService
                .Include(e => e.BusinessEntity)
                .Include(e => e.Department)
                .Include(e => e.Shift)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (employeeDepartmentHistory == null)
            {
                return NotFound();
            }

            return View(employeeDepartmentHistory);
        }

        // POST: EmployeeDepartmentHistoriesService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeDepartmentHistory = await _context.EmployeeDepartmentHistoriesService.FindAsync(id);
            _context.EmployeeDepartmentHistoriesService.Remove(employeeDepartmentHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDepartmentHistoryExists(int id)
        {
            return _context.EmployeeDepartmentHistoriesService.Any(e => e.BusinessEntityId == id);
        }
    }
}
