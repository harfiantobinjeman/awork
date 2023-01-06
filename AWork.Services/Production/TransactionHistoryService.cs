using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Production;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Production
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private IProductionRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public TransactionHistoryService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(TransactionHistoryDto transactionHistoryDto)
        {
            //throw new NotImplementedException();
            var transactionModel = _mapper.Map<TransactionHistory>(transactionHistoryDto);
            _repositoryManager.TransactionHistoryRepository.Edit(transactionModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<TransactionHistoryDto>> GetAllTransaction(bool trackChange)
        {
            //throw new NotImplementedException();
            
            var transactionModel = await _repositoryManager.TransactionHistoryRepository.GetAllTransaction(trackChange);
            var transactionHistory = _mapper.Map<IEnumerable<TransactionHistoryDto>>(transactionModel);
            return transactionHistory;
        }

        public async Task<TransactionHistoryDto> GetTransactionHistoryById(int transactioId, bool trackChange)
        {
            //throw new NotImplementedException();
            /*var locationModel = await _repositoryManager.LocationRepository.GetLocationById(locationId, trackChange);
            var locationDto = _mapper.Map<LocationDto>(locationModel);
            return locationDto;*/
            var transactionModel = await _repositoryManager.TransactionHistoryRepository.GetTransactionHistoryById(transactioId, trackChange);
            var transactionDto = _mapper.Map<TransactionHistoryDto>(transactionModel);
            return transactionDto;
        }

        public void Insert(TransactionHistoryForCreateDto transactionHistoryForCreateDto)
        {
            //throw new NotImplementedException();
            var transactionModel = _mapper.Map<TransactionHistory>(transactionHistoryForCreateDto);
            _repositoryManager.TransactionHistoryRepository.Insert(transactionModel);
            _repositoryManager.Save();
        }

        public void Remove(TransactionHistoryDto transactionHistoryDto)
        {
            //throw new NotImplementedException();
            /*var locationModel = _mapper.Map<Location>(locationDto);
            _repositoryManager.LocationRepository.Remove(locationModel);
            _repositoryManager.Save();*/
            var transactHistory = _mapper.Map<TransactionHistory>(transactionHistoryDto);
            _repositoryManager.TransactionHistoryRepository.Remove(transactHistory);
            _repositoryManager.Save();
        }

        public TransactionHistoryDto EditTransaction()
        {
            //throw new NotImplementedException();
            var aktualCost = new TransactionHistory
            {
                ActualCost = 123,
                ReferenceOrderId = 72603,
                ReferenceOrderLineId = 0,
                TransactionDate = DateTime.Now,
                TransactionType = "Y",
                Quantity = 200,
            };
            _repositoryManager.TransactionHistoryRepository.Edit(aktualCost);
            _repositoryManager.Save();
            var transaction_Dto = _mapper.Map<TransactionHistoryDto>(aktualCost);
            return transaction_Dto;
        }
    }
}
