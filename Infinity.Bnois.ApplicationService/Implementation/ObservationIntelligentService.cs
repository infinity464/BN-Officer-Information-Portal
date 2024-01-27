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
    public class ObservationIntelligentService : IObservationIntelligentService
    {
        private readonly IBnoisRepository<ObservationIntelligent> observationIntelligentRepository;
        public ObservationIntelligentService(IBnoisRepository<ObservationIntelligent> observationIntelligentRepository)
        {
            this.observationIntelligentRepository = observationIntelligentRepository;
        }

        //public List<ObservationIntelligentModel> GetObservationIntelligents(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<ObservationIntelligent> observationIntelligents = observationIntelligentRepository.FilterWithInclude(x => x.IsActive
        //        && (x.Employee.PNo.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "Employee1");
        //    total = observationIntelligents.Count();
        //    observationIntelligents = observationIntelligents.OrderByDescending(x => x.ObservationIntelligentId).Skip((pn - 1) * ps).Take(ps);
        //    List<ObservationIntelligentModel> models = ObjectConverter<ObservationIntelligent, ObservationIntelligentModel>.ConvertList(observationIntelligents.ToList()).ToList();
        //    models = models.Select(x =>
        //    {
        //        x.TypeName = Enum.GetName(typeof(ObservationIntelligentType), x.Type);
        //        return x;
        //    }).ToList();
        //    return models;
        //}

        public async Task<ObservationIntelligentModel> GetObservationIntelligent(int id)
        {
            if (id <= 0)
            {
                return new ObservationIntelligentModel();
            }
            ObservationIntelligent observationIntelligent = await observationIntelligentRepository.FindOneAsync(x => x.ObservationIntelligentId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch","Employee1", "Employee1.Rank", "Employee1.Batch" });
            if (observationIntelligent == null)
            {
                throw new InfinityNotFoundException("Observation Intelligent Report not found");
            }
            ObservationIntelligentModel model = ObjectConverter<ObservationIntelligent, ObservationIntelligentModel>.Convert(observationIntelligent);
            return model;
        }

    
        public async Task<ObservationIntelligentModel> SaveObservationIntelligent(int id, ObservationIntelligentModel model)
        {
          
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Observation Intelligent Report  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ObservationIntelligent observationIntelligent = ObjectConverter<ObservationIntelligentModel, ObservationIntelligent>.Convert(model);
            if (id > 0)
            {
                observationIntelligent = await observationIntelligentRepository.FindOneAsync(x => x.ObservationIntelligentId == id);
                if (observationIntelligent == null)
                {
                    throw new InfinityNotFoundException("Observation Intelligent Report not found !");
                }

                observationIntelligent.ModifiedDate = DateTime.Now;
                observationIntelligent.ModifiedBy = userId;
            }
            else
            {
                observationIntelligent.IsActive = true;
                observationIntelligent.CreatedDate = DateTime.Now;
                observationIntelligent.CreatedBy = userId;
            }
            observationIntelligent.EmployeeId = model.EmployeeId;
            observationIntelligent.GivenEmployeeId = model.GivenEmployeeId;
            observationIntelligent.GivenTransferId = model.GivenTransferId;
            observationIntelligent.Type = model.Type;
            observationIntelligent.Date =model.Date ?? observationIntelligent.Date;
            observationIntelligent.Remarks = model.Remarks;

            observationIntelligent.IsBackLog = model.IsBackLog;
            observationIntelligent.RankId = model.Employee.RankId;
            observationIntelligent.TransferId = model.Employee.TransferId;

            if (model.IsBackLog)
            {

                observationIntelligent.RankId = model.RankId;
                observationIntelligent.TransferId = model.TransferId;
            }

            model.Employee = null;
            model.Employee1 = null;

            await observationIntelligentRepository.SaveAsync(observationIntelligent);
            model.ObservationIntelligentId = observationIntelligent.ObservationIntelligentId;
            return model;
        }


        public async Task<bool> DeleteObservationIntelligent(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ObservationIntelligent observationIntelligent = await observationIntelligentRepository.FindOneAsync(x => x.ObservationIntelligentId == id);
            if (observationIntelligent == null)
            {
                throw new InfinityNotFoundException("Observation Intelligent Report not found");
            }
            else
            {
                return await observationIntelligentRepository.DeleteAsync(observationIntelligent);
            }
        }

        public List<SelectModel> GetObservationIntelligentTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(ObservationIntelligentType)).Cast<ObservationIntelligentType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public List<ObservationIntelligentModel> GetObservationIntelligents(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
