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
    public class RankCategoryService : IRankCategoryService
    {
        private readonly IBnoisRepository<RankCategory> rankCategoryRepository;
        public RankCategoryService(IBnoisRepository<RankCategory> rankCategoryRepository)
        {
            this.rankCategoryRepository = rankCategoryRepository;
        }

        public List<RankCategoryModel> GetRankCategories(int ps, int pn, string qs, out int total)
        {
            IQueryable<RankCategory> rankCategories = rankCategoryRepository.FilterWithInclude(x => x.RankCategoryId > 0
                 && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs))));
            total = rankCategories.Count();
            rankCategories = rankCategories.OrderByDescending(x => x.RankCategoryId).Skip((pn - 1) * ps).Take(ps);
            List<RankCategoryModel> models = ObjectConverter<RankCategory, RankCategoryModel>.ConvertList(rankCategories.ToList()).ToList();
            return models;
        }

        public async Task<RankCategoryModel> GetRankCategory(int id)
        {
            if (id <= 0)
            {
                return new RankCategoryModel();
            }
            RankCategory rankCategory = await rankCategoryRepository.FindOneAsync(x => x.RankCategoryId == id);
            if (rankCategory == null)
            {
                throw new InfinityNotFoundException("RankCategory not found");
            }
            RankCategoryModel model = ObjectConverter<RankCategory, RankCategoryModel>.Convert(rankCategory);
            return model;
        }

        public async Task<RankCategoryModel> SaveRankCategory(int id, RankCategoryModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("RankCategory data missing");
            }

            bool isExist = rankCategoryRepository.Exists(x => x.Name == model.Name && x.RankCategoryId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            RankCategory rankCategory = ObjectConverter<RankCategoryModel, RankCategory>.Convert(model);
            if (id > 0)
            {
                rankCategory = await rankCategoryRepository.FindOneAsync(x => x.RankCategoryId == id);
                if (rankCategory == null)
                {
                    throw new InfinityNotFoundException("RankCategory not found !");
                }
                rankCategory.ModifiedDate = DateTime.Now;
                rankCategory.ModifiedBy = userId;
            }
            else
            {
                rankCategory.IsActive = true;
                rankCategory.CreatedDate = DateTime.Now;
                rankCategory.CreatedBy = userId;
            }
            rankCategory.Name = model.Name;
       
            rankCategory.Remarks = model.Remarks;
            await rankCategoryRepository.SaveAsync(rankCategory);
            return model;
        }


        public async Task<bool> DeleteRankCategoryAsync(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            RankCategory rankCategory = await rankCategoryRepository.FindOneAsync(x => x.RankCategoryId == id);
            if (rankCategory == null)
            {
                throw new InfinityNotFoundException("RankCategory not found");
            }
            else
            {
                return await rankCategoryRepository.DeleteAsync(rankCategory);
            }
        }

        public async Task<List<SelectModel>> GetRankCategorySelectModels()
        {
            ICollection<RankCategory> rankCategories = await rankCategoryRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = rankCategories.OrderBy(x => x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.RankCategoryId
            }).ToList();
            return selectModels;
        }
    }
}
