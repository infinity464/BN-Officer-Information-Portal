using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IAppointmentCategoryService
	{
		List<AptCatModel> GetAppointmentCategorys(int ps, int pn, string qs, out int total);
		Task<AptCatModel> GetAppointmentCategory(int id);
		Task<AptCatModel> SaveAppointmentCategory(int v, AptCatModel model);
		Task<bool> DeleteAppointmentCategory(int id);
	    Task<List<SelectModel>> GetCategorySelectListByNature(int id);
	    Task<List<SelectModel>> GetCategorySelectModel();
    }
}
