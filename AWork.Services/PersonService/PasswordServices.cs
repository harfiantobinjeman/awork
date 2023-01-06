using AutoMapper;
using AWork.Contracts.Dto;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.PersonModul;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.PersonService
{
    public class PasswordServices : IPasswordServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PasswordServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(PasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PasswordDto>> GetAllPassword(bool trackChanges)
        {
            var passwordMdl = await _repositoryManager.PasswordRepository.GetAllPassword(trackChanges);
            var passwordDto = _mapper.Map<IEnumerable<PasswordDto>>(passwordMdl);
            return passwordDto;
        }

        public async Task<PasswordDto> GetAllPasswordById(int passwordId, bool trackChanges)
        {
            var passwordMdl = await _repositoryManager.PasswordRepository.GetPasswordById(passwordId, trackChanges);
            var passwordDto = _mapper.Map<PasswordDto>(passwordMdl);
            return passwordDto;
        }

        public void Insert(PasswordForCreateDto passwordForCreateDto)
        {
            var passwordMdl = _mapper.Map<Password>(passwordForCreateDto);
            _repositoryManager.PasswordRepository.Insert(passwordMdl);
            _repositoryManager.Save();
        }

        public void Remove(PasswordDto passwordDto)
        {
            var passwordMdl = _mapper.Map<Password>(passwordDto);
            _repositoryManager.PasswordRepository.Remove(passwordMdl);
            _repositoryManager.SaveAsync();
        }
    }
}
