using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class AdvanceSearchModel
    {
        public SelectModel[] AreasSelected { get; set; }
        public SelectModel[] TransferAreasSelected { get; set; }
        public SelectModel[] TrainingInstitutesSelected { get; set; }
        public SelectModel[] TransferAdminAuthoritiesSelected { get; set; }
        public SelectModel[] CourseCountriesSelected { get; set; }
        public SelectModel[] CoursesDoneSelected { get; set; }
        public SelectModel[] CoursesNotDoneSelected { get; set; }
        public SelectModel[] CoursesDoingSelected { get; set; }
        public SelectModel[] CourseSubCategoriesDoneSelected { get; set; }
        public SelectModel[] CourseSubCategoriesNotDoneSelected { get; set; }
        public SelectModel[] CourseSubCategoriesDoingSelected { get; set; }
        public SelectModel[] CourseCategoriesDoneSelected { get; set; }
        public SelectModel[] CourseCategoriesNotDoneSelected { get; set; }
        public SelectModel[] CourseCategoriesDoingSelected { get; set; }
        public SelectModel[] OprRanksSelected { get; set; }
        public SelectModel[] OccasionsSelected { get; set; }
        public SelectModel[] OfficerServicesSelected { get; set; }
        public SelectModel[] ReligionCastsSelected { get; set; }
        public SelectModel[] ReligionsSelected { get; set; }
        public SelectModel[] SubCategoriesSelected { get; set; }
        public SelectModel[] CategoriesSelected { get; set; }
        public SelectModel[] PromotionStatusSelected { get; set; }
        public SelectModel[] PhysicalStructuresSelected { get; set; }
        public SelectModel[] VisitCategoriesSelected { get; set; }
        public SelectModel[] VisitSubCategoriesSelected { get; set; }
        public SelectModel[] CountriesSelected { get; set; }
        public SelectModel[] SubjectsSelected { get; set; }
        public SelectModel[] RanksSelected { get; set; }
        public SelectModel[] MedicalCategoriesSelected { get; set; }
        public SelectModel[] DistrictsSelected { get; set; }
        public SelectModel[] CurrentStatusSelected { get; set; }
        public SelectModel[] CommissionTypesSelected { get; set; }
        public SelectModel[] InstitutesSelected { get; set; }
        public SelectModel[] ExamsSelected { get; set; }
        public SelectModel[] BloodGroupsSelected { get; set; }
        public SelectModel[] SubBranchesSelected { get; set; }
        public SelectModel[] BranchesSelected { get; set; }
        public SelectModel[] BatchesSelected { get; set; }
        public SelectModel[] AdminAuthoritiesSelected { get; set; }
        public SelectModel[] AreaTypesSelected { get; set; }
        public SelectModel[] VisitCountriesSelected { get; set; }
        public SelectModel[] MissionCountriesSelected { get; set; }
        public SelectModel[] ResultsSelected { get; set; }

        public SelectModel[] OfficesSelected { get; set; }
        public SelectModel[] AppointmentsSelected { get; set; }

        public SelectModel[] NotServedOfficesSelected { get; set; }

        public SelectModel[] NotServedAppointmentsSelected { get; set; }

        public SelectModel[] ServingOfficesSelected { get; set; }
        public SelectModel[] ServingAppointmentsSelected { get; set; }
        public SelectModel[] PunishmentCategoriesSelected { get; set; }
        public SelectModel[] PunishmentSubCategoriesSelected { get; set; }
        public SelectModel[] AwardsSelected { get; set; }
        public SelectModel[] MedalsSelected { get; set; }
        public SelectModel[] PublicationsSelected { get; set; }
        public SelectModel[] PublicationCategoriesSelected { get; set; }
        public SelectModel[] CommendationsSelected { get; set; }
        public SelectModel[] AppreciationsSelected { get; set; }
        public SelectModel[] LeaveCountriesSelected { get; set; }
        public SelectModel[] ShipsSelected { get; set; }
        public SelectModel[] ClearancesSelected { get; set; }
        public SelectModel[] DoingCourseCountriesSelected { get; set; }
        public SelectModel[] NotDoneCourseCountriesSelected { get; set; }
        public SelectModel[] AppointmentCategoriesSelected { get; set; }
        public SelectModel[] EducationSubjectsSelected { get; set; }
   
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public int Result { get; set; }
        public string CommissionService { get; set; }
        public string CommissionFromDate { get; set; }
        public string CommissionToDate { get; set; }
        public int CommissionFromDuration { get; set; }
        public int CommissionToDuration { get; set; }
        public int CommissionDurationType { get; set; }
        public string CourseFromDate { get; set; }
        public string CourseToDate { get; set; }
        public double FromResult { get; set; }
        public double ToResult { get; set; }
        public int? DrivingLicense { get; set; }
        public int ForeignVisit { get; set; }
        public int GoneAbroad { get; set; }
        public int Gender { get; set; }
        public int FromFt { get; set; }
        public int FromIn { get; set; }
        public int ToFt { get; set; }
        public int ToIn { get; set; }
        public string JoiningFromDate { get; set; }
        public string JoiningToDate { get; set; }
        public string LprFromDate { get; set; }
        public string LprToDate { get; set; }
        public int MaritalStatus { get; set; }
        public string OfficerName { get; set; }
        public int OprCount { get; set; }
        public int LastValue { get; set; }
        public bool OprAverage { get; set; }
        public string OprCheck { get; set; }
        public double OprGrade { get; set; }
        public int? Passport { get; set; }
        public int? FreedomFighter { get; set; }
        public string RetirementFromDate { get; set; }
        public string RetirementToDate { get; set; }
        public int ReturnFromAbroad { get; set; }
        public string ReturnFromDate { get; set; }
        public string ReturnToDate { get; set; }
        public int ServiceExamCategory { get; set; }
        public int ExamResult { get; set; }
        public int SeaServiceType { get; set; }
        public int NoOfOfficer { get; set; }
        public int SeaFromYear { get; set; }
        public int SeaToYear { get; set; }
        public int TransferArea { get; set; }
        public int ShipType { get; set; }
        public int Mission { get; set; }
        public int DoneMission { get; set; }
        public int MissionTime { get; set; }
        public string BeforeDate { get; set; }
        public string MissionDate { get; set; }
        public int MissionDuration { get; set; }
        public int ServingType { get; set; }
        public bool HodService { get; set; }
        public bool CommandService { get; set; }
        public string TransferFromDate { get; set; }
        public string TransferToDate { get; set; }
        public int TransferMode { get; set; }
        public bool ServedOffice { get; set; }
        public bool NotServedOffice { get; set; }
        public bool ServingOffice { get; set; }
        public bool DoneCourse { get; set; }
        public bool NotDoneCourse { get; set; }
        public bool DoingCourse { get; set; }
        public bool NotServedHodService { get; set; }
        public bool NotServedCommandService { get; set; }

        public bool ServingHodService { get; set; }
        public bool ServingCommandService { get; set; }
        public int Issb { get; set; }
        public int IssbResult { get; set; }
        public double OprToGrade { get; set; }

        public int FromBatch { get; set; }
        public int ToBatch { get; set; }
        public int TransferNo { get; set; }
        public string PromotionFromDate { get; set; }
        public string PromotionToDate { get; set; }
        public int ServiceExtension { get; set; }
        public int StatusType { get; set; }

        public string RlFromDate { get; set; }
        public string RlToDate { get; set; }

        public string VisitFromDate { get; set; }
        public string VisitToDate { get; set; }

        public string LeaveFromDate { get; set; }
        public string LeaveToDate { get; set; }

        public double GpaFrom { get; set; }
        public double GpaTo { get; set; }

    }


}