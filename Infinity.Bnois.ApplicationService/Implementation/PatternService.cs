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
	public class PatternService : IPatternService
	{
		private readonly IBnoisRepository<Pattern> patternRepository;
		public PatternService(IBnoisRepository<Pattern> patternRepository)
		{
			this.patternRepository = patternRepository;
		}

		public async Task<bool> DeletePattern(int id)
		{
			if (id < 0)
			{
				throw new InfinityArgumentMissingException("Invalid Request");
			}
			Pattern pattern = await patternRepository.FindOneAsync(x => x.PatternId == id);
			if (pattern == null)
			{
				throw new InfinityNotFoundException("Pattern not found");
			}
			else
			{
				return await patternRepository.DeleteAsync(pattern);
			}
		}

		public async Task<PatternModel> GetPattern(int id)
		{
			if (id <= 0)
			{
				return new PatternModel();
			}
			Pattern pattern = await patternRepository.FindOneAsync(x => x.PatternId == id);
			if (pattern == null)
			{
				throw new InfinityNotFoundException("Pattern not found");
			}
			PatternModel model = ObjectConverter<Pattern, PatternModel>.Convert(pattern);
			return model;
		}

		public List<PatternModel> GetPatterns(int ps, int pn, string qs, out int total)
		{
			IQueryable<Pattern> patterns = patternRepository.FilterWithInclude(x => x.Name.Contains(qs) || String.IsNullOrEmpty(qs));
			total = patterns.Count();
			patterns = patterns.OrderByDescending(x => x.PatternId).Skip((pn - 1) * ps).Take(ps);
			List<PatternModel> models = ObjectConverter<Pattern, PatternModel>.ConvertList(patterns.ToList()).ToList();
			return models;
		}

		public async Task<PatternModel> SavePattern(int id, PatternModel model)
		{
			if (model == null)
			{
				throw new InfinityArgumentMissingException("Pattern data missing");
			}
			bool isExist = patternRepository.Exists(x => x.Name == model.Name  && x.PatternId != id);
			if (isExist)
			{
				throw new InfinityInvalidDataException("Data already exists !");
			}

			string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
			Pattern pattern = ObjectConverter<PatternModel, Pattern>.Convert(model);
		    if (id > 0)
		    {
		        pattern = await patternRepository.FindOneAsync(x => x.PatternId == id);
		        if (pattern == null)
		        {
		            throw new InfinityNotFoundException("Pattern not found !");
		        }

		        pattern.ModifiedDate = DateTime.Now;
		        pattern.ModifiedBy = userId;
		    }
		    else
		    {
		        pattern.IsActive = true;
		        pattern.CreatedDate = DateTime.Now;
		        pattern.CreatedBy = userId;
		    }

            pattern.Name = model.Name;
			pattern.PType = model.PType;
			pattern.IsMoveAble = model.IsMoveAble;		
		
			await patternRepository.SaveAsync(pattern);
			model.PatternId = pattern.PatternId;
			return model;
		}


	    public async Task<List<SelectModel>> GetPatternTypeSelectModels()
	    {
	        ICollection<Pattern> patterns = await patternRepository.FilterAsync(x=>x.IsActive);
            List<Pattern> query = patterns.OrderBy(x => x.Name).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
	        {
	            Text = x.Name,
	            Value = x.PatternId
	        }).ToList();
	        return selectModels;
	    }
    }
}
