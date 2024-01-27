using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class BulkUserService : IBulkUserService
    {
        private readonly IBnoisRepository<Employee> employeeRepository;
        BnoisDbContext db = new BnoisDbContext();
        public BulkUserService(IBnoisRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public object GetAdminDashboard()
        {
            DataTable dataTable;
            dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [getDashboardData]"));
            return dataTable.ToJson().ToList();
        }

        public List<object> UserList(int pageSize, int pageNumber, string searchText, out int total)
        {
            DataTable dataTable;
            if (searchText != null)
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [getEmployee] {0},{1},'{2}'", pageSize, pageNumber, searchText));
                total =  db.Database.SqlQuery<int>("Select Count(*) from Employee E left join EmployeeGeneral EG on E.EmployeeId = EG.EmployeeId where E.PNo not in (Select PNo from Administration.AspNetUsers where PNo is not  null) and EG.ContactNo like '%"+searchText+"%'").SingleOrDefault();
            }
            else
            {
                dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [getEmployee] {0},{1}", pageSize, pageNumber));
                total = db.Database.SqlQuery<int>("Select Count(*) from Employee E left join EmployeeGeneral EG on E.EmployeeId = EG.EmployeeId where E.PNo not in (Select PNo from Administration.AspNetUsers where PNo is not  null)").SingleOrDefault();
            }
            return dataTable.ToJson().ToList();
        }

        public bool IsEmployeeActive(string pNo)
        {
            bool doesEmployeeActive = false;
            if(pNo != null)
            {
                int employeeId = db.Database.SqlQuery<int>("Select EmployeeId from Employee where PNo='%" + pNo + "%'and EmployeeStatusId=1").SingleOrDefault();
                doesEmployeeActive = employeeId > 0 ? true : false;
            }
            else
            {
                doesEmployeeActive = false;
            }
            return doesEmployeeActive;
        }
    }
}
