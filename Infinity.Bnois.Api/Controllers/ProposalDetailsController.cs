using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Configuration;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ProposalDetails)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.TRANSFER_PROPOSALS)]
    public class ProposalDetailsController : PermissionController
    {
        private readonly IProposalDetailService proposalDetailService;
        private readonly IOfficeService officeService;
        private readonly IOfficeAppointmentService officeAppointmentService;
        private readonly ITransferService transferService;
        public ProposalDetailsController(IProposalDetailService proposalDetailService,
            IOfficeService officeService,
            IOfficeAppointmentService officeAppointmentService,
            ITransferService transferService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.proposalDetailService = proposalDetailService;
            this.officeService = officeService;
            this.officeAppointmentService = officeAppointmentService;
            this.transferService = transferService;
        }
        [HttpGet]
        [Route("get-proposal-details")]
        public IHttpActionResult GetProposalDetails(int transferProposalId, int ps, int pn, string qs)
        {
            int total = 0;
            List<ProposalDetailModel> models = proposalDetailService.GetProposalDetails(transferProposalId,ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.TRANSFER_PROPOSALS);
            return Ok(new ResponseMessage<List<ProposalDetailModel>>()
            {
                Result = models,
                Total = total,
                Permission = permission
            });
        }

        [HttpGet]
        [Route("get-proposal-detail")]
        public async Task<IHttpActionResult> GetProposalDetail(int id)
        {
            ProposalDetailViewModel vm = new ProposalDetailViewModel();
            vm.ProposalDetail = await proposalDetailService.GetProposalDetail(id);
            vm.TransferTypes = transferService.GetTransferTypeSelectModels();
            if (vm.ProposalDetail.ProposalDetailId>0)
            {
                vm.Offices = await officeService.GetOfficeSelectModel(vm.ProposalDetail.TransferType);
                vm.OfficeAppointments = await officeAppointmentService.GetOfficeAppointmentsByOfficeId(vm.ProposalDetail.AttachOfficeId);
            }
            return Ok(new ResponseMessage<ProposalDetailViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-proposal-detail/{transferProposalId}")]
        public async Task<IHttpActionResult> SaveProposalDetail(int transferProposalId,[FromBody] ProposalDetailModel model)
        {
            model.TransferProposalId = transferProposalId;
            return Ok(new ResponseMessage<ProposalDetailModel>
            {
                Result = await proposalDetailService.SaveProposalDetail(0, model)
            });
        }

        [HttpPut]
        [Route("update-proposal-detail/{id}")]
        public async Task<IHttpActionResult> UpdateProposalDetail(int id, [FromBody] ProposalDetailModel model)
        {
            return Ok(new ResponseMessage<ProposalDetailModel>
            {
                Result = await proposalDetailService.SaveProposalDetail(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-proposal-detail/{id}")]
        public async Task<IHttpActionResult> DeleteProposalDetails (int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await proposalDetailService.DeleteProposalDetails(id)
            });
        }
    }
}
