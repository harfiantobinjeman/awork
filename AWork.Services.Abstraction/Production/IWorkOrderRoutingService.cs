using AWork.Contracts.Dto.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IWorkOrderRoutingService
    {
        Task<IEnumerable<WorkOrderRoutingDto>> GetAllRouting(bool trackChange);
        Task<WorkOrderRoutingDto>GetRoutingById(int id,bool trackChange);
        void Insert (WorkOrderRoutingForCreateDto workOrderRoutingForCreateDto);
        void Edit (WorkOrderRoutingDto workOrderRoutingDto);
        void Remove (WorkOrderRoutingDto workOrderRoutingDto);

        //paged
        Task<IEnumerable<WorkOrderRoutingDto>> GetWorkrderRoutingPaged(int pageIndex, int pageSize, bool trackChanges, int id);
    }
}
