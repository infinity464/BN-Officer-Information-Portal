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
    public class SportService : ISportService
    {
        private readonly IBnoisRepository<Sport> sportRepository;
        public SportService(IBnoisRepository<Sport> sportRepository)
        {
            this.sportRepository = sportRepository;
        }
        
        public async Task<SportModel> GetSport(int id)
        {
            if (id <= 0)
            {
                return new SportModel();
            }
            Sport sport = await sportRepository.FindOneAsync(x => x.SportId == id);
            if (sport == null)
            {
                throw new InfinityNotFoundException("Sports not found");
            }
            SportModel model = ObjectConverter<Sport, SportModel>.Convert(sport);
            return model;
        }

        public List<SportModel> GetSports(int ps, int pn, string qs, out int total)
        {
            IQueryable<Sport> sports = sportRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = sports.Count();
            sports = sports.OrderByDescending(x => x.SportId).Skip((pn - 1) * ps).Take(ps);
            List<SportModel> models = ObjectConverter<Sport, SportModel>.ConvertList(sports.ToList()).ToList();
            return models;
        }

        public async Task<SportModel> SaveSport(int id, SportModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Sports data missing");
            }
            bool isExist = sportRepository.Exists(x => x.Name == model.Name && x.SportId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Sport sport = ObjectConverter<SportModel, Sport>.Convert(model);
            if (id > 0)
            {
                sport = await sportRepository.FindOneAsync(x => x.SportId == id);
                if (sport == null)
                {
                    throw new InfinityNotFoundException("Sports not found !");
                }

                sport.ModifiedDate = DateTime.Now;
                sport.ModifiedBy = userId;
            }
            else
            {
                sport.IsActive = true;
                sport.CreatedDate = DateTime.Now;
                sport.CreatedBy = userId;
            }
            sport.Name = model.Name;
            sport.Remarks = model.Remarks;


            await sportRepository.SaveAsync(sport);
            model.SportId = sport.SportId;
            return model;
        }
        public async Task<bool> DeleteSport(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Sport sport = await sportRepository.FindOneAsync(x => x.SportId == id);
            if (sport == null)
            {
                throw new InfinityNotFoundException("Sports not found");
            }
            else
            {
                return await sportRepository.DeleteAsync(sport);
            }
        }

        public async Task<List<SelectModel>> GetSportsSelectModels()
        {
            ICollection<Sport> sports = await sportRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = sports.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.SportId
            }).ToList();
            return selectModels;
        }
    }
}
