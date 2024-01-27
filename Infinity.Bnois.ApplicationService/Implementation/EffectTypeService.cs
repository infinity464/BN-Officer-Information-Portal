using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Implementation
{
	public class EffectTypeService : IEffectTypeService
	{
		private readonly IBnoisRepository<EffectType> effectTypeRepository;
		public EffectTypeService(IBnoisRepository<EffectType> effectTypeRepository)
		{
			this.effectTypeRepository = effectTypeRepository;
		}

		public async Task<List<SelectModel>> GetEffectTypeSelectModel()
		{
			ICollection<EffectType> leaveType = await effectTypeRepository.FilterAsync(x => x.IsActive == true );
			List<SelectModel> selectModels = leaveType.Select(x => new SelectModel
			{
				Text = x.Name,
				Value = x.ShortName
			}).ToList();
			return selectModels;
		}
	}
}
