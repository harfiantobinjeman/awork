using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using AWork.Contracts.Dto.Sales.SpecialOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Sales
{
    public interface ISpecialOfferProductService
    {
        Task<IEnumerable<SpecialOfferProductDto>> GetAllSpecialOfferProduct(bool trackChanges);
        Task<SpecialOfferProductDto> GetSpecialOfferProductById(int specialOfferId, bool trackChanges);

        void Insert(SpecialOfferProductForCreateDto specialOfferProductForCreateDto);

        void Edit(SpecialOfferProductDto specialOfferProductForCreateDto);

        void Remove(SpecialOfferProductDto SpecialOfferProductDto);
    }
}
