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
    public class SiblingService : ISiblingService
    {

        private readonly IBnoisRepository<Sibling> siblingRepository;
        public SiblingService(IBnoisRepository<Sibling> siblingRepository)
        {
            this.siblingRepository = siblingRepository;
        }


        public List<SiblingModel> GetSiblings(int employeeId)
        {
            List<Sibling> educations = siblingRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "Occupation").ToList();
            List<SiblingModel> models = ObjectConverter<Sibling, SiblingModel>.ConvertList(educations.ToList()).ToList();
            models = models.Select(x =>
            {
                x.SiblingTypeName = Enum.GetName(typeof(SiblingType), x.SiblingType);
                return x;
            }).ToList();

            return models;
        }

        public async Task<SiblingModel> GetSibling(int siblingId)
        {
            if (siblingId <= 0)
            {
                return new SiblingModel();
            }
            Sibling sibling = await siblingRepository.FindOneAsync(x => x.SiblingId == siblingId, new List<string> { "Employee" });
            if (sibling == null)
            {
                throw new InfinityNotFoundException(" Sibling not found");
            }
            SiblingModel model = ObjectConverter<Sibling, SiblingModel>.Convert(sibling);
            return model;
        }

        public async Task<SiblingModel> SaveSibling(int siblingId, SiblingModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Sibling data missing");
            }

            bool isExistData = siblingRepository.Exists(x => x.OccupationId == model.OccupationId && x.EmployeeId != model.EmployeeId && x.Name == model.Name && x.SiblingType==model.SiblingType && x.SiblingId != siblingId);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Sibling sibling = ObjectConverter<SiblingModel, Sibling>.Convert(model);
            if (siblingId > 0)
            {
                sibling = await siblingRepository.FindOneAsync(x => x.SiblingId == siblingId);
                if (sibling == null)
                {
                    throw new InfinityNotFoundException("Sibling data not found !");
                }

                sibling.ModifiedDate = DateTime.Now;
                sibling.ModifiedBy = userId;
            }
            else
            {
                sibling.IsActive = true;
                sibling.CreatedDate = DateTime.Now;
                sibling.CreatedBy = userId;
            }

            sibling.EmployeeId = model.EmployeeId;
            sibling.OccupationId = model.OccupationId;
            sibling.Name = model.Name;
            sibling.SpouseName = model.SpouseName;
            sibling.DateOfBirth=model.DateOfBirth;
           // sibling.Age=DateTime.Today.Year-model.DateOfBirth.Year;
            sibling.SiblingType = model.SiblingType;

            await siblingRepository.SaveAsync(sibling);
            model.SiblingId = sibling.SiblingId;

            return model;
        }


        public async Task<bool> DeleteSibling(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Sibling sibling = await siblingRepository.FindOneAsync(x => x.SiblingId == id);
            if (sibling == null)
            {
                throw new InfinityNotFoundException("Sibling not found");
            }
            else
            {
                return await siblingRepository.DeleteAsync(sibling);
            }
        }


        public List<SelectModel> GetSiblingTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(SiblingType)).Cast<SiblingType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public async Task<SiblingModel> UpdateSibling(SiblingModel model)
        {
                if (model == null)
                {
                    throw new InfinityArgumentMissingException("Sibling data missing");
                }

                string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                Sibling Sibling = ObjectConverter<SiblingModel, Sibling>.Convert(model);

                Sibling = await siblingRepository.FindOneAsync(x => x.SiblingId == model.SiblingId);
                if (Sibling == null)
                {
                    throw new InfinityNotFoundException("Sibling not found !");
                }

                if (model.FileName != null)
                {
                    Sibling.FileName = model.FileName;
                }

                Sibling.ModifiedDate = DateTime.Now;
                Sibling.ModifiedBy = userId;
                await siblingRepository.SaveAsync(Sibling);
                model.SiblingId = Sibling.SiblingId;
                return model;
           
        }
    }
}