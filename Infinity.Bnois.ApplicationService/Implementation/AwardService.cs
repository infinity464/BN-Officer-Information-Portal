using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   public class AwardService : IAwardService
    {

        private readonly IBnoisRepository<Award> awardRepository;
        public AwardService(IBnoisRepository<Award> awardRepository)
        {
            this.awardRepository = awardRepository;
        }

        public List<AwardModel> GetAwards(int ps, int pn, string qs, out int total)
        {
            IQueryable<Award> awards = awardRepository.FilterWithInclude(x => x.IsActive && (x.NameEng.Contains(qs) || String.IsNullOrEmpty(qs) ));
            total = awards.Count();
            awards = awards.OrderByDescending(x => x.AwardId).Skip((pn - 1) * ps).Take(ps);
            List<AwardModel> models = ObjectConverter<Award, AwardModel>.ConvertList(awards.ToList()).ToList();
            return models;
        }

        public async Task<AwardModel> GetAward(int id)
        {
            if (id <= 0)
            {
                return new AwardModel();
            }
            Award award = await awardRepository.FindOneAsync(x => x.AwardId == id);
            if (award == null)
            {
                throw new InfinityNotFoundException("Award not found");
            }
            AwardModel model = ObjectConverter<Award, AwardModel>.Convert(award);
            return model;
        }

        //public async Task<AwardModel> SaveAward(int id, AwardModel model)
        //{
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Award data missing");
        //    }
        //    bool isExist = awardRepository.Exists(x => x.NameEng == model.NameEng  && x.AwardId != id);
        //    if (isExist)
        //    {
        //        throw new InfinityInvalidDataException("Data already exists !");
        //    }

        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    Award award = ObjectConverter<AwardModel, Award>.Convert(model);
        //    if (id > 0)
        //    {
        //        award = await awardRepository.FindOneAsync(x => x.AwardId == id);
        //        if (award == null)
        //        {
        //            throw new InfinityNotFoundException("Award not found !");
        //        }

        //        award.ModifiedDate = DateTime.Now;
        //        award.ModifiedBy = userId;
        //    }
        //    else
        //    {
        //        award.IsActive = true;
        //        award.CreatedDate = DateTime.Now;
        //        award.CreatedBy = userId;
        //    }
        //    award.NameEng = model.NameEng;
        //    award.NameBan = model.NameBan;
        //    award.ShortNameBan = model.ShortNameBan;
        //    award.ShortNameEng = model.ShortNameEng;
        //    award.Priority = model.Priority;
        //    award.GoToOPR = model.GoToOPR;
        //    award.GoToTrace = model.GoToTrace;
        //    award.ANmCon = model.ANmCon;
        //    award.NmRGF = model.NmRGF;
        //    award.Description = model.Description;
        //    award.GoToSASB = model.GoToSASB;


        //    await awardRepository.SaveAsync(award);
        //    model.AwardId = award.AwardId;
        //    return model;
        //}

        public async Task<bool> DeleteAward(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Award award = await awardRepository.FindOneAsync(x => x.AwardId == id);
            if (award == null)
            {
                throw new InfinityNotFoundException("Award not found");
            }
            else
            {
                return await awardRepository.DeleteAsync(award);
            }
        }

        public async Task<List<SelectModel>> GetAwardSelectModels()
        {
            ICollection<Award> models = await awardRepository.FilterAsync(x => x.IsActive);
            return models.OrderBy(x => x.NameEng).Select(x => new SelectModel()
            {
                Text = x.NameEng,
                Value = x.AwardId
            }).ToList();

        }

        public Task<AwardModel> SaveAward(int id, AwardModel model)
        {
            throw new NotImplementedException();
        }
    }
}
