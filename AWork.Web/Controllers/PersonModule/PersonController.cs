using System;
using System.Collections.Generic;
using AWork.Contracts.Dto.PersonModule.Profile;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction;
using AWork.Contracts.Dto.PersonModule;
using X.PagedList;
using AWork.Contracts.Dto.PersonModule.Profile;
using Microsoft.AspNetCore.Identity;

namespace AWork.Web.Controllers.PersonModule
{
    public class PersonController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IPersonServiceManager _personServiceManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public PersonController(AdventureWorks2019Context context, IPersonServiceManager personServiceManager = null)
        {
            _context = context;
            _personServiceManager = personServiceManager;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            var persons = await _context.People.ToListAsync();
            var personDto = await _personServiceManager.PersonServices.GetAllPerson(false);
            return View(personDto);
            //var adventureWorks2019Context = _context.People.Include(p => p.BusinessEntity);
            //return View(await adventureWorks2019Context.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var person = await _context.People
            //    .Include(p => p.BusinessEntity)
            //    .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            var personDto = await _personServiceManager.PersonServices.GetAllPersonById((int)id, false);
            if (personDto == null)
            {
                return NotFound();
            }

            return View(personDto);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonType,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,EmailPromotion,AdditionalContactInfo,Demographics,Rowguid,ModifiedDate")] PersonForCreateDto personForCreateDto)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(person);
                //await _context.SaveChangesAsync();
                _personServiceManager.PersonServices.Insert(personForCreateDto);
                return RedirectToAction(nameof(Index));
            }
            return View(personForCreateDto);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var person = await _context.People.FindAsync(id);
            var person = await _personServiceManager.PersonServices.GetAllPersonById((int)id, true);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,PersonType,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,EmailPromotion,AdditionalContactInfo,Demographics,Rowguid,ModifiedDate")] PersonDto personDto)
        {
            if (id != personDto.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(person);
                    //await _context.SaveChangesAsync();
                    _personServiceManager.PersonServices.Edit(personDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(personDto.BusinessEntityId))
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
            return View(personDto);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var person = await _context.People
            //    .Include(p => p.BusinessEntity)
            //    .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            var person = await _personServiceManager.PersonServices.GetAllPersonById((int)id, false);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var person = await _context.People.FindAsync(id);
            //_context.People.Remove(person);
            //await _context.SaveChangesAsync();
            var person = await _personServiceManager.PersonServices.GetAllPersonById((int)id, true); 
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.BusinessEntityId == id);
        }

        public async Task<IActionResult> MyProfile(ProfileDto profileDto, int id)
        {
            var person = await _personServiceManager.PersonServices.GetAllPersonById((int)id, true);
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(PersonController.Edit), "Person");
            }
            else
            {
                ModelState.AddModelError("", "404");
                return View();
            }
        }


    }
}
