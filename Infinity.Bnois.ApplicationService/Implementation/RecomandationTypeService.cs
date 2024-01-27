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
	public class RecomandationTypeService : IRecomandationTypeService
	{	

		private readonly IBnoisRepository<RecomandationType> recomandationTypeRepository;
		public RecomandationTypeService(IBnoisRepository<RecomandationType> recomandationTypeRepository)
		{
			this.recomandationTypeRepository = recomandationTypeRepository;
		}

	
        public async Task<List<SelectModel>> GetRecomandationTypeSelectModels()
        {
            ICollection<RecomandationType> branches = await recomandationTypeRepository.FilterAsync(x=>x.IsActive);
            List<SelectModel> selectModels = branches.OrderBy(x => x.ShortName).Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.RecomandationTypeId
            }).ToList();
            return selectModels;
        }

      
    }
}
