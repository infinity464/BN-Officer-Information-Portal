using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class PhysicalConditionModel
    {
        public int PhysicalConditionId { get; set; }
        public int EmployeeId { get; set; }
        public int EyeColorId { get; set; }
        public int SkinColorId { get; set; }
        public int HairColorId { get; set; }
        public int EyeVisionId { get; set; }
        public int BloodGroupId { get; set; }
        public int PhysicalStructureId { get; set; }
        public int MedicalCategoryId { get; set; }
        public string FileName { get; set; }
        public bool IsPerMedicalCategory { get; set; }
        public double HeightInFeet { get; set; }
        public double HeightInInc { get; set; }
        public double HeightInCM { get; set; }
        public bool IsHeightInCM { get; set; }
        public double Weight { get; set; }
        public string IdentificationMark { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual BloodGroupModel BloodGroup { get; set; }
        public virtual ColorModel Color { get; set; }
        public virtual ColorModel Color1 { get; set; }
        public virtual ColorModel Color2 { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual EyeVisionModel EyeVision { get; set; }
        public virtual MedicalCategoryModel MedicalCategory { get; set; }
        public virtual PhysicalStructureModel PhysicalStructure { get; set; }
    }
}
