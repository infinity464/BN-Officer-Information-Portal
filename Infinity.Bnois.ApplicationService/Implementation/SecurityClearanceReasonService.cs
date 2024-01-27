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
   public class SecurityClearanceReasonService : ISecurityClearanceReasonService
    {

        private readonly IBnoisRepository<SecurityClearanceReason> securityClearanceReasonRepository;
        public SecurityClearanceReasonService(IBnoisRepository<SecurityClearanceReason> securityClearanceReasonRepository)
        {
            this.securityClearanceReasonRepository = securityClearanceReasonRepository;
        }


        public List<SecurityClearanceReasonModel> GetSecurityClearanceReasons(int ps, int pn, string qs, out int total)
        {
            IQueryable<SecurityClearanceReason> securityClearanceReasons = securityClearanceReasonRepository.FilterWithInclude(x => x.IsActive && (x.Reason.Contains(qs) || String.IsNullOrEmpty(qs) ));
            total = securityClearanceReasons.Count();
            securityClearanceReasons = securityClearanceReasons.OrderByDescending(x => x.SecurityClearanceReasonId).Skip((pn - 1) * ps).Take(ps);
            List<SecurityClearanceReasonModel> models = ObjectConverter<SecurityClearanceReason, SecurityClearanceReasonModel>.ConvertList(securityClearanceReasons.ToList()).ToList();
            return models;
        }

        public async Task<SecurityClearanceReasonModel> GetSecurityClearanceReason(int id)
        {
            if (id <= 0)
            {
                return new SecurityClearanceReasonModel();
            }
            SecurityClearanceReason securityClearanceReason = await securityClearanceReasonRepository.FindOneAsync(x => x.SecurityClearanceReasonId == id);
            if (securityClearanceReason == null)
            {
                throw new InfinityNotFoundException("Security Clearance Reason not found");
            }
            SecurityClearanceReasonModel model = ObjectConverter<SecurityClearanceReason, SecurityClearanceReasonModel>.Convert(securityClearanceReason);
            return model;
        }

        public async Task<SecurityClearanceReasonModel> SaveSecurityClearanceReason(int id, SecurityClearanceReasonModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Security Clearanc eReason data missing");
            }
            bool isExist = securityClearanceReasonRepository.Exists(x => x.Reason == model.Reason  && x.SecurityClearanceReasonId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            SecurityClearanceReason securityClearanceReason = ObjectConverter<SecurityClearanceReasonModel, SecurityClearanceReason>.Convert(model);
            if (id > 0)
            {
                securityClearanceReason = await securityClearanceReasonRepository.FindOneAsync(x => x.SecurityClearanceReasonId == id);
                if (securityClearanceReason == null)
                {
                    throw new InfinityNotFoundException("Security Clearance Reason not found !");
                }

                securityClearanceReason.ModifiedDate = DateTime.Now;
                securityClearanceReason.ModifiedBy = userId;
            }
            else
            {
                securityClearanceReason.IsActive = true;
                securityClearanceReason.CreatedDate = DateTime.Now;
                securityClearanceReason.CreatedBy = userId;
            }
            securityClearanceReason.Reason = model.Reason;
            securityClearanceReason.Remarks = model.Remarks;
     
            
            await securityClearanceReasonRepository.SaveAsync(securityClearanceReason);
            model.SecurityClearanceReasonId = securityClearanceReason.SecurityClearanceReasonId;
            return model;
        }

        public async Task<bool> DeleteSecurityClearanceReason(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            SecurityClearanceReason securityClearanceReason = await securityClearanceReasonRepository.FindOneAsync(x => x.SecurityClearanceReasonId == id);
            if (securityClearanceReason == null)
            {
                throw new InfinityNotFoundException("Security Clearance Reason not found");
            }
            else
            {
                return await securityClearanceReasonRepository.DeleteAsync(securityClearanceReason);
            }
        }

        public async Task<List<SelectModel>> GetSecurityClearanceReasonSelectModels()
        {
            ICollection<SecurityClearanceReason> models = await securityClearanceReasonRepository.FilterAsync(x => x.IsActive);
            return models.OrderBy(x => x.Reason).Select(x => new SelectModel()
            {
                Text = x.Reason,
                Value = x.SecurityClearanceReasonId
            }).ToList();

        }




    }
}
