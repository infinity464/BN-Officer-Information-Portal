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
	public class PftTypeService : IPftTypeService
	{	

		private readonly IBnoisRepository<PftType> pftRepository;
		public PftTypeService(IBnoisRepository<PftType> pftRepository)
		{
			this.pftRepository = pftRepository;
		}

	
        public async Task<List<SelectModel>> GetPftTypeSelectModels()
        {
            ICollection<PftType> branches = await pftRepository.FilterAsync(x=>x.IsActive);
            List<SelectModel> selectModels = branches.OrderBy(x => x.PftTitle).Select(x => new SelectModel
            {
                Text = x.PftTitle,
                Value = x.PftTypeId
            }).ToList();
            return selectModels;
        }

      
    }
}
