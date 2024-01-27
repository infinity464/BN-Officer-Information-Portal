using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class StatusChangeModel
    {
        public int StatusChangeId { get; set; }
        public int EmployeeId { get; set; }
        public int PreviousId { get; set; }
        public int NewId { get; set; }
        public int StatusType { get; set; }
        [Required]
        public Nullable<System.DateTime> Date { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
    }
}