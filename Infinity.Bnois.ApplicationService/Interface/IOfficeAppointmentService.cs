using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IOfficeAppointmentService
    {
        List<OfficeAppointmentModel> GetOfficeAppointments(int ps, int pn, string qs, int officeId,int type, out int total);
        Task<OfficeAppointmentModel> GetOfficeAppointment(int id);
        Task<List<SelectModel>> GetOfficeAppointmentsByOfficeId(int officeId);
        Task<List<SelectModel>> GetOfficeAppointmentSelectModels();
        Task<List<SelectModel>> GetOfficeAdditionalAppointmentsByOfficeId(int officeId);
        Task<List<SelectModel>> GetAppointmentByShipType(int shipType);
        Task<List<SelectModel>> GetAppointmentByOrganizationPattern(int officeId);
        Task<List<SelectModel>> GetParentAppointmentSelectModels(int officeId, int OffAppId);

        Task<OfficeAppointmentModel> SaveOfficeAppointment(int id, OfficeAppointmentModel model);
        Task<OfficeAppointmentModel> SaveOfficeAdditionalAppointment(int id, OfficeAppointmentModel model);
        Task<bool> DeleteOfficeAppointment(int id);
        List<SelectModel> GetCommandingOfficerListByOfficeId();

    }
}
