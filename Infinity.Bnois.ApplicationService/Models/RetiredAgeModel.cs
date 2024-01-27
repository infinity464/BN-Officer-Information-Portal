using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class RetiredAgeModel
    { 
    public int RetiredAgeId { get; set; }
    public int CategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public int RankId { get; set; }
    public int AgeLimit { get; set; }
    public int RStatus { get; set; }
    public string RStatusName { get; set; }
    public string Remarks { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public System.DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual CategoryModel Category { get; set; }
    public virtual RankModel Rank { get; set; }
    public virtual SubCategoryModel SubCategory { get; set; }
    }
}