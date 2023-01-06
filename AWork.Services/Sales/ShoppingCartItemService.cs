using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Contracts.Dto.Sales.ShoppingCartItem;
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
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly ISalesRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ShoppingCartItemService(ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(ShoppingCartItemDto shopCartItemDto)
        {
            var editData = _mapper.Map<ShoppingCartItem>(shopCartItemDto);
            _repositoryManager.ShoppingCartItemRepository.Edit(editData);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ShoppingCartItemDto>> FilterCustId(string custId, bool trackChanges)
        {
            var shoppingModel = await _repositoryManager.ShoppingCartItemRepository.FilterCustId(custId, trackChanges);
            var shoppingDto = _mapper.Map<IEnumerable<ShoppingCartItemDto>>(shoppingModel);
            return shoppingDto;
        }

        public async Task<IEnumerable<ShoppingCartItemDto>> GetAllShopCartItem(bool trackChanges)
        {
            var shopCartItemModel = await _repositoryManager.ShoppingCartItemRepository.GetAllShoppingCartItem(trackChanges);
            var shopCartItemDto = _mapper.Map<IEnumerable<ShoppingCartItemDto>>(shopCartItemModel);
            return shopCartItemDto;
        }

        public async Task<IEnumerable<ProductOnSaleDto>> GetCartItemByCustId(string custId, bool trackChanges)
        {
            var cartItem = await _repositoryManager.ShoppingCartItemRepository.GetCartItemByCustId(custId, trackChanges);
            var cartModel = _mapper.Map<IEnumerable<ProductOnSaleDto>>(cartItem);
            return cartModel;
        }

        public async Task<ShoppingCartItemDto> GetDataCartCustomerById(int shoppingCartId, int productId, bool trackChanges)
        {
            var shoppingCartItemModel = await _repositoryManager.ShoppingCartItemRepository.GetDataCartCustomerById(shoppingCartId, productId, trackChanges);
            var shoppingItemDto = _mapper.Map<ShoppingCartItemDto>(shoppingCartItemModel);
            return shoppingItemDto;
        }

        public async Task<ProductOnSaleDto> GetDataProductById(int productId, bool trackChanges)
        {
            var product = await _repositoryManager.ShoppingCartItemRepository.GetProductById(productId, trackChanges);
            var productDto = _mapper.Map<ProductOnSaleDto>(product);
            return productDto;
        }

        public async Task<IEnumerable<ProductOnSaleDto>> GetProductOnSales(bool trackChanges)
        {
            var product = await _repositoryManager.ShoppingCartItemRepository.GetProductOnSales(trackChanges);
            var productDto = _mapper.Map<IEnumerable<ProductOnSaleDto>>(product);
            return productDto;
        }

        public async Task<ShoppingCartItemDto> GetShopCartItemById(int shopItemId, bool trackChanges)
        {
            var shopCartItemModel = await _repositoryManager.ShoppingCartItemRepository.GetShoppingCartItemById(shopItemId, trackChanges);
            var shopCartItemDto = _mapper.Map<ShoppingCartItemDto>(shopCartItemModel);
            return shopCartItemDto;
        }

        public void Insert(ShoppingCartItemForCreateDto shopCartItemForCreateDto)
        {
            var insertData = _mapper.Map<ShoppingCartItem>(shopCartItemForCreateDto);
            _repositoryManager.ShoppingCartItemRepository.Insert(insertData);
            _repositoryManager.Save();
        }

        public void Remove(ShoppingCartItemDto shopCartItemDto)
        {
            var removeData = _mapper.Map<ShoppingCartItem>(shopCartItemDto);
            _repositoryManager.ShoppingCartItemRepository.Remove(removeData);
            _repositoryManager.Save();
        }

        public void RemoveListCartItem(List<ShoppingCartItemDto> shopCartItem)
        {
            foreach (var item in shopCartItem)
            {
                var removeData = _mapper.Map<ShoppingCartItem>(item);
                _repositoryManager.ShoppingCartItemRepository.Remove(removeData);
            }
            _repositoryManager.Save();
        }
    }
}
