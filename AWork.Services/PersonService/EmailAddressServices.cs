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
    public class EmailAddressServices : IEmailAddressServices
    {
        private IPersonRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EmailAddressServices(IPersonRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(EmailAddressDto emailAddressDto)
        {
            var emailAddresMdl = _mapper.Map<EmailAddress>(emailAddressDto);
            _repositoryManager.EmailAddressRepository.Edit(emailAddresMdl);
            _repositoryManager.Save();
        }
        public async Task<IEnumerable<EmailAddressDto>> GetAllEmailAddress(bool trackChanges)
        {
            var emailAddressMdl = await _repositoryManager.EmailAddressRepository.GetAllEmailAddress(trackChanges);
            var emailAddressDto = _mapper.Map<IEnumerable<EmailAddressDto>>(emailAddressMdl);
            return emailAddressDto;
        }

        /*       public async Task<EmailAddressDto> GetAllEmailAddressById(int emailAddressId, bool trackChanges)
               {
                   var emailAddressMdl = await _repositoryManager.EmailAddressRepository.GetEmailAddressById(emailAddressId, trackChanges);
                   var emailAddressDto = _mapper.Map<EmailAddressDto>(emailAddressMdl);
                   return emailAddressDto;
               }*/
        public async Task<EmailAddressDto> GetEmailAddress(string emailAddressId, bool trackChanges)
        {
            var emailMdl = await _repositoryManager.EmailAddressRepository.GetEmailAddress(emailAddressId, trackChanges);
            var emailDto = _mapper.Map<EmailAddressDto>(emailMdl);
            return emailDto;
        }

        public async Task<EmailAddressDto> GetEmailId(int businessEntity, bool trackChanges)
        {
            var emailMdl = await _repositoryManager.EmailAddressRepository.GetEmailId(businessEntity, trackChanges);
            var emailDto = _mapper.Map<EmailAddressDto>(emailMdl);
            return emailDto;
        }

        public void Insert(EmailAddressForCreateDto emailAddressForCreateDto)
        {
            var emailAddressMdl = _mapper.Map<EmailAddress>(emailAddressForCreateDto);
            _repositoryManager.EmailAddressRepository.Insert(emailAddressMdl);
            _repositoryManager.Save();
        }

        public void Delete(EmailAddressDto emailAddressDto)
        {
            var emailAddressDtomdl = _mapper.Map<EmailAddress>(emailAddressDto);
            _repositoryManager.EmailAddressRepository.Remove(emailAddressDtomdl);
            _repositoryManager.Save();
        }

        public async Task<EmailAddressDto> CreateEmail(EmailAddressForCreateDto emailAddressForCreateDto)
        {

            var EmailMdl = _mapper.Map<EmailAddress>(emailAddressForCreateDto);
            _repositoryManager.EmailAddressRepository.Insert(EmailMdl);
            _repositoryManager.Save();

            var emailDto = _mapper.Map<EmailAddressDto>(EmailMdl);
            return emailDto;

        }

        public async Task<EmailAddressDto> GetEmailAddressId(string emailAddress, int businessEntity, bool trackChanges)
        {
            var emailMdl = await _repositoryManager.EmailAddressRepository.GetEmailAddressId(emailAddress, businessEntity, trackChanges);
            var emailDto = _mapper.Map<EmailAddressDto>(emailMdl);
            return emailDto;
        }
    }
}
