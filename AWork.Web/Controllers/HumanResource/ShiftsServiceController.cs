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
    public class ShiftsServiceController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IHumanResourceServiceManager _humanResourceServiceContext;

        public ShiftsServiceController(AdventureWorks2019Context context, IHumanResourceServiceManager humanResourceServiceContext)
        {
            _context = context;
            _humanResourceServiceContext = humanResourceServiceContext;
        }

        // GET: ShiftsService
        public async Task<IActionResult> Index()
        {
            var shift = await _context.Shifts.ToListAsync();
            var shiftDto = await _humanResourceServiceContext.ShiftService.GetAllShift(false);
            return View(shiftDto);
        }

        // GET: ShiftsService/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .FirstOrDefaultAsync(m => m.ShiftId == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: ShiftsService/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftsService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftId,Name,StartTime,EndTime,ModifiedDate")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shift);
        }

        // GET: ShiftsService/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            return View(shift);
        }

        // POST: ShiftsService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("ShiftId,Name,StartTime,EndTime,ModifiedDate")] Shift shift)
        {
            if (id != shift.ShiftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.ShiftId))
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
            return View(shift);
        }

        // GET: ShiftsService/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .FirstOrDefaultAsync(m => m.ShiftId == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: ShiftsService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(byte id)
        {
            return _context.Shifts.Any(e => e.ShiftId == id);
        }
    }
}
