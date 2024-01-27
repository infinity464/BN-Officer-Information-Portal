using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IBulkUserService
    {
        List<object> UserList(int pageSize, int pageNumber, string searchText, out int total);
        object GetAdminDashboard();
    }
}
