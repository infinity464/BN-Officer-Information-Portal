using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeReportService
    {
       dynamic GetEmployeeReports();
        Task<EmployeeReportModel> GetEmployeeReport(int id);
        Task<EmployeeReportModel> SaveEmployeeReport(int v, EmployeeReportModel model);
        Task<bool> DeleteEmployeeReport(int id);
        bool DeleteEmployeeReports();

        int SaveGraphicalReport(int fromYear, int toYear, string userId);

        Chart GetOprYearlyChart(int fromYear, int toYear, string userId);
        Chart GetTraceChart( string userId);
        Chart GetCourseResultChart(int categoryId, int? subCategoryId,int venue, string userId);

        Chart GetLastOPRChart(int lastOprNo, string userId);
        Chart GetSeaServiceChart( string userId);
        Chart GetSeaCommandServiceChart( string userId);


    }
}
