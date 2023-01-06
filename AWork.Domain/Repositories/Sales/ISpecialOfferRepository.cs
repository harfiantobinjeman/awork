using AWork.Domain.Dto.Sales;
using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface ISpecialOfferRepository
    {
        Task<IEnumerable<SpecialOffer>> GetAllSpecialOffer(bool trackChanges);
       
        Task<SpecialOffer> GetSpecialOfferById(int specialOfferId, bool trackChanges);
        
        /*Task<IEnumerable<SpecialOffer>> GetSpecialOfferPaged(int pageIndex, int pageSize, bool trackChanges);*/

        public Task<IEnumerable<Product>> GetProductByName(string productName, bool trackChanges);

        void Insert(SpecialOffer specialOffer);
        void Edit(SpecialOffer specialOffer);
        void Remove(SpecialOffer specialOffer);
    }
}
