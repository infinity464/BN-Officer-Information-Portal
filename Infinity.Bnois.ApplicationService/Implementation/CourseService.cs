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
    public class CourseService : ICourseService
    {
        private readonly IBnoisRepository<Course> courseRepository;
        public CourseService(IBnoisRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        //public List<CourseModel> GetCourses(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<Course> courses = courseRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.FullName.Contains(qs) || String.IsNullOrEmpty(qs)) || (x.ShortName.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.CourseCategory.Name.Contains(qs) || String.IsNullOrEmpty(qs)) || (x.CourseSubCategory.Name.Contains(qs) || String.IsNullOrEmpty(qs))),"Country", "CourseCategory", "CourseSubCategory");
        //    total = courses.Count();
        //    courses = courses.OrderByDescending(x => x.CourseId).Skip((pn - 1) * ps).Take(ps);
        //    List<CourseModel> models = ObjectConverter<Course, CourseModel>.ConvertList(courses.ToList()).ToList();
        //    return models;
        //}

        public async Task<CourseModel> GetCourse(int id)
        {
            if (id <= 0)
            {
                return new CourseModel();
            }
            Course course = await courseRepository.FindOneAsync(x => x.CourseId == id);
            if (course == null)
            {
                throw new InfinityNotFoundException("Course not found");
            }
            CourseModel model = ObjectConverter<Course, CourseModel>.Convert(course);
            return model;
        }

        public async Task<CourseModel> SaveCourse(int id, CourseModel model)
        {
            if (model == null) 
            {
                throw new InfinityArgumentMissingException("Course  data missing");
            }

            bool isExistData = courseRepository.Exists(x => x.CourseCategoryId == model.CourseCategoryId && x.CountryId == model.CountryId && x.FullName == model.FullName  && x.CourseSubCategoryId==model.CourseSubCategoryId && x.CourseId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Course course = ObjectConverter<CourseModel, Course>.Convert(model);
            if (id > 0)
            {
                course = await courseRepository.FindOneAsync(x => x.CourseId == id);
                if (course == null)
                {
                    throw new InfinityNotFoundException("Course not found !");
                }

                course.ModifiedDate = DateTime.Now;
                course.ModifiedBy = userId;
            }
            else
            {
                course.IsActive = true;
                course.CreatedDate = DateTime.Now;
                course.CreatedBy = userId;
            }
            course.FullName = model.FullName;
            course.CourseCategoryId = model.CourseCategoryId;
            course.CourseSubCategoryId = model.CourseSubCategoryId;
            course.CountryId = model.CountryId;
            course.ShortName = model.ShortName;
            course.NameInBangla = model.NameInBangla;
            course.Priority = model.Priority;
            course.ANGF = model.ANGF;
            course.SplQualification = model.SplQualification;
            await courseRepository.SaveAsync(course);
            model.CourseId = course.CourseId;
            return model;
        }


        public async Task<bool> DeleteCourse(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Course course = await courseRepository.FindOneAsync(x => x.CourseId == id);
            if (course == null)
            {
                throw new InfinityNotFoundException("Course not found");
            }
            else
            {
                return await courseRepository.DeleteAsync(course);
            }
        }

    
        public async Task<List<SelectModel>> GetCourseSelectModels(int catId, int subCatId,int countryId)
        {
            ICollection<Course> categories = await courseRepository.FilterAsync(x => x.IsActive && x.CourseCategoryId==catId && x.CourseSubCategoryId==subCatId && x.CountryId==countryId);
            List<SelectModel> selectModels = categories.OrderBy(x => x.FullName).Select(x => new SelectModel
            {
                Text = x.FullName,
                Value = x.CourseId
            }).ToList();
            return selectModels;

        }



        public async Task<List<SelectModel>> GetCourseBySubCategory(int subCatId)
        {
            ICollection<Course> categories = await courseRepository.FilterAsync(x => x.IsActive &&  x.CourseSubCategoryId==subCatId);
            List<SelectModel> selectModels = categories.OrderBy(x => x.FullName).Select(x => new SelectModel
            {
                Text = x.FullName,
                Value = x.CourseId
            }).ToList();
            return selectModels;

        }


        public async Task<List<SelectModel>> GetCourseByCategory(int catId)
        {
            ICollection<Course> categories = await courseRepository.FilterAsync(x => x.IsActive && x.CourseCategoryId == catId);
            List<SelectModel> selectModels = categories.OrderBy(x => x.FullName).Select(x => new SelectModel
            {
                Text = x.FullName,
                Value = x.CourseId
            }).ToList();
            return selectModels;

        }


        public async Task<List<SelectModel>> GetCourseSelectModels()
        {
            ICollection<Course> categories = await courseRepository.FilterAsync(x => x.IsActive);
            List<Course> query = categories.OrderBy(x => x.CourseCategoryId).ToList();
            List<SelectModel> selectModels = categories.Select(x => new SelectModel
            {
                Text = x.FullName,
                Value = x.CourseId
            }).ToList();
            return selectModels;

        }

        public List<CourseModel> GetCourses(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
