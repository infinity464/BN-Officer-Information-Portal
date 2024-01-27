
using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class PunishmentAccidentModel
    {

        public int PunishmentAccidentId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> PunishmentCategoryId { get; set; }
        public Nullable<int> PunishmentSubCategoryId { get; set; }
        public Nullable<int> PunishmentNatureId { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferId { get; set; }
        public int Type { get; set; }
        public Nullable<int> AccedentType { get; set; }
        public Nullable<int> DurationMonths { get; set; }
        public Nullable<int> DurationDays { get; set; }
        [Required]
        public Nullable<System.DateTime> Date { get; set; }
        public string PunishmentType { get; set; }
        public string Remarks { get; set; }
        public string Reason { get; set; }
        
        public double PunishmentValue { get; set; }
        public double SkipYear { get; set; }
        public double DeductPercentage { get; set; }
        public int DeductYear { get; set; }
        public double PtAfterDeduct { get; set; }
        public int YearCount { get; set; }
        public bool IsProcessed { get; set; }
        public string FileName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public string TypeName { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual RankModel Rank { get; set; }
        public virtual PunishmentCategoryModel PunishmentCategory { get; set; }
        public virtual PunishmentNatureModel PunishmentNature { get; set; }
        public virtual PunishmentSubCategoryModel PunishmentSubCategory { get; set; }
    }
}