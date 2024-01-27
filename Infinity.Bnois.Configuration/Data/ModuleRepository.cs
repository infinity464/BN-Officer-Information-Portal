
using Infinity.Bnois.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infinity.Bnois.Configuration.Data
{
    public class ModuleRepository : CompanyConfigRepository<Module>, IModuleRepository
    {
        internal SqlConnection Connection;
        public ModuleRepository(ConfigurationDbContext context) : base(context)
        {
            Connection = context.Database.Connection as SqlConnection;
        }

        public List<Module> GetAllModules(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<Module> moduleQuery = context.Modules.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                moduleQuery = moduleQuery.Where(x => x.Name.Contains(searchText));
                total = moduleQuery.Count();
                return moduleQuery.OrderByDescending(x => x.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

            total = moduleQuery.Count();
            return moduleQuery.OrderByDescending(x => x.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public DataTable ExecWithSqlQuery(string query)
        {
            try
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(query, Connection);
                DataTable dt = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                return dt;
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                if
                    (Connection.State == ConnectionState.Open)
                {

                    Connection.Close();
                }
            }
        }
    }
}
