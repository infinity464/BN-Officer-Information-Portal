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
   public class DivisionService : IDivisionService
    {

        private readonly IBnoisRepository<Division> divisionRepository;
        public DivisionService(IBnoisRepository<Division> divisionRepository)
        {
            this.divisionRepository = divisionRepository;
        }


        public List<DivisionModel> GetDivisions(int ps, int pn, string qs, out int total)
        {
            IQueryable<Division> divisions = divisionRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs) ));
            total = divisions.Count();
            divisions = divisions.OrderByDescending(x => x.DivisionId).Skip((pn - 1) * ps).Take(ps);
            List<DivisionModel> models = ObjectConverter<Division, DivisionModel>.ConvertList(divisions.ToList()).ToList();
            return models;
        }

        public async Task<DivisionModel> GetDivision(int id)
        {
            if (id <= 0)
            {
                return new DivisionModel();
            }
            Division division = await divisionRepository.FindOneAsync(x => x.DivisionId == id);
            if (division == null)
            {
                throw new InfinityNotFoundException("Division not found");
            }
            DivisionModel model = ObjectConverter<Division, DivisionModel>.Convert(division);
            return model;
        }

        public async Task<DivisionModel> SaveDivision(int id, DivisionModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Division data missing");
            }
            bool isExist = divisionRepository.Exists(x => x.Name == model.Name  && x.DivisionId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Division division = ObjectConverter<DivisionModel, Division>.Convert(model);
            if (id > 0)
            {
                division = await divisionRepository.FindOneAsync(x => x.DivisionId == id);
                if (division == null)
                {
                    throw new InfinityNotFoundException("Division not found !");
                }

                division.ModifiedDate = DateTime.Now;
                division.ModifiedBy = userId;
            }
            else
            {
                division.IsActive = true;
                division.CreatedDate = DateTime.Now;
                division.CreatedBy = userId;
            }
            division.Name = model.Name;
            division.Description = model.Description;
     
            
            await divisionRepository.SaveAsync(division);
            model.DivisionId = division.DivisionId;
            return model;
        }

        public async Task<bool> DeleteDivision(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Division division = await divisionRepository.FindOneAsync(x => x.DivisionId == id);
            if (division == null)
            {
                throw new InfinityNotFoundException("Division not found");
            }
            else
            {
                return await divisionRepository.DeleteAsync(division);
            }
        }

        public async Task<List<SelectModel>> GetDivisionSelectModels()
        {
            ICollection<Division> models = await divisionRepository.FilterAsync(x => x.IsActive);
            return models.Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.DivisionId
            }).ToList();

        }




    }
}
