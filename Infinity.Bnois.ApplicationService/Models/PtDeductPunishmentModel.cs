using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class PtDeductPunishmentModel
    {
        public int PtDeductPunishmentId { get; set; }
        public int TraceSettingId { get; set; }
        public int PunishmentSubCategoryId { get; set; }
        public int PunishmentNatureId { get; set; }
        public double PunishmentValue { get; set; }
        public double SkipYear { get; set; }
        public double DeductPercentage { get; set; }
        public int DeductionYear { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual PunishmentNatureModel PunishmentNature { get; set; }
        public virtual PunishmentSubCategoryModel PunishmentSubCategory { get; set; }
        public virtual TraceSettingModel TraceSetting { get; set; }
    }
}
