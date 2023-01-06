using AutoMapper;
using AWork.Contracts.Dto.Production;
using AWork.Domain.Base;
using AWork.Domain.Models;
using AWork.Services.Abstraction.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWork.Services.Production
{
    public class UnitMeasureService : IUnitMeasureService
    {
        private IProductionRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UnitMeasureService(IProductionRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }



        public async Task<IEnumerable<UnitMeasureDto>> GetAllUnitMeasure(bool trackChanges)
        {
            var unitMeasureModel = await _repositoryManager.UnitMeasureRepository.GetAllUnitMeasure(trackChanges);
            var unitMeasureDto = _mapper.Map<IEnumerable<UnitMeasureDto>>(unitMeasureModel);

            return unitMeasureDto;
        }

        public async Task<UnitMeasureDto> GetUnitAllMeasureById(string UnitMeasureCode, bool trackChanges)
        {
            /* var categoryModel = await _repositoryManager.CategoryRepository.GetCategoryById(categoryId, trackChanges);
             var categoryDto = _mapper.Map<CategoryDto>(categoryModel);
             return categoryDto;*/
            var unitMeasureModel = await _repositoryManager.UnitMeasureRepository.GetUnitMeasureById(UnitMeasureCode, trackChanges);
            var unitMeasureDto = _mapper.Map<UnitMeasureDto>(unitMeasureModel);

            return unitMeasureDto;

            // throw new NotImplementedException();
        }

        public void Insert(UnitMeasureForCreateDto unitMeasureForCreateDto)
        {
            /* var categoryModel = _mapper.Map<Category>(categoryForCreateDto);
             _repositoryManager.CategoryRepository.Insert(categoryModel);
             _repositoryManager.Save();*/
            var unitMeasureModel = _mapper.Map<UnitMeasure>(unitMeasureForCreateDto);
            _repositoryManager.UnitMeasureRepository.Insert(unitMeasureModel);
            _repositoryManager.Save();

            //throw new NotImplementedException();
        }

        public void Remove(UnitMeasureDto unitMeasureDto)
        {
            /*var categoryModel = _mapper.Map<Category>(categoryDto);
            _repositoryManager.CategoryRepository.Remove(categoryModel);
            _repositoryManager.SaveAsync();*/
            var unitMeasureModel = _mapper.Map<UnitMeasure>(unitMeasureDto);
            _repositoryManager.UnitMeasureRepository.Remove(unitMeasureModel);
            _repositoryManager.Save();

            //throw new NotImplementedException();
        }

        public void Edit(UnitMeasureDto unitMeasureDto)
        {
            var unitMeasureModel = _mapper.Map<UnitMeasure>(unitMeasureDto);
            _repositoryManager.UnitMeasureRepository.Edit(unitMeasureModel);
            _repositoryManager.Save();
        }


    }
}
