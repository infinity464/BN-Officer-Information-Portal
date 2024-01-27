using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
  public  class CountryModel
    {
        public int CountryId { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Iso { get; set; }
        public string Nationality { get; set; }
        public string Iso3 { get; set; }
        public string NumCode { get; set; }
        public string PhoneCode { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
