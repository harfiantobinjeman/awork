using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Purchasing
{
    public interface IShipMethodRepository
    {
        //trackChange => Feature untuk mendeteksi perubahan data di object category
        Task<IEnumerable<ShipMethod>> GetAllShipMethod(bool trackChanges);

        Task<ShipMethod> GetShipMethodById(int id, bool trackChanges);

        void Insert(ShipMethod shipMethod);

        void Edit(ShipMethod shipMethod);

        void Remove(ShipMethod shipMethod);
    }
}
