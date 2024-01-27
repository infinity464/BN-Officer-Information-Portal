using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class ObservationIntelligentModel
    {
        public int ObservationIntelligentId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferId { get; set; }
        public Nullable<int> GivenEmployeeId { get; set; }
        public Nullable<int> GivenTransferId { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        [Required]
        public Nullable<System.DateTime> Date { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual EmployeeModel Employee1 { get; set; }
    }
}