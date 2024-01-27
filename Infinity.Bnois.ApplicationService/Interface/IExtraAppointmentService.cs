using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IExtraAppointmentService
    {
        List<ExtraAppointmentModel> GetExtraAppointments(int ps, int pn, string qs, out int total);
        Task<ExtraAppointmentModel> GetExtraAppointment(int id);
        Task<ExtraAppointmentModel> SaveExtraAppointment(int v, ExtraAppointmentModel model);
        Task<bool> DeleteExtraAppointment(int id);


    }
}
