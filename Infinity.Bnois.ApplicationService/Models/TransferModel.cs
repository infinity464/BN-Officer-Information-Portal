using System;
using System.ComponentModel.DataAnnotations;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public  class TransferModel
    {
        public int TransferId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferFor { get; set; }
        public int TransferMode { get; set; }
        public int TranferType { get; set; }
        public int TempTransferType { get; set; }
        [Required]
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> CurrentBornOfficeId { get; set; }
        public Nullable<int> AttachOfficeId { get; set; }
        public Nullable<int> AppointmentId { get; set; }
      

        public Nullable<int> AppointmentType { get; set; }
        public Nullable<int> NominationId { get; set; }
        public string FileName { get; set; }
        public bool IsExtraAppointment { get; set; }
        public bool IsBackLog { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }



        public virtual EmployeeModel Employee { get; set; }
        public virtual DistrictModel District { get; set; }

      
        public virtual NominationModel Nomination { get; set; }
        public virtual OfficeModel Office { get; set; }
        public virtual OfficeAppointmentModel OfficeAppointment { get; set; }
        public virtual RankModel Rank { get; set; }

    }

}