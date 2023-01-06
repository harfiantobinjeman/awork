using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.PersonModul
{
    public interface IBusinessEntityContactRepository
    {
        Task<IEnumerable<BusinessEntityContact>> GetAllBusinessEntityContact(bool trackChanges);

        Task<BusinessEntityContact> GetBusinessEntityContactById(int businessEntityContactId, bool trackChanges);

        void Insert(BusinessEntityContact businessEntityContact);

        void Edit(BusinessEntityContact businessEntityContact);

        void Remove(BusinessEntityContact businessEntityContact);
    }
}
