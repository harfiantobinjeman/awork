using AWork.Domain.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.HumanResource
{
    public class EmployeeChartController : Controller
    {
        public IHRRepositoryManager _repositoryManager;

        public EmployeeChartController(IHRRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public JsonResult GetEmployeeTiapDepartement()
        {
            var result = _repositoryManager.EmployeeRepository.GetEmployeeTiapDepartement();
            return Json(result);
        }
        public JsonResult GetSalaryRateEmp()
        {
            var result = _repositoryManager.EmployeeRepository.GetSalaryRateEmp();
            return Json(result);
        }

        // GET: EmployeeChartController
        public IActionResult IndexChart()
        {
            return View();
        }


    }
}
