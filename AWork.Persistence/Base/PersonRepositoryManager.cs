using AWork.Domain.Base;
using AWork.Domain.Repositories.PersonModul;
using AWork.Persistence.Repositories.PersonModul;
using System.Threading.Tasks;

namespace AWork.Persistence.Base
{
    public class PersonRepositoryManager : IPersonRepositoryManager
    {
        private AdventureWorks2019Context _dbContext;
        private IBusinessEntityRepository _businessEntityRepository;
        private IPersonRepository _personRepository;
        private IStateProvinceRepository _stateProvinceRepository;
        private IAddressRepository _addressRepository;
        private IContactTypeRepository _contactTypeRepository;
        private ICountryRegionRepository _countryRegionRepository;
        private IPasswordRepository _passwordRepository;
        private IPersonPhoneRepository _personPhoneRepository;
        private IAddressTypeRepository _addressTypeRepository;
        private IEmailAddressRepository _emailAddressRepository;
        private IBusinessEntityContactRepository _businessEntityContactRepository;
        private IBusinessEntityAddressRepository _businessEntityAddressRepository;
        private IPhoneNumberTypeRepository _phoneNumberTypeRepository;

        public PersonRepositoryManager(AdventureWorks2019Context dbContext)
        {
            _dbContext = dbContext;
        }

        public IBusinessEntityRepository BusinessEntityRepository
        {
            get
            {
                if (_businessEntityRepository == null)
                {
                    _businessEntityRepository = new BusinessEntityRepository(_dbContext);
                }
                return _businessEntityRepository;
            }

        }

        public IPersonRepository PersonRepository
        {
            get
            {
                if (_personRepository == null)
                {
                    _personRepository = new PersonRepository(_dbContext);
                }
                return _personRepository;
            }
        }

        public IStateProvinceRepository StateProvinceRepository
        {
            get
            {
                if (_stateProvinceRepository == null)
                {
                    _stateProvinceRepository = new StateProvinceRepository(_dbContext);
                }
                return _stateProvinceRepository;
            }
        }

        public IAddressRepository AddressRepository
        {
            get
            {
                if (_addressRepository == null)
                {
                    _addressRepository = new AddressRepository(_dbContext);
                }
                return _addressRepository;
            }
        }

        public IContactTypeRepository ContactTypeRepository
        {
            get
            {
                if (_contactTypeRepository == null)
                {
                    _contactTypeRepository = new ContactTypeRepository(_dbContext);
                }
                return _contactTypeRepository;
            }
        }

        public ICountryRegionRepository CountryRegionRepository
        {
            get
            {
                if (_countryRegionRepository == null)
                {
                    _countryRegionRepository = new CountryRegionRepository(_dbContext);
                }
                return _countryRegionRepository;
            }
        }
        public IPasswordRepository PasswordRepository
        {
            get
            {
                if (_passwordRepository == null)
                {
                    _passwordRepository = new PasswordRepository(_dbContext);
                }
                return _passwordRepository;
            }
        }
        public IPersonPhoneRepository PersonPhoneRepository
        {
            get
            {
                if (_personPhoneRepository == null)
                {
                    _personPhoneRepository = new PersonPhoneRepository(_dbContext);
                }
                return _personPhoneRepository;
            }
        }

        public IAddressTypeRepository AddressTypeRepository
        {
            get
            {
                if (_addressTypeRepository == null)
                {
                    _addressTypeRepository = new AddressTypeRepository(_dbContext);
                }
                return _addressTypeRepository;
            }
        }
        public IEmailAddressRepository EmailAddressRepository
        {
            get
            {
                if (_emailAddressRepository == null)
                {
                    _emailAddressRepository = new EmailAddressRepository(_dbContext);
                }
                return _emailAddressRepository;
            }
        }
        public IBusinessEntityContactRepository BusinessEntityContactRepository
        {
            get
            {
                if (_businessEntityContactRepository == null)
                {
                    _businessEntityContactRepository = new BusinessEntityContactRepository(_dbContext);
                }
                return _businessEntityContactRepository;
            }
        }

        public IBusinessEntityAddressRepository BusinessEntityAddressRepository
        {
            get
            {
                if (_businessEntityAddressRepository == null)
                {
                    _businessEntityAddressRepository = new BusinessEntityAddressRepository(_dbContext);
                }
                return _businessEntityAddressRepository;
            }
        }
        public IPhoneNumberTypeRepository PhoneNumberTypeRepository
        {
            get
            {
                if (_phoneNumberTypeRepository == null)
                {
                    _phoneNumberTypeRepository = new PhoneNumberTypeRepository(_dbContext);
                }
                return _phoneNumberTypeRepository;
            }
        }


        public void Save() => _dbContext.SaveChanges();
        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

    }
}