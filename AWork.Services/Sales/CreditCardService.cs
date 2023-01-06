using AutoMapper;
using AWork.Contracts.Dto.Sales.CreditCard;
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
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
    public class CreditCardService : ICreditCardService
    {

        private readonly ISalesRepositoryManager _resitoryManager;
        private readonly IMapper _mapper;

        public CreditCardService(ISalesRepositoryManager resitoryManager, IMapper mapper)
        {
            _resitoryManager = resitoryManager;
            _mapper = mapper;
        }

        public void Edit(CreditCardDto creditCardDto)
        {
            var edit = _mapper.Map<CreditCard>(creditCardDto);
            _resitoryManager.CreditCardRepository.Change(edit);
            _resitoryManager.Save();
        }

        public async Task<IEnumerable<CreditCardDto>> GetAllCreditCard(bool trackChanges)
        {
            var creditCardForModel = await _resitoryManager.CreditCardRepository.GetAllCreditCard(trackChanges);
            var creditCardForDto = _mapper.Map<IEnumerable<CreditCardDto>>(creditCardForModel);
            return creditCardForDto;
        }

        public async Task<CreditCardDto> GetAllCreditCardById(int CreditCardId, bool trackChanges)
        {
            var creditCardForModel = await _resitoryManager.CreditCardRepository.GetCreditCardById(CreditCardId, trackChanges);
            var creditCardForDto = _mapper.Map<CreditCardDto>(creditCardForModel);
            return creditCardForDto;
        }

        public async Task<IEnumerable<CreditCardDto>> GetCreditCardPage(int pageIndex, int pageSize, bool trackChanges)
        {
            var creditCardForModel = await  _resitoryManager.CreditCardRepository.GetCreditCardPage(pageIndex,pageSize,trackChanges);
            var creditCardForDto = _mapper.Map<IEnumerable<CreditCardDto>>(creditCardForModel);
            return creditCardForDto;
        }

        public void Insert(CreditCardForCreateDto creditCardForCreateDto)
        {
            var newUser = _mapper.Map<CreditCard>(creditCardForCreateDto);
            _resitoryManager.CreditCardRepository.Insert(newUser);
            _resitoryManager.Save();
        }

        public void Remove(CreditCardDto creditCardDto)
        {
           var deleteUser =_mapper.Map<CreditCard>(creditCardDto);
            _resitoryManager.CreditCardRepository.Remove(deleteUser);
            _resitoryManager.Save();
        }
    }
}
