using Microsoft.AspNetCore.Mvc;

namespace AWork.Web.Controllers.Person
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
