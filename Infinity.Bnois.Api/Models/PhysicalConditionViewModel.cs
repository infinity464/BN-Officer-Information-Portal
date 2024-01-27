using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    internal class PhysicalConditionViewModel
    {
        public PhysicalConditionModel PhysicalCondition { get; set; }
        public FileModel File { get; set; }
        public List<SelectModel> EyeColors { get; set; }
        public List<SelectModel> SkinColors { get; set; }
        public List<SelectModel> HairColors { get; set; }
        public List<SelectModel> EyeVisions { get; set; }
        public List<SelectModel> BloodGroups { get; set; }
        public List<SelectModel> MedicalCategories { get; set; }
        public List<SelectModel> PhysicalStructures { get; set; }
    }
}
