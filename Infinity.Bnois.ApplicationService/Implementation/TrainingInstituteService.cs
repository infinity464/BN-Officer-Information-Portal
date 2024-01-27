using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class TrainingInstituteService : ITrainingInstituteService
    {
        private readonly IBnoisRepository<TrainingInstitute> trainingInstituteRepository;
        public TrainingInstituteService(IBnoisRepository<TrainingInstitute> trainingInstituteRepository)
        {
            this.trainingInstituteRepository = trainingInstituteRepository;
        }

        //public List<TrainingInstituteModel> GetTrainingInstitutes(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<TrainingInstitute> trainingInstitutes = trainingInstituteRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.Country.FullName.Contains(qs)) || (x.FullName.Contains(qs)) || String.IsNullOrEmpty(qs)), "Country");
        //    total = trainingInstitutes.Count();
        //    trainingInstitutes = trainingInstitutes.OrderByDescending(x => x.InstituteId).Skip((pn - 1) * ps).Take(ps);
        //    List<TrainingInstituteModel> models = ObjectConverter<TrainingInstitute, TrainingInstituteModel>.ConvertList(trainingInstitutes.ToList()).ToList();
        //    return models;
        //}

        public async Task<TrainingInstituteModel> GetTrainingInstitute(int id)
        {
            if (id <= 0)
            {
                return new TrainingInstituteModel();
            }
            TrainingInstitute trainingInstitute = await trainingInstituteRepository.FindOneAsync(x => x.InstituteId == id);
            if (trainingInstitute == null)
            {
                throw new InfinityNotFoundException("Training Institute not found");
            }
            TrainingInstituteModel model = ObjectConverter<TrainingInstitute, TrainingInstituteModel>.Convert(trainingInstitute);
            return model;
        }


        public async Task<TrainingInstituteModel> SaveTrainingInstitute(int id, TrainingInstituteModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Training Institute data missing");
            }

            bool isExistData = trainingInstituteRepository.Exists(x => x.CountryId == model.CountryId && x.FullName==model.FullName && x.InstituteId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            TrainingInstitute trainingInstitute = ObjectConverter<TrainingInstituteModel, TrainingInstitute>.Convert(model);
            if (id > 0)
            {
                trainingInstitute = await trainingInstituteRepository.FindOneAsync(x => x.InstituteId == id);
                if (trainingInstitute == null)
                {
                    throw new InfinityNotFoundException("Training Institute not found !");
                }
                trainingInstitute.ModifiedDate = DateTime.Now;
                trainingInstitute.ModifiedBy = userId;
            }
            else
            {
                trainingInstitute.IsActive = true;
                trainingInstitute.CreatedDate = DateTime.Now;
                trainingInstitute.CreatedBy = userId;
            }
            trainingInstitute.CountryId = model.CountryId;
            trainingInstitute.FullName = model.FullName;
            trainingInstitute.ShortName = model.ShortName;
            trainingInstitute.NameInBangla = model.NameInBangla;
            trainingInstitute.AddressInfo = model.AddressInfo;
            trainingInstitute.CountryType = model.CountryType;


            await trainingInstituteRepository.SaveAsync(trainingInstitute);
            model.InstituteId = trainingInstitute.InstituteId;
            return model;
        }


        public async Task<bool> DeleteTrainingInstitute(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            TrainingInstitute trainingInstitute = await trainingInstituteRepository.FindOneAsync(x => x.InstituteId == id);
            if (trainingInstitute == null)
            {
                throw new InfinityNotFoundException("Training Institute not found");
            }
            else
            {
                return await trainingInstituteRepository.DeleteAsync(trainingInstitute);
            }
        }



        public List<SelectModel> GetCountryTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(CountryType)).Cast<CountryType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }


        public async Task<List<SelectModel>> GetTrainingInstituteSelectModels(int countryId)
        {
            ICollection<TrainingInstitute> trainingInstitutes = await trainingInstituteRepository.FilterAsync(x => x.IsActive && x.CountryId==countryId);
            List<SelectModel> selectModels = trainingInstitutes.OrderBy(x=>x.FullName).Select(x => new SelectModel
            {
                Text = x.FullName,
                Value = x.InstituteId
            }).ToList();
            return selectModels;

        }

        public async Task<List<SelectModel>> GetTrainingInstituteSelectModels()
        {
            ICollection<TrainingInstitute> trainingInstitutes = await trainingInstituteRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = trainingInstitutes.OrderBy(x => x.FullName).Select(x => new SelectModel
            {
                Text = x.FullName,
                Value = x.InstituteId
            }).ToList();
            return selectModels;

        }

        public List<TrainingInstituteModel> GetTrainingInstitutes(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}