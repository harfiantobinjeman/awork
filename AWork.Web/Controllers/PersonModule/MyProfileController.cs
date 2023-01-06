using AutoMapper;
using AWork.Contracts.Dto.PersonModule.Profile;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence.Base;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.PersonModul;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWork.Web.Controllers.PersonModule
{
    public class MyProfileController : Controller
    {
        private readonly IPersonRepositoryManager _repositoryManager;
        private readonly IPersonServiceManager _servisManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;



        public MyProfileController(IPersonRepositoryManager repositoryManager, IPersonServiceManager servisManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _servisManager = servisManager;
            _mapper = mapper;

        }

        public IActionResult Index()
        {

            return View();
        }

        //get
        /*      public async Task<IActionResult>Edit(int? id)
              {
                  if(id == null)
                  {
                      return NotFound();
                  }
                  var person = _servisManager.
              }*/



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {




            //phone number
            var phone = await _servisManager.PhoneNumberTypeServices.GetAllPhoneNumberType(false);
            ViewData["PhoneNumbers"] = new SelectList(phone, "PhoneNumberTypeId", "Name");

            //adrress
            var addressTypes = await _servisManager.AddressTypeServices.GetAllAddressType(false);
            ViewData["AddressType"] = new SelectList(addressTypes, "AddressTypeId", "Name");

            return View();


        }
    }
}

/*
        [HttpPost]
        public async Task<IActionResult> DataProfile(int businessEntity, ProfileDto profileDto)
        {
            if (profileDto == null)
            {
                return NotFound();
            }

            
            if (ModelState.IsValid)
            {
                var profile = await _servisManager.MyProfileServices.SaveAll(businessEntity, true);
                _repositoryManager.Save();
                return RedirectToAction("MyProfile", "Edit");
            }

            return View(profileDto);
        }
    }
}*/



/*if (profileDto == null)
{
    return NotFound();
}

var businessEn = _servisManager.BusinessEntityServices.CreateBusinessEntity();
if (ModelState.IsValid)
{
    var profile = await _servisManager.MyProfileServices.SaveAll(businessEntity, true);
    _repositoryManager.Save();
    return RedirectToAction("MyProfile", "Details");
}

return View(profileDto);*/