
using AWork.Domain.Repositories.PersonModul;
using System.Threading.Tasks;

namespace AWork.Domain.Base
{
    public interface IPersonRepositoryManager
    {

        IBusinessEntityRepository BusinessEntityRepository { get; }
        IPersonRepository PersonRepository { get; }
        IStateProvinceRepository StateProvinceRepository { get; }
        IAddressRepository AddressRepository { get; }
        IContactTypeRepository ContactTypeRepository { get; }
        ICountryRegionRepository CountryRegionRepository { get; }

        IPasswordRepository PasswordRepository { get; }
        IPersonPhoneRepository PersonPhoneRepository { get; }
        IAddressTypeRepository AddressTypeRepository { get; }
        IEmailAddressRepository EmailAddressRepository { get; }
        IBusinessEntityContactRepository BusinessEntityContactRepository { get; }
        IBusinessEntityAddressRepository BusinessEntityAddressRepository { get; }
        IPhoneNumberTypeRepository PhoneNumberTypeRepository { get; }

        void Save();

        Task SaveAsync();
    }
}
