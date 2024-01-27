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
  public class EmployeeReportRepository : BnoisRepository<EmployeeReport>,IEmployeeReportRepository
    {
        public EmployeeReportRepository(BnoisDbContext context) : base(context)
        {

        }

        public int SaveGraphReport(int year, int employeeId, string userId)
        {
            string saveQuery = String.Format(@"insert into GraphReport values ({0},{1},{2},'{3}')", year, employeeId,0, userId);

            return context.Database.ExecuteSqlCommand(saveQuery);
        }

        public int DeleteGraphReport(string userId)
        {
            string saveQuery = String.Format(@"delete GraphReport where UserId='{0}'", userId);

            return context.Database.ExecuteSqlCommand(saveQuery);
        }

        public List<EmployeeReportDataModel> GetOprGraphListReport(int lastOprNo,string userId)
        {
            string saveQuery = String.Format(@"exec spGetOprGraphListReport  {0},'{1}'", lastOprNo, userId);

            return context.Database.SqlQuery<EmployeeReportDataModel>(saveQuery).ToList();
        }

        public List<EmployeeReportDataModel> GetGetOprGraphYearlyReport( string userId)
        {
            string saveQuery = String.Format(@"exec spGetOprGraphYearlyReport  '{0}'",  userId);

            return context.Database.SqlQuery<EmployeeReportDataModel>(saveQuery).ToList();
        }


        public List<EmployeeReportDataModel> GetSeaServiceGraph(string userId)
        {
            string saveQuery = String.Format(@"exec spGetSeaServiceGraphicalReport    '{0}'", userId);

            return context.Database.SqlQuery<EmployeeReportDataModel>(saveQuery).ToList();
        }

        public List<EmployeeReportDataModel> GetTraceGraph(int employeeId,string userId)
        {
            string saveQuery = String.Format(@"exec [dbo].[spGetTraceGraphicalReport]    @EmployeeId='{0}',@UserId='{1}'",employeeId, userId);

            return context.Database.SqlQuery<EmployeeReportDataModel>(saveQuery).ToList();
        }

       


        public List<EmployeeReportDataModel> GetSeaCommandServiceGraph(string userId)
        {
            string saveQuery = String.Format(@"exec spGetSeaCommandServiceGraphicalReport  '{0}'", userId);

            return context.Database.SqlQuery<EmployeeReportDataModel>(saveQuery).ToList();
        }

        public List<EmployeeReportDataModel> GetCourseResultGraph(int categoryId, int? subCategoryId, int venue, string userId)
        {
            string saveQuery = String.Format(@"exec spGetCourseResultGraphicalReport {0},{1},{2},'{3}'",categoryId,subCategoryId,venue,userId);

            return context.Database.SqlQuery<EmployeeReportDataModel>(saveQuery).ToList();
        }


    }
}
