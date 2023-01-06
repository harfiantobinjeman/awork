using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AWork.Domain.Models;
using AWork.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AWork.Services.Abstraction;
using AWork.Contracts.Dto.Authentication;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto;
using AWork.Services;
using AWork.Domain.Repositories.PersonModul;
using AWork.Domain.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using AWork.Contracts.Dto.PersonModule.Profile;
using AutoMapper;
using X.PagedList;
using AWork.Contracts.Dto.Purchasing;
using AWork.Persistence.Base;
using AWork.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AWork.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonServiceManager _servisManager;
        private readonly IPersonRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly AdventureWorks2019Context _dbContext;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager,
            IPersonServiceManager servisManager, IPersonRepositoryManager repositoryManager, IMapper mapper, AdventureWorks2019Context dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _servisManager = servisManager;
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            /*      var myName = _userManager.GetUserName(User);*/
            var myUser = await _userManager.GetUserAsync(User);
            var email = await _servisManager.EmailAddressServices.GetEmailAddress(myUser.Email, false);
            var businessEntity = email.BusinessEntityId;
            return View("Index", email);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string email)
        {
            if(email == null)
            {
                return NotFound();
            }
            var getEmail = await _servisManager.EmailAddressServices.GetEmailAddress(email, false);

            var personMdl = await _servisManager.PersonServices.GetAllPersonById(getEmail.BusinessEntityId, false);
            var person = _mapper.Map<PersonDto>(personMdl);

            var personPhoneDto = await _servisManager.PersonPhoneServices.GetPersonPhoneById(getEmail.BusinessEntityId, false);
            var listPersonPhone = new List<PersonPhoneDto>();
            foreach (var item in personPhoneDto)
            {
                listPersonPhone.Add(item);
            }



            var businessEntityAddress = await _servisManager.BusinessEntityAddressServices.GetBusinessEntityAddressByBusinessEntityId(getEmail.BusinessEntityId, false);

            var tempAddress = new AddressDto();
            var tempCountry = new CountryRegionDto();
            if(businessEntityAddress != null)
            {
                var address = await _servisManager.AddressServices.GetAllAddressById(businessEntityAddress.AddressId, false);
                tempAddress = address;
                
                if(address != null)
                {
                    var countryRegionDto = await _servisManager.CountryRegionServices.GetAllCountryRegionByCode(address.StateProvince.CountryRegionCode, false);
                    tempCountry = countryRegionDto;
                }
                
            }


           

            //phone number
            var phone = await _servisManager.PhoneNumberTypeServices.GetAllPhoneNumberType(false);
            ViewData["PhoneNumberType"] = new SelectList(from p in phone.ToList() select new {IDP = p.PhoneNumberTypeId, DeviceType = p.Name}, "IDP", "DeviceType");

            

            //adrress
            var addressTypes = await _servisManager.AddressTypeServices.GetAllAddressType(false);
            ViewData["AddressType"] = new SelectList(from a in addressTypes.ToList() select new {AddressID = a.AddressTypeId, AddType = a.Name}, "AddressID", "AddType");



            var countryRegion = await _servisManager.CountryRegionServices.GetAllCountryRegion(false);
            ViewData["CountryRegion"] = new SelectList(from c in countryRegion.ToList() select new {CountryCD = c.CountryRegionCode, CountryNM = c.Name}, "CountryCD", "CountryNM");

            var stateProvince = await _servisManager.StateProvinceServices.GetAllStateprovince(false);
            ViewData["StateProvince"] = new SelectList(from s in stateProvince.ToList() select new {StateID = s.StateProvinceId, StateNM = s.Name}, "StateID", "StateNM");



            var profile = new ProfileDto
            {
                PersonDto = person,
                PersonPhoneDtos = listPersonPhone,
                BusinessEntityAddressDto = businessEntityAddress,
                AddressDto = tempAddress,
                CountryRegionDto = tempCountry


            };
            

            profile.EmailAddressForCreateDtos.Add(new EmailAddressForCreateDto() { BusinessEntityId = personMdl.BusinessEntityId });
            profile.PersonPhoneForCreateDto.Add(new PersonPhoneForCreateDto() { BusinessEntityId = personMdl.BusinessEntityId });


            return View(profile);

        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProfileDto profileDto)
        {
            if (ModelState.IsValid)
            {
                var profileMdl = profileDto;


                var profileEdit = new PersonDto
                {
                    BusinessEntityId = profileMdl.PersonDto.BusinessEntityId,
                    PersonType = profileMdl.PersonDto.PersonType,
                    FirstName = profileMdl.PersonDto.FirstName,
                    MiddleName = profileMdl.PersonDto.MiddleName,
                    LastName = profileMdl.PersonDto.LastName,
                    Suffix = profileMdl.PersonDto.Suffix,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                _servisManager.PersonServices.Edit(profileEdit);
                _repositoryManager.Save();

                var email = new List<EmailAddressForCreateDto>();
                foreach (var item in profileMdl.EmailAddressForCreateDtos)
                {
                    if (item.EmailAddress1 != null)
                    {
                        var emailEdit = new EmailAddressForCreateDto
                        {
                            BusinessEntityId = profileMdl.PersonDto.BusinessEntityId,
                            EmailAddress1 = item.EmailAddress1,
                        };
                        email.Add(emailEdit);
                        _servisManager.EmailAddressServices.Insert(emailEdit);
                        _repositoryManager.Save();
                    }
                }

                var personPhone = new List<PersonPhoneForCreateDto>();
                foreach (var item in profileMdl.PersonPhoneForCreateDto)
                {
                    if (item.PhoneNumber != null)
                    {

                        var phoneNumberEdit = new PersonPhoneForCreateDto
                        {
                            BusinessEntityId = profileMdl.PersonDto.BusinessEntityId,
                            PhoneNumber = item.PhoneNumber,
                            PhoneNumberTypeId = profileMdl.PersonPhoneDto.PhoneNumberTypeId

                        };
                        personPhone.Add(phoneNumberEdit);
                        _servisManager.PersonPhoneServices.Insert(phoneNumberEdit);
                        _repositoryManager.Save();
                    }
                }

                var address = new AddressForCreateDto
                {
                    AddressLine1 = profileMdl.AddressDto.AddressLine1,
                    City = profileMdl.AddressDto.City,
                    StateProvinceId = profileMdl.AddressDto.StateProvince.StateProvinceId,
                    PostalCode = profileMdl.AddressDto.PostalCode,
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                var businessEntityAddress = new BusinessEntityAddressForCreateDto
                {
                    BusinessEntityId = profileMdl.PersonDto.BusinessEntityId,
                    AddressTypeId = profileMdl.BusinessEntityAddressDto.AddressTypeId,
                    ModifiedDate = DateTime.Now
                };
                /*_servisManager.BusinessEntityAddressServices.insert(businessEntityAddress);*/
                _servisManager.AddressServices.Insert(address, businessEntityAddress);
                _repositoryManager.Save();






              /*  _servisManager.PersonServices.personProfileEdit(profileEdit, email, personPhone, address);*/
                return RedirectToAction("Index", "Home");
            }

            return View();

        }

        public IActionResult Privacy()
        {
            var hour = DateTime.Now.Hour;
            //ternari operation (like if else)
            var greeting = hour > 12 ? "Good Day" : "Good Morning";
            return View("Privacy", greeting);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<JsonResult>GetTotalCountryRegionStateProvince (string name)
        {
            var countryRegionStateProvince = _dbContext.GetCountryRegionStateProvinceSQL.FromSqlRaw("select distinct c.CountryRegionCode,c.name,s.StateProvinceCode,s.Name as Province,s.StateProvinceID\r\nfrom Person.CountryRegion c full outer join \r\nPerson.StateProvince s on c.CountryRegionCode=s.CountryRegionCode\r\nwhere s.Name is not null");
            return Json(countryRegionStateProvince);
        }

       
    }
}