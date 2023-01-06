using AutoMapper;
using AWork.Contracts.Dto.Purchasing;
using AWork.Domain.Base;
using AWork.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AWork.Web.Controllers
{
    public class OnSaleController : Controller
    {
        // GET: OnSaleController
        private readonly IPurchasingServiceManager _purchasingServiceManager;
        private readonly IPurchasingRepositoryManager _purchasingRepositoryManager;
        private readonly IMapper _mapper;
        public OnSaleController(IMapper mapper, IPurchasingRepositoryManager purchasingRepositoryManager, IPurchasingServiceManager purchasingServiceManager)
        {
            _mapper = mapper;
            _purchasingRepositoryManager = purchasingRepositoryManager;
            _purchasingServiceManager = purchasingServiceManager;
        }
        public async Task<ActionResult> Index()
        {
            var onSale = await _purchasingServiceManager.PurchaseOrderDetailService.GetAllPurchaseOD(false);
            return View(onSale);
        }
        public async Task<ActionResult> AddCart(PurchaseOrderDetailsDto purchaseOrderDetailsDto)
        {
            if (ModelState.IsValid)
            {

            }
            return View(purchaseOrderDetailsDto);
        }

        // GET: OnSaleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OnSaleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OnSaleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OnSaleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OnSaleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OnSaleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OnSaleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
