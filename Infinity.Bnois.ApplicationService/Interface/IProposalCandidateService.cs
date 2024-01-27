using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IProposalCandidateService
    {
        List<ProposalCandidateModel> GetProposalCandidates(int proposalDetailId);
        Task<ProposalCandidateModel> SaveProposalCadidate(int id, ProposalCandidateModel model);
        Task<bool> DeleteProposalCadidate(int id);
    }
}
