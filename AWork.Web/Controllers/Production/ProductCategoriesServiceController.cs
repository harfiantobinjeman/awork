using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.Production
{
    public class ProductCategoriesServiceController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceContext;
        private readonly IProductionRepositoryManager _productionRepository;

        public ProductCategoriesServiceController(AdventureWorks2019Context context, IProductionServiceManager productionServiceContext, IProductionRepositoryManager productionRepository)
        {
            _context = context;
            _productionServiceContext = productionServiceContext;
            _productionRepository = productionRepository;
        }
        public async Task<JsonResult> GetTotalProductByCategories(string category)
        {
            var result = await _productionRepository.ProductCategoryRepository.GetTotalProductByCategories(category);
            return Json(result);

        }
        public async Task<JsonResult> GetTotalProductBySub(string subcategory)
        {
            var result = await _productionRepository.ProductCategoryRepository.GetTotalProductBySub(subcategory);
            return Json(result);

        }
        public async Task<JsonResult> GetCategoryById(string category)
        {
            var result = await _productionRepository.ProductCategoryRepository.GetCategoryById(category);
            return Json(result);

        }

        public async Task<JsonResult> GetSubcategoriesById(string subcategory)
        {
            var result = await _productionRepository.ProductCategoryRepository.GetSubcategoriesById(subcategory);
            return Json(result);

        }
        public async Task<IActionResult> Chart()
        {
            var allCate = await _productionRepository.ProductSubCategoryRepository.GetAllProdcSubCategory(false);
            ViewData["Name"] = new SelectList(allCate, "Name", "Name");

            return View();
        }

        public async Task<IActionResult> Index2()
        {
            var allCate = await _productionRepository.ProductCategoryRepository.GetAllProdcCategory(false);
            ViewData["Name"] = new SelectList(allCate, "Name", "Name");
     
            return View();
        }

        public IActionResult Index3()
        {
           var mdl = new SelectProductSubCategoryDto();
            return View(mdl);
        }
             

        // GET: ProductCategoriesService
        public async Task<IActionResult> Index(int ?id)
        {
            var model = new SelectProductSubCategoryDto();
            model.productCategoryDto = await _productionServiceContext.ProductCategoryService.GetAllProdcCategory(false);

            if (id.HasValue)
            {
                model.productSubCategoryDto = await _productionServiceContext.ProductSubCategoryService.GetProCateInSubByID((int)id, false);
            }

            return View(model); ;
        }

        // GET: ProductCategoriesService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var productCategory = await _context.ProductCategories
               // .FirstOrDefaultAsync(m => m.ProductCategoryId == id);

            var productCategory = await _productionServiceContext.ProductCategoryService.GetProcdCateById((int)id, false);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: ProductCategoriesService/Create
        public IActionResult Create()
        {
            ProductCategoryForCreatDto categoryDto = new ProductCategoryForCreatDto();
            categoryDto.ProductSubCategoryForCreateDto.Add(new ProductSubCategoryForCreateDto() { ProductCategoryId = 1});
          

            return View(categoryDto);
        }

        // POST: ProductCategoriesService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategoryForCreatDto dto)
        {
            /* foreach (ProductSubCategoryDto subDto in dto.SubCategoryDtos)
             {
                 if (subDto.Name == null)
                     dto.SubCategoryDtos.Remove(subDto);
             }*/

            if (ModelState.IsValid)
            {
                _productionServiceContext.ProductCategoryService.Insert(dto);
                return RedirectToAction(nameof(Index));
            }
            //_productionServiceContext.ProductCategoryService.Insert(dto);
            return View("Index");


        }

        // GET: ProductCategoriesService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _productionServiceContext.ProductCategoryService.GetProcdCateById((int)id, false);
            if (productCategory == null)
            {
                return NotFound();
            }
            productCategory.ProductSubCategoryForCreateDto.Add(new ProductSubCategoryForCreateDto() { ProductCategoryId = 1 });
            return View(productCategory);
        }

        // POST: ProductCategoriesService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCategoryDto productCategory)
        {
            if (id != productCategory.ProductCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(productCategory);
                    await _context.SaveChangesAsync();*/

                    _productionServiceContext.ProductCategoryService.Edit(productCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.ProductCategoryId))
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
            return View(productCategory);
        }

        // GET: ProductCategoriesService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(m => m.ProductCategoryId == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: ProductCategoriesService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(int id)
        {
            return _context.ProductCategories.Any(e => e.ProductCategoryId == id);
        }
    }
}
