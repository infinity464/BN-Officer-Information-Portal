using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class ExtraAppointmentModel
    {
        public int ExtraAppointmentId { get; set; }
        public Nullable<int> TransferId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> AppointmentId { get; set; }
        public int OfficeId { get; set; }
        [Required]
        public Nullable<System.DateTime> AssignDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual OfficeModel Office { get; set; }
        public virtual OfficeAppointmentModel OfficeAppointment { get; set; }
        public virtual TransferModel Transfer { get; set; }
    }
}