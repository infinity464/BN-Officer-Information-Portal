using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class UpazilaModel
    {

        public int UpazilaId { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual DistrictModel District { get; set; }
        public virtual Division Division { get; set; }

    }
}