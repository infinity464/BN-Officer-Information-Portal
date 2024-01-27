using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class RemarkModel
    {
        public int RemarkId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> TransferId { get; set; }
        public Nullable<int> NoteType { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int Type { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
    }
}