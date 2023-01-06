using AWork.Contracts.Dto.Production;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IScrapReasonService
    {
        Task<IEnumerable<ScrapReasonDto>> GetAllScrapReason(bool trackChange);
        Task<ScrapReasonDto> GetLocationById(short reasonId, bool trackChange);

        void Insert(ScrapReasonForCreateDto scrapReasonDtoForCreate);
        void Edit(ScrapReasonDto scrapReasonDto);
        void Remove(ScrapReasonDto scrapReasonDto);
    }
}
