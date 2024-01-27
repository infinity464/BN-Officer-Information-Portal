using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class RetiredAgeService : IRetiredAgeService
    {
        private readonly IBnoisRepository<RetiredAge> retiredAgeRepository;
        public RetiredAgeService(IBnoisRepository<RetiredAge> retiredAgeRepository)
        {
            this.retiredAgeRepository = retiredAgeRepository;
        }

        //public List<RetiredAgeModel> GetRetiredAges(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<RetiredAge> retiredAges = retiredAgeRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.Category.Name.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.SubCategory.Name.Contains(qs) || String.IsNullOrEmpty(qs))) || (x.Rank.FullName.Contains(qs) || String.IsNullOrEmpty(qs)), "Category", "SubCategory", "Rank");
        //    total = retiredAges.Count();
        //    retiredAges = retiredAges.OrderByDescending(x => x.RetiredAgeId).Skip((pn - 1) * ps).Take(ps);
        //    List<RetiredAgeModel> models = ObjectConverter<RetiredAge, RetiredAgeModel>.ConvertList(retiredAges.ToList()).ToList();
        //    models = models.Select(x =>
        //    {
        //        x.RStatusName = Enum.GetName(typeof(RStatus), x.RStatus);
        //        return x;
        //    }).ToList();
        //    return models;
        //}

        public async Task<RetiredAgeModel> GetRetiredAge(int id)
        {
            if (id <= 0)
            {
                return new RetiredAgeModel();
            }
            RetiredAge RetiredAge = await retiredAgeRepository.FindOneAsync(x => x.RetiredAgeId == id);
            if (RetiredAge == null)
            {
                throw new InfinityNotFoundException("Retired Age not found");
            }
            RetiredAgeModel model = ObjectConverter<RetiredAge, RetiredAgeModel>.Convert(RetiredAge);
            return model;
        }


        public async Task<RetiredAgeModel> SaveRetiredAge(int id, RetiredAgeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Retired Age data missing");
            }


            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            RetiredAge retiredAge = ObjectConverter<RetiredAgeModel, RetiredAge>.Convert(model);
            if (id > 0)
            {
                retiredAge = await retiredAgeRepository.FindOneAsync(x => x.RetiredAgeId == id);
                if (retiredAge == null)
                {
                    throw new InfinityNotFoundException("Retired Age not found !");
                }
                retiredAge.ModifiedDate = DateTime.Now;
                retiredAge.ModifiedBy = userId;
            }
            else
            {
                retiredAge.IsActive = true;
                retiredAge.CreatedDate = DateTime.Now;
                retiredAge.CreatedBy = userId;
            }
            retiredAge.CategoryId = model.CategoryId;
            retiredAge.SubCategoryId = model.SubCategoryId;
            retiredAge.RankId = model.RankId;
            retiredAge.AgeLimit = model.AgeLimit;
            retiredAge.RStatus = model.RStatus;
            retiredAge.Remarks = model.Remarks;


            await retiredAgeRepository.SaveAsync(retiredAge);
            model.RetiredAgeId = retiredAge.RetiredAgeId;
            return model;
        }


        public async Task<bool> DeleteRetiredAge(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            RetiredAge retiredAge = await retiredAgeRepository.FindOneAsync(x => x.RetiredAgeId == id);
            if (retiredAge == null)
            {
                throw new InfinityNotFoundException("Retired Age not found");
            }
            else
            {
                return await retiredAgeRepository.DeleteAsync(retiredAge);
            }
        }



        public List<SelectModel> GetRStatusSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(RStatus)).Cast<RStatus>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value =Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public List<RetiredAgeModel> GetRetiredAges(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}