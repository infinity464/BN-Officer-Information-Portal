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
    public class CourseSubCategoryService : ICourseSubCategoryService
    {
        private readonly IBnoisRepository<CourseSubCategory> courseSubCategoryRepository;
        public CourseSubCategoryService(IBnoisRepository<CourseSubCategory> courseSubCategoryRepository)
        {
            this.courseSubCategoryRepository = courseSubCategoryRepository;
        }

        //public List<CourseSubCategoryModel> GetCourseSubCategories(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<CourseSubCategory> courseSubCategories = courseSubCategoryRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs)) || (x.ShortName.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.CourseCategory.Name.Contains(qs) || String.IsNullOrEmpty(qs))), "CourseCategory");
        //    total = courseSubCategories.Count();
        //    courseSubCategories = courseSubCategories.OrderByDescending(x => x.CourseSubCategoryId).Skip((pn - 1) * ps).Take(ps);
        //    List<CourseSubCategoryModel> models = ObjectConverter<CourseSubCategory, CourseSubCategoryModel>.ConvertList(courseSubCategories.ToList()).ToList();
        //    return models;
        //}

        public async Task<CourseSubCategoryModel> GetCourseSubCategory(int id)
        {
            if (id <= 0)
            {
                return new CourseSubCategoryModel();
            }
            CourseSubCategory courseSubCategory = await courseSubCategoryRepository.FindOneAsync(x => x.CourseSubCategoryId == id);
            if (courseSubCategory == null)
            {
                throw new InfinityNotFoundException("Course Sub Category not found");
            }
            CourseSubCategoryModel model = ObjectConverter<CourseSubCategory, CourseSubCategoryModel>.Convert(courseSubCategory);
            return model;
        }

        public async Task<CourseSubCategoryModel> SaveCourseSubCategory(int id, CourseSubCategoryModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Course Sub Category data missing");
            }

            bool isExistData = courseSubCategoryRepository.Exists(x => x.CourseCategoryId == model.CourseCategoryId && x.Name == model.Name && x.CourseSubCategoryId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            CourseSubCategory courseSubCategory = ObjectConverter<CourseSubCategoryModel, CourseSubCategory>.Convert(model);
            if (id > 0)
            {
                courseSubCategory = await courseSubCategoryRepository.FindOneAsync(x => x.CourseSubCategoryId == id);
                if (courseSubCategory == null)
                {
                    throw new InfinityNotFoundException("Course Sub Category not found !");
                }

                courseSubCategory.ModifiedDate = DateTime.Now;
                courseSubCategory.ModifiedBy = userId;
            }
            else
            {
                courseSubCategory.IsActive = true;
                courseSubCategory.CreatedDate = DateTime.Now;
                courseSubCategory.CreatedBy = userId;
            }
            courseSubCategory.Name = model.Name;
            courseSubCategory.NameBan = model.NameBan;
            courseSubCategory.CourseCategoryId = model.CourseCategoryId;
            courseSubCategory.ShortName = model.ShortName;
            courseSubCategory.ShortNameBan = model.ShortNameBan;
            courseSubCategory.Priority = model.Priority;
            courseSubCategory.BnListPriority = model.BnListPriority;
            courseSubCategory.Trace = model.Trace;
            courseSubCategory.ANmCon = model.ANmCon;
            courseSubCategory.NmRGF = model.NmRGF;
            
            courseSubCategory.Remarks = model.Remarks;
            await courseSubCategoryRepository.SaveAsync(courseSubCategory);
            model.CourseSubCategoryId = courseSubCategory.CourseSubCategoryId;
            return model;
        }


        public async Task<bool> DeleteCourseSubCategory(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            CourseSubCategory courseSubCategory = await courseSubCategoryRepository.FindOneAsync(x => x.CourseSubCategoryId == id);
            if (courseSubCategory == null)
            {
                throw new InfinityNotFoundException("Course Sub Category not found");
            }
            else
            {
                return await courseSubCategoryRepository.DeleteAsync(courseSubCategory);
            }
        }



        public async Task<List<SelectModel>> GetCourseSubCategorySelectModels(int id)
        {
            ICollection<CourseSubCategory> categories = await courseSubCategoryRepository.FilterAsync(x => x.IsActive && x.CourseCategoryId==id);
            List<SelectModel> selectModels = categories.OrderBy(x => x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.CourseSubCategoryId
            }).ToList();
            return selectModels;

        }
        public async Task<List<SelectModel>> GetCourseSubCategorySelectModels()
        {
            ICollection<CourseSubCategory> categories = await courseSubCategoryRepository.FilterAsync(x => x.IsActive );
            List<CourseSubCategory> query = categories.OrderBy(x => x.CourseCategoryId).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.CourseSubCategoryId
            }).ToList();
            return selectModels;

        }

        public List<CourseSubCategoryModel> GetCourseSubCategories(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
