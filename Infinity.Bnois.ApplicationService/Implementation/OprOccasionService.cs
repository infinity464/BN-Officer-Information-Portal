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
	public class OprOccasionService : IOprOccasionService
	{	

		private readonly IBnoisRepository<OprOccasion> oprOccasionRepository;
		public OprOccasionService(IBnoisRepository<OprOccasion> oprOccasionRepository)
		{
			this.oprOccasionRepository = oprOccasionRepository;
		}

	
        public async Task<List<SelectModel>> GetOprOccasionSelectModels()
        {
            ICollection<OprOccasion> branches = await oprOccasionRepository.FilterAsync(x=>x.IsActive);
            List<SelectModel> selectModels = branches.OrderBy(x => x.Title).Select(x => new SelectModel
            {
                Text = x.Title,
                Value = x.OccasionId
            }).ToList();
            return selectModels;
        }

      
    }
}
