using AutoMapper;
using AWork.Contracts.Dto.Sales.Store;
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
    public class StoreService : IStoreService
    {
        private readonly ISalesRepositoryManager _salesRepositoryManager;
        private readonly IMapper _mapper;

        public StoreService(ISalesRepositoryManager salesRepositoryManager, IMapper mapper)
        {
            _salesRepositoryManager = salesRepositoryManager;
            _mapper = mapper;
        }

        public void Change(StoreDto storeDto)
        {
            var change = _mapper.Map<Store>(storeDto);
            _salesRepositoryManager.StoreRepository.Change(change);
            _salesRepositoryManager.Save();
        }

        public async Task<IEnumerable<StoreDto>> GetAllAsync(bool trackChanges)
        {
            var storeModel = await _salesRepositoryManager.StoreRepository.GetAllAsync(trackChanges);
            //sumber = model, target = DTO
            var storeDto = _mapper.Map<IEnumerable<StoreDto>>(storeModel);
            return storeDto;
        }

        public async Task<StoreDto> GetCurrencyByCode(int storeId, bool trackChanges)
        {
            var storeModel = await _salesRepositoryManager.StoreRepository.GetCurrencyByCode(storeId, trackChanges);
            //sumber = model, target = DTO
            var storeDto = _mapper.Map<StoreDto>(storeModel);
            return storeDto;
        }

        public void Insert(StoreForCreateDto storeForCreateDto)
        {
            var edit = _mapper.Map<Store>(storeForCreateDto);
            _salesRepositoryManager.StoreRepository.Insert(edit);
            _salesRepositoryManager.Save();
        }

        public void Remove(StoreDto storeDto)
        {
            var edit = _mapper.Map<Store>(storeDto);
            _salesRepositoryManager.StoreRepository.Remove(edit);
            _salesRepositoryManager.Save();
        }
    }
}
