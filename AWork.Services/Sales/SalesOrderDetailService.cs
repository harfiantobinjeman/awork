using AutoMapper;
using AWork.Contracts.Dto.Sales.SalesOrderDetail;
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
    public class SalesOrderDetailService : ISalesOrderDetailService
    {
        private readonly ISalesRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SalesOrderDetailService(ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(SalesOrderDetailDto salesOrderDetailDto)
        {
            var edit = _mapper.Map<SalesOrderDetail>(salesOrderDetailDto);
            _repositoryManager.SalesOrderDetailRepository.Edit(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<SalesOrderDetailDto>> GetAllSalesOrderDetail(bool trackChanges)
        {
            var salesOrderDetailModel = await _repositoryManager.SalesOrderDetailRepository.GetAllSalesOrderDetail(trackChanges);
            var salesOrderDetailDto = _mapper.Map<IEnumerable<SalesOrderDetailDto>>(salesOrderDetailModel);
            return salesOrderDetailDto;
        }

        public async Task<SalesOrderDetailDto> GetSalesOrderDetailById(int specialOfferId, bool trackChanges)
        {
            var salesOrderDetailModel = await _repositoryManager.SalesOrderDetailRepository.GetSalesOrderDetailById(specialOfferId, trackChanges);
            var salesOrderDetailDto = _mapper.Map<SalesOrderDetailDto>(salesOrderDetailModel);
            return salesOrderDetailDto;
        }

        public void Insert(SalesOrderDetailForCreateDto salesOrderDetailForCreateDto)
        {
            var edit = _mapper.Map<SalesOrderDetail>(salesOrderDetailForCreateDto);
            _repositoryManager.SalesOrderDetailRepository.Insert(edit);
            _repositoryManager.Save();
        }

        public void Remove(SalesOrderDetailDto salesOrderDetailDto)
        {
            var edit = _mapper.Map<SalesOrderDetail>(salesOrderDetailDto);
            _repositoryManager.SalesOrderDetailRepository.Remove(edit);
            _repositoryManager.Save();
        }
    }
}
