using AutoMapper;
using AWork.Contracts.Dto.Sales.Currency;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Sales
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ISalesRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CurrencyService(ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public void Edit(CurrencyDto currencyDto)
        {
            var edit = _mapper.Map<Currency>(currencyDto);
            _repositoryManager.CurrencyRepository.Change(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllCurrency(bool trackChanges)
        {
            var currencyModel = await _repositoryManager.CurrencyRepository.GetAllAsync(trackChanges);
            
            var currencyDto= _mapper.Map<IEnumerable<CurrencyDto>>(currencyModel);
            return currencyDto;
        }

        public async Task<CurrencyDto> GetCurrencyByCode(string currencyCode, bool trackChanges)
        {
            var currencyModel = await _repositoryManager.CurrencyRepository.GetCurrencyByCode(currencyCode, trackChanges);
            
            var currencyDto = _mapper.Map<CurrencyDto>(currencyModel);
            return currencyDto;
        }

        public void Insert(CurrencyForCreateDto currencyForCreateDto)
        {
            var edit = _mapper.Map<Currency>(currencyForCreateDto);
            _repositoryManager.CurrencyRepository.Insert(edit);
            _repositoryManager.Save();
        }

        public void Remove(CurrencyDto currencyDto)
        {
            var edit = _mapper.Map<Currency>(currencyDto);
            _repositoryManager.CurrencyRepository.Remove(edit);
            _repositoryManager.Save();
        }
    }
}
