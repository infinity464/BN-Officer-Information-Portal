using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IAppointmentNatureService
	{
		List<AptNatModel> GetAppointmentNaturies(int ps, int pn, string qs, out int total);
		Task<AptNatModel> GetAppointmentNature(int id);
		Task<AptNatModel> SaveAppointmentNature(int v, AptNatModel model);
		Task<bool> DeleteAppointmentNature(int id);
        Task<List<SelectModel>> GetNatureSelectList();
    }
}
