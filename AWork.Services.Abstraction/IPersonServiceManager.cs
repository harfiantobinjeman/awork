using AWork.Services.Abstraction.PersonModul;

namespace AWork.Services.Abstraction
{
    public interface IPersonServiceManager
    {

        IBusinessEntityServices BusinessEntityServices { get; }
        IPersonServices PersonServices { get; }
        IStateProvinceServices StateProvinceServices { get; }
        IAddressServices AddressServices { get; }
        IContactTypeServices ContactTypeServices { get; }
        ICountryRegionServices CountryRegionServices { get; }
        IProfileServices ProfileServices { get; }


        IPasswordServices PasswordServices { get; }
        IPersonPhoneServices PersonPhoneServices { get; }
        IAddressTypeServices AddressTypeServices { get; }
        IEmailAddressServices EmailAddressServices { get; }
        IBusinessEntityContactServices BusinessEntityContactServices { get; }
        IBusinessEntityAddressServices BusinessEntityAddressServices { get; }
        IPhoneNumberTypeServices PhoneNumberTypeServices { get; }


    }
}
