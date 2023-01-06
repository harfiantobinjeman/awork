using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWork.Domain.Models;
using AWork.Persistence;

namespace AWork.Web.Controllers.HumanResource
{
    public class EmployeePayHistoriesController : Controller
    {
        private readonly AdventureWorks2019Context _context;

        public EmployeePayHistoriesController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: EmployeePayHistories
        public async Task<IActionResult> Index()
        {
            var adventureWorks2019Context = _context.EmployeePayHistories.Include(e => e.BusinessEntity);
            return View(await adventureWorks2019Context.ToListAsync());
        }

        // GET: EmployeePayHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePayHistory = await _context.EmployeePayHistories
                .Include(e => e.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (employeePayHistory == null)
            {
                return NotFound();
            }

            return View(employeePayHistory);
        }

        // GET: EmployeePayHistories/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender");
            return View();
        }

        // POST: EmployeePayHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,RateChangeDate,Rate,PayFrequency,ModifiedDate")] EmployeePayHistory employeePayHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeePayHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", employeePayHistory.BusinessEntityId);
            return View(employeePayHistory);
        }

        // GET: EmployeePayHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePayHistory = await _context.EmployeePayHistories.FindAsync(id);
            if (employeePayHistory == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", employeePayHistory.BusinessEntityId);
            return View(employeePayHistory);
        }

        // POST: EmployeePayHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,RateChangeDate,Rate,PayFrequency,ModifiedDate")] EmployeePayHistory employeePayHistory)
        {
            if (id != employeePayHistory.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeePayHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeePayHistoryExists(employeePayHistory.BusinessEntityId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", employeePayHistory.BusinessEntityId);
            return View(employeePayHistory);
        }

        // GET: EmployeePayHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePayHistory = await _context.EmployeePayHistories
                .Include(e => e.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (employeePayHistory == null)
            {
                return NotFound();
            }

            return View(employeePayHistory);
        }

        // POST: EmployeePayHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeePayHistory = await _context.EmployeePayHistories.FindAsync(id);
            _context.EmployeePayHistories.Remove(employeePayHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeePayHistoryExists(int id)
        {
            return _context.EmployeePayHistories.Any(e => e.BusinessEntityId == id);
        }
    }
}
