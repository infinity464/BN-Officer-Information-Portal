using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data.Models;

namespace Infinity.Bnois.Data
{
    public interface ISearchRepository : IBnoisRepository<Object>
    {

        OfficerViewModel GetSearchOfficers(string userId);

        int InsertMultipleResult();
        int UpdateSearchResult(string userId);
        int DeleteAllSearchParams(string userId);

        int SaveSingleParams(string userId,int genderId, int fromAge, int toAge, int? hasDrivingLicense, int? hasPassport,string joiningFromDate, string joiningToDate, 
            string lprFromDate, string lprToDate, int maritalStatusId, string retirementFromDate, string retirementToDate,
              string commissionFromDate, string commissionToDate, int commissionFromDuration, int commissionToDuration,
            string commissionService,int commissionDurationType,string officerName, int fromFt, double fromIn, int toFt, double toIn,int seaServiceType,int noOfOfficer, int seaFromYear,int seaToYear, 
            int mission,string missionDate,int missionDuration, int serviceExamCategory, int examResult,int returnFromAbroad, string returnFromDate, string returnToDate,int foreignVisit,int goneAbroad,
	        int fromYear,int toYear, int result,string courseFromDate,string courseToDate, double fromResult, double toResult,int oprCount, int lastValue, bool oprAverage, string oprCheck, double oprGrade,int transferArea, 
            int shipType, int doneMission, int missionTime, string beforeDate,int servingType,bool hodService,bool commandService,string transferFromDate,string transferToDate,int transferMode,
            bool servedOffice,bool notServedOffice,bool servingOffice,bool doneCourse, bool notDoneCourse, bool doingCourse, bool notServedHodService, bool notServedCommandService,int? freedomFighter, int issb, int issbResult, 
            double oprToGrade,int fromBatch,int toBatch, int transferNo,bool servingHODServing,bool servingCommandService, string promotionFromDate, string promotionToDate, int serviceExtension, string rlFromDate, string rlToDate,int statusType,
            string visitFromDate, string visitToDate, string leaveFromDate, string leaveToDate, double gpaFrom, double gpaTo);

        int SaveAreasSelected(object value, string userId);
        int SaveTransferAreaSelected(object value, string userId);
	    int SaveTransferAdminAuthoritySelected(object value, string userId);
		int SaveTrainingInstitutesSelected(object value, string userId);
		int SaveMissionCountriesSelected(object value, string userId);
        int SaveCourseCountriesSelected(object value, string userId);
        int SaveCoursesDoneSelected(object value, string userId);
        int SaveNotDoneCourseCountrySelected(object value, string userId);
        int SaveCoursesNotDoneSelected(object value, string userId);
        int SaveCoursesDoingSelected(object value, string userId);
        int SaveCourseSubCategoriesDoneSelected(object value, string userId);
        int SaveCourseSubCategoriesNotDoneSelected(object value, string userId);
        int SaveCourseSubCategoriesDoingSelected(object value, string userId);
        int SaveCourseCategoriesDoneSelected(object value, string userId);
        int SaveCourseCategoriesNotDoneSelected(object value, string userId);
        int SaveCourseCategoriesDoingSelected(object value, string userId);
        int SaveOprRanksSelected(object value, string userId);
        int SaveOccasionsSelected(object value, string userId);
        int SaveOfficerServicesSelected(object value, string userId);
        int SaveReligionCastsSelected(object value, string userId);
        int SaveReligionsSelected(object value, string userId);
        int SaveSubCategoriesSelected(object value, string userId);
        int SaveCategoriesSelected(object value, string userId);
        int SavePromotionStatusSelected(object value, string userId);
        int SavePhysicalStructuresSelected(object value, string userId);
        int SaveVisitCategoriesSelected(object value, string userId);
        int SaveCountriesSelected(object value, string userId);
        int SaveSubjectsSelected(object value, string userId);
        int SaveRanksSelected(object value, string userId);
        int SaveMedicalCategoriesSelected(object value, string userId);
        int SaveDistrictsSelected(object value, string userId);
        int SaveCurrentStatusSelected(object value, string userId);
        int SaveCommissionTypesSelected(object value, string userId);
        int SaveInstitutesSelected(object value, string userId);
        int SaveExamsSelected(object value, string userId);
        int SaveBloodGroupsSelected(object value, string userId);
        int SaveSubBranchesSelected(object value, string userId);
        int SaveBranchesSelected(object value, string userId);
        int SaveBatchesSelected(object value, string userId);
        int SaveAdminAuthoritiesSelected(object value, string userId);
        int SaveAreaTypesSelected(object value, string userId);
        int SaveVisitCountriesSelected(object value, string userId);
        int SaveResultsSelected(object itemValue, string userId);

        int SaveOfficesSelected(object value, string userId);
        int SaveAppointmentsSelected(object value, string userId);
        int SaveVisitSubCategoriesSelected(object value, string userId);
        int SaveAppointmentCategoriesSelected(object value, string userId);

        int SaveNotServedOfficesSelected(object value, string userId);
        int SaveNotServedAppointmentsSelected(object value, string userId);


        int SaveServingOfficesSelected(object value, string userId);
        int SaveServingAppointmentsSelected(object value, string userId);
        int SavePunishmentCategoriesSelected(object value, string userId);
        int SavePunishmentSubCategoriesSelected(object value, string userId);
        int SaveAwardsSelected(object value, string userId);
        int SaveShipSelected(object value, string userId);
        int SaveEducationSubjectSelected(object value, string userId);
        int SaveDoingCourseCountrySelected(object value, string userId);
        int SaveClearanceSelected(object value, string userId);
        int SaveMedalsSelected(object value, string userId);
        int SavePublicationsSelected(object value, string userId);
        int SavePublicationCategoriesSelected(object value, string userId);
        int SaveCommendationsSelected(object value, string userId);
        int SaveAppreciationsSelected(object value, string userId);
        int SaveLeaveCountriesSelected(object value, string userId);
       







        List<string> SelectCheckedColumn(string userId);
      
    }
}
