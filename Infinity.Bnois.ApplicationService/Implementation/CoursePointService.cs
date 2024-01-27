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
  public  class CoursePointService: ICoursePointService
    {
        private readonly IBnoisRepository<CoursePoint> coursePointRepository;
        public CoursePointService(IBnoisRepository<CoursePoint> coursePointRepository)
        {
            this.coursePointRepository = coursePointRepository;
        }

        public async Task<CoursePointModel> GetCoursePoint(int coursePointId)
        {
            if (coursePointId <= 0)
            {
                return new CoursePointModel();
            }
            CoursePoint coursePoint = await coursePointRepository.FindOneAsync(x => x.CoursePointId == coursePointId, new List<string> { "CourseCategory" });

            if (coursePoint == null)
            {
                throw new InfinityNotFoundException("Course Point not found!");
            }
            CoursePointModel model = ObjectConverter<CoursePoint, CoursePointModel>.Convert(coursePoint);
            return model;
        }

        public List<CoursePointModel> GetCoursePoints(int traceSettingId)
        {
            List<CoursePoint> coursePoints = coursePointRepository.FilterWithInclude(x => x.TraceSettingId == traceSettingId, "CourseCategory").ToList();
            List<CoursePointModel> models = ObjectConverter<CoursePoint, CoursePointModel>.ConvertList(coursePoints.ToList()).ToList();
            return models;
        }

        public async Task<CoursePointModel> SaveCoursePoint(int coursePointId, CoursePointModel model)
        {
            bool isExist = await coursePointRepository.ExistsAsync(x =>x.TraceSettingId==model.TraceSettingId && x.CourseCategoryId == model.CourseCategoryId  && x.CoursePointId != model.CoursePointId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Course Category data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Course Point data missing");
            }
            CoursePoint coursePoint = ObjectConverter<CoursePointModel, CoursePoint>.Convert(model);

            if (coursePointId > 0)
            {
                coursePoint = await coursePointRepository.FindOneAsync(x => x.CoursePointId == coursePointId);
                if (coursePoint == null)
                {
                    throw new InfinityNotFoundException("Course Point not found!");
                }
                coursePoint.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                coursePoint.ModifiedDate = DateTime.Now;
            }
            else
            {
                coursePoint.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                coursePoint.CreatedDate = DateTime.Now;
            }
            coursePoint.TraceSettingId = model.TraceSettingId;
            coursePoint.CourseCategoryId = model.CourseCategoryId;
            coursePoint.Max = model.Max;
            coursePoint.Min = model.Min;

            await coursePointRepository.SaveAsync(coursePoint);
            model.CoursePointId = coursePoint.CoursePointId;
            return model;
        }
    }
}
