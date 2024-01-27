using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class CourseCategoryModel
    {
        public int CourseCategoryId { get; set; }
        public string NameBan { get; set; }
        public string Name { get; set; }
        public string ShortNameBan { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> Priority { get; set; }
        public bool Trace { get; set; }
        public bool SASB { get; set; }
        public bool PromotionBoard { get; set; }
        public bool BnList { get; set; }
        public Nullable<int> BnListPriority { get; set; }
        public bool TransferProposal { get; set; }
        
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}