using Infinity.Bnois;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class MaritalTypeService : IMaritalTypeService
    {
        private readonly IBnoisRepository<MaritalType> maritalTypeRepository;
        public MaritalTypeService(IBnoisRepository<MaritalType> maritalTypeRepository)
        {
            this.maritalTypeRepository = maritalTypeRepository;
        }
        public List<MaritalTypeModel> GetMaritalTypes(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<MaritalType> maritalTypes = maritalTypeRepository.FilterWithInclude(x => x.IsActive
               && ((x.Name.Contains(searchText) || String.IsNullOrEmpty(searchText))));
            total = maritalTypes.Count();
            maritalTypes = maritalTypes.OrderByDescending(x => x.MaritalTypeId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            List<MaritalTypeModel> models = ObjectConverter<MaritalType, MaritalTypeModel>.ConvertList(maritalTypes.ToList()).ToList();
            return models;
        }

        public async Task<MaritalTypeModel> GetMaritalType(int id)
        {
            if (id <= 0)
            {
                return new MaritalTypeModel();
            }
            MaritalType maritalType = await maritalTypeRepository.FindOneAsync(x => x.MaritalTypeId == id);
            if (maritalType == null)
            {
                throw new InfinityNotFoundException("Marital Type not found");
            }
            MaritalTypeModel model = ObjectConverter<MaritalType, MaritalTypeModel>.Convert(maritalType);
            return model;
        }


        public async Task<MaritalTypeModel> SaveMaritalType(int id, MaritalTypeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Marital Type data missing");
            }
            MaritalType maritalType = ObjectConverter<MaritalTypeModel, MaritalType>.Convert(model);
            if (id > 0)
            {
                maritalType = await maritalTypeRepository.FindOneAsync(x => x.MaritalTypeId == id);
                if (maritalType == null)
                {
                    throw new InfinityNotFoundException("Marital Type not found !");
                }
                maritalType.ModifiedDate = DateTime.Now;
                maritalType.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                maritalType.CreatedDate = DateTime.Now;
                maritalType.CreatedBy = model.CreatedBy;
                maritalType.IsActive = true;
            }
            maritalType.Name = model.Name;
            maritalType.Remarks = model.Remarks;
            await maritalTypeRepository.SaveAsync(maritalType);
            model.MaritalTypeId = maritalType.MaritalTypeId;
            return model;
        }
        public async Task<bool> DeleteMaritalType(int id)
        {

            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            MaritalType maritalType = await maritalTypeRepository.FindOneAsync(x => x.MaritalTypeId == id);
            if (maritalType == null)
            {
                throw new InfinityNotFoundException("Marital Type not found");
            }
            else
            {
                return await maritalTypeRepository.DeleteAsync(maritalType);
            }
        }

        public async Task<List<SelectModel>> GetMaritalTypeSelectModels()
        {
            ICollection<MaritalType> maritalTypes = await maritalTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = maritalTypes.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.MaritalTypeId
            }).ToList();
            return selectModels;

        }
    }
}
