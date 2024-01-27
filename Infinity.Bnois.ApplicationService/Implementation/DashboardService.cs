using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class DashboardService : IDashboardService
    {

        private readonly IBnoisRepository<Employee> employeeRepository;

        public DashboardService(IBnoisRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;

        }



        public List<object> GetDashboardOutSideNavy(int officeId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardOutSideNavy] {0} ", officeId));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetDashboardBranch()
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardBranch]"));

            return dataTable.ToJson().ToList();
        }
        public List<object> GetDashboardAdminAuthority()
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardAdminAuthority]"));

            return dataTable.ToJson().ToList();
        }
        public List<object> GetDashboardUnderMission(int rankId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardUnderMission] {0}", rankId));

            return dataTable.ToJson().ToList();
        }
        public List<object> GetDashboardUnderCourse(int rankId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardUnderCourse] {0}", rankId));

            return dataTable.ToJson().ToList();
        }
        public List<object> GetDashboardInsideNavyOrganization()
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashBoardInsideNavyOrganization]"));

            return dataTable.ToJson().ToList();
        }
        public List<object> GetDashboardBCGOrganization()
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashBoardBCGOrganization]"));

            return dataTable.ToJson().ToList();
        }
        public List<object> GetDashboardLeave()
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardLeave]"));

            return dataTable.ToJson().ToList();
        }


        public List<object> GetDashboardExBDLeave()
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardExBDLeave]"));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetDashboardStream(int streamId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardStream] {0}",streamId));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetDashboardCategory(int categoryId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardCategory] {0}",categoryId));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetDashboardGender(int genderId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardGender] {0}", genderId));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetDashboardOfficeAppointment(int officeType, int rankId, int displayId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetDashboardOfficeAppointment] {0},{1},{2}", officeType,rankId,displayId));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetOutsideNavyOfficer(int officeId, int rankLevel,int parentId)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetOutsideNavyOfficer] {0},{1},{2}", officeId,rankLevel, parentId));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetAdminAuthorityOfficer(int officeId, int rankLevel,int type)
        {
            if (type == 1)
            {
                DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetAdminAuthorityOfficer] {0},{1}", officeId, rankLevel));
                return dataTable.ToJson().ToList();
            }
            else if (type == 2)
            {
                DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetInsideNavyOfficer] {0},{1}", officeId, rankLevel));
                return dataTable.ToJson().ToList();
            }
            else
            {
                DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetBCGOfficer] {0},{1}", officeId, rankLevel));
                return dataTable.ToJson().ToList();
            }
           
        }

        public List<object> GetLeaveOfficer(int leaveType, int rankLevel)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetLeaveOfficer] {0},{1}", leaveType, rankLevel));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetExBDLeaveOfficer(int rankLevel)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetExBDLeaveOfficer] {0}", rankLevel));

            return dataTable.ToJson().ToList();
        }

        public List<object> GetBranchOfficer(int rankId, string branch)
        {
            string query= String.Format("exec [spGetBranchOfficer] {0},'{1}'", rankId, branch);
            if (branch == null)
            {
                query = String.Format("exec [spGetBranchOfficer] {0},null", rankId);
            }

            DataTable dataTable = employeeRepository.ExecWithSqlQuery(query);
            
            return dataTable.ToJson().ToList();
        }
        public List<object> GetStreamOfficer(int rankId, string branch,int streamId)
        {
            string query = String.Format("exec [spGetStreamOfficer] {0},'{1}',{2}", rankId, branch, streamId);

            if (branch == null)
            {
                query = String.Format("exec [spGetStreamOfficer] {0},null,{1}", rankId, streamId);
            }
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(query);

            return dataTable.ToJson().ToList();
        }

        public List<object> GetCategoryOfficer(int rankId, string branch, int categoryId)
        {
            string query = String.Format("exec [spGetCategoryOfficer] {0},'{1}',{2}", rankId, branch, categoryId);

            if (branch == null)
            {
                query = String.Format("exec [spGetCategoryOfficer] {0},null,{1}", rankId, categoryId);
            }
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(query);

            return dataTable.ToJson().ToList();
        }

        public List<object> GetGenderOfficer(int rankId, string branch, int genderId)
        {
            string query = String.Format("exec [spGetGenderOfficer] {0},'{1}',{2}", rankId, branch, genderId);

            if (branch == null)
            {
                query = String.Format("exec [spGetGenderOfficer] {0},null,{1}", rankId, genderId);
            }
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(query);

            return dataTable.ToJson().ToList();
        }
    }
}