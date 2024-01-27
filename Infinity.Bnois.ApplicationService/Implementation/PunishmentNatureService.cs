using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   

    public class PunishmentNatureService : IPunishmentNatureService
    {
        private readonly IBnoisRepository<PunishmentNature> punishmentNatureRepository;
        public PunishmentNatureService(IBnoisRepository<PunishmentNature> punishmentNatureRepository)
        {
            this.punishmentNatureRepository = punishmentNatureRepository;
        }

        public List<PunishmentNatureModel> GetPunishmentNatures(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<PunishmentNature> punishmentNatures = punishmentNatureRepository
                .FilterWithInclude(x => x.IsActive
                && ((x.Name.Contains(searchText)) || String.IsNullOrEmpty(searchText)));
            total = punishmentNatures.Count();
            punishmentNatures = punishmentNatures.OrderByDescending(x => x.PunishmentNatureId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            List<PunishmentNatureModel> models = ObjectConverter<PunishmentNature, PunishmentNatureModel>.ConvertList(punishmentNatures.ToList()).ToList();
            return models;
        }

        public async Task<PunishmentNatureModel> GetPunishmentNature(int  id)
        {
            if (id <= 0)
            {
                return new PunishmentNatureModel();
            }
            PunishmentNature punishmentNature = await punishmentNatureRepository.FindOneAsync(x => x.PunishmentNatureId == id);

            if (punishmentNature == null)
            {
                throw new InfinityNotFoundException("PunishmentNature not found!");
            }
            PunishmentNatureModel model = ObjectConverter<PunishmentNature, PunishmentNatureModel>.Convert(punishmentNature);
            return model;
        }

        public async Task<PunishmentNatureModel> SavePunishmentNature(int id, PunishmentNatureModel model)
        {
            bool isExist = await punishmentNatureRepository.ExistsAsync(x => (x.Name == model.Name) && x.PunishmentNatureId != model.PunishmentNatureId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("PunishmentNature data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("PunishmentNature data missing!");
            }

            PunishmentNature punishmentNature = ObjectConverter<PunishmentNatureModel, PunishmentNature>.Convert(model);

            if (id > 0)
            {
                punishmentNature = await punishmentNatureRepository.FindOneAsync(x => x.PunishmentNatureId == id);
                if (punishmentNature == null)
                {
                    throw new InfinityNotFoundException("PunishmentNature not found!");
                }
                punishmentNature.ModifiedDate = DateTime.Now;
                punishmentNature.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                punishmentNature.CreatedBy = model.CreatedBy;
                punishmentNature.CreatedDate = DateTime.Now;
                punishmentNature.IsActive = true;
            }
            punishmentNature.Name = model.Name;
            punishmentNature.ShortName = model.ShortName;
            punishmentNature.Priority = model.Priority;

            await punishmentNatureRepository.SaveAsync(punishmentNature);
            model.PunishmentNatureId = punishmentNature.PunishmentNatureId;
            return model;
        }

        public async Task<bool> DeletePunishmentNature(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PunishmentNature punishmentNature = await punishmentNatureRepository.FindOneAsync(x => x.PunishmentNatureId == id);
            if (punishmentNature == null)
            {
                throw new InfinityNotFoundException("Punishment Nature not found!");
            }
            else
            {
                return await punishmentNatureRepository.DeleteAsync(punishmentNature);
            }
        }

        public async Task<List<SelectModel>> GetPunishmentNatureSelectModels()
        {
            ICollection<PunishmentNature> models = await punishmentNatureRepository.FilterAsync(x => x.IsActive);
            return models.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.PunishmentNatureId
            }).ToList();
        }
   
    }
}