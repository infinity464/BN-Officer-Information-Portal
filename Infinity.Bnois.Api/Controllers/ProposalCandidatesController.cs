using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ProposalCadidates)]
    [EnableCors("*", "*", "*")]
    public class ProposalCandidatesController : BaseController
    {
        private readonly IProposalCandidateService proposalCandidateService;
        public ProposalCandidatesController(IProposalCandidateService proposalCandidateService)
        {
            this.proposalCandidateService = proposalCandidateService;
        }

        [HttpGet]
        [Route("get-proposal-candidates/{proposalDetailId}")]
        public IHttpActionResult GetProposalCadidates(int proposalDetailId)
        {
            int total = 0;
            List<ProposalCandidateModel> models = proposalCandidateService.GetProposalCandidates(proposalDetailId);
            return Ok(new ResponseMessage<List<ProposalCandidateModel>>()
            {
                Result = models
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-proposal-candidate/{proposalDetailId}")]
        public async Task<IHttpActionResult> SaveProposalCadidate(int proposalDetailId, [FromBody] ProposalCandidateModel model)
        {
            model.ProposalDetailId = proposalDetailId;
            return Ok(new ResponseMessage<ProposalCandidateModel>
            {
                Result = await proposalCandidateService.SaveProposalCadidate(0, model)
            });
        }


        [HttpDelete]
        [Route("delete-proposal-candidate/{proposalCandidateId}")]
        public async Task<IHttpActionResult> DeleteProposalCadidate(int proposalCandidateId)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await proposalCandidateService.DeleteProposalCadidate(proposalCandidateId)
            });
        }
    }
}
