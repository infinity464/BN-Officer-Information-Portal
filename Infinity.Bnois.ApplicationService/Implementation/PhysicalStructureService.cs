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
   public class PhysicalStructureService: IPhysicalStructureService
    {
        private readonly IBnoisRepository<PhysicalStructure> physicalStructureRepository;
        public PhysicalStructureService(IBnoisRepository<PhysicalStructure> physicalStructureRepository)
        {
            this.physicalStructureRepository = physicalStructureRepository;
        }
        
        public List<PhysicalStructureModel> GetPhysicalStructures(int ps, int pn, string qs, out int total)
        {
            IQueryable<PhysicalStructure> physicalStructures = physicalStructureRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = physicalStructures.Count();
            physicalStructures = physicalStructures.OrderByDescending(x => x.PhysicalStructureId).Skip((pn - 1) * ps).Take(ps);
            List<PhysicalStructureModel> models = ObjectConverter<PhysicalStructure, PhysicalStructureModel>.ConvertList(physicalStructures.ToList()).ToList();
            return models;
        }

        public async Task<PhysicalStructureModel> GetPhysicalStructure(int id)
        {
            if (id <= 0)
            {
                return new PhysicalStructureModel();
            }
            PhysicalStructure physicalStructure = await physicalStructureRepository.FindOneAsync(x => x.PhysicalStructureId == id);
            if (physicalStructure == null)
            {
                throw new InfinityNotFoundException("PhysicalStructure not found");
            }
            PhysicalStructureModel model = ObjectConverter<PhysicalStructure, PhysicalStructureModel>.Convert(physicalStructure);
            return model;
        }

        public async Task<PhysicalStructureModel> SavePhysicalStructure(int id, PhysicalStructureModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("PhysicalStructure data missing");
            }
            bool isExist = physicalStructureRepository.Exists(x => x.Name == model.Name && x.PhysicalStructureId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            PhysicalStructure physicalStructure = ObjectConverter<PhysicalStructureModel, PhysicalStructure>.Convert(model);
            if (id > 0)
            {
                physicalStructure = await physicalStructureRepository.FindOneAsync(x => x.PhysicalStructureId == id);
                if (physicalStructure == null)
                {
                    throw new InfinityNotFoundException("PhysicalStructure not found !");
                }
                physicalStructure.ModifiedDate = DateTime.Now;
                physicalStructure.ModifiedBy = userId;
            }
            else
            {
                physicalStructure.IsActive = true;
                physicalStructure.CreatedDate = DateTime.Now;
                physicalStructure.CreatedBy = userId;
            }
            physicalStructure.Name = model.Name;
            physicalStructure.Remarks = model.Remarks;
     
            physicalStructure.Remarks = model.Remarks;
            await physicalStructureRepository.SaveAsync(physicalStructure);
            model.PhysicalStructureId = physicalStructure.PhysicalStructureId;
            return model;
        }

        public async Task<bool> DeletePhysicalStructure(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PhysicalStructure physicalStructure = await physicalStructureRepository.FindOneAsync(x => x.PhysicalStructureId == id);
            if (physicalStructure == null)
            {
                throw new InfinityNotFoundException("PhysicalStructure not found");
            }
            else
            {
                return await physicalStructureRepository.DeleteAsync(physicalStructure);
            }
        }

        public async Task<List<SelectModel>> GetPhysicalStructureSelectModels()
        {
            ICollection<PhysicalStructure> physicalStructures = await physicalStructureRepository.FilterAsync(x => x.IsActive);
            return physicalStructures.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.PhysicalStructureId
            }).ToList();
        }
    }
}
