using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISeaServiceService
    {
        List<SeaServiceModel> GetSeaServices(int ps, int pn, string qs, out int total);
        Task<SeaServiceModel> GetSeaService(int id);
        Task<SeaServiceModel> SaveSeaService(int v, SeaServiceModel model);
        Task<bool> DeleteSeaService(int id);
        List<SelectModel> GetShipTypeSelectModels();

    }
}
