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
    public class WorkOrderService : IWorkOrderService
    {
        private IProductionRepositoryManager _repositoryManager;

        private readonly IMapper _mapper;

        public WorkOrderService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkOrderDto>> GetAllWorkOrder(bool trackChange)
        {
            //throw new NotImplementedException();
            var workOrderModel = await _repositoryManager.WorkOrderRepository.GetAllOrder(trackChange);
            var workOrderDto = _mapper.Map<IEnumerable<WorkOrderDto>>(workOrderModel);
            return workOrderDto;
        }

        public async Task<WorkOrderDto> GetWorkOrderById(int workOrderId, bool trackChange)
        {
            //throw new NotImplementedException();
            var workOrderModel = await _repositoryManager.WorkOrderRepository.GetOrderById(workOrderId, trackChange);
            var workOrderDto = _mapper.Map<WorkOrderDto>(workOrderModel);
            return workOrderDto;
        }

        public void Insert(WorkOrderForCreateDto workOrderForCreateDto)
        {
            //throw new NotImplementedException();
            var workOrderDto = _mapper.Map<WorkOrder>(workOrderForCreateDto);
            _repositoryManager.WorkOrderRepository.Insert(workOrderDto);
            _repositoryManager.Save();
        }

        public void Edit(WorkOrderDto workOrderDto)
        {
            //throw new NotImplementedException();
            var orderModel = _mapper.Map<WorkOrder>(workOrderDto);
            _repositoryManager.WorkOrderRepository.Edit(orderModel);
            _repositoryManager.Save();
        }

        public void Remove(WorkOrderDto workOrderDto)
        {
            //throw new NotImplementedException();
            var WorkOrderModel = _mapper.Map<WorkOrder>(workOrderDto);
            _repositoryManager.WorkOrderRepository.Remove(WorkOrderModel);
            _repositoryManager.Save();
        }

        public WorkOrderRoutingDto CreateWorkOrder()
        {
            //throw new NotImplementedException();
            var workOrder = new WorkOrder
            {
                ModifiedDate = DateTime.Now
            };
            /*_repositoryManager.LocationRepository.Insert(location);
            _repositoryManager.Save();
            var location_Dto = _mapper.Map<LocationDto>(location);
            return location_Dto;*/
            _repositoryManager.WorkOrderRepository.Insert(workOrder);
            _repositoryManager.Save();
            var workOrderRoutingDto = _mapper.Map<WorkOrderRoutingDto>(workOrder);
            return workOrderRoutingDto;
        }

        //paged
        public async Task<IEnumerable<WorkOrderDto>> GetWorkrderPaged(int pageIndex, int pageSize, bool trackChanges, int id)
        {
            //throw new NotImplementedException();
            var workOrderModel = await _repositoryManager.WorkOrderRepository.GetWorkOPaged(pageIndex, pageSize, trackChanges, id);
            var workOrderDto = _mapper.Map<IEnumerable<WorkOrderDto>>(workOrderModel) ;
            return workOrderDto;
        }

        //include workorder & workorderrouting
        public async Task<IEnumerable<WorkOrderDto>> GetIncludeID(int includeWorkOrderId, bool trackChanges)
        {
            //throw new NotImplementedException();
            var includeWoM = await _repositoryManager.WorkOrderRepository.GetIncludeID(includeWorkOrderId, trackChanges);
            var includeWoDto = _mapper.Map<IEnumerable<WorkOrderDto>>(includeWoM);
            return includeWoDto;
        }

        public async Task<IEnumerable<WorkOrderDto>> GetWorkOrders(int workOrderId, bool trackChange)
        {
            //throw new NotImplementedException();
            var includeWoM = await _repositoryManager.WorkOrderRepository.GetWorkOrder(workOrderId, trackChange);
            var includeWoDto = _mapper.Map<IEnumerable<WorkOrderDto>>(includeWoM);
            return includeWoDto;
        }

        public async Task<IEnumerable<WorkOrderDto>> GetPencarian(int workOrderId, int productId, bool trackChange)
        {
            //throw new NotImplementedException();
            var includeWoM = await _repositoryManager.WorkOrderRepository.GerPencarian(workOrderId, productId, trackChange);
            var includeWoDto = _mapper.Map<IEnumerable<WorkOrderDto>>(includeWoM);
            return includeWoDto;
        }
    }
}
