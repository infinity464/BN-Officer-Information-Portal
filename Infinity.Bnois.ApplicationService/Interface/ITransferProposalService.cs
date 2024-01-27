using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ITransferProposalService
    {
        List<TransferProposalModel> GetTransferProposals(int ps, int pn, string qs, out int total);
        Task<TransferProposalModel> GetTransferProposal(int id);
        Task<TransferProposalModel> SaveTransferProposal(int v, TransferProposalModel model);
        Task<bool> DeleteTransferProposal(int id);
    }
}
