using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.Configuration;

namespace Infinity.Bnois.ApplicationService.Implementation
{
  public  class PhysicalConditionService : IPhysicalConditionService
    {
        private readonly IBnoisRepository<PhysicalCondition> physicalConditionRepository;
        public PhysicalConditionService(IBnoisRepository<PhysicalCondition> physicalConditionRepository)
        {
            this.physicalConditionRepository = physicalConditionRepository;
        }

        public async Task<PhysicalConditionModel> GetPhysicalConditions(int employeeId)
        {
            if (employeeId <= 0)

            {
                return new PhysicalConditionModel();
            }
            PhysicalCondition physicalCondition = await physicalConditionRepository.FindOneAsync(x => x.EmployeeId == employeeId, new List<string> { "Color", "Color1", "Color2", "EyeVision", "BloodGroup", "PhysicalStructure", "MedicalCategory" });

            if (physicalCondition == null)
            {
                return new PhysicalConditionModel();
            }
            PhysicalConditionModel model = ObjectConverter<PhysicalCondition, PhysicalConditionModel>.Convert(physicalCondition);
            return model;
        }

        public async Task<PhysicalConditionModel> SavePhysicalCondition(int employeeId, PhysicalConditionModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Physical Condition data missing!");
            }

            PhysicalCondition physicalCondition = ObjectConverter<PhysicalConditionModel, PhysicalCondition>.Convert(model);

            if (physicalCondition.PhysicalConditionId > 0)
            {
                physicalCondition = await physicalConditionRepository.FindOneAsync(x => x.EmployeeId == employeeId);
                if (physicalCondition == null)
                {
                    throw new InfinityNotFoundException("Physical Condition data not found!");
                }
                physicalCondition.ModifiedDate = DateTime.Now;
                physicalCondition.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                physicalCondition.EmployeeId = employeeId;
                physicalCondition.CreatedBy = model.CreatedBy;
                physicalCondition.CreatedDate = DateTime.Now;
                physicalCondition.IsActive = true;
            }



            physicalCondition.EyeColorId = model.EyeColorId;
            physicalCondition.SkinColorId = model.SkinColorId;
            physicalCondition.HairColorId = model.HairColorId;
            physicalCondition.EyeVisionId = model.EyeVisionId;
            physicalCondition.EmployeeId = employeeId;
            physicalCondition.BloodGroupId = model.BloodGroupId;
            physicalCondition.MedicalCategoryId = model.MedicalCategoryId;
            physicalCondition.IsPerMedicalCategory = model.IsPerMedicalCategory;
            physicalCondition.HeightInFeet = model.HeightInFeet;
            physicalCondition.HeightInInc = model.HeightInInc;
            physicalCondition.Weight = model.Weight;
            physicalCondition.IdentificationMark = model.IdentificationMark;
            physicalCondition.Remarks = model.Remarks;
            physicalCondition.HeightInCM = ((model.HeightInFeet*12)+model.HeightInInc)* 2.54;
            await physicalConditionRepository.SaveAsync(physicalCondition);
            model.PhysicalConditionId = physicalCondition.PhysicalConditionId;
            return model;
        }



        public async Task<PhysicalConditionModel> UpdatePhysicalCondition(PhysicalConditionModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Physical Condition data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            PhysicalCondition physicalCondition = ObjectConverter<PhysicalConditionModel, PhysicalCondition>.Convert(model);

            physicalCondition = await physicalConditionRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
            if (physicalCondition == null)
            {
                throw new InfinityNotFoundException("Physical Condition not found !");
            }

            if (model.FileName != null)
            {
                physicalCondition.FileName = model.FileName;
            }


            physicalCondition.ModifiedDate = DateTime.Now;
            physicalCondition.ModifiedBy = userId;
            await physicalConditionRepository.SaveAsync(physicalCondition);
            model.PhysicalConditionId = physicalCondition.PhysicalConditionId;
            return model;
        }
    }
}
