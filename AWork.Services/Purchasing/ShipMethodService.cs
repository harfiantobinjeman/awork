using AutoMapper;
using AWork.Contracts.Dto.Purchasing;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction;
using AWork.Services.Abstraction.Purchasing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Purchasing
{
    public class ShipMethodService : IShipMethodService
    {
        private IPurchasingRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ShipMethodService(IPurchasingRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(ShipMethodDto shipMethod)
        {
            var shipMethodModel = _mapper.Map<ShipMethod>(shipMethod);
            _repositoryManager.ShipMethodRepository.Edit(shipMethodModel);
            _repositoryManager.Save();

        }

        public async Task<IEnumerable<ShipMethodDto>> GetAllShipMethod(bool trackChanges)
        {
            var shipMethodModel = await _repositoryManager.ShipMethodRepository.GetAllShipMethod(trackChanges);
            // source shipMethodModel, target shipMethodDto
            var shipMethodDto = _mapper.Map<IEnumerable<ShipMethodDto>>(shipMethodModel);
            return shipMethodDto;
        }

        public async Task<ShipMethodDto> GetShipMethodById(int shipMethodID, bool trackChanges)
        {
            var shipMethodById = await _repositoryManager.ShipMethodRepository.GetShipMethodById(shipMethodID, trackChanges);
            return _mapper.Map<ShipMethodDto>(shipMethodById);
        }

        public void Insert(ShipMethodForCreateDto shipMethod)
        {
            var shipMethodModel = _mapper.Map<ShipMethod>(shipMethod);
            _repositoryManager.ShipMethodRepository.Insert(shipMethodModel);
            _repositoryManager.Save();
        }

        public void Remove(ShipMethodDto shipMethod)
        {
            var shipMethodModel = _mapper.Map<ShipMethod>(shipMethod);
            _repositoryManager.ShipMethodRepository.Remove(shipMethodModel);
            _repositoryManager.Save();
        }
    }
}
