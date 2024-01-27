using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data.Models;

namespace Infinity.Bnois.Data
{
    public class SearchRepository : BnoisRepository<Object>, ISearchRepository
    {
        public SearchRepository(BnoisDbContext context) : base(context)
        {

        }

        public OfficerViewModel GetSearchOfficers(string userId)
        {
            OfficerViewModel officerviewModel = new OfficerViewModel();
            string sql = $@"exec spGetSearchResult '{userId}'";
            officerviewModel.DataTable = ExecWithSqlQuery(sql);           
            List<OfficerGroupModel> groupdata = new List<OfficerGroupModel>();
            for (int i = 0; i < officerviewModel.DataTable.Rows.Count; i++)
            {
                OfficerGroupModel model = new OfficerGroupModel();
                model.Seniority = officerviewModel.DataTable.Rows[i]["Seniority"].ToString();
                groupdata.Add(model);
            }
 
            groupdata = groupdata.ToList();
            officerviewModel.GetOfficerGroupModels =
        groupdata.GroupBy(c => new
        {
            c.Seniority,

        })
        .Select(gcs => new OfficerGroupModel()
        {
            Seniority = gcs.Key.Seniority,


        }).ToList();

            return officerviewModel;

        }

        public int UpdateSearchResult(string userId)
        {
            string sql = $@"exec spUpdateSearchResult '{userId}'";
            return ExecNoneQuery(sql);
        }

        public int InsertMultipleResult()
        {
            string sql = $@"exec spInsertMultipleResult";
            return ExecNoneQuery(sql);
        }

        public int SaveSingleParams(string userId, int genderId, int fromAge, int toAge, int? hasDrivingLicense, int? hasPassport, string joiningFromDate, string joiningToDate, string lprFromDate, string lprToDate,
            int maritalStatusId, string retirementFromDate, string retirementToDate, string commissionFromDate, string commissionToDate, int commissionFromDuration,
            int commissionToDuration, string commissionService, int commissionDurationType, string officerName, int fromFt, double fromIn, int toFt, double toIn, int seaServiceType, int noOfOfficer, int seaFromYear, int seaToYear,
            int mission, string missionDate, int missionDuration, int serviceExamCategory, int examResult, int returnFromAbroad, string returnFromDate, string returnToDate, int foreignVisit, int goneAbroad, int fromYear, int toYear, int result,
             string courseFromDate, string courseToDate, double fromResult, double toResult, int oprCount, int lastValue, bool oprAverage, string oprCheck, double oprGrade, int transferArea, int shipType, int doneMission, int missionTime, string beforeDate,
             int servingType, bool hodService, bool commandService, string transferFromDate, string transferToDate, int transferMode, bool servedOffice, bool notServedOffice, bool servingOffice, bool doneCourse, bool notDoneCourse, bool doingCourse, bool notServedHodService,
            bool notServedCommandService, int? freedomFighter, int issb, int issbResult, double oprToGrade, int fromBatch, int toBatch, int transferNo, bool servingHODServing, bool servingCommandService, string promotionFromDate, string promotionToDate, int serviceExtension,
            string rlFromDate, string rlToDate, int statusType, string visitFromDate, string visitToDate, string leaveFromDate, string leaveToDate, double gpaFrom, double gpaTo)
        {
            string saveQuery = String.Format(@"insert into SearchSingleParams values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}','{60}','{61}','{62}','{63}','{64}','{65}','{66}','{67}','{68}','{69}','{70}','{71}','{72}','{73}','{74}','{75}','{76}','{77}','{78}','{79}','{80}','{81}','{82}','{83}','{84}','{85}','{86}','{87}','{88}','{89}')",
                userId, genderId, fromAge, toAge, hasDrivingLicense, hasPassport, joiningFromDate, joiningToDate, lprFromDate, lprToDate, maritalStatusId, retirementFromDate, retirementToDate
                , commissionFromDate, commissionToDate, commissionFromDuration, commissionToDuration, commissionService, commissionDurationType, officerName, fromFt, fromIn, toFt, toIn, seaServiceType,
                noOfOfficer, seaFromYear, seaToYear, mission, missionDate, missionDuration, serviceExamCategory, examResult, returnFromAbroad, returnFromDate, returnToDate, foreignVisit, goneAbroad,
                fromYear, toYear, result, courseFromDate, courseToDate, fromResult, toResult, oprCount, lastValue, oprAverage, oprCheck, oprGrade, transferArea, shipType, doneMission, missionTime,
                beforeDate, servingType, hodService, commandService, transferFromDate, transferToDate, transferMode, servedOffice, notServedOffice, servingOffice, doneCourse, notDoneCourse, doingCourse,
                notServedHodService, notServedCommandService, freedomFighter, issb, issbResult, oprToGrade, fromBatch, toBatch, transferNo, servingHODServing, servingCommandService, promotionFromDate, promotionToDate, 
                serviceExtension,rlFromDate,rlToDate,statusType,visitFromDate,visitToDate,leaveFromDate,leaveToDate,gpaFrom,gpaTo);

            ExecNoneQuery(saveQuery);
            if (hasDrivingLicense == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set HasDrivingLicense=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (hasPassport == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set HasPassport=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }


            if (freedomFighter == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set FreedomFighter=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }


            if (joiningFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set JoiningFromDate=null,JoiningToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (joiningFromDate != null && joiningToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set JoiningToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (lprFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set LprFromDate=null,LprToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (lprFromDate != null && lprToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set LprToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (rlFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set RlFromDate=null,RlToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (rlFromDate != null && rlToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set RlToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (visitFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set VisitFromDate=null,VisitToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (visitFromDate != null && visitToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set VisitToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (retirementFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set RetirementFromDate=null,RetirementToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (retirementFromDate != null && retirementToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set RetirementToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (commissionFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set CommissionFromDate=null,CommissionToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (commissionFromDate != null && commissionToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set CommissionToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (returnFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set ReturnFromDate=null,ReturnToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (returnFromDate != null && returnToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set ReturnToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }


            if (courseFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set courseFromDate=null,CourseToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (courseFromDate != null && courseToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set CourseToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (transferFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set TransferFromDate=null,TransferToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (transferFromDate != null && transferToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set TransferToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (promotionFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set PromotionFromDate=null,PromotionToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (promotionFromDate != null && promotionToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set PromotionToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (leaveFromDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set LeaveFromDate=null,LeaveToDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (leaveFromDate != null && leaveToDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set LeaveToDate=getDate() where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (missionDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set MissionDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }

            if (beforeDate == null)
            {
                string updateQuery = String.Format(@"Update SearchSingleParams Set BeforeDate=null where userId='{0}'", userId);
                ExecNoneQuery(updateQuery);
            }
            return 0;

        }




        public int SaveNotDoneCourseCountrySelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchNotDoneCourseCountryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveDoingCourseCountrySelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchDoingCourseCountryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveShipSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchShipParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveEducationSubjectSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchEducationSubjectParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }


        public int SaveClearanceSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchClearanceParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }



        public int SaveAwardsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchAwardParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }


        public int SaveMedalsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchMedalParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SavePublicationsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchPublicationParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SavePublicationCategoriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchPublicationCategoryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveCommendationsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCommendationParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveAppreciationsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchAppreciationParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveLeaveCountriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchLeaveCountryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }



        public int SaveAreasSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchZoneParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }



        public int SavePunishmentCategoriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchPunishmentCategoryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }


        public int SavePunishmentSubCategoriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchPunishmentSubCategoryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }


        public int SaveResultsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchResultParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveOfficesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchOfficeParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveAppointmentsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchAppointmentParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }



        public int SaveVisitSubCategoriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchVisitSubCategoryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }


        public int SaveAppointmentCategoriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchAppointmentCategoryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }




        public int SaveNotServedOfficesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchNotServedOfficeParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveNotServedAppointmentsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchNotServedAppointmentParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }



        public int SaveServingOfficesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchServingOfficeParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveServingAppointmentsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchServingAppointmentParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }





        public int SaveTransferAreaSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchTransferAreaParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }


        public int SaveTransferAdminAuthoritySelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchTransferAdminAuthorityParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);

        }

        public int SaveTrainingInstitutesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchTrainingInstituteParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCourseCountriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCourseCountryParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCoursesDoneSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCourseDoneParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCoursesNotDoneSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCourseNotDoneParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveMissionCountriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchMissionCountryParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCoursesDoingSelected(object value, string userId)
        {
            string saveQuery =
                String.Format(@"insert into SearchCourseDoingParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCourseSubCategoriesDoneSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCourseSubCategoryDoneParams values ('{0}','{1}')",
                value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCourseSubCategoriesNotDoneSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCourseSubCategoryNotDoneParams values ('{0}','{1}')",
                value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCourseSubCategoriesDoingSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCourseSubCategoryDoingParams values ('{0}','{1}')",
                value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCourseCategoriesDoneSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCourseCategoryDoneParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCourseCategoriesNotDoneSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCourseCategoryNotDoneParams values ('{0}','{1}')",
                value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCourseCategoriesDoingSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCourseCategoryDoingParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveOprRanksSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchOprRankParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveOccasionsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchOccasionParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveOfficerServicesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchOfficerServiceParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveReligionCastsSelected(object value, string userId)
        {
            string saveQuery =
                String.Format(@"insert into SearchReligionCastParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveReligionsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchReligionParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveSubCategoriesSelected(object value, string userId)
        {
            string saveQuery =
                String.Format(@"insert into SearchSubCategoryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCategoriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCategoryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SavePromotionStatusSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchPromotionStatusParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SavePhysicalStructuresSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchPhysicalStructureParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveVisitCategoriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchVisitCategoryParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCountriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCountryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveSubjectsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchSubjectParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveRanksSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchRankParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveMedicalCategoriesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchMedicalCategoryParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveDistrictsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchDistrictParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCurrentStatusSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCurrentStatusParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveCommissionTypesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchCommissionTypeParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveInstitutesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchInstituteParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveExamsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchExamParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveBloodGroupsSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchBloodGroupParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveSubBranchesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchSubBranchParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveBranchesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchBranchParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveBatchesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchBatchParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveAdminAuthoritiesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchAdminAuthorityParams values ('{0}','{1}')", value,
                userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveAreaTypesSelected(object value, string userId)
        {
            string saveQuery = String.Format(@"insert into SearchAreaTypeParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }

        public int SaveVisitCountriesSelected(object value, string userId)
        {
            string saveQuery =
                String.Format(@"insert into SearchVisitCountryParams values ('{0}','{1}')", value, userId);
            return ExecNoneQuery(saveQuery);
        }






        //----------------------------------------
        public int DeleteAllSearchParams(string userId)
        {
            string sql = $@"exec spDeleteAllSearchParams '{userId}'";
            return ExecNoneQuery(sql);
        }

        public List<string> SelectCheckedColumn(string userId)
        {
            string query = String.Format(@"select Value from CheckedColumn where UserId='{0}'", userId);
            return context.Database.SqlQuery<string>(query).ToList();
        }
    }
}
