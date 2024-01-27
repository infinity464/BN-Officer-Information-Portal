using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public  class CommendationModel
    {
        public int CommendationId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Type { get; set; }
		public string TypeName { get; set; }
		public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }


	
}