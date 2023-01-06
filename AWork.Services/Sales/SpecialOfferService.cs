using AutoMapper;
using AWork.Contracts.Dto.Sales.SpecialOffer;
using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Sales
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly ISalesRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public SpecialOfferService(ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void create2table(SpecialOfferForCreateDto specialOfferForCreateDto, SpecialOfferProductForCreateDto specialOfferProductForCreateDto)
        {
            var insertSpecialOffer = _mapper.Map<SpecialOffer>(specialOfferForCreateDto);
            _repositoryManager.SpecialOfferRepository.Insert(insertSpecialOffer);
            _repositoryManager.Save();

            var insertSpecialOfferProduct = _mapper.Map<SpecialOfferProduct>(specialOfferProductForCreateDto);
            _repositoryManager.SpecialOfferProductRepository.Insert(insertSpecialOfferProduct);
            _repositoryManager.Save();
        }

        public SpecialOfferDto CreateSpecialOfferProduct(SpecialOfferForCreateDto specialOfferForCreateDto)
        {
            var specialOfferModel = _mapper.Map<SpecialOffer>(specialOfferForCreateDto);
            _repositoryManager.SpecialOfferRepository.Insert(specialOfferModel);
            _repositoryManager.Save();
            var specialOfferDto = _mapper.Map<SpecialOfferDto>(specialOfferModel);
            return specialOfferDto;

        }

        public void Edit(SpecialOfferDto SpecialOfferDto)
        {
            var specialOfferModel = _mapper.Map<SpecialOffer>(SpecialOfferDto);
            _repositoryManager.SpecialOfferRepository.Edit(specialOfferModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<SpecialOfferDto>> GetAllSpecialOffer(bool trackChanges)
        {
            var specialOfferModel = await _repositoryManager.SpecialOfferRepository.GetAllSpecialOffer(trackChanges);
            var specialOfferDto = _mapper.Map<IEnumerable<SpecialOfferDto>>(specialOfferModel);
            return specialOfferDto;
        }

        public async Task<SpecialOfferDto> GetSpecialOfferById(int specialOfferId, bool trackChanges)
        {
            var specialOfferModel = await _repositoryManager.SpecialOfferRepository.GetSpecialOfferById(specialOfferId, trackChanges);
            var specialOfferDto = _mapper.Map<SpecialOfferDto>(specialOfferModel);
            return specialOfferDto;
        }

      /*  public async Task<IEnumerable<SpecialOfferDto>> GetSpecialOfferPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            var specialOfferModel = await _repositoryManager.SpecialOfferRepository.GetSpecialOfferPaged(pageIndex, pageSize, trackChanges);
            var specialOfferDto = _mapper.Map<IEnumerable<SpecialOfferDto>>(specialOfferModel);
            return specialOfferDto;
        }*/

        public void Insert(SpecialOfferForCreateDto specialOfferForCreateDto)
        {
            var specialOfferCreate = new SpecialOfferForCreateDto
            {
                Description = specialOfferForCreateDto.Description,
                Type = specialOfferForCreateDto.Type,
                Category = specialOfferForCreateDto.Category,
                DiscountPct = specialOfferForCreateDto.DiscountPct,
                MinQty = specialOfferForCreateDto.MinQty,
                MaxQty = specialOfferForCreateDto.MaxQty,
                StartDate = specialOfferForCreateDto.StartDate,
                EndDate = specialOfferForCreateDto.EndDate,
                Rowguid = System.Guid.NewGuid(),
                //ModifiedDate = DateTime.Now,
            };
            var specialOfferModel = _mapper.Map<SpecialOffer>(specialOfferForCreateDto);
            _repositoryManager.SpecialOfferRepository.Insert(specialOfferModel);
            _repositoryManager.Save();

            foreach(var item in specialOfferForCreateDto.SpecialOfferProductForCreateDto)
            {
                //item.ProductId = specialOfferModel.SpecialOfferId;
                //item.ProductId = sp;
                var productSpecial = _mapper.Map<SpecialOfferProduct>(item);
                _repositoryManager.SpecialOfferProductRepository.Insert(productSpecial);
            }
            _repositoryManager.Save();

        }

        public void Remove(SpecialOfferDto specialOfferDto)
        {
            var specialOfferModel = _mapper.Map<SpecialOffer>(specialOfferDto);
            _repositoryManager.SpecialOfferRepository.Remove(specialOfferModel);
            _repositoryManager.Save();
        }
    }
}