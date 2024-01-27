using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data.Models;

namespace Infinity.Bnois.Data
{
  public class ProcessRepository : BnoisRepository<Object>,IProcessRepository
    {
        public ProcessRepository(BnoisDbContext context) : base(context)
        {

        }

		public async Task<int>  UpdateNamingConvention(int employeeId)
		{
	
			string sql = @"[SpBuildNamingConvention] @EmployeeId";
			var empIdParams = new SqlParameter("@EmployeeId", employeeId);
			return await this.context.Database.ExecuteSqlCommandAsync(sql, empIdParams);
		}



	}
}
