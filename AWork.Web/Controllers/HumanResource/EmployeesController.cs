using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.HumanResource
{
    public class EmployeesController : Controller
    {
        //private readonly AdventureWorks2019Context _context;
        private readonly IHumanResourceServiceManager _context;

        public EmployeesController(IHumanResourceServiceManager context)
        {
            _context = context;
        }


        // GET: Employees
        public async Task<IActionResult> Index()
        {
            /*var adventureWorks2019Context = _context.EmployeeService.Include(e => e.BusinessEntity);
            return View(await adventureWorks2019Context.ToListAsync());*/
            var employeeDto = await _context.EmployeeService.GetAllEmployee(false);
            return View(employeeDto);
        }


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var employee = await _context.Employees
                .Include(e => e.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);*/
            var employee = await _context.EmployeeService.GetEmployeeDtoById((int)id, false);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            //ViewData["BusinessEntityId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,NationalIdnumber,LoginId,OrganizationLevel,JobTitle,BirthDate,MaritalStatus,Gender,HireDate,SalariedFlag,VacationHours,SickLeaveHours,CurrentFlag,Rowguid,ModifiedDate")] EmployeeForCreateDto employee)
        {
            if (ModelState.IsValid)
            {
                /* _context.Add(employee);
                 await _context.SaveChangesAsync();*/
                _context.EmployeeService.Insert(employee);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BusinessEntityId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName", employee.BusinessEntityId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employee = await _context.Employees.FindAsync(id);
            var employee = await _context.EmployeeService.GetEmployeeDtoById((int)id, false);
            if (employee == null)
            {
                return NotFound();
            }
            //ViewData["BusinessEntityId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName", employee.BusinessEntityId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,NationalIdnumber,LoginId,OrganizationLevel,JobTitle,BirthDate,MaritalStatus,Gender,HireDate,SalariedFlag,VacationHours,SickLeaveHours,CurrentFlag,Rowguid,ModifiedDate")] EmployeeDto employee)
        {
            if (id != employee.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(employee);
                    await _context.SaveChangesAsync();*/
                    _context.EmployeeService.Edit(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!EmployeeExists(employee.BusinessEntityId))
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
            //ViewData["BusinessEntityId"] = new SelectList(_context.People, "BusinessEntityId", "FirstName", employee.BusinessEntityId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var employee = await _context.Employees
                .Include(e => e.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);*/
            var employee = await _context.EmployeeService.GetEmployeeDtoById((int)id, false);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();*/
            var employee = await _context.EmployeeService.GetEmployeeDtoById((int)id, false);
            _context.EmployeeService.Remove(employee);
            return RedirectToAction(nameof(Index));
        }

        /*private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.BusinessEntityId == id);
        }*/
    }
}
