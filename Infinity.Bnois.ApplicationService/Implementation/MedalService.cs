using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class MedalService : IMedalService
    {

        private readonly IBnoisRepository<Medal> medalRepository;
        public MedalService(IBnoisRepository<Medal> medalRepository)
        {
            this.medalRepository = medalRepository;
        }


        public List<MedalModel> GetMedals(int ps, int pn, string qs, out int total)
        {
            IQueryable<Medal> medals = medalRepository.FilterWithInclude(x => x.IsActive && (x.NameEng.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = medals.Count();
            medals = medals.OrderByDescending(x => x.MedalId).Skip((pn - 1) * ps).Take(ps);
            List<MedalModel> models = ObjectConverter<Medal, MedalModel>.ConvertList(medals.ToList()).ToList();

            models = models.Select(x =>
            {
                x.MedalTypeName = Enum.GetName(typeof(MedalType), x.MedalType);
                return x;
            }).ToList();
            return models;
        }

        public async Task<MedalModel> GetMedal(int id)
        {
            if (id <= 0)
            {
                return new MedalModel();
            }
            Medal medal = await medalRepository.FindOneAsync(x => x.MedalId == id);
            if (medal == null)
            {
                throw new InfinityNotFoundException("Medal not found");
            }
            MedalModel model = ObjectConverter<Medal, MedalModel>.Convert(medal);
            return model;
        }

        public async Task<MedalModel> SaveMedal(int id, MedalModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Medal data missing");
            }
            bool isExist = medalRepository.Exists(x => x.NameEng == model.NameEng && x.MedalId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Medal medal = ObjectConverter<MedalModel, Medal>.Convert(model);
            if (id > 0)
            {
                medal = await medalRepository.FindOneAsync(x => x.MedalId == id);
                if (medal == null)
                {
                    throw new InfinityNotFoundException("Medal not found !");
                }

                medal.ModifiedDate = DateTime.Now;
                medal.ModifiedBy = userId;
            }
            else
            {
                medal.IsActive = true;
                medal.CreatedDate = DateTime.Now;
                medal.CreatedBy = userId;
            }
            medal.NameEng = model.NameEng;
            medal.NameBan = model.NameBan;
            medal.ShortNameBan = model.ShortNameBan;
            medal.ShortNameEng = model.ShortNameEng;
            medal.Priority = model.Priority;
            medal.MedalType = model.MedalType;
            //medal.GoToTrace = model.GoToTrace;
            medal.ANmCon = model.ANmCon;
            medal.NmRGF = model.NmRGF;
            medal.Description = model.Description;


            await medalRepository.SaveAsync(medal);
            model.MedalId = medal.MedalId;
            return model;
        }

        public async Task<bool> DeleteMedal(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Medal medal = await medalRepository.FindOneAsync(x => x.MedalId == id);
            if (medal == null)
            {
                throw new InfinityNotFoundException("Medal not found");
            }
            else
            {
                return await medalRepository.DeleteAsync(medal);
            }
        }

        public async Task<List<SelectModel>> GetMedalSelectModels(int medalType)
        {
            ICollection<Medal> models = await medalRepository.FilterAsync(x => x.IsActive && x.MedalType == medalType);
            return models.OrderBy(x => x.NameEng).Select(x => new SelectModel()
            {
                Text = x.NameEng,
                Value = x.MedalId
            }).ToList();

        }
        public async Task<List<SelectModel>> GetMedalSelectModels()
        {
            ICollection<Medal> models = await medalRepository.FilterAsync(x => x.IsActive );
            return models.OrderBy(x=>x.NameEng).Select(x => new SelectModel()
            {
                Text = x.NameEng,
                Value = x.MedalId
            }).ToList();

        }

        public List<SelectModel> GetMedalTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(MedalType)).Cast<MedalType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public Task<List<SelectModel>> GetTraceMedalSelectModels(int medalType)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<SelectModel>> GetTraceMedalSelectModels(int medalType)
        //{
        //    ICollection<Medal> models = await medalRepository.FilterAsync(x => x.IsActive && x.MedalType == medalType && x.GoToTrace);
        //    return models.Select(x => new SelectModel()
        //    {
        //        Text = x.NameEng,
        //        Value = x.MedalId
        //    }).ToList();
        //}
    }
}
