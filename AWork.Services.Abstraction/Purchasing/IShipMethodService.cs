using AWork.Contracts.Dto.Purchasing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Purchasing
{
    public interface IShipMethodService
    {
        Task<IEnumerable<ShipMethodDto>> GetAllShipMethod(bool trackChanges);

        Task<ShipMethodDto> GetShipMethodById(int id, bool trackChanges);

        void Insert(ShipMethodForCreateDto shipMethod);

        void Edit(ShipMethodDto shipMethod);

        void Remove(ShipMethodDto shipMethod);
    }
}
