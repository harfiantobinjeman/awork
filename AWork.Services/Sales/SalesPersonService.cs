using AutoMapper;
using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesPerson;
using AWork.Contracts.Dto.Sales.Store;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Sales
{
    public class SalesPersonService : ISalesPersonService
    {
        private readonly ISalesRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public SalesPersonService(ISalesRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void CreateSalesPersonStore(SalesPersonForCreateDto salesPersonForCreateDto, List<StoreForCreateDto> storesForCreate)
        {
            var salesModel = _mapper.Map<SalesPerson>(salesPersonForCreateDto);
            _repositoryManager.SalesPersonRepository.Insert(salesModel);
            _repositoryManager.Save();

            foreach (var item in storesForCreate)
            {
                var store = _mapper.Map<Store>(item);
                _repositoryManager.StoreRepository.Insert(store);
            }
            _repositoryManager.Save();
        }

        public void DeleteSalesPersonStore(SalesPersonDto salesPerson, List<StoreDto> stores)
        {
            foreach (var item in stores)
            {
                var store = _mapper.Map<Store>(item);
                _repositoryManager.StoreRepository.Remove(store);
            }
            _repositoryManager.Save();

            var deleteSalesPerson = _mapper.Map<SalesPerson>(salesPerson);
            _repositoryManager.SalesPersonRepository.Remove(deleteSalesPerson);
            _repositoryManager.Save();
        }

        public void Edit(SalesPersonDto salesPersonDto)
        {
            var edit = _mapper.Map<SalesPerson>(salesPersonDto);
            _repositoryManager.SalesPersonRepository.Edit(edit);
            _repositoryManager.Save();
        }

        public void EditSalesPersonStore(SalesPersonDto salesPersonDto, List<StoreDto> storesDto)
        {
            var salesModel = _mapper.Map<SalesPerson>(salesPersonDto);
            _repositoryManager.SalesPersonRepository.Edit(salesModel);
            _repositoryManager.Save();

            foreach (var item in storesDto)
            {
                var store = _mapper.Map<Store>(item);
                _repositoryManager.StoreRepository.Change(store);
            }
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<SalesPersonDto>> GetAllSalesPerson(bool trackChanges)
        {
            var salesPersonModel = await _repositoryManager.SalesPersonRepository.GetAllSalesPerson(trackChanges);
            var salesPersonDto = _mapper.Map<IEnumerable<SalesPersonDto>>(salesPersonModel);
            return salesPersonDto;
        }

        public async Task<SalesPersonDto> GetSalesPersonById(int bussinessEntityId, bool trackChanges)
        {
            var salesPersonModel = await _repositoryManager.SalesPersonRepository.GetSalesPersonById(bussinessEntityId, trackChanges);
            var salesPersonDto = _mapper.Map<SalesPersonDto>(salesPersonModel);
            return salesPersonDto;
        }

        public async Task<SalesPersonGroupEditDto> GetSalesPersonEditById(int bussinessEntityId, bool trackChanges)
        {
            var salesPersonModel = await _repositoryManager.SalesPersonRepository.GetSalesPersonById(bussinessEntityId, trackChanges);
            // source = categoryModel, target = CategoryDto
            var salesPersonDto = _mapper.Map<SalesPersonGroupEditDto>(salesPersonModel);
            return salesPersonDto;
        }

        public async Task<IEnumerable<PersonDto>> GetSalesPersonNotExistsEmployee(bool trackChanges)
        {
            var salesPersonModel = await _repositoryManager.SalesPersonRepository.GetSalesPersonNotExistsEmployee(trackChanges);
            // source = categoryModel, target = CategoryDto
            var salesPersonDto = _mapper.Map<IEnumerable<PersonDto>>(salesPersonModel);
            return salesPersonDto;
        }

        public void Insert(SalesPersonForCreateDto salesPersonForCreateDto)
        {
            var edit = _mapper.Map<SalesPerson>(salesPersonForCreateDto);
            _repositoryManager.SalesPersonRepository.Insert(edit);
            _repositoryManager.Save();
        }

        public void Remove(SalesPersonDto salesPersonDto)
        {
            var edit = _mapper.Map<SalesPerson>(salesPersonDto);
            _repositoryManager.SalesPersonRepository.Remove(edit);
            _repositoryManager.Save();
        }
    }
}
