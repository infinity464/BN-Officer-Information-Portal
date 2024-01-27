using System;
using System.ComponentModel.DataAnnotations;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmployeeOprModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> OfficeId { get; set; }
        public int OprRankId { get; set; }
        public int OccasionId { get; set; }
        public double OprGrade { get; set; }
        public string GradingStatus { get; set; }
        [Required]
        public Nullable<System.DateTime> OprFromDate { get; set; }
        [Required]
        public Nullable<System.DateTime> OprToDate { get; set; }
        public Nullable<int> RecomandationTypeId { get; set; }
        public string FileName { get; set; }
        public string ImageSec2 { get; set; }
        public string ImageSec4 { get; set; }
        public bool IsAdverseRemark { get; set; }
        public string AdverseRemark { get; set; }
        public string Section2 { get; set; }
        public string Section3 { get; set; }
        public string Section4 { get; set; }
        public Nullable<double> Overweight { get; set; }
        public Nullable<double> Underweight { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string ApptRecom { get; set; }
        public Nullable<int> BOffCId { get; set; }
        public Nullable<int> AppoinmentId { get; set; }

        public string OtherAppointment { get; set; }


        public string FileUrl { get; set; }
        public string Section2ImageUrl { get; set; }
        public string Section4ImageUrl { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual OfficeModel Office { get; set; }
        public virtual Office Office1 { get; set; }
        public virtual RankModel Rank { get; set; }
        public virtual OprOccasionModel OprOccasion { get; set; }
        public virtual OfficeAppointmentModel OfficeAppointment { get; set; }
        public virtual RecomandationTypeModel RecomandationType { get; set; }

        public OprAptSuitability EmployeeOprAptSuitability;
    }
}