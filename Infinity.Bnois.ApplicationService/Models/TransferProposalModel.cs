using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
  public  class TransferProposalModel
    {
        public int TransferProposalId { get; set; }
        public string Name { get; set; }
        [Required]
        public Nullable<System.DateTime> ProposalDate { get; set; }
        public Nullable<int> LtCdrLevel { get; set; }
        public bool WithPicture { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
