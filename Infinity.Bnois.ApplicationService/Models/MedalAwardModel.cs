using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class MedalAwardModel
    {
        public int MedalAwardId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> MedalId { get; set; }
        public Nullable<int> AwardId { get; set; }
        public Nullable<int> PublicationId { get; set; }
        public Nullable<int> PublicationCategoryId { get; set; }
        public Nullable<int> Type { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferId { get; set; }
        [Required]
        public Nullable<System.DateTime> Date { get; set; }
        public string Remarks { get; set; }
        public string FileName { get; set; }
        public string TypeName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual AwardModel Award { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual MedalModel Medal { get; set; }
        public virtual PublicationModel Publication { get; set; }
        public virtual PublicationCategoryModel PublicationCategory { get; set; }
    }
}