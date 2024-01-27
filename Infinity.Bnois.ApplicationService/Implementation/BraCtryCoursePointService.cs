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
  public  class BraCtryCoursePointService: IBraCtryCoursePointService
    {
        private readonly IBnoisRepository<BraCtryCoursePoint> braCtryCoursePointRepository;
        public BraCtryCoursePointService(IBnoisRepository<BraCtryCoursePoint> braCtryCoursePointRepository)
        {
            this.braCtryCoursePointRepository = braCtryCoursePointRepository;
        }

        public async Task<BraCtryCoursePointModel> GetBraCtryCoursePoint(int braCtryCoursePointId)
        {
            if (braCtryCoursePointId <= 0)
            {
                return new BraCtryCoursePointModel();
            }
            BraCtryCoursePoint braCtryCoursePoint = await braCtryCoursePointRepository.FindOneAsync(x => x.BraCtryCoursePointId == braCtryCoursePointId);

            if (braCtryCoursePoint == null)
            {
                throw new InfinityNotFoundException("Brach and County Course Point not found!");
            }
            BraCtryCoursePointModel model = ObjectConverter<BraCtryCoursePoint, BraCtryCoursePointModel>.Convert(braCtryCoursePoint);
            return model;
        }

        public List<BraCtryCoursePointModel> GetBraCtryCoursePoints(int traceSettingId)
        {
            List<BraCtryCoursePoint> braCtryCoursePoints = braCtryCoursePointRepository.FilterWithInclude(x => x.TraceSettingId == traceSettingId, "CourseCategory","CourseSubCategory","RankCategory","Branch","Country").ToList();
            List<BraCtryCoursePointModel> models = ObjectConverter<BraCtryCoursePoint, BraCtryCoursePointModel>.ConvertList(braCtryCoursePoints.ToList()).ToList();
            return models;
        }

        public async Task<BraCtryCoursePointModel> SaveBraCtryCoursePoint(int braCtryCoursePointId, BraCtryCoursePointModel model)
        {
            bool isExist = await braCtryCoursePointRepository.ExistsAsync(x => x.TraceSettingId == model.TraceSettingId && x.CourseCategoryId==model.CourseCategoryId
            && x.CourseSubCategoryId==model.CourseSubCategoryId 
            && x.RankCategoryId==model.RankCategoryId 
            && x.BranchId==model.BranchId 
            && x.CountryId==model.CountryId 
            && x.BraCtryCoursePointId != braCtryCoursePointId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Brach and County Course Point data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Brach and County Course Point data missing");
            }
            BraCtryCoursePoint braCtryCoursePoint = ObjectConverter<BraCtryCoursePointModel, BraCtryCoursePoint>.Convert(model);

            if (braCtryCoursePointId > 0)
            {
                braCtryCoursePoint = await braCtryCoursePointRepository.FindOneAsync(x => x.BraCtryCoursePointId == braCtryCoursePointId);
                if (braCtryCoursePoint == null)
                {
                    throw new InfinityNotFoundException("Course Point not found!");
                }
                braCtryCoursePoint.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                braCtryCoursePoint.ModifiedDate = DateTime.Now;
            }
            else
            {
                braCtryCoursePoint.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                braCtryCoursePoint.CreatedDate = DateTime.Now;
            }
            braCtryCoursePoint.TraceSettingId = model.TraceSettingId;
            braCtryCoursePoint.CourseCategoryId = model.CourseCategoryId;
            braCtryCoursePoint.CourseSubCategoryId = model.CourseSubCategoryId;
            braCtryCoursePoint.RankCategoryId = model.RankCategoryId;
            braCtryCoursePoint.BranchId = model.BranchId;
            braCtryCoursePoint.CountryId = model.CountryId;
            braCtryCoursePoint.Max = model.Max;
            braCtryCoursePoint.Min = model.Min;
            await braCtryCoursePointRepository.SaveAsync(braCtryCoursePoint);
            model.BraCtryCoursePointId = braCtryCoursePoint.BraCtryCoursePointId;
            return model;
        }
    }
}
