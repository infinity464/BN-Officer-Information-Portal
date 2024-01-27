using System;
using System.ComponentModel.DataAnnotations;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmployeePftModel
    {
        public int EmployeePftId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> PftTypeId { get; set; }
        public Nullable<int> PftResultId { get; set; }
        [Required]
        public Nullable<DateTime> PftDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual PftResultModel PftResult { get; set; }
        public virtual PftTypeModel PftType { get; set; }
    }
}