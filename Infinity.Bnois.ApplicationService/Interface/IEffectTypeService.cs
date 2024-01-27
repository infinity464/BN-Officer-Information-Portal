using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IEffectTypeService
	{
		Task<List<SelectModel>> GetEffectTypeSelectModel();
	}
}
