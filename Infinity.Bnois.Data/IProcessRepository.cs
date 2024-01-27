using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data.Models;

namespace Infinity.Bnois.Data
{
    public interface IProcessRepository : IBnoisRepository<Object>
    {

		 Task<int> UpdateNamingConvention(int employeeId);

    }
}
