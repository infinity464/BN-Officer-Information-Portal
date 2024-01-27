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
	public class ReligionService : IReligionService
	{
		private readonly IBnoisRepository<Religion> religionRepository;
		public ReligionService(IBnoisRepository<Religion> relegionRepository)
		{
			this.religionRepository = relegionRepository;
		}

		public async Task<bool> DeleteReligion(int id)
		{
			if (id <= 0)
			{
				throw new InfinityArgumentMissingException("Invalid Request");
			}
			Religion relegion = await religionRepository.FindOneAsync(x => x.ReligionId == id);
			if (relegion == null)
			{
				throw new InfinityNotFoundException("Religion not found");
			}
			else
			{
				return await religionRepository.DeleteAsync(relegion);
			}
		}

		public async Task<ReligionModel> GetReligion(int id)
		{
			if (id <= 0)
			{
				return new ReligionModel();
			}
			Religion relegion = await religionRepository.FindOneAsync(x => x.ReligionId == id);
			if (relegion == null)
			{
				throw new InfinityNotFoundException("Relegion not found");
			}
			ReligionModel model = ObjectConverter<Religion, ReligionModel>.Convert(relegion);
			return model;
		}

		public List<ReligionModel> GetReligions(int ps, int pn, string qs, out int total)
		{
			IQueryable<Religion> relegions = religionRepository.FilterWithInclude(x => x.IsActive == true
	  && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs))));

			total = relegions.Count();
			relegions = relegions.OrderByDescending(x => x.ReligionId).Skip((pn - 1) * ps).Take(ps);
			List<ReligionModel> models = ObjectConverter<Religion, ReligionModel>.ConvertList(relegions.ToList()).ToList();
			return models;
		}

       

        public async Task<ReligionModel> SaveReligion(int id, ReligionModel model)
		{
			string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
			if (model == null)
			{
				throw new InfinityArgumentMissingException("Religion data missing");
			}
			Religion religion = ObjectConverter<ReligionModel, Religion>.Convert(model);
			if (id > 0)
			{
				religion = await religionRepository.FindOneAsync(x => x.ReligionId == id);
				if (religion == null)
				{
					throw new InfinityNotFoundException("Religion not found !");
				}

			    religion.Modified = DateTime.Now;
			    religion.ModifiedBy = userId;
            }
			else
			{
				religion.Created = DateTime.Now;
				religion.CreatedBy = userId;
			}
			religion.Name = model.Name;
			religion.Remarks = model.Remarks;
		
			religion.IsActive = true;
			await religionRepository.SaveAsync(religion);
			model.ReligionId = religion.ReligionId;
			return model;
		}

        public async Task<List<SelectModel>> GetReligionSelectModels()
        {
            ICollection<Religion> nationalities = await religionRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = nationalities.OrderBy(x => x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.ReligionId
            }).ToList();
            return selectModels;
        }
    }
}
