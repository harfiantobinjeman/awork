using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AWork.Contracts.Dto.Authentication;
using AWork.Domain.Models;
using System.Threading.Tasks;
using AWork.Domain.Configuration;
using AWork.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using AWork.Contracts.Dto.PersonModule;
using AWork.Services;
using AWork.Domain.Base;
using AWork.Persistence.Base;
using AWork.Contracts.Dto;

namespace AWork.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPersonRepositoryManager _personRepository;
        private readonly IPersonServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IMapper mapper, UserManager<User> userManager,
            SignInManager<User> signInManager, IPersonServiceManager serviceManager, IPersonRepositoryManager personRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _serviceManager = serviceManager;
            _personRepository = personRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto,
            string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginDto);
            }
            //Check Valit email and password
            var result = await _signInManager.PasswordSignInAsync(
                userLoginDto.Email,
                userLoginDto.Password,
                userLoginDto.RememberMe,
                false
                );
            if (result.Succeeded)
            {

                return RedirecToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        private IActionResult RedirecToLocal(string returnUlr)
        {
            if (Url.IsLocalUrl(returnUlr))
            {
                return Redirect(returnUlr);
            }
            else
            {

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto, PersonForCreateDto personForCreateDto,
            EmailAddressForCreateDto emailAddressForCreateDto)
        {

            if (!ModelState.IsValid)
            {
                return View(userRegistrationDto);
            }
            //Mapping between user and Dto
            var userMdl = _mapper.Map<User>(userRegistrationDto);

            //insert user to Dto
            var result = await _userManager.CreateAsync(userMdl, userRegistrationDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userRegistrationDto);
            }
            else
            {

                var servisManager = new PersonServiceManager(_personRepository, _mapper);
                var businessEntity = _serviceManager.BusinessEntityServices.CreateBusinessEntity();
                var personForCreate = new PersonForCreateDto()
                {
                    BusinessEntityId = businessEntity.BusinessEntityId,
                    FirstName = userRegistrationDto.FirstName,
                    PersonType = userRegistrationDto.PersonType,
                    LastName = userRegistrationDto.LastName

                };
                var personDto = await _serviceManager.PersonServices.CreatePerson(personForCreate);

                var email = new EmailAddressForCreateDto()
                {
                    BusinessEntityId = personDto.BusinessEntityId,
                    EmailAddressId = emailAddressForCreateDto.EmailAddressId,
                    EmailAddress1 = userRegistrationDto.Email
                };
                var emailDto = await _serviceManager.EmailAddressServices.CreateEmail(email);

                var iPersonType = "";
                if (userRegistrationDto.PersonType == "EM")
                {
                    iPersonType = "EMPLOYEE";

                    await _userManager.AddToRoleAsync(userMdl, iPersonType);
                    var x = await _userManager.AddToRoleAsync(userMdl, iPersonType);
                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }
                else if (userRegistrationDto.PersonType == "SP")
                {
                    iPersonType = "SALES PERSON";
                    await _userManager.AddToRoleAsync(userMdl, iPersonType);
                    var x = _userManager.AddToRoleAsync(userMdl, iPersonType);
                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }
                else if (userRegistrationDto.PersonType == "IN")
                {
                    iPersonType = "INDIVIDUAL CUSTOMER";
                    await _userManager.AddToRoleAsync(userMdl, iPersonType);
                    var x = _userManager.AddToRoleAsync(userMdl, iPersonType);
                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }
                else if (userRegistrationDto.PersonType == "SC")
                {
                    iPersonType = "STORE CONTACT";

                    await _userManager.AddToRoleAsync(userMdl, iPersonType);

                    var x = _userManager.AddToRoleAsync(userMdl, iPersonType);

                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }
                else if (userRegistrationDto.PersonType == "VC")
                {
                    iPersonType = "VENDOR CONTACT";

                    await _userManager.AddToRoleAsync(userMdl, iPersonType);

                    var x = _userManager.AddToRoleAsync(userMdl, iPersonType);

                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }
                else if (userRegistrationDto.PersonType == "GC")
                {
                    iPersonType = "GENERAL CONTACT";

                    await _userManager.AddToRoleAsync(userMdl, iPersonType);
                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }

                return View(User);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }



    }
}