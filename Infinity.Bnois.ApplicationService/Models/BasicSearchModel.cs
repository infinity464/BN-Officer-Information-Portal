using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class BasicSearchModel
    {
        public object[] AreasSelected { get; set; }
        public object[] RanksSelected { get; set; }
        public object[] TrainingInstitutesSelected { get; set; }
        public object[] CourseCountriesSelected { get; set; }
        public object[] CoursesDoneSelected { get; set; }
        public object[] CoursesNotDoneSelected { get; set; }
        public object[] CoursesDoingSelected { get; set; }
        public object[] CourseSubCategoriesDoneSelected { get; set; }
        public object[] CourseSubCategoriesNotDoneSelected { get; set; }
        public object[] CourseSubCategoriesDoingSelected { get; set; }
        public object[] CourseCategoriesDoneSelected { get; set; }
        public object[] CourseCategoriesNotDoneSelected { get; set; }
        public object[] CourseCategoriesDoingSelected { get; set; }
        public object[] CommissionTypesSelected { get; set; }

        public object[] InstitutesSelected { get; set; }
        public object[] ExamsSelected { get; set; }
        public object[] SubBranchesSelected { get; set; }
        public object[] BranchesSelected { get; set; }
        public object[] AdminAuthoritiesSelected { get; set; }

        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public int Result { get; set; }
        public string Service { get; set; }
        public DateTime CommissionFromDate { get; set; }
        public DateTime CommissionToDate { get; set; }
        public int CommissionDuration { get; set; }
        public string CommissionDurationType { get; set; }
        public DateTime CourseFromDate { get; set; }
        public DateTime CourseToDate { get; set; }
        public int FromResult { get; set; }
        public int ToResult { get; set; }
        public string OfficerName { get; set; }
        public string PNo { get; set; }
        public int SeaService { get; set; }
        public int NoOfOfficer { get; set; }
        public int SeaFromYear { get; set; }
        public int SeaToYear { get; set; }



    }
}