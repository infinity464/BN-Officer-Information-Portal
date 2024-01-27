using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class MissionAppointmentModel
    {
        public int MissionAppointmentId { get; set; }
        public int AppointmentCategoryId { get; set; }
        public int AppointmentNatureId { get; set; }
        public int MissionScheduleId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }


        public int[] RankIds { get; set; }
        public int[] BranchIds { get; set; }


        public virtual AptCatModel AptCat { get; set; }
        public virtual AptNatModel AptNat { get; set; }

        public virtual NominationScheduleModel NominationSchedule { get; set; }
    }
}