using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.PersonModule
{
    public class DashboardController : Controller
    {
        public IPersonRepositoryManager _repositoryManager;

        public DashboardController(IPersonRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<JsonResult> GetTotalPersonByPersonType(string personType)
        {
            var result = await _repositoryManager.PersonRepository.GetTotalPersonByPersonType(personType);
            return Json(result);

        }
/*        public async Task<JsonResult> GetTotalPersonByCountry(string countryRegionCode)
        {
            var result = await _repositoryManager.CountryRegionRepository.GetTotalPersonByCountry(countryRegionCode);
            return Json(result);

        }*/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostPerson([FromBody] PersonForCreateDto personForCreateDto)
        {
            var personForCreate = personForCreateDto;
            var result = new JsonResult(null)
            {
                Value = "Succeed"
            };
            return Ok(result);
        }
    }

}
