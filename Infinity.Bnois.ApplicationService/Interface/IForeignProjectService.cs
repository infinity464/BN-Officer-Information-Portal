using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IForeignProjectService
    {
        List<ForeignProjectModel> GetForeignProjects(int ps, int pn, string qs, out int total);
        Task<ForeignProjectModel> GetForeignProject(int id);
        Task<ForeignProjectModel> SaveForeignProject(int v, ForeignProjectModel model);
        Task<bool> DeleteForeignProject(int id);
   

    }
}
