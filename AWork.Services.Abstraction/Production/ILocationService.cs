using AWork.Contracts.Dto.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAllLocation(bool trackChange);

        //tambahan getseparuh
        Task<IEnumerable<LocationDto>> GetSetLocation(bool trackChange);

        Task<LocationDto> GetLocationById(short locationId, bool trackChange);

        LocationDto CreateLocation ();

        void Insert(LocationForCreateDto locationForCreateDto);

        void Edit(LocationDto locationDto);

        void Remove(LocationDto locationDto);
    }
}
