using AWork.Contracts.Dto.Production;
using AWork.Persistence;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.Production
{
    public class ProductPhotoesServiceController : Controller
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IProductionServiceManager _productionserviceContext;

        public ProductPhotoesServiceController(AdventureWorks2019Context context, IProductionServiceManager productionserviceContext = null)
        {
            _context = context;
            _productionserviceContext = productionserviceContext;
        }

        // GET: ProductPhotoesService
        public async Task<IActionResult> Index()
        {
            var productPhoto = await _context.ProductPhotos.ToListAsync();
            var productPhotoDto = await _productionserviceContext.ProductPhotoService.GetAllProductPhoto(false);
            //return View(await _context.ProductPhotos.ToListAsync());
            return View(productPhotoDto);
        }

        // GET: ProductPhotoesService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            /*var productPhoto = await _context.ProductPhotos
                 .FirstOrDefaultAsync(m => m.ProductPhotoId == id);*/
            var productPhotoDto = await _productionserviceContext.ProductPhotoService.GetProductPhotoById((int)id, false);
            if (productPhotoDto == null)
            {
                return NotFound();
            }

            return View(productPhotoDto);
        }

        // GET: ProductPhotoesService/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductPhotoesService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThumbnailPhotoFileName,LargePhotoFileName,ModifiedDate, FilePhoto")] ProductPhotoForCreateDto productPhotoForCreateDto)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var file = productPhotoForCreateDto.FilePhoto;
                    var folderName = Path.Combine("Resources", "images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Count > 0)
                    {
                        foreach (var files in file)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                files.CopyTo(stream);
                            }
                            var productDto = new ProductPhotoDto
                            {
                                LargePhotoFileName = productPhotoForCreateDto.LargePhotoFileName,
                                ModifiedDate = productPhotoForCreateDto.ModifiedDate,
                                ThumbnailPhotoFileName = fileName
                            };

                            _productionserviceContext.ProductPhotoService.Insert(productDto);
                            /*_context.Add(productPhoto);
                            await _context.SaveChangesAsync();*/
                        }
                return RedirectToAction(nameof(Index));
                     
            }
        }

                catch (Exception ex)
                {
                    throw;
                }
            }
            return View(productPhotoForCreateDto);
        }


        // GET: ProductPhotoesService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhoto = await _productionserviceContext.ProductPhotoService.GetProductPhotoById((int)id, true);
            //var productPhoto = await _context.ProductPhotos.FindAsync(id);
            if (productPhoto == null)
            {
                return NotFound();
            }
            return View(productPhoto);
        }

        // POST: ProductPhotoesService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductPhotoId,ThumbNailPhoto,ThumbnailPhotoFileName,LargePhoto,LargePhotoFileName,ModifiedDate")] ProductPhotoDto productPhotoDto)
        {
            if (id != productPhotoDto.ProductPhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(productPhoto);
                    await _context.SaveChangesAsync();*/
                    _productionserviceContext.ProductPhotoService.Edit(productPhotoDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPhotoExists(productPhotoDto.ProductPhotoId))
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
            return View(productPhotoDto);
        }

        // GET: ProductPhotoesService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var productPhoto = await _context.ProductPhotos
            //.FirstOrDefaultAsync(m => m.ProductPhotoId == id);
            var productPhoto = await _productionserviceContext.ProductPhotoService.GetProductPhotoById((int)id, false);
            if (productPhoto == null)
            {
                return NotFound();
            }

            return View(productPhoto);
        }

        // POST: ProductPhotoesService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productPhotoModel1 = await _productionserviceContext.ProductPhotoService.GetProductPhotoById((int)id, false);
            _productionserviceContext.ProductPhotoService.Remove(productPhotoModel1);
            /*var productPhoto = await _context.ProductPhotos.FindAsync(id);
            _context.ProductPhotos.Remove(productPhoto);
            await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPhotoExists(int id)
        {
            return _context.ProductPhotos.Any(e => e.ProductPhotoId == id);
        }
    }
}
