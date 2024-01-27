using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IProposalDetailService
    {
        List<ProposalDetailModel> GetProposalDetails(int transferProposalId,int ps, int pn, string qs, out int total);
        Task<ProposalDetailModel> GetProposalDetail(int id);
        Task<ProposalDetailModel> SaveProposalDetail(int v, ProposalDetailModel model);
        Task<bool> DeleteProposalDetails(int id);
    }
}
