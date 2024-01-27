using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.PreviousTransfers)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public  class PreviousTransfersController:PermissionController
    {
        private readonly IPreviousTransferService previousTransferService;
        private readonly IPreCommissionRankService preCommissionRankService;
        public PreviousTransfersController(IPreviousTransferService previousTransferService, IPreCommissionRankService preCommissionRankService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.previousTransferService = previousTransferService;
            this.preCommissionRankService = preCommissionRankService;
           
        }

      

        [HttpGet]
        [Route("get-previous-transfers")]
        public IHttpActionResult GetPreviousTransfers(int employeeId)
        {
            List<PreviousTransferModel> models = previousTransferService.GetPreviousTransfers(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<PreviousTransferModel>>()
            {
                Result = models,
                Permission = permission
            });

        }
        [HttpGet]
        [Route("get-previous-transfer")]
        public async Task<IHttpActionResult> GetPreviousTransfer( int previousTransferId)
        {
            PreviousTransferViewModel vm=new PreviousTransferViewModel();
            vm.Ranks = await preCommissionRankService.GetPreCommissionRankSelectModels();
            vm.PreviousTransfer = await previousTransferService.GetPreviousTransfer(previousTransferId);

            return Ok(new ResponseMessage<PreviousTransferViewModel>()
            {
                Result = vm
        });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-previous-transfer/{employeeId}")]
        public async Task<IHttpActionResult> SavePreviousTransfer(int employeeId, [FromBody] PreviousTransferModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<PreviousTransferModel>()
            {
                Result = await previousTransferService.SavePreviousTransfer(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-previous-transfer/{previousTransferId}")]
        public async Task<IHttpActionResult> UpdatePreviousTransfer(int previousTransferId, [FromBody] PreviousTransferModel model)
        {
            return Ok(new ResponseMessage<PreviousTransferModel>()
            {
                Result = await previousTransferService.SavePreviousTransfer(previousTransferId, model)
            });
        }


        [HttpDelete]
        [Route("delete-previous-Transfer/{id}")]
        public async Task<IHttpActionResult> DeletePreviousTransfer(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await previousTransferService.DeletePreviousTransfer(id)
            });
        }
    }
}
