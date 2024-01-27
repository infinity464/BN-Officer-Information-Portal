using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data.Models;

namespace Infinity.Bnois.Data
{
    public interface IEmployeeReportRepository : IBnoisRepository<EmployeeReport>
    {

        int SaveGraphReport(int year, int employeeId, string userId);
        List<EmployeeReportDataModel> GetOprGraphListReport(int lastOprNo, string userId);

        List<EmployeeReportDataModel> GetGetOprGraphYearlyReport(string userId);
        List<EmployeeReportDataModel> GetSeaServiceGraph(string userId);
        List<EmployeeReportDataModel> GetTraceGraph(int employeeId, string userId);
        List<EmployeeReportDataModel> GetSeaCommandServiceGraph(string userId);
        List<EmployeeReportDataModel> GetCourseResultGraph(int categoryId, int? subCategoryId, int venue,string userId);
        int DeleteGraphReport(string userId);
    }
}
