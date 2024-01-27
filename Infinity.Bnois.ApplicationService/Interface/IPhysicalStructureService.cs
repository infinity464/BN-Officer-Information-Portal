using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPhysicalStructureService
    {
        List<PhysicalStructureModel> GetPhysicalStructures(int ps, int pn, string qs, out int total);
        Task<PhysicalStructureModel> GetPhysicalStructure(int id);
        Task<PhysicalStructureModel> SavePhysicalStructure(int v, PhysicalStructureModel model);
        Task<bool> DeletePhysicalStructure(int id);
        Task<List<SelectModel>> GetPhysicalStructureSelectModels();
    }
}
