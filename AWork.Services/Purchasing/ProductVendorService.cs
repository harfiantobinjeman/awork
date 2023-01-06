using AutoMapper;
using AWork.Contracts.Dto.Purchasing;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Purchasing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Purchasing
{
    public class ProductVendorService : IProductVendorService
    {
        private IPurchasingRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public ProductVendorService(IPurchasingRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVendorDto>> GetAllProductVendor(bool trackChanges)
        {
            var ProductVendorModel = await _repositoryManager.ProductVendorRepository.GetAllProductVendor(false);
            var ProductVendorDto = _mapper.Map<IEnumerable<ProductVendorDto>>(ProductVendorModel);
            return ProductVendorDto;
        }

        public async Task<ProductVendorDto> GetProductVendorById(int productVendorId,bool trackChanges)
        {
            var ProductVendorModel = await _repositoryManager.ProductVendorRepository.GetProductVendorById(productVendorId, trackChanges);
            var ProductVendorDto = _mapper.Map<ProductVendorDto>(ProductVendorModel);
            return ProductVendorDto;
        }

        public void Insert(ProductVendorDto productVendorForCreateDto)
        {
            var ProductVendorModel = _mapper.Map<ProductVendor>(productVendorForCreateDto);
            _repositoryManager.ProductVendorRepository.Insert(ProductVendorModel);
            _repositoryManager.Save();
        }

        public void Edit(ProductVendorDto productVendorDto)
        {
            var ProductVendorModel = _mapper.Map<ProductVendor>(productVendorDto);
            _repositoryManager.ProductVendorRepository.Edit(ProductVendorModel);
            _repositoryManager.Save();
        }

        public void Remove(ProductVendorDto productVendorDto)
        {
            var ProductVendorModel = _mapper.Map<ProductVendor>(productVendorDto);
            _repositoryManager.ProductVendorRepository.Remove(ProductVendorModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ProductVendorDto>> FilterEmpId(int empId, bool trackChanges)
        {
            var productModel = await _repositoryManager.ProductVendorRepository.FilterEmplid(empId, trackChanges);
            var product = _mapper.Map<IEnumerable<ProductVendorDto>>(productModel);
            return product;
        }
    }
}
