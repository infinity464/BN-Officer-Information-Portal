using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class OfficeAppointmentModel
    {
        public int OffAppId { get; set; }
        public int OfficeId { get; set; }
        public int AptNatId { get; set; }
        public int AptCatId { get; set; }
        public int AppointmentType { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string NameBangla { get; set; }
        public string ShortNameBangla { get; set; }
        public bool GovtApproved { get; set; }
        public bool HeadofDpt { get; set; }
        public bool OfficeHead { get; set; }
        public bool IsInstrServiceCount { get; set; }
        public int ParentOffAppId { get; set; }
        public bool IsAdditional { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }


        public int[] RankIds { get; set; }
        public int[] BranchIds { get; set; }


        public virtual AptCatModel AptCat { get; set; }
        public virtual AptNatModel AptNat { get; set; }
        public virtual OfficeModel Office { get; set; }
    }
}