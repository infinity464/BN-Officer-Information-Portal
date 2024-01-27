using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IMissionAppointmentService
    {
        List<MissionAppointmentModel> GetMissionAppointments(int ps, int pn, string qs, out int total);
        Task<MissionAppointmentModel> GetMissionAppointment(int id);
        Task<List<SelectModel>> GetMissionAppointmentsByMissionId(int missionId);

        Task<MissionAppointmentModel> SaveMissionAppointment(int v, MissionAppointmentModel model);
        Task<bool> DeleteMissionAppointment(int id);

        string GetMissionSchedule(int missionId);

    }
}
