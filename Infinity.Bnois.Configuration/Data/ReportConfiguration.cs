using Infinity.Bnois.Configuration.Models;
using System.Collections.Generic;
using System.Linq;


namespace Infinity.Bnois.Configuration.Data
{
    public static class ReportConfiguration
    {
        private static List<Report> _reports;
        private static List<ReportPropery> _reportPropertyList;
        static ReportConfiguration()
        {
            _reports = SetReportList();
            _reportPropertyList = SetReportPropertyList();
        }
        private static List<Report> SetReportList()
        {
            return new List<Report>()
            {
               new Report(){ReportId=1,ReportTitle="SubjectList"},
               new Report(){ReportId=2,ReportTitle="PostList"},
               new Report(){ReportId=3,ReportTitle="UpazilaList"},
               new Report(){ReportId=4,ReportTitle="Post_SubjectList"},
               new Report(){ReportId=5,ReportTitle="BoardList"},
               new Report(){ReportId=6,ReportTitle="UniversityList"},
               new Report(){ReportId=7,ReportTitle="DistrictList"},
               new Report(){ReportId=8,ReportTitle="DivissionList"},
               new Report(){ReportId=9,ReportTitle="ExamCategoryList"},
               new Report(){ ReportId=10,ReportTitle="ExaminationCenterList"},
               new Report(){ ReportId=11,ReportTitle="FreedomFighterList"},
               new Report(){ ReportId=12,ReportTitle="GenderList"},
               new Report(){ ReportId=13,ReportTitle="GradeList"},
               new Report(){ ReportId=14,ReportTitle="ResultList"},
               new Report(){ ReportId=15,ReportTitle="OrganizationList"}

            };
        }
        public static List<Report> GetReportList()
        {
            return _reports;
        }

        public static List<ReportPropery> SetReportPropertyList()
        {
            return new List<ReportPropery>()
            {
               new ReportPropery(){ReportId=1,RiportName="SubjectList.rdlc",DataSet="SubjectDataSet",ProcedureName="spSubjectList"},
               new ReportPropery(){ReportId=2,RiportName="PostList.rdlc",DataSet="PostDataSet",ProcedureName="spPostList"},
               new ReportPropery(){ReportId=3,RiportName="UpazillaList.rdlc",DataSet="UpazillaDataSet",ProcedureName="spUpazilaList"},
               new ReportPropery(){ReportId=4,RiportName="PostSubjectList.rdlc",DataSet="PostSubjectDataSet",ProcedureName="spPostSubjectList"},
               new ReportPropery(){ReportId=5,RiportName="BoardList.rdlc",DataSet="BoardDataSet",ProcedureName="spBoardList"},
               new ReportPropery(){ReportId=6,RiportName="UniversityList.rdlc",DataSet="UniversityDataSet",ProcedureName="spUniversityList"},
               new ReportPropery(){ReportId=7,RiportName="DistrictList.rdlc",DataSet="DistrictDataSet",ProcedureName="spDistrictList"},
               new ReportPropery(){ReportId=8,RiportName="DivissionList.rdlc",DataSet="DivissionDataSet",ProcedureName="spDivissionList"},
               new ReportPropery(){ReportId=9,RiportName="ExamCategoryList.rdlc",DataSet="ExamCategoryDataSet",ProcedureName="spExamCategoryList"},
               new ReportPropery(){ReportId=10,RiportName="ExaminationCenterList.rdlc",DataSet="ExaminationCenterDataSet",ProcedureName="spExaminationCenterList"},
               new ReportPropery(){ReportId=11,RiportName="FreedomFighterList.rdlc",DataSet="FreedomFighterDataSet",ProcedureName="spFreedomFighterList"},
               new ReportPropery(){ReportId=12,RiportName="GenderList.rdlc",DataSet="GenderDataSet",ProcedureName="spGenderList"},
               new ReportPropery(){ReportId=13,RiportName="GradeList.rdlc",DataSet="GraderDataSet",ProcedureName="spGradeList"},
               new ReportPropery(){ReportId=14,RiportName="ResultList.rdlc",DataSet="ResultDataSet",ProcedureName="spResultList"},
               new ReportPropery(){ReportId=15,RiportName="OrganizationList.rdlc",DataSet="OrganizationDataSet",ProcedureName="spOrganizationList"}
            };
        }

        public static ReportPropery GetReportPropertyByReportId(int reportId)
        {
            return _reportPropertyList.FirstOrDefault(x => x.ReportId == reportId);
        }
        public static Report GetReport(int reportId)
        {
            return _reports.FirstOrDefault(x => x.ReportId == reportId);
        }
    }
}
