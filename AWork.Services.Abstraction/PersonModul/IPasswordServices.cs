using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWork.Contracts.Dto;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.PersonModul
{
    public interface IPasswordServices
    {
        Task<IEnumerable<PasswordDto>> GetAllPassword(bool trackChanges);
        Task<PasswordDto> GetAllPasswordById(int passwordId, bool trackChanges);
        void Insert(PasswordForCreateDto passwordForCreateDto);
        void Edit(PasswordDto passwordDto);
        void Remove(PasswordDto passwordDto);
    }
}
