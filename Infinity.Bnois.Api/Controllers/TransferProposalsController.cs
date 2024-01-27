using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.TransferProposals)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.TRANSFER_PROPOSALS)]

    public class TransferProposalsController : PermissionController
    {
        private readonly ITransferProposalService transferProposalService;

        public TransferProposalsController(ITransferProposalService transferProposalService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.transferProposalService = transferProposalService;
        }

        [HttpGet]
        [Route("get-transfer-proposals")]
        public IHttpActionResult GetTransferProposals(int ps, int pn, string qs)
        {
            int total = 0;
            List<TransferProposalModel> models = transferProposalService.GetTransferProposals(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.TRANSFER_PROPOSALS);
            return Ok(new ResponseMessage<List<TransferProposalModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-transfer-proposal")]
        public async Task<IHttpActionResult> GetTransferProposal(int id)
        {
            TransferProposalModel model = await transferProposalService.GetTransferProposal(id);
            return Ok(new ResponseMessage<TransferProposalModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-transfer-proposal")]
        public async Task<IHttpActionResult> SaveTransferProposal([FromBody] TransferProposalModel model)
        {
            return Ok(new ResponseMessage<TransferProposalModel>
            {
                Result = await transferProposalService.SaveTransferProposal(0, model)
            });
        }



        [HttpPut]
        [Route("update-transfer-proposal/{id}")]
        public async Task<IHttpActionResult> UpdateTransferProposal(int id, [FromBody] TransferProposalModel model) => Ok(new ResponseMessage<TransferProposalModel>
        {
            Result = await transferProposalService.SaveTransferProposal(id, model)
        });



        [HttpDelete]
        [Route("delete-transfer-proposal/{id}")]
        public async Task<IHttpActionResult> DeleteTransferProposal(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await transferProposalService.DeleteTransferProposal(id)
            });
        }


    }
}