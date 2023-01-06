using AutoMapper;
using AWork.Domain.Base;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.PersonModul;
using AWork.Services.PersonService;
using System;

namespace AWork.Services
{
    public class PersonServiceManager : IPersonServiceManager
    {
        private readonly Lazy<IBusinessEntityServices> _lazybusinessEntityServices;
        private readonly Lazy<IPersonServices> _lazypersonServices;
        private readonly Lazy<IStateProvinceServices> _lazystateProvinceServices;
        private readonly Lazy<IAddressServices> _lazyAddressServices;
        private readonly Lazy<IContactTypeServices> _lazycontactTypeServices;
        private readonly Lazy<ICountryRegionServices> _lazycountryRegionServices;
        private readonly Lazy<IPasswordServices> _lazypasswordServices;
        private readonly Lazy<IPersonPhoneServices> _lazypersonPhoneServices;
        private readonly Lazy<IAddressTypeServices> _lazyaddressTypeServices;
        private readonly Lazy<IEmailAddressServices> _lazyemailAddressServices;
        private readonly Lazy<IBusinessEntityContactServices> _lazybusinessEntityContactServices;
        private readonly Lazy<IBusinessEntityAddressServices> _lazybusinessEntityAddressServices;
        private readonly Lazy<IPhoneNumberTypeServices> _lazyphoneNumberTypeServices;
        private readonly Lazy<IProfileServices> _lazyProfileServices;


        public PersonServiceManager(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazybusinessEntityServices = new Lazy<IBusinessEntityServices>(
                () => new BusinessEntityServices(repositoryManager, mapper)
                );

            _lazypersonServices = new Lazy<IPersonServices>(
                () => new PersonServices(repositoryManager, mapper)
                );

            _lazyAddressServices = new Lazy<IAddressServices>(
                () => new AddressServices(repositoryManager, mapper)
                );
            _lazystateProvinceServices = new Lazy<IStateProvinceServices>(
                () => new StateProvinceServices(repositoryManager, mapper)
                );
            _lazycontactTypeServices = new Lazy<IContactTypeServices>(
                () => new ContactTypeServices(repositoryManager, mapper)
                );
            _lazycountryRegionServices = new Lazy<ICountryRegionServices>(
                () => new CountryRegionServices(repositoryManager, mapper)
                );
            _lazycountryRegionServices = new Lazy<ICountryRegionServices>(
                () => new CountryRegionServices(repositoryManager, mapper)
                );
            _lazypasswordServices = new Lazy<IPasswordServices>(
                () => new PasswordServices(repositoryManager, mapper)
                );
            _lazypasswordServices = new Lazy<IPasswordServices>(
                () => new PasswordServices(repositoryManager, mapper)
                );
            _lazypersonPhoneServices = new Lazy<IPersonPhoneServices>(
                 () => new PersonPhoneServices(repositoryManager, mapper)
                );
            _lazyaddressTypeServices = new Lazy<IAddressTypeServices>(
                () => new AddressTypeServices(repositoryManager, mapper)
                );
            _lazyemailAddressServices = new Lazy<IEmailAddressServices>(
                () => new EmailAddressServices(repositoryManager, mapper)
               );
            _lazybusinessEntityContactServices = new Lazy<IBusinessEntityContactServices>(
                    () => new BusinessEntityContactServices(repositoryManager, mapper)
                    );
            _lazybusinessEntityAddressServices = new Lazy<IBusinessEntityAddressServices>(
                    () => new BusinessEntityAddressServices(repositoryManager, mapper)
                    );
            _lazyphoneNumberTypeServices = new Lazy<IPhoneNumberTypeServices>(
                    () => new PhoneNumberTypeServices(repositoryManager, mapper)
                    );
            _lazyProfileServices = new Lazy<IProfileServices>(
                () => new ProfileServices(repositoryManager, mapper)
                );


        }

        public IBusinessEntityServices BusinessEntityServices => _lazybusinessEntityServices.Value;

        public IPersonServices PersonServices => _lazypersonServices.Value;

        public IStateProvinceServices StateProvinceServices => _lazystateProvinceServices.Value;

        public IAddressServices AddressServices => _lazyAddressServices.Value;

        public IContactTypeServices ContactTypeServices => _lazycontactTypeServices.Value;

        public ICountryRegionServices CountryRegionServices => _lazycountryRegionServices.Value;

        public IPasswordServices PasswordServices => _lazypasswordServices.Value;

        public IPersonPhoneServices PersonPhoneServices => _lazypersonPhoneServices.Value;

        public IAddressTypeServices AddressTypeServices => _lazyaddressTypeServices.Value;

        public IEmailAddressServices EmailAddressServices => _lazyemailAddressServices.Value;

        public IBusinessEntityContactServices BusinessEntityContactServices => _lazybusinessEntityContactServices.Value;

        public IBusinessEntityAddressServices BusinessEntityAddressServices => _lazybusinessEntityAddressServices.Value;

        public IPhoneNumberTypeServices PhoneNumberTypeServices => _lazyphoneNumberTypeServices.Value;

        public IProfileServices ProfileServices => _lazyProfileServices.Value;
    }
}