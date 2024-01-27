using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IOfficeService
    {
        List<OfficeModel> GetOffices();
        List<OfficeModel> GetOfficeStructures();
        Task<OfficeModel> GetOffice(int id);
        Task<OfficeModel> SaveOffice(int id, OfficeModel model);
        Task<bool> DeleteOffice(int id);
        Task<List<SelectModel>> GetAdminAuthoritySelectModel();
        Task<List<SelectModel>> GetOfficeSelectModelByShip(int ship);
    
        Task<List<SelectModel>> GetBornOfficeSelectModel();
        Task<List<SelectModel>> GetBornOfficeFullNameSelectModel();
        Task<List<SelectModel>> GetOrganizationSelectModel();
        Task<List<SelectModel>> GetParentOfficeSelectModel();
     
        Task<List<SelectModel>> GetMinistryOfficeSelectModel();
        Task<List<SelectModel>> GetOfficeSelectModel(int type);
        Task<List<SelectModel>> GetChildOfficeSelectModel(int parentId);
         List<SelectModel> GetShipTypeSelectModels();
         Task<List<SelectModel>> GetOfficeWithoutShipSelectModels();
         Task<List<SelectModel>> GetShipSelectModels();
         Task<List<SelectModel>> GetShipOfficeSelectModels();
        List<SelectModel> GetObjectiveSelectModels();
        Task<OfficeModel> ShipMovement(int officeId, OfficeModel model);

        List<object> GetAppointedOfficer(int officeId);
        List<object> GetVacantAppointment(int officeId);

    }
}
