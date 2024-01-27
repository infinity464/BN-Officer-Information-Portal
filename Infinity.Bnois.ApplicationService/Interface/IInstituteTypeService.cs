using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IInstituteTypeService
    {
        List<InstituteTypeModel> InstituteTypes(int ps, int pn, string qs, out int total);
        Task<InstituteTypeModel> GetInstituteType(int id);
        Task<InstituteTypeModel> SaveInstituteType(int id, InstituteTypeModel model);
        Task<bool> DeleteInstituteType(int id);
    }
}
