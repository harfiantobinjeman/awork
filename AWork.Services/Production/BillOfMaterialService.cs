using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Production
{
    public class BillOfMaterialService : IBillOfMaterialService
    {

        private IProductionRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public BillOfMaterialService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BillOfMaterialDto>> GetAllBillOfMaterial(bool trackChanges)
        {
            var billOfMaterialModel = await _repositoryManager.billOfMaterialRepository.GetAllBillOfMaterial(trackChanges);
            var billOfMaterialDto = _mapper.Map<IEnumerable<BillOfMaterialDto>>(billOfMaterialModel);

            return billOfMaterialDto;
        }

        public async Task<BillOfMaterialDto> GetBillOfMaterialById(int BillOfMaterialsId, bool trackChanges)
        {
            var billOfMaterialModel = await _repositoryManager.billOfMaterialRepository.GetBillOfMaterialById(BillOfMaterialsId, trackChanges);
            var billOfMaterialDto = _mapper.Map<BillOfMaterialDto>(billOfMaterialModel);

            return billOfMaterialDto;
        }

        public void Insert(BillOfMaterialForCreateDto billOfMaterialForCreateDto)
        {
            var billOfMaterialModel = _mapper.Map<BillOfMaterial>(billOfMaterialForCreateDto);
            _repositoryManager.billOfMaterialRepository.Insert(billOfMaterialModel);
            _repositoryManager.Save();
        }

        public void Remove(BillOfMaterialDto billOfMaterialDto)
        {
            var billOfMaterialModel = _mapper.Map<BillOfMaterial>(billOfMaterialDto);
            _repositoryManager.billOfMaterialRepository.Remove(billOfMaterialModel);
            _repositoryManager.Save();
        }
        public void Edit(BillOfMaterialDto billOfMaterialDto)
        {
            var billOfMaterialModel = _mapper.Map<BillOfMaterial>(billOfMaterialDto);
            _repositoryManager.billOfMaterialRepository.Edit(billOfMaterialModel);
            _repositoryManager.Save();
        }
    }
}
