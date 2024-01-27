using System;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.Api.Models
{
    public class TrainingInstituteModel
    {
        public int InstituteId { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        public string NameInBangla { get; set; }

        public string AddressInfo { get; set; }

        public int CountryType { get; set; }

        public int CountryId { get; set; }

        public string CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }



        public virtual CountryModel Country { get; set; }
    }
}