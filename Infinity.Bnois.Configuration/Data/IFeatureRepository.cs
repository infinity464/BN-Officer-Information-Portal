
using Infinity.Bnois.Configuration.Models;
using System.Collections.Generic;
using System.Data;

namespace Infinity.Bnois.Configuration.Data
{
    public interface IFeatureRepository : ICompanyConfigRepository<Feature>
    {
        List<Feature> GetAllFeatures(int pageSize, int pageNumber, string catagory, string searchText, out int total);
        DataTable ExecWithSqlQuery(string v);
    }
}
