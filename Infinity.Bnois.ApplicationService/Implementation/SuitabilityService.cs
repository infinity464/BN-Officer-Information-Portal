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
	public class SuitabilityService : ISuitabilityService
	{	

		private readonly IBnoisRepository<Suitability> suitabilityRepository;
		public SuitabilityService(IBnoisRepository<Suitability> suitabilityRepository)
		{
			this.suitabilityRepository = suitabilityRepository;
		}

	
        public async Task<List<SelectModel>> GetSuitabilitySelectModels()
        {
            ICollection<Suitability> branches = await suitabilityRepository.FilterAsync(x=>x.IsActive);
            List<SelectModel> selectModels = branches.OrderBy(x => x.SuitabilityName).Select(x => new SelectModel
            {
                Text = x.SuitabilityName,
                Value = x.Id
            }).ToList();
            return selectModels;
        }

      
    }
}
