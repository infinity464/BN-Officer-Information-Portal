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
	public class OprGradingService : IOprGradingService
	{	

		private readonly IBnoisRepository<OprGrading> oprGradingRepository;
		public OprGradingService(IBnoisRepository<OprGrading> oprGradingRepository)
		{
			this.oprGradingRepository = oprGradingRepository;
		}

	
        public async Task<List<OprGradingModel>> GetOprGradings()
        {
	        ICollection<OprGrading> oprGradings = await oprGradingRepository.FilterAsync(x => x.IsActive);

	        List<OprGradingModel> models = ObjectConverter<OprGrading, OprGradingModel>.ConvertList(oprGradings.ToList()).ToList();
	        return models;
		}

      
    }
}
