using AWork.Domain.Models;
using AWork.Domain.Repositories.PersonModul;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Persistence.Repositories.PersonModul
{
    public class PasswordRepository : RepositoryBase<Password>, IPasswordRepository

    {
        public PasswordRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public void Edit(Password password)
        {
            Update(password);
        }

        public async Task<IEnumerable<Password>> GetAllPassword(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.PasswordHash).ToListAsync();
        }

        public async Task<Password> GetPasswordById(int passwordId, bool trackChanges)
        {
            return await FindByCondition(c => c.PasswordHash.Equals(passwordId), trackChanges).SingleOrDefaultAsync();
        }

        public void Insert(Password password)
        {
            Create(password);
        }

        public void Remove(Password password)
        {
            Delete(password);
        }
    }
}
