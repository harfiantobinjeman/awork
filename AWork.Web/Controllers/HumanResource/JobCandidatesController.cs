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
    public class JobCandidatesController : Controller
    {
        private readonly AdventureWorks2019Context _context;

        public JobCandidatesController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: JobCandidates
        public async Task<IActionResult> Index()
        {
            var adventureWorks2019Context = _context.JobCandidates.Include(j => j.BusinessEntity);
            return View(await adventureWorks2019Context.ToListAsync());
        }

        // GET: JobCandidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCandidate = await _context.JobCandidates
                .Include(j => j.BusinessEntity)
                .FirstOrDefaultAsync(m => m.JobCandidateId == id);
            if (jobCandidate == null)
            {
                return NotFound();
            }

            return View(jobCandidate);
        }

        // GET: JobCandidates/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender");
            return View();
        }

        // POST: JobCandidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobCandidateId,BusinessEntityId,Resume,ModifiedDate")] JobCandidate jobCandidate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobCandidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", jobCandidate.BusinessEntityId);
            return View(jobCandidate);
        }

        // GET: JobCandidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCandidate = await _context.JobCandidates.FindAsync(id);
            if (jobCandidate == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", jobCandidate.BusinessEntityId);
            return View(jobCandidate);
        }

        // POST: JobCandidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobCandidateId,BusinessEntityId,Resume,ModifiedDate")] JobCandidate jobCandidate)
        {
            if (id != jobCandidate.JobCandidateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobCandidate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobCandidateExists(jobCandidate.JobCandidateId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", jobCandidate.BusinessEntityId);
            return View(jobCandidate);
        }

        // GET: JobCandidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCandidate = await _context.JobCandidates
                .Include(j => j.BusinessEntity)
                .FirstOrDefaultAsync(m => m.JobCandidateId == id);
            if (jobCandidate == null)
            {
                return NotFound();
            }

            return View(jobCandidate);
        }

        // POST: JobCandidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobCandidate = await _context.JobCandidates.FindAsync(id);
            _context.JobCandidates.Remove(jobCandidate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobCandidateExists(int id)
        {
            return _context.JobCandidates.Any(e => e.JobCandidateId == id);
        }
    }
}
