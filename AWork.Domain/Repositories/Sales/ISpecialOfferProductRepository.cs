using AWork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Domain.Repositories.Sales
{
    public interface ISpecialOfferProductRepository
    {
        Task<IEnumerable<SpecialOfferProduct>> GetAllSpecialOfferProduct(bool trackChanges);

        Task<SpecialOfferProduct> GetSpecialOfferProductById(int specialOfferId, bool trackChanges);

        void Insert(SpecialOfferProduct specialOfferProduct);
        void Edit(SpecialOfferProduct specialOfferProduct);
        void Remove(SpecialOfferProduct specialOfferProduct);
    }
}
