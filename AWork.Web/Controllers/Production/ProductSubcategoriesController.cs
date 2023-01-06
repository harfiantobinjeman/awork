using AWork.Contracts.Dto.Production;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Contracts.Dto.Category;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.Production
{
    public class ProductSubcategoriesController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceContext;


        public ProductSubcategoriesController(AdventureWorks2019Context context, IProductionServiceManager productionServiceContext)
        {
            _context = context;
            _productionServiceContext = productionServiceContext;
        }
        public async Task<IActionResult> Index1(int? id,SelectProductSubCategoryDto selectProductSubCategoryDto)
        {
            var product = selectProductSubCategoryDto;
             

            return View(product);

        }

 
        // GET: ProductSubcategories
        public async Task<IActionResult> Index(int? id)
        {

            var model = new SelectProductSubCategoryDto();
            model.productCategoryDto = await _productionServiceContext.ProductCategoryService.GetAllProdcCategory(false);

            if (id.HasValue)
            {
                model.productSubCategoryDto = await _productionServiceContext.ProductSubCategoryService.GetProCateInSubByID((int)id, false);
            }

            return View(model);

        }

        public async Task<IActionResult> Click(int? id)
        {
            var model = new SelectProductSubCategoryDto();
            model.productCategoryDto = await _productionServiceContext.ProductCategoryService.GetAllProdcCategory(false);

            if (id.HasValue)
            {
                model.productSubCategoryDto = await _productionServiceContext.ProductSubCategoryService.GetProCateInSubByID((int)id, false);
            }
          
            return View(model); 
        }




        public async Task<IActionResult> Select(int? id, SelectProductSubCategoryDto selectProductSubCategoryDto)
        {

            var viewModel = _productionServiceContext.ProductSubCategoryService.GetAllProdcSubCategory(false);
            var subcate = _productionServiceContext.ProductSubCategoryService.GetAllProdcSubCategory(false);
            var viewSubcate = await _productionServiceContext.ProductSubCategoryService.GetProCateInSubByID((int)id, false);
            var proSubCateDto = await _productionServiceContext.ProductSubCategoryService.GetAllProdcSubCategory(false);
            

            return View(viewSubcate);
        }

        // GET: ProductSubcategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSubcategory = await _context.ProductSubcategories
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.ProductSubcategoryId == id);
            if (productSubcategory == null)
            {
                return NotFound();
            }

            return View(productSubcategory);
        }

        // GET: ProductSubcategories/Create
        // GET: PSC/Create
        public IActionResult Create()
        {
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name");
            return View();
        }

        // POST: PSC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductSubcategoryId,ProductCategoryId,Name,Rowguid,ModifiedDate")] ProductSubCategoryForCreateDto subcate)
        {
            if (ModelState.IsValid)
            {
                /*context.Add(productSubcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));*/
                _productionServiceContext.ProductSubCategoryService.Insert(subcate);
            }
            var allCate = await _productionServiceContext.ProductCategoryService.GetAllProdcCategory(false);
            ViewData["ProductCategoryId"] = new SelectList(allCate, "ProductCategoryId", "Name");
            /*ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", productSubcategory.ProductCategoryId);
            return View(productSubcategory);*/
            return View();
        }

        public void Add(IEnumerable<ProductSubCategoryForCreateDto> dto)
        {
            foreach (ProductSubCategoryForCreateDto item in dto)
            {
                
                _productionServiceContext.ProductSubCategoryService.Insert(item);
            }
        }

        // POST: ProductSubcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductSubcategoryId,ProductCategoryId,Name,Rowguid,ModifiedDate")] ProductSubcategory productSubcategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productSubcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", productSubcategory.ProductCategoryId);
            return View(productSubcategory);
        }

        // GET: ProductSubcategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSubcategory = await _context.ProductSubcategories.FindAsync(id);
            if (productSubcategory == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", productSubcategory.ProductCategoryId);
            return View(productSubcategory);
        }

        // POST: ProductSubcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductSubcategoryId,ProductCategoryId,Name,Rowguid,ModifiedDate")] ProductSubcategory productSubcategory)
        {
            if (id != productSubcategory.ProductSubcategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSubcategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSubcategoryExists(productSubcategory.ProductSubcategoryId))
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
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", productSubcategory.ProductCategoryId);
            return View(productSubcategory);
        }

        // GET: ProductSubcategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSubcategory = await _context.ProductSubcategories
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.ProductSubcategoryId == id);
            if (productSubcategory == null)
            {
                return NotFound();
            }

            return View(productSubcategory);
        }

        // POST: ProductSubcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSubcategory = await _context.ProductSubcategories.FindAsync(id);
            _context.ProductSubcategories.Remove(productSubcategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSubcategoryExists(int id)
        {
            return _context.ProductSubcategories.Any(e => e.ProductSubcategoryId == id);
        }
    }
}
