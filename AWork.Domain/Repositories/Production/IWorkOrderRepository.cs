using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IWorkOrderRepository
    {
        Task<IEnumerable<WorkOrder>> GetAllOrder(bool trackChange);
        Task<IEnumerable<WorkOrder>> GetWorkOrder(int workId, bool trackChange);
        //pencarian
        Task<IEnumerable<WorkOrder>> GerPencarian(int workId, int productId, bool trackChange);

        Task<WorkOrder> GetOrderById(int workId, bool trackChange);
        void Insert (WorkOrder workOrder);
        void Edit (WorkOrder workOrder);
        void Remove (WorkOrder workOrder);

        //paged
        Task<IEnumerable<WorkOrder>> GetWorkOPaged(int pageIndex, int pageSize, bool trackChanges, int id);
        //include workorder & workorderRouting
        Task<IEnumerable<WorkOrder>> GetIncludeID(int workOrderIncludeId, bool trackChanges);
    }
}
