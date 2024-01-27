using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class OprAptSuitabilityModel
    {
        public int Id { get; set; }
        public int EmployeeOprId { get; set; }
        public int SpecialAptTypeId { get; set; }
        public int SuitabilityId { get; set; }
        public string Note { get; set; }


        public virtual EmployeeOprModel EmployeeOpr { get; set; }
        public virtual SpecialAptTypeModel SpecialAptType { get; set; }
        public virtual SuitabilityModel Suitability { get; set; }
    }
}