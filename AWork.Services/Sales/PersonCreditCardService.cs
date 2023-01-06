using AutoMapper;
using AWork.Contracts.Dto.Sales.PersonCreditCard;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Persistence.Base;
using AWork.Services.Abstraction.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Sales
{
    public class PersonCreditCardService : IPersonCreditCardService
    {
        private readonly ISalesRepositoryManager _resitoryManager;
        private readonly IMapper _mapper;

        public PersonCreditCardService(ISalesRepositoryManager resitoryManager, IMapper mapper)
        {
            _resitoryManager = resitoryManager;
            _mapper = mapper;
        }

        public void Edit(PersonCreditCardDto personCreditCardDto)
        {
            var edit = _mapper.Map<PersonCreditCard>(personCreditCardDto);
            _resitoryManager.PersonCreditCardRepository.Edit(edit);
            _resitoryManager.Save();
        }
        public async Task<IEnumerable<PersonCreditCardDto>> GetAllPersonCreditCards(bool trackChanges)
        {
            var personCreditCardForModel = await _resitoryManager.PersonCreditCardRepository.GetAllPersonCreditCard(trackChanges);
            var personCreditCardForDto = _mapper.Map<IEnumerable<PersonCreditCardDto>>(personCreditCardForModel);
            return personCreditCardForDto;
        }

        public async Task<PersonCreditCardDto> GetPersonCreditCardById(int creditCardId, bool trackChanges)
        {
            var personCreditCardForModel = await _resitoryManager.PersonCreditCardRepository.GetByIdPersonCreditCard(creditCardId, trackChanges);
            var personCeditCardForDto = _mapper.Map<PersonCreditCardDto>(personCreditCardForModel);
            return personCeditCardForDto;
        }

        public void Insert(PersonCreditCardForCreateDto personCreditCardForCreateDto)
        {
            var newUser = _mapper.Map<PersonCreditCard>(personCreditCardForCreateDto);
            _resitoryManager.PersonCreditCardRepository.Insert(newUser);
            _resitoryManager.Save();
        }

        public void Remove(PersonCreditCardDto personCreditCardDto)
        {
            var deleteUser = _mapper.Map<PersonCreditCard>(personCreditCardDto);
            _resitoryManager.PersonCreditCardRepository.Remove(deleteUser);
            _resitoryManager.Save();
        }
    }
}
