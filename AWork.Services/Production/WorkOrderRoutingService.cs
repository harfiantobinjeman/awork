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
    public class WorkOrderRoutingService : IWorkOrderRoutingService
    {
        private IProductionRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;

        public WorkOrderRoutingService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(WorkOrderRoutingDto workOrderRoutingDto)
        {
            //throw new NotImplementedException();
            var routingModel = _mapper.Map<WorkOrderRouting>(workOrderRoutingDto);
            _repositoryManager.WorkOrderRoutingRepository.Edit(routingModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<WorkOrderRoutingDto>> GetAllRouting(bool trackChange)
        {
            //throw new NotImplementedException();
            var routingModel = await _repositoryManager.WorkOrderRoutingRepository.GetAllRouting(trackChange);
            var routingDto = _mapper.Map<IEnumerable<WorkOrderRoutingDto>>(routingModel);
            return routingDto;
        }

        public async Task<WorkOrderRoutingDto> GetRoutingById(int id, bool trackChange)
        {
            //throw new NotImplementedException();
            var routingModel = await _repositoryManager.WorkOrderRoutingRepository.GetRoutingById(id, trackChange);
            var routingDto = _mapper.Map<WorkOrderRoutingDto>(routingModel);
            return routingDto;
        }

        public void Insert(WorkOrderRoutingForCreateDto workOrderRoutingForCreateDto)
        {
            //throw new NotImplementedException();
            var routingModel = _mapper.Map<WorkOrderRouting>(workOrderRoutingForCreateDto);
            _repositoryManager.WorkOrderRoutingRepository.Insert(routingModel);
            _repositoryManager.SaveAsync();
        }

        public void Remove(WorkOrderRoutingDto workOrderRoutingDto)
        {
            //throw new NotImplementedException();
            var RoutingModel = _mapper.Map<WorkOrderRouting>(workOrderRoutingDto);
            _repositoryManager.WorkOrderRoutingRepository.Remove(RoutingModel);
            _repositoryManager.Save();
        }

        //paged
        public async Task<IEnumerable<WorkOrderRoutingDto>> GetWorkrderRoutingPaged(int pageIndex, int pageSize, bool trackChanges,int id)
        {
            //throw new NotImplementedException();
            var routingModel = await _repositoryManager.WorkOrderRoutingRepository.GetProductPaged(pageIndex, pageSize, trackChanges,id);
            var routingDto = _mapper.Map<IEnumerable<WorkOrderRoutingDto>>(routingModel);
            return routingDto;
        }
    }
}
