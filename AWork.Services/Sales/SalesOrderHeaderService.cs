using AutoMapper;
using AWork.Contracts.Dto.Sales.SalesOrderDetail;
using AWork.Contracts.Dto.Sales.SalesOrderHeader;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Domain.Repositories.Sales;
using AWork.Services.Abstraction.Sales;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Sales
{
    public class SalesOrderHeaderService : ISalesOrderHeaderService
    {
        private readonly  ISalesRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SalesOrderHeaderService(ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void CreateSalesOrderHeaderDetail(SalesOrderHeaderForCreateDto salesOrderHeader, List<SalesOrderDetailForCreateDto> salesOrderDetail)
        {
            var soh = _mapper.Map <SalesOrderHeader>(salesOrderHeader);
            _repositoryManager.SalesOrderHeaderRepository.Insert(soh);
            _repositoryManager.Save();

            foreach (var item in salesOrderDetail)
            {
                var sod = _mapper.Map<SalesOrderDetail>(item);
                sod.SalesOrderId = soh.SalesOrderId;
                _repositoryManager.SalesOrderDetailRepository.Insert(sod);
            }
            _repositoryManager.Save();
        }

        public void Edit(SalesOrderHeaderDto salesOrderHeaderForDto)
        {
            var edit = _mapper.Map<SalesOrderHeader>(salesOrderHeaderForDto);
            _repositoryManager.SalesOrderHeaderRepository.Edit(edit);
            _repositoryManager.Save();

        }

        public async Task<IEnumerable<SalesOrderHeaderDto>> GetAllSalesOrderHeader(bool trackChanges)
        {
            /*            throw new NotImplementedException();
            */
            /*var salesOrderHeaderModel = await _repositoryManager.SalesOrderHeaderRepository.GetAllSalesOrderHeader(trackChanges);
            var salesOrderHeaderForDto = _mapper.Map<INullable<SalesOrderHeaderDto>>(salesOrderHeaderModel);*/
            var salesOrderHeaderForModel = await _repositoryManager.SalesOrderHeaderRepository.GetAllSalesOrderHeader(trackChanges);
            var salesOrderHeaderForDto = _mapper.Map<IEnumerable<SalesOrderHeaderDto>>(salesOrderHeaderForModel);
            return salesOrderHeaderForDto;
        }

        public async Task<SalesOrderHeaderDto> GetSalesOrderHeaderById(int bussinessEntityId, bool trackChanges)
        {
            var salesOrderHeaderForModel =await _repositoryManager.SalesOrderHeaderRepository.GetByIdSalesOrderHeader(bussinessEntityId, trackChanges);
            var salesOrderHeaderForDto = _mapper.Map<SalesOrderHeaderDto>(salesOrderHeaderForModel);
            return salesOrderHeaderForDto;
        }

        public void Insert(SalesOrderHeaderForCreateDto salesOrderHeaderForCreateDto)
        {
            var newUser = _mapper.Map<SalesOrderHeader>(salesOrderHeaderForCreateDto);
            _repositoryManager.SalesOrderHeaderRepository.Insert(newUser);
            _repositoryManager.Save();

        }

        public void Remove(SalesOrderHeaderDto salesOrderHeaderForDto)
        {
            var deteleUser = _mapper.Map<SalesOrderHeader>(salesOrderHeaderForDto);
            _repositoryManager.SalesOrderHeaderRepository.Remove(deteleUser);
            _repositoryManager.Save();
        }
    }
}
