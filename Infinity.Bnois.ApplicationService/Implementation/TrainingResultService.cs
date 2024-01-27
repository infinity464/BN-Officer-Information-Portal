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
    public class TrainingResultService : ITrainingResultService
    {
        private readonly IBnoisRepository<TrainingResult> trainingResultRepository;
        private readonly IBnoisRepository<TrainingPlan> trainingPlanRepository;
        private readonly IProcessRepository processRepository;
        private readonly IBnoisRepository<NominationDetail> nominationDetailRepository;
        private readonly IBnoisRepository<Nomination> nominationRepository;
        public TrainingResultService(IBnoisRepository<TrainingResult> trainingResultRepository, IBnoisRepository<TrainingPlan> trainingPlanRepository, IProcessRepository processRepository, IBnoisRepository<NominationDetail> nominationDetailRepository, IBnoisRepository<Nomination> nominationRepository)
        {
            this.trainingResultRepository = trainingResultRepository;
            this.trainingPlanRepository = trainingPlanRepository;
            this.processRepository = processRepository;
            this.nominationDetailRepository = nominationDetailRepository;
            this.nominationRepository = nominationRepository;
        }

        //public List<TrainingResultModel> GetTrainingResults(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<TrainingResult> trainingResults = trainingResultRepository.FilterWithInclude(x => x.IsActive
        //        && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs)||x.ResultType.TypeName==qs || String.IsNullOrEmpty(qs)), "Employee", "TrainingPlan", "ResultType", "TrainingPlan.Course");
        //    total = trainingResults.Count();
        //    trainingResults = trainingResults.OrderByDescending(x => x.TrainingResultId).Skip((pn - 1) * ps).Take(ps);
        //    List<TrainingResultModel> models = ObjectConverter<TrainingResult, TrainingResultModel>.ConvertList(trainingResults.ToList()).ToList();
        //    return models;
        //}

        public async Task<TrainingResultModel> GetTrainingResult(int id)
        {
            if (id <= 0)
            {
                return new TrainingResultModel();
            }
            TrainingResult trainingResult = await trainingResultRepository.FindOneAsync(x => x.TrainingResultId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch","TrainingPlan.Country" });
            if (trainingResult == null)
            {
                throw new InfinityNotFoundException("Training Result not found");
            }
            TrainingResultModel model = ObjectConverter<TrainingResult, TrainingResultModel>.Convert(trainingResult);
            return model;
        }

        public async Task<TrainingResultModel> GetTrainingResultByEmployee(int id)
        {
            if (id <= 0)
            {
                return new TrainingResultModel();
            }
            TrainingResult trainingResult = await trainingResultRepository.FindOneAsync(x => x.EmployeeId == id, new List<string> { "Employee", "TrainingPlan", "TrainingPlan.Course", "TrainingPlan.TrainingInstitute", "TrainingPlan.Country", "TrainingPlan.CourseCategory", "TrainingPlan.CourseSubCategory", "ResultType" });
            if (trainingResult == null)
            {
                throw new InfinityNotFoundException("Training Result not found");
            }
            TrainingResultModel model = ObjectConverter<TrainingResult, TrainingResultModel>.Convert(trainingResult);
            return model;
        }

        public async Task<TrainingResultModel> SaveTrainingResult(int id, TrainingResultModel model)
        {
            model.Employee = null;

            if (model == null)
            {
                throw new InfinityArgumentMissingException("Training Result  data missing");
            }     
            if (model.TrainingPlanId >0)
            {
                TrainingPlan trainingPlan = await trainingPlanRepository.FindOneAsync(x => x.TrainingPlanId == model.TrainingPlanId);

                if (trainingPlan.ToDate > DateTime.Today && (model.ResultTypeId>1))
                { 
                    throw new InfinityArgumentMissingException("Course not finished yet.");
                }
              

                Nomination nominationWithoutTransfer = await nominationRepository.FindOneAsync(x => x.EnitityType == 1 && x.EntityId == model.TrainingPlanId && x.WithoutTransfer == false);
                if (nominationWithoutTransfer != null)
                {
                    bool isExistData = nominationDetailRepository.Exists(x => x.NominationId == nominationWithoutTransfer.NominationId && x.EmployeeId == model.EmployeeId);
                    if (!isExistData)
                    {
                        throw new InfinityInvalidDataException("Officer is not nominated for this course. !!");
                    }
                }

                Nomination nominationWithTransfer = await nominationRepository.FindOneAsync(x => x.EnitityType==1&& x.EntityId == model.TrainingPlanId && x.WithoutTransfer);
                if (nominationWithTransfer != null)
                {
                    bool isExistData = nominationDetailRepository.Exists(x => x.NominationId == nominationWithTransfer.NominationId && x.EmployeeId == model.EmployeeId);
                    if (!isExistData)
                    {
                        throw new InfinityInvalidDataException("Officer is not nominated for this course. !!");
                    }
                }
                bool isExist = await trainingResultRepository.ExistsAsync(x =>x.TrainingPlanId == model.TrainingPlanId && x.EmployeeId == model.EmployeeId && x.TrainingResultId != model.TrainingResultId);

                if (isExist)
                {
                    throw new InfinityInvalidDataException("This result already exist !");
                }
                model.CourseCategoryId = trainingPlan.CourseCategoryId;
                model.CourseSubCategoryId = trainingPlan.CourseSubCategoryId;
               
            }


            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            TrainingResult trainingResult = ObjectConverter<TrainingResultModel, TrainingResult>.Convert(model);
            if (id > 0)
            {
                trainingResult = await trainingResultRepository.FindOneAsync(x => x.TrainingResultId == id);
                if (trainingResult == null)
                {
                    throw new InfinityNotFoundException("Training Result not found !");
                }

                trainingResult.ModifiedDate = DateTime.Now;
                trainingResult.ModifiedBy = userId;
            }
            else
            {
                trainingResult.IsActive = true;
                trainingResult.CreatedDate = DateTime.Now;
                trainingResult.CreatedBy = userId;
            }
            trainingResult.EmployeeId = model.EmployeeId;
            trainingResult.TrainingPlanId = model.TrainingPlanId;
            trainingResult.ResultTypeId = model.ResultTypeId;
            trainingResult.CourseCategoryId = model.CourseCategoryId;
            trainingResult.CourseSubCategoryId = model.CourseSubCategoryId;
            trainingResult.Positon = model.Positon;
            trainingResult.Percentage = model.Percentage;
            trainingResult.ResultDate = model.ResultDate ?? trainingResult.ResultDate;
            trainingResult.ResultStatus = model.ResultStatus;
            trainingResult.Remarks = model.Remarks;
            trainingResult.Unit = model.Unit;

            await trainingResultRepository.SaveAsync(trainingResult);
            model.TrainingResultId = trainingResult.TrainingResultId;

            await processRepository.UpdateNamingConvention(trainingResult.EmployeeId);

            return model;
        }

        public async Task<bool> DeleteTrainingResult(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            TrainingResult trainingResult = await trainingResultRepository.FindOneAsync(x => x.TrainingResultId == id);
            if (trainingResult == null)
            {
                throw new InfinityNotFoundException("Training Result not found");
            }
            else
            {
                var result = await trainingResultRepository.DeleteAsync(trainingResult);
                await processRepository.UpdateNamingConvention(trainingResult.EmployeeId);
                return result;
            }
        }


        public List<SelectModel> GetResultStatusSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(ResultStatus)).Cast<ResultStatus>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public async Task<TrainingResultModel> UpdateTrainingResult(TrainingResultModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Training Result  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            TrainingResult trainingResult = ObjectConverter<TrainingResultModel, TrainingResult>.Convert(model);

            trainingResult = await trainingResultRepository.FindOneAsync(x => x.TrainingResultId == model.TrainingResultId);
            if (trainingResult == null)
            {
                throw new InfinityNotFoundException("Training Result not found !");
            }
        
            if (model.ResultSection != null)
            {
                trainingResult.ResultSection = model.ResultSection;
                trainingResult.FileName = model.ResultSection;
            }
            trainingResult.ModifiedDate = DateTime.Now;
            trainingResult.ModifiedBy = userId;
            await trainingResultRepository.SaveAsync(trainingResult);
            model.TrainingResultId = trainingResult.TrainingResultId;
            return model;
        }

        public List<TrainingResultModel> GetTrainingResults(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
