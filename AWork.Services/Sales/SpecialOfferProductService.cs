using AutoMapper;
using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Sales
{
    public class SpecialOfferProductService : ISpecialOfferProductService
    {
        private readonly ISalesRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SpecialOfferProductService(ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(SpecialOfferProductDto specialOfferProductDto)
        {
            var edit = _mapper.Map<SpecialOfferProduct>(specialOfferProductDto);
            _repositoryManager.SpecialOfferProductRepository.Edit(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<SpecialOfferProductDto>> GetAllSpecialOfferProduct(bool trackChanges)
        {
            var specialOfferProductModel = await _repositoryManager.SpecialOfferProductRepository.GetAllSpecialOfferProduct(trackChanges);
            // source = specialOfferProductModel, target = specialOfferProductDto
            var specialOfferProductDto = _mapper.Map<IEnumerable<SpecialOfferProductDto>>(specialOfferProductModel);
            return specialOfferProductDto;
        }


        public async Task<SpecialOfferProductDto> GetSpecialOfferProductById(int productId, bool trackChanges)
        {
            var specialOfferProductModel = await _repositoryManager.SpecialOfferProductRepository.GetSpecialOfferProductById(productId, trackChanges);
            // source = specialOfferProductModel, target = specialOfferProductDto
            var specialOfferProductDto = _mapper.Map<SpecialOfferProductDto>(specialOfferProductModel);
            return specialOfferProductDto;
        }

        public void Insert(SpecialOfferProductForCreateDto specialOfferProductForCreateDto)
        {
            var edit = _mapper.Map<SpecialOfferProduct>(specialOfferProductForCreateDto);
            _repositoryManager.SpecialOfferProductRepository.Insert(edit);
            _repositoryManager.Save();
        }

        public void Remove(SpecialOfferProductDto specialOfferProductDto)
        {
            var edit = _mapper.Map<SpecialOfferProduct>(specialOfferProductDto);
            _repositoryManager.SpecialOfferProductRepository.Remove(edit);
            _repositoryManager.Save();
        }
    }
}
