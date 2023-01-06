using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using AWork.Contracts.Dto.Sales.SpecialOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWork.Domain.Models;

namespace AWork.Services.Abstraction.Sales
{
    public interface ISpecialOfferService
    {
        Task<IEnumerable<SpecialOfferDto>> GetAllSpecialOffer(bool trackChanges);
        Task<SpecialOfferDto> GetSpecialOfferById(int productId, bool trackChanges);

        SpecialOfferDto CreateSpecialOfferProduct(SpecialOfferForCreateDto specialOfferForCreateDto);

        /*Task<IEnumerable<SpecialOfferDto>> GetSpecialOfferPaged(int pageIndex, int pageSize, bool trackChanges);*/

        void create2table (SpecialOfferForCreateDto specialOfferForCreateDto, SpecialOfferProductForCreateDto specialOfferProductForCreateDto);
        void Insert(SpecialOfferForCreateDto specialOfferrForCreateDto);

        void Edit(SpecialOfferDto specialOfferDto);

        void Remove(SpecialOfferDto specialOfferDto);
    }
}
