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
   public class ResultGradeService: IResultGradeService
    {
        private readonly IBnoisRepository<ResultGrade> resultGradeRepository;
        public ResultGradeService(IBnoisRepository<ResultGrade> resultGradeRepository)
        {
            this.resultGradeRepository = resultGradeRepository;
        }
        
        public List<ResultGradeModel> GetResultGrades(int ps, int pn, string qs, out int total)
        {
            IQueryable<ResultGrade> resultGrades = resultGradeRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = resultGrades.Count();
            resultGrades = resultGrades.OrderBy(x => x.Name).Skip((pn - 1) * ps).Take(ps);
            List<ResultGradeModel> models = ObjectConverter<ResultGrade, ResultGradeModel>.ConvertList(resultGrades.ToList()).ToList();
            return models;
        }

        public async Task<ResultGradeModel> GetResultGrade(int id)
        {
            if (id <= 0)
            {
                return new ResultGradeModel();
            }
            ResultGrade resultGrade = await resultGradeRepository.FindOneAsync(x => x.ResultGradeId == id);
            if (resultGrade == null)
            {
                throw new InfinityNotFoundException("Grade not found");
            }
            ResultGradeModel model = ObjectConverter<ResultGrade, ResultGradeModel>.Convert(resultGrade);
            return model;
        }

        public async Task<ResultGradeModel> SaveResultGrade(int id, ResultGradeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Grade data missing");
            }
            bool isExist = resultGradeRepository.Exists(x => x.Name == model.Name && x.ResultGradeId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Grade already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ResultGrade resultGrade = ObjectConverter<ResultGradeModel, ResultGrade>.Convert(model);
            if (id > 0)
            {
                resultGrade = await resultGradeRepository.FindOneAsync(x => x.ResultGradeId == id);
                if (resultGrade == null)
                {
                    throw new InfinityNotFoundException("Grade not found !");
                }

                resultGrade.ModifiedDate = DateTime.Now;
                resultGrade.ModifiedBy = userId;
            }
            else
            {
                resultGrade.IsActive = true;
                resultGrade.CreatedDate = DateTime.Now;
                resultGrade.CreatedBy = userId;
            }
            resultGrade.Name = model.Name;

            await resultGradeRepository.SaveAsync(resultGrade);
            model.ResultGradeId = resultGrade.ResultGradeId;
            return model;
        }

        public async Task<bool> DeleteResultGrade(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ResultGrade resultGrade = await resultGradeRepository.FindOneAsync(x => x.ResultGradeId == id);
            if (resultGrade == null)
            {
                throw new InfinityNotFoundException("Grade not found");
            }
            else
            {
                return await resultGradeRepository.DeleteAsync(resultGrade);
            }
        }

        public async Task<List<SelectModel>> getGradeSelectModels()
        {
            ICollection<ResultGrade> resultGrades = await resultGradeRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = resultGrades.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.ResultGradeId
            }).ToList();
            return selectModels;
        }
    }
}
