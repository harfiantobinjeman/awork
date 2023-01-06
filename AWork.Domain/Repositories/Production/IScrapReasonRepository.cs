using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Production
{
    public interface IScrapReasonRepository
    {
        Task<IEnumerable<ScrapReason>> GetAllScrapReason(bool trackChange);
        Task<ScrapReason> GetLocationById(short reasonId, bool trackChange);

        void Insert (ScrapReason scrapReason);
        void Edit (ScrapReason scrapReason);
        void Remove (ScrapReason scrapReason);

    }
}
