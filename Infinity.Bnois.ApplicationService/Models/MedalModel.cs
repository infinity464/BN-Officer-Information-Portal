using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class MedalModel
    {
        public int MedalId { get; set; }
        public string NameEng { get; set; }
        public string NameBan { get; set; }
        public string ShortNameEng { get; set; }
        public string ShortNameBan { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
	    public int MedalType { get; set; }
	    public string MedalTypeName { get; set; }
		public bool GoToTrace { get; set; }
        public bool ANmCon { get; set; }
        public bool NmRGF { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}