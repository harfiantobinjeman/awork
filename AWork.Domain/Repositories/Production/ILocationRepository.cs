using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocation(bool trackChange);

        //tambahan separoh
        Task<IEnumerable<Location>> GetSetLocation(bool trackChange);

        Task<Location> GetLocationById(short locationId, bool trackChange);

        void Insert(Location location);

        void Edit(Location location);

        void Remove(Location location);
    }
}
