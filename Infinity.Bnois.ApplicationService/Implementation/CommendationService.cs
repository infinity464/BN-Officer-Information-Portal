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
   

    public class CommendationService : ICommendationService
    {
        private readonly IBnoisRepository<Commendation> commendationRepository;
        public CommendationService(IBnoisRepository<Commendation> commendationRepository)
        {
            this.commendationRepository = commendationRepository;
        }

        public List<CommendationModel> GetCommendations(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<Commendation> commendations = commendationRepository
                .FilterWithInclude(x => x.IsActive
                && ((x.Name.Contains(searchText)) || String.IsNullOrEmpty(searchText)));
            total = commendations.Count();
            commendations = commendations.OrderByDescending(x => x.CommendationId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            List<CommendationModel> models = ObjectConverter<Commendation, CommendationModel>.ConvertList(commendations.ToList()).ToList();

            models = models.Select(x =>
            {
                x.TypeName = Enum.GetName(typeof(CommendationType), x.Type);
                return x;
            }).ToList();
            return models;
        }

        public async Task<CommendationModel> GetCommendation(int  id)
        {
            if (id <= 0)
            {
                return new CommendationModel();
            }
            Commendation commendation = await commendationRepository.FindOneAsync(x => x.CommendationId == id);

            if (commendation == null)
            {
                throw new InfinityNotFoundException("Commendation not found!");
            }
            CommendationModel model = ObjectConverter<Commendation, CommendationModel>.Convert(commendation);
            return model;
        }

        public async Task<CommendationModel> SaveCommendation(int id, CommendationModel model)
        {
            bool isExist = await commendationRepository.ExistsAsync(x => (x.Name == model.Name) && x.CommendationId != model.CommendationId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Commendation data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Commendation data missing!");
            }

            Commendation commendation = ObjectConverter<CommendationModel, Commendation>.Convert(model);

            if (id > 0)
            {
                commendation = await commendationRepository.FindOneAsync(x => x.CommendationId == id);
                if (commendation == null)
                {
                    throw new InfinityNotFoundException("Commendation not found!");
                }
                commendation.ModifiedDate = DateTime.Now;
                commendation.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                commendation.CreatedBy = model.CreatedBy;
                commendation.CreatedDate = DateTime.Now;
                commendation.IsActive = true;
            }
            commendation.Name = model.Name;
            commendation.ShortName = model.ShortName;
	        commendation.Type = model.Type;

            await commendationRepository.SaveAsync(commendation);
            model.CommendationId = commendation.CommendationId;
            return model;
        }

        public async Task<bool> DeleteCommendation(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Commendation commendation = await commendationRepository.FindOneAsync(x => x.CommendationId == id);
            if (commendation == null)
            {
                throw new InfinityNotFoundException("Commendation not found!");
            }
            else
            {
                return await commendationRepository.DeleteAsync(commendation);
            }
        }

        public async Task<List<SelectModel>> GetCommendationSelectModels()
        {
            ICollection<Commendation> models = await commendationRepository.FilterAsync(x => x.IsActive && x.Type==(int)CommendationType.Commendation);
            return models.Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.CommendationId
            }).ToList();
        }

        public async Task<List<SelectModel>> GetAppreciationSelectModels()
        {
            ICollection<Commendation> models = await commendationRepository.FilterAsync(x => x.IsActive && x.Type == (int)CommendationType.Appreciation);
            return models.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.CommendationId
            }).ToList();
        }

        public List<SelectModel> GetCommendationTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(CommendationType)).Cast<CommendationType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public async Task<List<SelectModel>> GetCommendationAppreciationSelectModels()
        {
            ICollection<Commendation> models = await commendationRepository.FilterAsync(x => x.IsActive);
            return models.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.CommendationId
            }).ToList();
        }
    }
}