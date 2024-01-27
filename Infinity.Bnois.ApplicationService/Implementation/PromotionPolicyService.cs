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
    public class PromotionPolicyService : IPromotionPolicyService
    {
        private readonly IBnoisRepository<PromotionPolicy> promotionPolicyRepository;
        public PromotionPolicyService(IBnoisRepository<PromotionPolicy> promotionPolicyRepository)
        {
            this.promotionPolicyRepository = promotionPolicyRepository;
        }
        

        public List<PromotionPolicyModel> GetPromotionPolicies()
        {
            IQueryable<PromotionPolicy> promotionPolicies = promotionPolicyRepository.FilterWithInclude(x => x.IsActive,"Rank","Rank1");
            promotionPolicies = promotionPolicies.OrderByDescending(x => x.PromotionPolicyId);
            List<PromotionPolicyModel> models = ObjectConverter<PromotionPolicy, PromotionPolicyModel>.ConvertList(promotionPolicies.ToList()).ToList();
            return models;
        }

        public async Task<PromotionPolicyModel> GetPromotionPolicy(int id)
        {
            if (id <= 0)
            {
                return new PromotionPolicyModel();
            }
            PromotionPolicy promotionPolicy = await promotionPolicyRepository.FindOneAsync(x => x.PromotionPolicyId == id);
            if (promotionPolicy == null)
            {
                throw new InfinityNotFoundException("Promotion Policy not found");
            }
            PromotionPolicyModel model = ObjectConverter<PromotionPolicy, PromotionPolicyModel>.Convert(promotionPolicy);
            return model;
        }

        public async Task<PromotionPolicyModel> SavePromotionPolicy(int id, PromotionPolicyModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Promotion Policy data missing");
            }
            bool isExist = promotionPolicyRepository.Exists(x => x.RankFromId == model.RankFromId && x.RankToId == model.RankToId && x.PromotionPolicyId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            PromotionPolicy promotionPolicy = ObjectConverter<PromotionPolicyModel, PromotionPolicy>.Convert(model);
            if (id > 0)
            {
                promotionPolicy = await promotionPolicyRepository.FindOneAsync(x => x.PromotionPolicyId == id);
                if (promotionPolicy == null)
                {
                    throw new InfinityNotFoundException("Promotion Policy not found !");
                }

                promotionPolicy.ModifiedDate = DateTime.Now;
                promotionPolicy.ModifiedBy = userId;
            }
            else
            {
                promotionPolicy.IsActive = true;
                promotionPolicy.CreatedDate = DateTime.Now;
                promotionPolicy.CreatedBy = userId;
            }
            promotionPolicy.RankFromId = model.RankFromId;
            promotionPolicy.RankToId = model.RankToId;
            promotionPolicy.ServiceYear = model.ServiceYear;
            promotionPolicy.IsOneYearPreRank = model.IsOneYearPreRank;
            promotionPolicy.IsOprRecom = model.IsOprRecom;
            promotionPolicy.IsPassLfCdrQExam = model.IsPassLfCdrQExam;
            promotionPolicy.IsSpcialCourse = model.IsSpcialCourse;

            await promotionPolicyRepository.SaveAsync(promotionPolicy);
            model.PromotionPolicyId = promotionPolicy.PromotionPolicyId;
            return model;
        }

        public async Task<bool> DeletePromotionPolicy(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PromotionPolicy promotionPolicy = await promotionPolicyRepository.FindOneAsync(x => x.PromotionPolicyId == id);
            if (promotionPolicy == null)
            {
                throw new InfinityNotFoundException("Promotion Policy not found");
            }
            else
            {
                return await promotionPolicyRepository.DeleteAsync(promotionPolicy);
            }
        }
    }
}
