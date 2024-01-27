using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   public class ResultTypeService : IResultTypeService
    {

        private readonly IBnoisRepository<ResultType> resultTypeRepository;
        public ResultTypeService(IBnoisRepository<ResultType> resultTypeRepository)
        {
            this.resultTypeRepository = resultTypeRepository;
        }


        public List<ResultTypeModel> GetResultTypes(int ps, int pn, string qs, out int total)
        {
            IQueryable<ResultType> resultTypes = resultTypeRepository.FilterWithInclude(x => x.IsActive && (x.TypeName.Contains(qs) || String.IsNullOrEmpty(qs) ));
            total = resultTypes.Count();
	        resultTypes = resultTypes.OrderByDescending(x => x.ResultTypeId).Skip((pn - 1) * ps).Take(ps);
            List<ResultTypeModel> models = ObjectConverter<ResultType, ResultTypeModel>.ConvertList(resultTypes.ToList()).ToList();
            return models;
        }

        public async Task<ResultTypeModel> GetResultType(int id)
        {
            if (id <= 0)
            {
                return new ResultTypeModel();
            }
            ResultType resultType = await resultTypeRepository.FindOneAsync(x => x.ResultTypeId == id);
            if (resultType == null)
            {
                throw new InfinityNotFoundException("Result Type not found");
            }
            ResultTypeModel model = ObjectConverter<ResultType, ResultTypeModel>.Convert(resultType);
            return model;
        }

        public async Task<ResultTypeModel> SaveResultType(int id, ResultTypeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Result Type data missing");
            }
            bool isExist = resultTypeRepository.Exists(x => x.TypeName == model.TypeName  && x.ResultTypeId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ResultType resultType = ObjectConverter<ResultTypeModel, ResultType>.Convert(model);
            if (id > 0)
            {
	            resultType = await resultTypeRepository.FindOneAsync(x => x.ResultTypeId == id);
                if (resultType == null)
                {
                    throw new InfinityNotFoundException("Result Type not found !");
                }

	            resultType.ModifiedDate = DateTime.Now;
	            resultType.ModifiedBy = userId;
            }
            else
            {
	            resultType.IsActive = true;
	            resultType.CreatedDate = DateTime.Now;
	            resultType.CreatedBy = userId;
            }
	        resultType.TypeName = model.TypeName;
	        resultType.Description = model.Description;
     
            
            await resultTypeRepository.SaveAsync(resultType);
            model.ResultTypeId = resultType.ResultTypeId;
            return model;
        }

        public async Task<bool> DeleteResultType(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ResultType resultType = await resultTypeRepository.FindOneAsync(x => x.ResultTypeId == id);
            if (resultType == null)
            {
                throw new InfinityNotFoundException("Result Type not found");
            }
            else
            {
                return await resultTypeRepository.DeleteAsync(resultType);
            }
        }

        public async Task<List<SelectModel>> GetResultTypeSelectModels()
        {
            ICollection<ResultType> models = await resultTypeRepository.FilterAsync(x => x.IsActive);
            return models.OrderBy(x=>x.TypeName).Select(x => new SelectModel()
            {
                Text = x.TypeName,
                Value = x.ResultTypeId
            }).ToList();

        }




    }
}
