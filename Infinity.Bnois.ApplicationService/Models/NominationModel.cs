using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class NominationModel
    {

        public int NominationId { get; set; }
        public int EnitityType { get; set; }
        public int EntityId { get; set; }
        [Required]
        public Nullable<System.DateTime> EntryDate { get; set; }
        public bool WithoutTransfer { get; set; }
        public bool WithoutAppointment { get; set; }
        public string Title { get; set; }
        public Nullable<int> MissoinAppointmentId { get; set; }
        public string Remarks { get; set; }
        public string NominationTypeName { get; set; }

        public virtual MissionAppointmentModel MissionAppointment { get; set; }


    }
}