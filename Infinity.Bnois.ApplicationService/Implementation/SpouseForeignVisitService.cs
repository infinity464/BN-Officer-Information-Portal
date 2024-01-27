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
    public class SpouseForeignVisitService : ISpouseForeignVisitService
    {
        private readonly IBnoisRepository<SpouseForeignVisit> spouseForignVisitRepository;
        public SpouseForeignVisitService(IBnoisRepository<SpouseForeignVisit> spouseForignVisitRepository)
        {
            this.spouseForignVisitRepository = spouseForignVisitRepository;
        }
        public List<SpouseForeignVisitModel> GetSpouseForeignVisits(int spouseId)
        {
            ICollection<SpouseForeignVisit> spouseForeignVisits = spouseForignVisitRepository.FilterWithInclude(x => x.IsActive && x.SpouseId==spouseId,"Spouse","Country").ToList();
            List<SpouseForeignVisitModel> models = ObjectConverter<SpouseForeignVisit, SpouseForeignVisitModel>.ConvertList(spouseForeignVisits).ToList();
            return models;
        }
        public async Task<SpouseForeignVisitModel> GetSpouseForeignVisit(int spouseForeignVisitId)
        {
            if (spouseForeignVisitId <= 0)
            {
                return new SpouseForeignVisitModel();
            }
            SpouseForeignVisit spouseForeignVisit = await spouseForignVisitRepository.FindOneAsync(x => x.SpouseForeignVisitId == spouseForeignVisitId);
            if (spouseForeignVisit == null)
            {
                throw new InfinityNotFoundException("Spouse Forign Visit not found");
            }
            SpouseForeignVisitModel model = ObjectConverter<SpouseForeignVisit, SpouseForeignVisitModel>.Convert(spouseForeignVisit);
            return model;
        }

        public async Task<SpouseForeignVisitModel> SaveSpouseForeignVisit(int spouseForeignVisitId, SpouseForeignVisitModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Division data missing");
            }
            
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            SpouseForeignVisit spouseForeignVisit = ObjectConverter<SpouseForeignVisitModel, SpouseForeignVisit>.Convert(model);
            if (spouseForeignVisitId > 0)
            {
                spouseForeignVisit = await spouseForignVisitRepository.FindOneAsync(x => x.SpouseForeignVisitId == spouseForeignVisitId);
                if (spouseForeignVisit == null)
                {
                    throw new InfinityNotFoundException("Spouse Foreign Visit not found !");
                }

                spouseForeignVisit.ModifiedDate = DateTime.Now;
                spouseForeignVisit.ModifiedBy = userId;
            }
            else
            {
                spouseForeignVisit.IsActive = true;
                spouseForeignVisit.CreatedDate = DateTime.Now;
                spouseForeignVisit.CreatedBy = userId;
            }
            spouseForeignVisit.SpouseId = model.SpouseId;
            spouseForeignVisit.CountryId = model.CountryId;
            spouseForeignVisit.Purpose = model.Purpose;
            spouseForeignVisit.AccompaniedBy = model.AccompaniedBy;
            spouseForeignVisit.StayFromDate = model.StayFromDate;
            spouseForeignVisit.StayToDate = model.StayToDate;

            await spouseForignVisitRepository.SaveAsync(spouseForeignVisit);
            model.SpouseForeignVisitId = spouseForeignVisit.SpouseForeignVisitId;
            return model;
        }
    }
}
