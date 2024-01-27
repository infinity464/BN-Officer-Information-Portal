using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class PunishmentSubCategoryModel
    {
        public int PunishmentSubCategoryId { get; set; }
        public int PunishmentCategoryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool GotoTrace { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual PunishmentCategoryModel PunishmentCategory { get; set; }
    }
}