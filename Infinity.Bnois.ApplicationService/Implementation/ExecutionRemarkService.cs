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
    public class ExecutionRemarkService : IExecutionRemarkService
    {
        private readonly IBnoisRepository<ExecutionRemark> executionRemarkRepository;
        public ExecutionRemarkService(IBnoisRepository<ExecutionRemark> executionRemarkRepository)
        {
            this.executionRemarkRepository = executionRemarkRepository;
        }

        public List<ExecutionRemarkModel> GetExecutionRemarks(int ps, int pn, string qs, out int total,int type)
        {
            IQueryable<ExecutionRemark> executionRemarks = executionRemarkRepository.FilterWithInclude(x => x.IsActive && x.Type==type && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = executionRemarks.Count();
            executionRemarks = executionRemarks.OrderByDescending(x => x.ExecutionRemarkId).Skip((pn - 1) * ps).Take(ps);
            List<ExecutionRemarkModel> models = ObjectConverter<ExecutionRemark, ExecutionRemarkModel>.ConvertList(executionRemarks.ToList()).ToList();
            return models;
        }

        public async Task<ExecutionRemarkModel> GetExecutionRemark(int id)
        {
            if (id <= 0)
            {
                return new ExecutionRemarkModel();
            }
            ExecutionRemark executionRemark = await executionRemarkRepository.FindOneAsync(x => x.ExecutionRemarkId == id);
            if (executionRemark == null)
            {
                throw new InfinityNotFoundException("ExecutionRemark not found");
            }
            ExecutionRemarkModel model = ObjectConverter<ExecutionRemark, ExecutionRemarkModel>.Convert(executionRemark);
            return model;
        }

        public async Task<ExecutionRemarkModel> SaveExecutionRemark(int id, ExecutionRemarkModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("ExecutionRemark data missing");
            }
            bool isExist = executionRemarkRepository.Exists(x => x.Name== model.Name  && x.ExecutionRemarkId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ExecutionRemark executionRemark = ObjectConverter<ExecutionRemarkModel, ExecutionRemark>.Convert(model);
            if (id > 0)
            {
                executionRemark = await executionRemarkRepository.FindOneAsync(x => x.ExecutionRemarkId == id);
                if (executionRemark == null)
                {
                    throw new InfinityNotFoundException("ExecutionRemark not found !");
                }

                executionRemark.ModifiedDate = DateTime.Now;
                executionRemark.ModifiedBy = userId;
            }
            else
            {
                executionRemark.IsActive = true;
                executionRemark.CreatedDate = DateTime.Now;
                executionRemark.CreatedBy = userId;
            }
            executionRemark.Type = model.Type;
            executionRemark.Name = model.Name;
            executionRemark.Remarks = model.Remarks;
            await executionRemarkRepository.SaveAsync(executionRemark);
            model.ExecutionRemarkId = executionRemark.ExecutionRemarkId;
            return model;
        }

        public async Task<bool> DeleteExecutionRemark(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ExecutionRemark executionRemark = await executionRemarkRepository.FindOneAsync(x => x.ExecutionRemarkId == id);
            if (executionRemark == null)
            {
                throw new InfinityNotFoundException("ExecutionRemark not found");
            }
            else
            {
                return await executionRemarkRepository.DeleteAsync(executionRemark);
            }
        }

        public async Task<List<SelectModel>> GetExecutionRemarkSelectModels(int type)
        {
            ICollection<ExecutionRemark> models = await executionRemarkRepository.FilterAsync(x => x.IsActive && x.Type==type);
            return models.Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.ExecutionRemarkId
            }).ToList();

        }
    }
}
