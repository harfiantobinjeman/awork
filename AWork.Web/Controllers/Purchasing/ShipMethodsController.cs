using AWork.Contracts.Dto.Purchasing;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.Purchasing
{
    public class ShipMethodsController : Controller
    {
        private readonly IPurchasingServiceManager _serviceContext;

        public ShipMethodsController(IPurchasingServiceManager serviceContext)
        {
            _serviceContext = serviceContext;
        }

        // GET: ShipMethods
        public async Task<IActionResult> Index()
        {
            var shipmethodDto = await _serviceContext.ShipMethodService.GetAllShipMethod(false);
            return View(shipmethodDto);
        }

        // GET: ShipMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var shipMethod = await _context.ShipMethods
                .FirstOrDefaultAsync(m => m.ShipMethodId == id);*/
            var shipMethod = await _serviceContext.ShipMethodService.GetShipMethodById((int)id, false);
            if (shipMethod == null)
            {
                return NotFound();
            }

            return View(shipMethod);
        }

        // GET: ShipMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShipMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ShipBase,ShipRate,Rowguid,ModifiedDate")] ShipMethodForCreateDto shipMethod)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(shipMethod);
                await _context.SaveChangesAsync();*/

                _serviceContext.ShipMethodService.Insert(shipMethod);
                return RedirectToAction(nameof(Index));
            }
            return View(shipMethod);
        }

        // GET: ShipMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var shipMethod = await _context.ShipMethods.FindAsync(id);*/
            var shipMethod = await _serviceContext.ShipMethodService.GetShipMethodById((int)id, false);
            if (shipMethod == null)
            {
                return NotFound();
            }
            return View(shipMethod);
        }

        // POST: ShipMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipMethodId,Name,ShipBase,ShipRate,Rowguid,ModifiedDate")] ShipMethodDto shipMethod)
        {
            if (id != shipMethod.ShipMethodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(shipMethod);
                    await _context.SaveChangesAsync();*/

                    _serviceContext.ShipMethodService.Edit(shipMethod);
                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!ShipMethodExists(shipMethod.ShipMethodId))
                    {
                        return NotFound();
                    }
                    else
                    {
                    }*/
                    throw;
                }
                var ship = await _serviceContext.ShipMethodService.GetAllShipMethod(false);
                ViewData["ShipMethodId"] = new SelectList(ship, "ShipMethodId", "Name", shipMethod.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(shipMethod);
        }

        // GET: ShipMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var shipMethod = await _context.ShipMethods
                .FirstOrDefaultAsync(m => m.ShipMethodId == id);*/
            var shipMethod = await _serviceContext.ShipMethodService.GetShipMethodById((int)id, false);
            if (shipMethod == null)
            {
                return NotFound();
            }

            return View(shipMethod);
        }

        // POST: ShipMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var shipMethod = await _context.ShipMethods.FindAsync(id);
            _context.ShipMethods.Remove(shipMethod);
            await _context.SaveChangesAsync();*/
            var shipMethodModel = await _serviceContext.ShipMethodService.GetShipMethodById((int)id, false);
            _serviceContext.ShipMethodService.Remove(shipMethodModel);

            return RedirectToAction(nameof(Index));
        }

        /*private bool ShipMethodExists(int id)
        {
            return _context.ShipMethods.Any(e => e.ShipMethodId == id);
        }*/
    }
}
