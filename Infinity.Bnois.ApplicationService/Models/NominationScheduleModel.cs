using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class NominationScheduleModel
	{
	    public int NominationScheduleId { get; set; }
	    public int NominationScheduleType { get; set; }
	    public Nullable<int> VisitCategoryId { get; set; }
	    public Nullable<int> VisitSubCategoryId { get; set; }
	    public string TitleName { get; set; }
	    public Nullable<int> CountryId { get; set; }
        public string Purpose { get; set; }
	    public string Location { get; set; }
        [Required]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}", ConvertEmptyStringToNull = false)]
        //[Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of Range")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}", ConvertEmptyStringToNull = false)]
       // [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of Range")]
        public Nullable<System.DateTime> ToDate { get; set; }
	    public Nullable<int> NumberOfPost { get; set; }
	    public Nullable<int> AssignPost { get; set; }
	    public string Remarks { get; set; }
	    public string CreatedBy { get; set; }
	    public System.DateTime CreatedDate { get; set; }
	    public string ModifiedBy { get; set; }
	    public Nullable<System.DateTime> ModifiedDate { get; set; }
	    public bool IsActive { get; set; }

	    public virtual CountryModel Country { get; set; }
	    public virtual VisitCategoryModel VisitCategory { get; set; }
	    public virtual VisitSubCategoryModel VisitSubCategory { get; set; }
    }
}