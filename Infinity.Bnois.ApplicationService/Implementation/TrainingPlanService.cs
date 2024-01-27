using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class TrainingPlanService : ITrainingPlanService
    {
        private readonly IBnoisRepository<TrainingPlan> trainingPlanRepository;
        private readonly IBnoisRepository<TrainingRank> trainingRankRepository;
        private readonly IBnoisRepository<TrainingBranch> trainingBranchRepository;
        public TrainingPlanService(IBnoisRepository<TrainingBranch> trainingBranchRepository,IBnoisRepository<TrainingRank> trainingRankRepository,IBnoisRepository<TrainingPlan> trainingPlanRepository)
        {
            this.trainingPlanRepository = trainingPlanRepository;
            this.trainingBranchRepository = trainingBranchRepository;
            this.trainingRankRepository = trainingRankRepository;
        }

        //public List<TrainingPlanModel> GetTrainingPlans(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<TrainingPlan> trainingPlans = trainingPlanRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.Course.FullName.Contains(qs) || (x.TrainingInstitute.FullName.Contains(qs) )||(x.TrainingInstitute.FullName.Contains(qs)) ||  (x.Country.FullName.Contains(qs))  || (x.CourseCategory.Name.Contains(qs)) || String.IsNullOrEmpty(qs))), "Course", "TrainingInstitute", "Country","CourseCategory","CourseSubCategory");
        //    total = trainingPlans.Count();
        //    trainingPlans = trainingPlans.OrderByDescending(x => x.TrainingPlanId).Skip((pn - 1) * ps).Take(ps);
        //    List<TrainingPlanModel> models = ObjectConverter<TrainingPlan, TrainingPlanModel>.ConvertList(trainingPlans.ToList()).ToList();
        //    return models;
        //}

        public async Task<TrainingPlanModel> GetTrainingPlan(int id)
        {
            if (id <= 0)
            {
                return new TrainingPlanModel();
            }
            TrainingPlan trainingPlan = await trainingPlanRepository.FindOneAsync(x => x.TrainingPlanId == id);
            if (trainingPlan == null)
            {
                throw new InfinityNotFoundException("Training Plan not found");
            }
            int[] rankIds = trainingRankRepository.Where(x => x.TrainingPlanId == id).Select(x => x.RankId).ToArray();
            int[] branchIds = trainingBranchRepository.Where(x => x.TrainingPlanId == id).Select(x => x.BranchId).ToArray();
            TrainingPlanModel model = ObjectConverter<TrainingPlan, TrainingPlanModel>.Convert(trainingPlan);
            model.RankIds = rankIds;
            model.BranchIds = branchIds;
            return model;
        }

        public async Task<TrainingPlanModel> SaveTrainingPlan(int id, TrainingPlanModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Training Plan  data missing");
            }
            bool isExistData = trainingPlanRepository.Exists(x => x.CountryId == model.CountryId && x.CourseId == model.CourseId && x.CourseNo==model.CourseNo && x.InstituteId==model.InstituteId && x.TrainingPlanId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            if (model.FromDate>model.ToDate)
            {
                throw new InfinityInvalidDataException("From Date Greater than To Date.");
            }



            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            TrainingPlan trainingPlan = ObjectConverter<TrainingPlanModel, TrainingPlan>.Convert(model);
            if (id > 0)
            {
                trainingPlan = await trainingPlanRepository.FindOneAsync(x => x.TrainingPlanId == id);
                if (trainingPlan == null)
                {
                    throw new InfinityNotFoundException("Training Plan not found !");
                }
                ICollection<TrainingRank>trainingRanks=await  trainingRankRepository.FilterAsync(x => x.TrainingPlanId == id);
                trainingRankRepository.RemoveRange(trainingRanks);

                ICollection<TrainingBranch> trainingBranches = await trainingBranchRepository.FilterAsync(x => x.TrainingPlanId == id);
                trainingBranchRepository.RemoveRange(trainingBranches);

                trainingPlan.ModifiedDate = DateTime.Now;
                trainingPlan.ModifiedBy = userId;
            }
            else
            {
                trainingPlan.IsClosed = "O";
                trainingPlan.IsActive = true;
                trainingPlan.CreatedDate = DateTime.Now;
                trainingPlan.CreatedBy = userId;
            
            }
            //if (model.RankIds.Any())
            //{
            //    trainingPlan.TrainingRanks = model.RankIds.Select(x => new TrainingRank() { RankId = x }).ToList();
            //}
            //if (model.BranchIds.Any())
            //{
            //    trainingPlan.TrainingBranches = model.BranchIds.Select(x => new TrainingBranch() { BranchId = x }).ToList();
            //}
            trainingPlan.CountryId = model.CountryId;
            trainingPlan.CourseId = model.CourseId;
            trainingPlan.CourseNo = model.CourseNo;
            trainingPlan.CourseCategoryId = model.CourseCategoryId;
            trainingPlan.CourseSubCategoryId = model.CourseSubCategoryId;
            trainingPlan.CountryType = model.CountryType;
            trainingPlan.InstituteId = model.InstituteId;
            trainingPlan.FromDate = model.FromDate ?? trainingPlan.FromDate;
            trainingPlan.ToDate = model.ToDate ?? trainingPlan.ToDate;
            trainingPlan.MaxParticipant = model.MaxParticipant;
            trainingPlan.IsPostponed = model.IsPostponed;
            trainingPlan.PDate = model.PDate;
            trainingPlan.PToDate = model.PToDate;
            trainingPlan.Purpose = model.Purpose;
            await trainingPlanRepository.SaveAsync(trainingPlan);
            model.TrainingPlanId = trainingPlan.TrainingPlanId;
            return model;
        }


        public async Task<bool> DeleteTrainingPlan(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            TrainingPlan trainingPlan = await trainingPlanRepository.FindOneAsync(x => x.TrainingPlanId == id);
            if (trainingPlan == null)
            {
                throw new InfinityNotFoundException("Training Plan not found");
            }
            else
            {
                trainingRankRepository.Delete(x => x.TrainingPlanId == trainingPlan.TrainingPlanId);
                trainingBranchRepository.Delete(x => x.TrainingPlanId == trainingPlan.TrainingPlanId);
                return await trainingPlanRepository.DeleteAsync(trainingPlan);
            }
        }

        public List<SelectModel> GetCountryTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(CountryType)).Cast<CountryType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        //public async Task<List<SelectModel>> GetTrainingPlanSelectModels()
        //{
        //    ICollection<TrainingPlan> models = await trainingPlanRepository.FilterWithInclude(x => x.IsActive  , "Course", "TrainingInstitute", "Country").ToListAsync();
        //    return models.OrderByDescending(x => x.FromDate).Select(x => new SelectModel()
        //    {
        //        Text = String.Format("{0} [{1} To {2},{3}]",x.Course.FullName, String.Format("{0:dd-MM-yyyy}", x.FromDate), String.Format("{0:dd-MM-yyyy}", x.ToDate), x.Country.FullName),
        //        Value = x.TrainingPlanId
        //    }).ToList();
        //}

        //public async Task<List<SelectModel>> GetTrainingPlanSelectModels(int trainingPlanId)
        //{
        //    ICollection<TrainingPlan> models = await trainingPlanRepository.FilterWithInclude(x => x.IsActive && x.TrainingPlanId == trainingPlanId, "Course", "TrainingInstitute", "Country").ToListAsync();
        //    return models.OrderByDescending(x => x.FromDate).Select(x => new SelectModel()
        //    {
        //        Text = String.Format("{0},{1}[{2} To {3}]", x.Course.FullName, x.TrainingInstitute.FullName, String.Format("{0:dd-MM-yyyy}", x.FromDate), String.Format("{0:dd-MM-yyyy}", x.ToDate)),
        //        Value = x.TrainingPlanId
        //    }).ToList();
        //}

        public List<TrainingPlanModel> GetTrainingPlans(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetTrainingPlanSelectModels()
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetTrainingPlanSelectModels(int trainingPlanId)
        {
            throw new NotImplementedException();
        }
    }
}
