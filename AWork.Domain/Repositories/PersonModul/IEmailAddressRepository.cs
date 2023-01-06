using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.PersonModul
{
    public interface IEmailAddressRepository
    {
        Task<IEnumerable<EmailAddress>> GetAllEmailAddress(bool trackChanges);

        Task<EmailAddress> GetEmailAddress(string emailAddress, bool trackChanges);

        Task<EmailAddress>GetEmailId(int businessEntity, bool trackChanges);
        Task<EmailAddress>GetEmailAddressId(string emailAddress, int businessEntity, bool trackChanges);

        void Insert(EmailAddress emailAddress);

        void Edit(EmailAddress emailAddress);

        void Remove(EmailAddress passwoemailAddressrd);
    }
}
