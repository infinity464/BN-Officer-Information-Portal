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
    public class AgeServicePolicyService :IAgeServicePolicyService
    {
        private readonly IBnoisRepository<AgeServicePolicy> ageServicePolicyRepository;
        public AgeServicePolicyService(IBnoisRepository<AgeServicePolicy> ageServicePolicyRepository)
        {
            this.ageServicePolicyRepository = ageServicePolicyRepository;
        }

        //public List<AgeServicePolicyModel> GetAgeServicePolicies(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<AgeServicePolicy> ageServicePolicies = ageServicePolicyRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.Category.Name.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.SubCategory.Name.Contains(qs) || String.IsNullOrEmpty(qs))) || (x.Rank.FullName.Contains(qs) || String.IsNullOrEmpty(qs)), "Category","SubCategory", "Rank");
        //    total = ageServicePolicies.Count();
        //    ageServicePolicies = ageServicePolicies.OrderByDescending(x => x.AgeServiceId).Skip((pn - 1) * ps).Take(ps);
        //    List<AgeServicePolicyModel> models = ObjectConverter<AgeServicePolicy, AgeServicePolicyModel>.ConvertList(ageServicePolicies.ToList()).ToList();
           
        //    models = models.Select(x =>
        //    {
        //        x.EarlyStatusName = Enum.GetName(typeof(EarlyStatus), x.EarlyStatus);
        //        return x;
        //    }).ToList();
        //    return models;
        //}

        public async Task<AgeServicePolicyModel> GetAgeServicePolicy(int id)
        {
            if (id <= 0)
            {
                return new AgeServicePolicyModel();
            }
            AgeServicePolicy ageServicePolicy = await ageServicePolicyRepository.FindOneAsync(x => x.AgeServiceId == id);
            if (ageServicePolicy == null)
            {
                throw new InfinityNotFoundException("Age Service Policy not found");
            }
            AgeServicePolicyModel model = ObjectConverter<AgeServicePolicy, AgeServicePolicyModel>.Convert(ageServicePolicy);
            return model;
        }


        public async Task<AgeServicePolicyModel> SaveAgeServicePolicy(int id, AgeServicePolicyModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Age Service Policy data missing");
            }


            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            AgeServicePolicy ageServicePolicy = ObjectConverter<AgeServicePolicyModel, AgeServicePolicy>.Convert(model);
            if (id > 0)
            {
                ageServicePolicy = await ageServicePolicyRepository.FindOneAsync(x => x.AgeServiceId == id);
                if (ageServicePolicy == null)
                {
                    throw new InfinityNotFoundException("Age Service Policy not found !");
                }
                ageServicePolicy.ModifiedDate = DateTime.Now;
                ageServicePolicy.ModifiedBy = userId;
            }
            else
            {
                ageServicePolicy.IsActive = true;
                ageServicePolicy.CreatedDate = DateTime.Now;
                ageServicePolicy.CreatedBy = userId;
            }
            ageServicePolicy.CategoryId = model.CategoryId;
            ageServicePolicy.SubCategoryId = model.SubCategoryId;
            ageServicePolicy.RankId = model.RankId;
            ageServicePolicy.ServiceLimit = model.ServiceLimit;
            ageServicePolicy.AgeLimit = model.AgeLimit;
            ageServicePolicy.EarlyStatus = model.EarlyStatus;

            await ageServicePolicyRepository.SaveAsync(ageServicePolicy);
            model.AgeServiceId = ageServicePolicy.AgeServiceId;
            return model;
        }


        public async Task<bool> DeleteAgeServicePolicy(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            AgeServicePolicy ageServicePolicy = await ageServicePolicyRepository.FindOneAsync(x => x.AgeServiceId == id);
            if (ageServicePolicy == null)
            {
                throw new InfinityNotFoundException("Age Service Policy not found");
            }
            else
            {
                return await ageServicePolicyRepository.DeleteAsync(ageServicePolicy);
            }
        }



        public List<SelectModel> GetEarlyStatusSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(EarlyStatus)).Cast<EarlyStatus>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

	    public async Task<DateTime?> GetLprServiceDate(EmployeeGeneralModel employee)
	    {
		    AgeServicePolicy policy = await ageServicePolicyRepository.FindOneAsync(x =>
			    x.CategoryId == employee.CategoryId && x.SubCategoryId == employee.SubCategoryId &&
			    x.RankId == employee.Employee.RankId);
		    if (policy != null)
		    {
			    DateTime ageLimit, serviceLimit;
			    serviceLimit = Convert.ToDateTime(employee.CommissionDate).AddYears(policy.ServiceLimit);
			    ageLimit = Convert.ToDateTime(employee.DoB).AddYears(policy.AgeLimit);

			    return serviceLimit > ageLimit ? serviceLimit : ageLimit;
			}
		    return null;
	    }

        public List<AgeServicePolicyModel> GetAgeServicePolicies(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}