using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.PersonModul
{
    public interface IPasswordRepository
    {
        Task<IEnumerable<Password>> GetAllPassword(bool trackChanges);

        Task<Password> GetPasswordById(int passwordId, bool trackChanges);

        void Insert(Password password);

        void Edit(Password password);

        void Remove(Password password);
    }
}
