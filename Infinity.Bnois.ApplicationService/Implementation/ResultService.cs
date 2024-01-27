using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class ResultService : IResultService
    {
        private readonly IBnoisRepository<Result> resultRepository;
        public ResultService(IBnoisRepository<Result> resultRepository)
        {
            this.resultRepository = resultRepository;
        }

        public List<ResultModel> GetResults(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<Result> results = resultRepository.FilterWithInclude(x => x.IsActive
               && ((x.Name.Contains(searchText)
               || String.IsNullOrEmpty(searchText))));
            total = results.Count();
            results = results.OrderBy(x => x.ResultCode).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return ObjectConverter<Result, ResultModel>.ConvertList(results.ToList()).ToList();
        }
        public async Task<ResultModel> GetResult(int resultId)
        {
            if (resultId == 0)
            {
                return new ResultModel();
            }
            Result result = await resultRepository.FindOneAsync(x => x.ResultId == resultId);
            if (result == null)
            {
                throw new InfinityNotFoundException("Result not found !");
            }
            return ObjectConverter<Result, ResultModel>.Convert(result);
        }

        public async Task<ResultModel> SaveResult(int resultId, ResultModel model)
        {
            bool isExist = await resultRepository.ExistsAsync(x =>x.Name == model.Name && x.ResultId != model.ResultId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Result data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Result data missing");
            }
            Result result = ObjectConverter<ResultModel, Result>.Convert(model);
            if (resultId > 0)
            {
                result = await resultRepository.FindOneAsync(x => x.ResultId == resultId);
                if (result == null)
                {
                    throw new InfinityNotFoundException("Result not found!");
                }
                result.ModifiedBy = model.ModifiedBy;
                result.ModifiedDate = DateTime.Now;
            }
            else
            {
                result.CreatedBy = model.CreatedBy;
                result.CreatedDate = DateTime.Now;
                result.IsActive = true;
            }
            result.ResultCode = model.ResultCode;
            result.Name = model.Name;
            result.Description = model.Description;
            result.MinGPA = model.MinGPA;
            result.MaxGPA = model.MaxGPA;
         
            await resultRepository.SaveAsync(result);
            model.ResultId = result.ResultId;
            return model;
        }

        public async Task<bool> DeleteResult(int resultId)
        {
            if (resultId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Result result = await resultRepository.FindOneAsync(x => x.ResultId == resultId);
            if (result == null)
            {
                throw new InfinityNotFoundException("Result not found!");
            }
            else
            {
             
                return await resultRepository.DeleteAsync(result);
            }
        }

        public async Task<List<SelectModel>> ResultsSelectModel(int? examCategoryId)
        {
            IQueryable<Result> queryable =  resultRepository.Where(x => x.IsActive).OrderBy(x => x.ResultCode);
            List<Result> results = await queryable.ToListAsync();
            List<SelectModel> resultSelectModels = results.OrderBy(x=>x.ResultCode).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.ResultId
            }).ToList();
            return resultSelectModels;
        }


        public async Task<List<SelectModel>> ResultSelectModels()
        {
            IQueryable<Result> queryable = resultRepository.Where(x => x.IsActive).OrderBy(x => x.ResultCode);
            List<Result> results = await queryable.ToListAsync();
            List<SelectModel> resultSelectModels = results.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.ResultId
            }).ToList();
            return resultSelectModels;
        }
    }
}
