using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IReligionService
	{
		List<ReligionModel> GetReligions(int ps, int pn, string qs, out int total);
		Task<ReligionModel> GetReligion(int id);
		Task<ReligionModel> SaveReligion(int v, ReligionModel model);
		Task<bool> DeleteReligion(int id);
        Task<List<SelectModel>> GetReligionSelectModels();
    }
}
