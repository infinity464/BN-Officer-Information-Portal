using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class AchievementModel
    {
        public int AchievementId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferId { get; set; }
        public Nullable<int> GivenEmployeeId { get; set; }
        public Nullable<int> GivenTransferId { get; set; }
        public Nullable<int> CommendationId { get; set; }
        public Nullable<int> PatternId { get; set; }
        public Nullable<int> OfficeId { get; set; }
        public string OfficerName { get; set; }
        public string OfficerDesignation { get; set; }
        public int GivenByType { get; set; }
        public int Type { get; set; }
        [Required]
        public Nullable<System.DateTime> Date { get; set; }
        public string CommAppType { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public string FileName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public string TypeName { get; set; }
        public string GivenByTypeName { get; set; }

        public virtual CommendationModel Commendation { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual EmployeeModel Employee1 { get; set; }
        public virtual OfficeModel Office { get; set; }
       
    }
}