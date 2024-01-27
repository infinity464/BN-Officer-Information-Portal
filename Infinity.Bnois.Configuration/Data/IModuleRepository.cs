

using Infinity.Bnois.Configuration.Models;
using System.Collections.Generic;
using System.Data;

namespace Infinity.Bnois.Configuration.Data
{
    public interface IModuleRepository : ICompanyConfigRepository<Module>
    {
        List<Module> GetAllModules(int pageSize, int pageNumber, string searchText, out int total);
        DataTable ExecWithSqlQuery(string v);
    }
}
