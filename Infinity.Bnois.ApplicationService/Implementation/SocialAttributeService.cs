using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class SocialAttributeService : ISocialAttributeService
    {
        private readonly IBnoisRepository<SocialAttribute> socialAttributeRepository;
        public SocialAttributeService(IBnoisRepository<SocialAttribute> socialAttributeRepository)
        {
            this.socialAttributeRepository = socialAttributeRepository;
        }

        public async Task<SocialAttributeModel> GetSocialAttribute(int employeeId)
        {
            if (employeeId <= 0)
            {
                return new SocialAttributeModel();
            }
            SocialAttribute socialAttribute = await socialAttributeRepository.FindOneAsync(x => x.EmployeeId == employeeId);
            if (socialAttribute == null)
            {
                return new SocialAttributeModel();
            }
            SocialAttributeModel model = ObjectConverter<SocialAttribute, SocialAttributeModel>.Convert(socialAttribute);
            return model;
        }

        public async Task<SocialAttributeModel> SaveSocialAttribute(int employeeId, SocialAttributeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Social Attribute data missing!");
            }

            SocialAttribute socialAttribute = ObjectConverter<SocialAttributeModel, SocialAttribute>.Convert(model);

            if (socialAttribute.SocialAttributeId > 0)
            {
                socialAttribute = await socialAttributeRepository.FindOneAsync(x => x.EmployeeId == employeeId);
                if (socialAttribute == null)
                {
                    throw new InfinityNotFoundException("Social Attribute data not found!");
                }
                socialAttribute.ModifiedDate = DateTime.Now;
                socialAttribute.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                socialAttribute.EmployeeId = employeeId;
                socialAttribute.CreatedBy = model.CreatedBy;
                socialAttribute.CreatedDate = DateTime.Now;
                socialAttribute.IsActive = true;
            }

            socialAttribute.IsSocialAttribute = model.IsSocialAttribute;
            socialAttribute.SARemarks = model.SARemarks;
            socialAttribute.IsCirculationValue = model.IsCirculationValue;
            socialAttribute.CVRemarks = model.CVRemarks;
            socialAttribute.IsPersonalityPerChar = model.IsPersonalityPerChar;
            socialAttribute.PPCRemarks = model.PPCRemarks;
            socialAttribute.Hobby = model.Hobby;
            await socialAttributeRepository.SaveAsync(socialAttribute);
            model.SocialAttributeId = socialAttribute.SocialAttributeId;
            return model;
        }
    }
}
