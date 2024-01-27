using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinity.Bnois.Configuration.Models 
{
  

    [Table("Feature", Schema = Schemas.Company)]
    public class Feature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string ActionNgHref { get; set; }
        public int ModuleId { get; set; }
        public int FeatureCode { get; set; }
        public int OrderNo { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<Guid> CreatedBy { get; set; }
        public Nullable<DateTime> EditedDate { get; set; }
        public Nullable<Guid> EditedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsReport { get; set; }
        public FeatureType FeatureTypeId { get; set; }
        public virtual Module Module { get; set; }
        
    }
}