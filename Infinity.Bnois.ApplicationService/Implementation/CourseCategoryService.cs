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
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly IBnoisRepository<CourseCategory> courseCategoryRepository;
        public CourseCategoryService(IBnoisRepository<CourseCategory> courseCategoryRepository)
        {
            this.courseCategoryRepository = courseCategoryRepository;
        }
       

        public List<CourseCategoryModel> GetCourseCategories(int ps, int pn, string qs, out int total)
        {
            IQueryable<CourseCategory> categories = courseCategoryRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)) || (x.ShortName.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = categories.Count();
            categories = categories.OrderByDescending(x => x.CourseCategoryId).Skip((pn - 1) * ps).Take(ps);
            List<CourseCategoryModel> models = ObjectConverter<CourseCategory, CourseCategoryModel>.ConvertList(categories.ToList()).ToList();
            return models;
        }

        public async Task<CourseCategoryModel> GetCourseCategory(int id)
        {
            if (id <= 0)
            {
                return new CourseCategoryModel();
            }
           CourseCategory courseCategory = await courseCategoryRepository.FindOneAsync(x => x.CourseCategoryId == id);
            if (courseCategory == null)
            {
                throw new InfinityNotFoundException("Course Category not found");
            }
            CourseCategoryModel model = ObjectConverter<CourseCategory, CourseCategoryModel>.Convert(courseCategory);
            return model;
        }

        public async Task<CourseCategoryModel> SaveCourseCategory(int id, CourseCategoryModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Course Category data missing");
            }
            bool isExist = courseCategoryRepository.Exists(x => x.Name == model.Name && x.ShortName==model.ShortName && x.CourseCategoryId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            CourseCategory courseCategory = ObjectConverter<CourseCategoryModel, CourseCategory>.Convert(model);
            if (id > 0)
            {
                courseCategory = await courseCategoryRepository.FindOneAsync(x => x.CourseCategoryId == id);
                if (courseCategory == null)
                {
                    throw new InfinityNotFoundException("Course Category not found !");
                }

                courseCategory.ModifiedDate = DateTime.Now;
                courseCategory.ModifiedBy = userId;
            }
            else
            {
                courseCategory.IsActive = true;
                courseCategory.CreatedDate = DateTime.Now;
                courseCategory.CreatedBy = userId;
            }
            courseCategory.Name = model.Name;
            courseCategory.NameBan = model.NameBan;
            courseCategory.ShortName = model.ShortName;
            courseCategory.ShortNameBan = model.ShortNameBan;
            courseCategory.Priority = model.Priority;
            courseCategory.Trace = model.Trace;
            courseCategory.SASB = model.SASB;
            courseCategory.Remarks = model.Remarks;
            courseCategory.PromotionBoard = model.PromotionBoard;
            courseCategory.BnList = model.BnList;
            courseCategory.BnListPriority = model.BnListPriority;
            courseCategory.TransferProposal = model.TransferProposal;
            await courseCategoryRepository.SaveAsync(courseCategory);
            return model;
        }

        public async Task<bool> DeleteCourseCategory(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            CourseCategory courseCategory = await courseCategoryRepository.FindOneAsync(x => x.CourseCategoryId == id);
            if (courseCategory == null)
            {
                throw new InfinityNotFoundException("Course Category not found");
            }
            else
            {
                return await courseCategoryRepository.DeleteAsync(courseCategory);
            }
        }

        public async Task<List<SelectModel>> GetCourseCategorySelectModels()
        {
            ICollection<CourseCategory> categories = await courseCategoryRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = categories.OrderBy(x=>x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.CourseCategoryId
            }).ToList();
            return selectModels;
        }

        public async Task<List<SelectModel>> GetCourseCategorySelectModelsByTrace()
        {
            ICollection<CourseCategory> categories = await courseCategoryRepository.FilterAsync(x => x.IsActive && x.Trace);
            List<SelectModel> selectModels = categories.OrderBy(x=>x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.CourseCategoryId
            }).ToList();
            return selectModels;
        }
    }
}
