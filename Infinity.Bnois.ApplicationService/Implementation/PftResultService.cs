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
	public class PftResultService : IPftResultService
	{	

		private readonly IBnoisRepository<PftResult> pftRepository;
		public PftResultService(IBnoisRepository<PftResult> pftRepository)
		{
			this.pftRepository = pftRepository;
		}

	
        public async Task<List<SelectModel>> GetPftResultSelectModels()
        {
            ICollection<PftResult> branches = await pftRepository.FilterAsync(x=>x.IsActive);
            List<SelectModel> selectModels = branches.OrderBy(x => x.ResultTitle).Select(x => new SelectModel
            {
                Text = x.ResultTitle,
                Value = x.PftResultId
            }).ToList();
            return selectModels;
        }

      
    }
}
