using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
 public   class ProposalDetailModel
    {
        public int ProposalDetailId { get; set; }
        public int TransferProposalId { get; set; }
        public int TransferType { get; set; }
        public int AttachOfficeId { get; set; }
        public int? AppointmentId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual TransferProposalModel TransferProposal { get; set; }
        public virtual OfficeModel Office { get; set; }
        public virtual OfficeAppointmentModel OfficeAppointment { get; set; }
    }
}
