using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IPatternService
	{
		List<PatternModel> GetPatterns(int ps, int pn, string qs, out int total);
		Task<PatternModel> GetPattern(int id);
		Task<PatternModel> SavePattern(int v, PatternModel model);
		Task<bool> DeletePattern(int id);
        Task<List<SelectModel>> GetPatternTypeSelectModels();

	}
}
