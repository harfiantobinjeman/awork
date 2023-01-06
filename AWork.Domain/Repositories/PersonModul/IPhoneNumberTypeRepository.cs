using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.PersonModul
{
    public interface IPhoneNumberTypeRepository
    {
        Task<IEnumerable<PhoneNumberType>> GetAllPhoneNumberType(bool trackChanges);

        Task<PhoneNumberType> GetPhoneNumberTypeById(int phoneNumberTypeId, bool trackChanges);

        void Insert(PhoneNumberType phoneNumberType);

        void Edit(PhoneNumberType phoneNumberType);

        void Remove(PhoneNumberType phoneNumberType);
    }
}
