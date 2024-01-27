using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinity.Bnois.Configuration.Models
{
    [Table("RoleFeature", Schema = Schemas.Company)]
    public class RoleFeature
    {
        [Required]
        [Key, Column(Order = 0)]
        [MaxLength(36)]
        public string RoleId { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public int  FeatureKey{ get; set; }

      
        public bool Add { get; set; }
      
        public bool Update { get; set; }
 
        public bool Delete { get; set; }
     
        public bool Report { get; set; }
    }
}