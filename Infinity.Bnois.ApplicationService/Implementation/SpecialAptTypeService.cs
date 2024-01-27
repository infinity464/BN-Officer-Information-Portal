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
	public class SpecialAptTypeService : ISpecialAptTypeService
	{	

		private readonly IBnoisRepository<SpecialAptType> specialAptTypeRepository;
		public SpecialAptTypeService(IBnoisRepository<SpecialAptType> specialAptTypeRepository)
		{
			this.specialAptTypeRepository = specialAptTypeRepository;
		}


        public List<SpecialAptTypeModel> GetSpecialAptTypes(int ps, int pn, string qs, out int total)
        {
            IQueryable<SpecialAptType> specialAptTypes = specialAptTypeRepository.FilterWithInclude(x => x.IsActive && (x.SpAptType.Contains(qs) || String.IsNullOrEmpty(qs) ));
            total = specialAptTypes.Count();
            specialAptTypes = specialAptTypes.OrderByDescending(x => x.Id).Skip((pn - 1) * ps).Take(ps);
            List<SpecialAptTypeModel> models = ObjectConverter<SpecialAptType, SpecialAptTypeModel>.ConvertList(specialAptTypes.ToList()).ToList();
            return models;
        }

        public async Task<SpecialAptTypeModel> GetSpecialAptType(int id)
        {
            if (id <= 0)
            {
                return new SpecialAptTypeModel();
            }
            SpecialAptType specialAptType = await specialAptTypeRepository.FindOneAsync(x => x.Id == id);
            if (specialAptType == null)
            {
                throw new InfinityNotFoundException("SpecialAptType not found");
            }
            SpecialAptTypeModel model = ObjectConverter<SpecialAptType, SpecialAptTypeModel>.Convert(specialAptType);
            return model;
        }

        public async Task<SpecialAptTypeModel> SaveSpecialAptType(int id, SpecialAptTypeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("SpecialAptType data missing");
            }
            bool isExist = specialAptTypeRepository.Exists(x => x.SpAptType == model.SpAptType && x.Id != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            SpecialAptType specialAptType = ObjectConverter<SpecialAptTypeModel, SpecialAptType>.Convert(model);
            if (id > 0)
            {
                specialAptType = await specialAptTypeRepository.FindOneAsync(x => x.Id == id);
                if (specialAptType == null)
                {
                    throw new InfinityNotFoundException("SpecialAptType not found !");
                }

               
            }
            else
            {
                specialAptType.IsActive = true;
               
            }
            specialAptType.SpAptType = model.SpAptType;
            specialAptType.Position = model.Position;

            await specialAptTypeRepository.SaveAsync(specialAptType);
            model.Id = specialAptType.Id;
            return model;
        }

        public async Task<bool> DeleteSpecialAptType(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            SpecialAptType specialAptType = await specialAptTypeRepository.FindOneAsync(x => x.Id == id);
            if (specialAptType == null)
            {
                throw new InfinityNotFoundException("SpecialAptType not found");
            }
            else
            {
                return await specialAptTypeRepository.DeleteAsync(specialAptType);
            }
        }



        public async Task<List<SelectModel>> GetSpecialAptTypeSelectModels()
        {
            ICollection<SpecialAptType> branches = await specialAptTypeRepository.FilterAsync(x=>x.IsActive);
            List<SelectModel> selectModels = branches.OrderBy(x => x.SpAptType).Select(x => new SelectModel
            {
                Text = x.SpAptType,
                Value = x.Id
            }).ToList();
            return selectModels;
        }

      
    }
}
