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
    public class EyeVisionService : IEyeVisionService
    {
        private readonly IBnoisRepository<EyeVision> eyeVisionRepository;
        public EyeVisionService(IBnoisRepository<EyeVision> eyeVisionRepository)
        {
            this.eyeVisionRepository = eyeVisionRepository;
        }

        public List<EyeVisionModel> GetEyeVisions(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<EyeVision> eyeVisions = eyeVisionRepository
                .FilterWithInclude(x => x.IsActive
                && ((x.Name.Contains(searchText)) || String.IsNullOrEmpty(searchText)));
            total = eyeVisions.Count();
            eyeVisions = eyeVisions.OrderByDescending(x => x.EyeVisionId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            List<EyeVisionModel> models = ObjectConverter<EyeVision, EyeVisionModel>.ConvertList(eyeVisions.ToList()).ToList();
            return models;
        }

        public async Task<EyeVisionModel> GetEyeVision(int eyeVisionId)
        {
            if (eyeVisionId <= 0)
            {
                return new EyeVisionModel();
            }
            EyeVision eyeVision = await eyeVisionRepository.FindOneAsync(x => x.EyeVisionId == eyeVisionId);

            if (eyeVision == null)
            {
                throw new InfinityNotFoundException("Eye Vision not found!");
            }
            EyeVisionModel model = ObjectConverter<EyeVision, EyeVisionModel>.Convert(eyeVision);
            return model;
        }

        public async Task<EyeVisionModel> SaveEyeVision(int eyeVisionId, EyeVisionModel model)
        {
            bool isExist = await eyeVisionRepository.ExistsAsync(x => (x.Name == model.Name) && x.EyeVisionId != model.EyeVisionId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Eye Vision data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Eye Vision data missing!");
            }

            EyeVision eyeVision = ObjectConverter<EyeVisionModel, EyeVision>.Convert(model);

            if (eyeVisionId > 0)
            {
                eyeVision = await eyeVisionRepository.FindOneAsync(x => x.EyeVisionId == eyeVisionId);
                if (eyeVision == null)
                {
                    throw new InfinityNotFoundException("Eye Vision not found!");
                }
                eyeVision.ModifiedDate = DateTime.Now;
                eyeVision.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                eyeVision.CreatedBy = model.CreatedBy;
                eyeVision.CreatedDate = DateTime.Now;
                eyeVision.IsActive = true;
            }
            eyeVision.Name = model.Name;
            eyeVision.Remarks = model.Remarks;
            await eyeVisionRepository.SaveAsync(eyeVision);
            model.EyeVisionId = eyeVision.EyeVisionId;
            return model;
        }

        public async Task<bool> DeleteEyeVision(int eyeVisionId)
        {
            if (eyeVisionId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EyeVision eyeVision = await eyeVisionRepository.FindOneAsync(x => x.EyeVisionId == eyeVisionId);
            if (eyeVision == null)
            {
                throw new InfinityNotFoundException("Eye Vision not found!");
            }
            else
            {
                return await eyeVisionRepository.DeleteAsync(eyeVision);
            }
        }

        public async Task<List<SelectModel>> GetEyeVisionSelectModels()
        {
            ICollection<EyeVision> models = await eyeVisionRepository.FilterAsync(x => x.IsActive);
            return models.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.EyeVisionId
            }).ToList();
        }

    }
}