using Infinity.Bnois.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Data
{
   public interface IBnoisRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DataTable ExecWithStoreProcedure(string query, IDictionary<string, object> values);
        DataTable ExecWithSqlQuery(string query);
        int ExecNoneQuery(string query);
    }
}
