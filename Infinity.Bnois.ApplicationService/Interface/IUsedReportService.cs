using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IUsedReportService
    {
        List<UsedReportModel> GetUsedReports(int ps, int pn, string qs, out int total);
        Task<UsedReportModel> GetUsedReport(int id);
        Task<UsedReportModel> SaveUsedReport(int v, UsedReportModel model);
        Task<bool> DeleteUsedReport(int id);
       
    }
}
