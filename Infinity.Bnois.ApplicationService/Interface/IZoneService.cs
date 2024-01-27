using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IZoneService
    {
        List<ZoneModel> GetZones(int ps, int pn, string qs, out int total);
        Task<ZoneModel> GetZone(int id);
        Task<ZoneModel> SaveZone(int v, ZoneModel model);
        Task<bool> DeleteZone(int id);
        Task<List<SelectModel>> GetZoneSelectModels();
    }
}
