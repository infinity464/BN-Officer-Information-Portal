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
   public class RankMapService: IRankMapService
    {
        private readonly IBnoisRepository<RankMap> rankMapRepository;
        public RankMapService(IBnoisRepository<RankMap> rankMapRepository)
        {
            this.rankMapRepository = rankMapRepository;
        }

        //public List<RankMapModel> GetRankMaps(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<RankMap> rankMaps = rankMapRepository.FilterWithInclude(x => x.IsActive 
        //           && ((x.Rank.FullName.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //           (x.Rank1.FullName.Contains(qs) || String.IsNullOrEmpty(qs))||
        //           (x.Rank2.FullName.Contains(qs) || String.IsNullOrEmpty(qs))
        //           ));
        //    total = rankMaps.Count();
        //    rankMaps = rankMaps.OrderByDescending(x => x.RankMapId).Skip((pn - 1) * ps).Take(ps);
        //    List<RankMapModel> models = ObjectConverter<RankMap, RankMapModel>.ConvertList(rankMaps.ToList()).ToList();
        //    return models;
        //}

        public async Task<RankMapModel> GetRankMap(int id)
        {
            if (id <= 0)
            {
                return new RankMapModel();
            }
            RankMap rankMap = await rankMapRepository.FindOneAsync(x => x.RankMapId == id);
            if (rankMap == null)
            {
                throw new InfinityNotFoundException("RankMap not found");
            }
            RankMapModel model = ObjectConverter<RankMap, RankMapModel>.Convert(rankMap);
            return model;
        }

        public async Task<RankMapModel> SaveRankMap(int id, RankMapModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("RankMap data missing");
            }

            bool isExist = rankMapRepository.Exists(x => x.AirForceRankId == model.AirForceRankId && x.ArmyRankId==model.ArmyRankId && x.NavyRankId==model.NavyRankId && x.RankMapId != model.RankMapId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            RankMap rankMap = ObjectConverter<RankMapModel, RankMap>.Convert(model);
            if (id > 0)
            {
                rankMap = await rankMapRepository.FindOneAsync(x => x.RankMapId == id);
                if (rankMap == null)
                {
                    throw new InfinityNotFoundException("RankMap not found !");
                }

                rankMap.ModifiedDate = DateTime.Now;
                rankMap.ModifiedBy = userId;
            }
            else
            {

                rankMap.CreatedDate = DateTime.Now;
                rankMap.CreatedBy = userId;
            }
            rankMap.AirForceRankId = model.AirForceRankId;
            rankMap.ArmyRankId = model.ArmyRankId;
            rankMap.NavyRankId = model.NavyRankId;
       
            rankMap.Remarks = model.Remarks;
            await rankMapRepository.SaveAsync(rankMap);
            return model;
        }

        public async Task<bool> DeleteRankMap(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            RankMap rankMap = await rankMapRepository.FindOneAsync(x => x.RankMapId == id);
            if (rankMap == null)
            {
                throw new InfinityNotFoundException("RankMap not found");
            }
            else
            {
                return await rankMapRepository.DeleteAsync(rankMap);
            }
        }

        public List<RankMapModel> GetRankMaps(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
