using System;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class ShipCategoryModel
	{
		public int ShipCategoryId { get; set; }

		public string Name { get; set; }

		public string ShortName { get; set; }

		public int Priority { get; set; }

		public string Purpose { get; set; }

		public string CreatedBy { get; set; }

		public System.DateTime CreatedDate { get; set; }

		public string ModifiedBy { get; set; }

		public Nullable<System.DateTime> ModifiedDate { get; set; }

		public bool IsActive { get; set; }
	}
}