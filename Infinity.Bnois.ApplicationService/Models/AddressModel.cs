using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
  public  class AddressModel
    {
        public int AddressId { get; set; }
        public int EmployeeId { get; set; }
        public int AddressType { get; set; }
        public string AddressTypeName { get; set; }
        public string CareOf { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int UpazilaId { get; set; }
        public string AddressDetailBangla { get; set; }
        public string AddressDetailEnglish { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string PostOfficeName { get; set; }
        public string PostCode { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual DistrictModel District { get; set; }
        public virtual DivisionModel Division { get; set; }
        public virtual UpazilaModel Upazila { get; set; }
    }
}
