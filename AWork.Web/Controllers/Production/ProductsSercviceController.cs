using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
using AWork.Contracts.Dto.HumanResources;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Models;
using AWork.Persistence;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.Production;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;
using AutoMapper;

namespace AWork.Web.Controllers.Production
{
    public class ProductsSercviceController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionServiceContext;
        private readonly IUtilityService _utilityService;
        private readonly IMapper _mapper;

        public ProductsSercviceController(AdventureWorks2019Context context, IProductionServiceManager productionServiceContext = null, IUtilityService utilityService = null, IMapper mapper = null)
        {
            _context = context;
            _productionServiceContext = productionServiceContext;
            _utilityService = utilityService;
            _mapper = mapper;
        }

        // GET: ProductsSercvice
        public async Task<IActionResult> Index(string sortOrder, string searchString,
            string currentFilter, int? page, int? pagesize)
        {
            // set page
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            // default size is 5 otherwise take pageSize value  
            int defaSize = (pagesize ?? 5);
            ViewBag.psize = defaSize;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.currentFilter = searchString;

            //var prodDtos = await _productionServiceContext.ProductService.GetProductPaged(pageIndex, defaSize, false);
            //Create a instance of our DataContext  
            var productDtos = await _productionServiceContext.ProductService.GetAllProduct(false);
            //IPagedList<ProductDto> products = null;

            //var totalRows = productDtos.Count();

            //var totalRows = prod.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                productDtos = productDtos.Where(p => p.Name.ToLower().Contains(searchString.ToLower()) ||
                     p.ProductNumber.ToLower().Contains(searchString.ToLower())); /*||
                p.ProductSubcategory.Name.ToLower().Contains(searchString.ToLower()));*/
            }

            //Dropdownlist code for PageSize selection  
            //In View Attach this  
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" }
            };


            // Sort Data
            ViewBag.NameProductSort = String.IsNullOrEmpty(sortOrder) ? "product_name" : "";
            ViewBag.UnitPriceSort = sortOrder == "UnitPrice" ? "unitPrice_desc" : "UnitPrice";

            switch (sortOrder)
            {
                case "product_name":
                    productDtos = productDtos.OrderByDescending(s => s.Name);
                    break;
                case "UnitProduct_desc":
                    productDtos = productDtos.OrderBy(s => s.ProductNumber);
                    break;
                default:
                    productDtos = productDtos.OrderBy(s => s.Name);
                    break;
            }

            //Alloting nos. of records as per pagesize and page index.  
            //productDtos = products.ToPagedList(pageIndex, defaSize);
            return View(productDtos.ToPagedList(pageIndex, defaSize));
            //return View(productDtosPaged);


           
        }

        // GET: ProductsSercvice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productionServiceContext.ProductService.GetProductById((int)id, false);
           
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult>CreateProductPhoto(UploadImages uploadImages)
        {

            if (ModelState.IsValid)
            {
                /*var uploadImagesDto1 = uploadImages;
                var Photo1 = new ProductPhotoForCreateDto();

                var photos = uploadImagesDto1.Photo1;
                var fileName1 = _utilityService.UploadSingleFile(photos);
                var photo1 = new ProductPhotoForCreateDto
                {
                    ThumbnailPhotoFileName = fileName1,
                    LargePhotoFileName = photos.FileName,
                    LargePhoto = ((byte)photos.Length),
                    ModifiedDate = System.DateTime.Now

                 };*/

          
                

                var uploadImagesDto = uploadImages;
                var listPhoto = new List<ProductPhotoForCreateDto>();

                foreach (var itemPhoto in uploadImagesDto.FileImages)
                {
                    var fileName = _utilityService.UploadSingleFile(itemPhoto);
                    var photo = new ProductPhotoForCreateDto
                    {
                        ThumbnailPhotoFileName = fileName,
                        LargePhotoFileName = itemPhoto.FileName,
                        LargePhoto = ((byte)itemPhoto.Length),
                        ModifiedDate = System.DateTime.Now
                    };

                    listPhoto.Add(photo);

                }
                _productionServiceContext.ProductService.CreateProductIdPhoto(uploadImagesDto.ProductFCDto, uploadImagesDto.ProductProductPhotoFCDto, listPhoto);
            }
                return RedirectToAction(nameof(Index));
        }
/*        public async Task<IActionResult> EditProductPhoto(UploadImages uploadImages)
        {

            if (ModelState.IsValid)
            {
                *//*var uploadImagesDto1 = uploadImages;
                var Photo1 = new ProductPhotoForCreateDto();

                var photos = uploadImagesDto1.Photo1;
                var fileName1 = _utilityService.UploadSingleFile(photos);
                var photo1 = new ProductPhotoForCreateDto
                {
                    ThumbnailPhotoFileName = fileName1,
                    LargePhotoFileName = photos.FileName,
                    LargePhoto = ((byte)photos.Length),
                    ModifiedDate = System.DateTime.Now

                 };*//*




                var uploadImagesDto = uploadImages;
                var listPhoto = new List<ProductPhotoDto>();

                foreach (var itemPhoto in uploadImagesDto.FileImages)
                {
                    var fileName = _utilityService.UploadSingleFile(itemPhoto);
                    var photo = new ProductPhotoDto
                    {
                        ThumbnailPhotoFileName = fileName,
                        LargePhotoFileName = itemPhoto.FileName,
                        ModifiedDate = System.DateTime.Now
                    };

                    listPhoto.Add(photo);

                }
                _productionServiceContext.ProductService.EditProductIdPhoto(uploadImagesDto.Product, uploadImagesDto.ProductProductPhotoFCDto, listPhoto);
            }
            return RedirectToAction(nameof(Index));
        }*/
        // GET: ProductsSercvice/Create
        public async Task<IActionResult> CreateAsync()
        {
            //ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name");
            ViewData["ProductSubcategoryId"] = new SelectList(await _productionServiceContext.ProductSubCategoryService.GetAllProdcSubCategory(false), "ProductSubcategoryId", "Name");
            ViewData["SizeUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(false), "UnitMeasureCode", "UnitMeasureCode");
            ViewData["WeightUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(false), "UnitMeasureCode", "UnitMeasureCode");
            ViewBag.Style = new List<SelectListItem>()
            {
                new SelectListItem() { Value="W", Text= "Women" },
                new SelectListItem() { Value="M", Text= "Man" },
                new SelectListItem() { Value="U", Text= "Universal" }
            };
            ViewBag.Class = new List<SelectListItem>()
            {
                new SelectListItem() { Value="H", Text= "High" },
                new SelectListItem() { Value="M", Text= "Medium" },
                new SelectListItem() { Value="L", Text= "low" }
            };
            ViewBag.ProductLine = new List<SelectListItem>()
            {
                new SelectListItem() { Value="R", Text= "Road" },
                new SelectListItem() { Value="M", Text= "Mountain" },
                new SelectListItem() { Value="T", Text= "Touring" },
                new SelectListItem() { Value="S", Text= "Standar" }
            };
            ViewBag.Size = new List<SelectListItem>()
            {
                new SelectListItem() { Value="S", Text= "Small" },
                new SelectListItem() { Value="M", Text= "Medium" },
                new SelectListItem() { Value="L", Text= "Large" },
                new SelectListItem() { Value="XL", Text= "XtraLarge" }
            };
            ViewBag.Color = new List<SelectListItem>()
            {
                new SelectListItem() { Value="Red", Text= "Red" },
                new SelectListItem() { Value="Black", Text= "Black" },
                new SelectListItem() { Value="Blue", Text= "Blue" },
                new SelectListItem() { Value="White", Text= "White" },
                new SelectListItem() { Value="Pink", Text= "Pink" },
                new SelectListItem() { Value="Silver", Text= "Silver" },
                new SelectListItem() { Value="Purple", Text= "Purple" },
                new SelectListItem() { Value="Green", Text= "Green" },
                new SelectListItem() { Value="Yellow", Text= "Yellow" }
            };
            return View();
        }

        // POST: ProductsSercvice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate")] ProductForCreateDto productForCreateDto)
        //public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate")] ProductForCreateDto productForCreateDto)

        {
            if (ModelState.IsValid)
            {
                /* _context.Add(product);
                 await _context.SaveChangesAsync();*/
                _productionServiceContext.ProductService.Insert(productForCreateDto);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductSubcategoryId"] = new SelectList(await _productionServiceContext.ProductSubCategoryService.GetAllProdcSubCategory(false), "ProductSubcategoryId", "Name", productForCreateDto.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(false), "UnitMeasureCode", "UnitMeasureCode", productForCreateDto.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(false), "UnitMeasureCode", "UnitMeasureCode", productForCreateDto.WeightUnitMeasureCode);

            return View(productForCreateDto);
        }

        // GET: ProductsSercvice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }
            var product = await _productionServiceContext.ProductService.GetProductById((int)id, true);
            //var product = await _context.Products.FindAsync(id);

            //var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductSubcategoryId"] = new SelectList(await _productionServiceContext.ProductSubCategoryService.GetAllProdcSubCategory(true), "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(true), "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(true), "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);
           
            return View(product);*/
            if (id == null)
            {
                return NotFound();
            }

            var productEdit = await _productionServiceContext.ProductService.GetProductById((int)id, true);
            var mapproductDto = _mapper.Map<ProductDto>(productEdit);


            var productProductPhotoEdit = await _productionServiceContext.ProductProductPhotoService.GetProductProductPhotoById((int)productEdit.ProductProductPhotos.FirstOrDefault().ProductPhotoId, true);
            var mapproductproductPhotoDto = _mapper.Map<ProductProductPhotoDto>(productProductPhotoEdit);

            var productPhotoEdit = await _productionServiceContext.ProductPhotoService.GetProductPhotoById((int)id, true);
            var mapproductPhotoDto = _mapper.Map<ProductPhotoDto>(productPhotoEdit);

            var uploadImagesEdits = new UploadImagesEdit
            {
                productDto = mapproductDto,
                ProductProductPhotoDto = mapproductproductPhotoDto,
                productPhotoDto = mapproductPhotoDto
            };

            if (uploadImagesEdits == null)
            {
                return NotFound();
            }
            /*
                        var TitleJob = await _repositoryContext.EmployeeRepository.GetJobTitle(false);
                        ViewData["JobTitle"] = new SelectList(TitleJob, "JobTitle", "JobTitle");

                        var hari = await _context.ShiftService.GetAllShift(false);
                        ViewData["ShiftoId"] = new SelectList(from s in hari.ToList() select new { IDS = s.ShiftId, ShiftTime = s.Name }, "IDS", "ShiftTime");

                        var departemon = await _context.DepartmentService.GetAllDepartment(false);
                        ViewData["DepartementId"] = new SelectList(from d in departemon.ToList() select new { IDD = d.DepartmentId, Dname = d.Name }, "IDD", "Dname");
                          ViewData["ProductSubcategoryId"] = new SelectList(await _productionServiceContext.ProductSubCategoryService.GetAllProdcSubCategory(true), "ProductSubcategoryId", "Name", uploadImagesEdit.productDto.ProductSubcategoryId);
                        ViewData["SizeUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(true), "UnitMeasureCode", "UnitMeasureCode", uploadImagesEdit.productDto.SizeUnitMeasureCode);
                        ViewData["WeightUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(true), "UnitMeasureCode", "UnitMeasureCode", uploadImagesEdit.productDto.WeightUnitMeasureCode);

            */
            ViewData["ProductSubcategoryId"] = new SelectList(await _productionServiceContext.ProductSubCategoryService.GetAllProdcSubCategory(true), "ProductSubcategoryId", "Name", uploadImagesEdits.productDto.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(true), "UnitMeasureCode", "UnitMeasureCode", uploadImagesEdits.productDto.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(true), "UnitMeasureCode", "UnitMeasureCode", uploadImagesEdits.productDto.WeightUnitMeasureCode);

            return View(uploadImagesEdits);

        }

        // POST: ProductsSercvice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UploadImagesEdit uploadImagesEdit)
        {
            /*if (id != uploadImages.Product.ProductId)
            {
                *//*var uploadImagesDto = uploadImages;
                var listPhoto = new List<ProductPhotoForCreateDto>();*//*
                return NotFound();
               
            }*/
            if ( ! ModelState.IsValid)
            {

                var productEdit = new ProductDto
                {
                    ProductId = uploadImagesEdit.productDto.ProductId,
                    Name = uploadImagesEdit.productDto.Name,
                    ProductNumber = uploadImagesEdit.productDto.ProductNumber,
                    Size = uploadImagesEdit.productDto.Size,
                    MakeFlag = uploadImagesEdit.productDto.MakeFlag,
                    FinishedGoodsFlag = uploadImagesEdit.productDto.MakeFlag,
                    Color = uploadImagesEdit.productDto.Color,
                    SafetyStockLevel = uploadImagesEdit.productDto.SafetyStockLevel,
                    ReorderPoint = uploadImagesEdit.productDto.ReorderPoint,
                    StandardCost = uploadImagesEdit.productDto.StandardCost,
                    ListPrice = uploadImagesEdit.productDto.ListPrice,
                    SizeUnitMeasureCode = uploadImagesEdit.productDto.SizeUnitMeasureCode,
                    WeightUnitMeasureCode = uploadImagesEdit.productDto.WeightUnitMeasureCode,
                    Weight = uploadImagesEdit.productDto.Weight,
                    DaysToManufacture = uploadImagesEdit.productDto.DaysToManufacture,
                    ProductLine = uploadImagesEdit.productDto.ProductLine,
                    Class = uploadImagesEdit.productDto.Class,
                    Style = uploadImagesEdit.productDto.Style,
                    ProductSubcategoryId = uploadImagesEdit.productDto.ProductSubcategoryId,
                    ProductModelId = uploadImagesEdit.productDto.ProductModelId,
                    SellStartDate = uploadImagesEdit.productDto.SellStartDate,
                    SellEndDate = uploadImagesEdit.productDto.SellEndDate,
                    DiscontinuedDate = uploadImagesEdit.productDto.DiscontinuedDate,
                    ModifiedDate = DateTime.Now
                };

                var productPhotoEdit = new ProductPhotoDto
                {
                    ProductPhotoId = uploadImagesEdit.productPhotoDto.ProductPhotoId,
                    ThumbnailPhotoFileName = uploadImagesEdit.productPhotoDto.ThumbnailPhotoFileName,
                    LargePhoto = uploadImagesEdit.productPhotoDto.LargePhoto,
                    LargePhotoFileName = uploadImagesEdit.productPhotoDto.LargePhotoFileName,
                    ModifiedDate = DateTime.Now
                };


                var uploadImagesDto = uploadImagesEdit;
                var listPhoto = new List<ProductPhotoDto>();

                foreach (var itemPhoto in uploadImagesDto.FileImages)
                {
                    var fileName = _utilityService.UploadSingleFile(itemPhoto);
                    var photo = new ProductPhotoDto
                    {
                        ThumbnailPhotoFileName = fileName,
                        LargePhotoFileName = itemPhoto.FileName,
                        //LargePhoto = ((byte)itemPhoto.Length),
                        ModifiedDate = System.DateTime.Now
                    };

                    listPhoto.Add(photo);

                }

                var productproductEdit = new ProductProductPhotoDto
                {
                    ProductId = productEdit.ProductId,
                    ProductPhotoId = productPhotoEdit.ProductPhotoId,
                    //Primary = uploadImagesEdit.ProductProductPhotoDto.Primary,
                    ModifiedDate = DateTime.Now
                };

                _productionServiceContext.ProductService.EditProductIdPhoto(uploadImagesEdit.productDto, uploadImagesEdit.ProductProductPhotoDto, listPhoto);
                //_productionServiceContext.ProductService.Edits(productEdit, productproductEdit, productPhotoEdit);
                return RedirectToAction("Index", "ProductService");
            }
            ViewData["ProductSubcategoryId"] = new SelectList(await _productionServiceContext.ProductSubCategoryService.GetAllProdcSubCategory(true), "ProductSubcategoryId", "Name", uploadImagesEdit.productDto.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(true), "UnitMeasureCode", "UnitMeasureCode", uploadImagesEdit.productDto.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(await _productionServiceContext.UnitMeasureservice.GetAllUnitMeasure(true), "UnitMeasureCode", "UnitMeasureCode", uploadImagesEdit.productDto.WeightUnitMeasureCode);

            return View("Index");
        }

        /*try
        {
            _productionServiceContext.ProductService.Edit(uploadImages);
        }
        catch (DbUpdateConcurrencyException)
        {

            if (!ProductExists(uploadImages.Product.ProductId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }*/
        /*  }
          return RedirectToAction(nameof(Index));
      }
*/
        /* if (ModelState.IsValid)
         {
             try
             {

                 _productionServiceContext.ProductService.Edit(productDto);
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!ProductExists(productDto.ProductId))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return RedirectToAction(nameof(Index));
         }*/



        // GET: ProductsSercvice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _productionServiceContext.ProductService.GetProductById((int)id, false);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductsSercvice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var productModel = await _productionServiceContext.ProductService.GetProductById((int)id, false);
            _productionServiceContext.ProductService.Remove(productModel);
           /* var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
