using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IUsedStoreProcedureService
    {
        List<UsedStoreProcedureModel> GetUsedStoreProcedures(int id);
        Task<UsedStoreProcedureModel> SaveUsedStoreProcedure(int id, UsedStoreProcedureModel model);
        Task<bool> DeleteUsedStoreProcedure(int id);
    
    }
}
