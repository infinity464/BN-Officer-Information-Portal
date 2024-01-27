using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   public class ExtracurricularTypeService: IExtracurricularTypeService
    {
        private readonly IBnoisRepository<ExtracurricularType> extracurricularTypeService;
        public ExtracurricularTypeService(IBnoisRepository<ExtracurricularType> extracurricularTypeService)
        {
            this.extracurricularTypeService = extracurricularTypeService;
        }

      
        public async Task<ExtracurricularTypeModel> GetExtracurricularType(int id)
        {
            if (id <= 0)
            {
                return new ExtracurricularTypeModel();
            }
            ExtracurricularType extracurricularType = await extracurricularTypeService.FindOneAsync(x => x.ExtracurricularTypeId == id);
            if (extracurricularType == null)
            {
                throw new InfinityNotFoundException("Extracurricular Type not found");
            }
            ExtracurricularTypeModel model = ObjectConverter<ExtracurricularType, ExtracurricularTypeModel>.Convert(extracurricularType);
            return model;
        }

        public List<ExtracurricularTypeModel> GetExtracurricularTypes(int ps, int pn, string qs, out int total)
        {
            IQueryable<ExtracurricularType> extracurricularTypes = extracurricularTypeService.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = extracurricularTypes.Count();
            extracurricularTypes = extracurricularTypes.OrderByDescending(x => x.ExtracurricularTypeId).Skip((pn - 1) * ps).Take(ps);
            List<ExtracurricularTypeModel> models = ObjectConverter<ExtracurricularType, ExtracurricularTypeModel>.ConvertList(extracurricularTypes.ToList()).ToList();
            return models;
        }

        public async Task<ExtracurricularTypeModel> SaveExtracurricularType(int id, ExtracurricularTypeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Extracurricular Type data missing");
            }

            bool isExistData = extracurricularTypeService.Exists(x => x.Name == model.Name  && x.ExtracurricularTypeId != model.ExtracurricularTypeId);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ExtracurricularType extracurricularType = ObjectConverter<ExtracurricularTypeModel, ExtracurricularType>.Convert(model);
            if (id > 0)
            {
                extracurricularType = await extracurricularTypeService.FindOneAsync(x => x.ExtracurricularTypeId == id);
                if (extracurricularType == null)
                {
                    throw new InfinityNotFoundException("Extracurricular Type not found !");
                }
                extracurricularType.ModifiedDate = DateTime.Now;
                extracurricularType.ModifiedBy = userId;

            }
            else
            {
                extracurricularType.IsActive = true;
                extracurricularType.CreatedDate = DateTime.Now;
                extracurricularType.CreatedBy = userId;
            }
            extracurricularType.Name = model.Name;
            extracurricularType.Remarks = model.Remarks;
            await extracurricularTypeService.SaveAsync(extracurricularType);
            model.ExtracurricularTypeId = extracurricularType.ExtracurricularTypeId;
            return model;
        }

        public async Task<bool> DeleteExtracurricularType(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ExtracurricularType extracurricularType = await extracurricularTypeService.FindOneAsync(x => x.ExtracurricularTypeId == id);
            if (extracurricularType == null)
            {
                throw new InfinityNotFoundException("Extracurricular Type not found");
            }
            else
            {
                return await extracurricularTypeService.DeleteAsync(extracurricularType);
            }
        }

        public async Task<List<SelectModel>> GetExtracurricularTypeSelectModels()
        {
            ICollection<ExtracurricularType> extracurricularTypes = await extracurricularTypeService.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = extracurricularTypes.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.ExtracurricularTypeId
            }).ToList();
            return selectModels;
        }
    }
}
