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
    [RoutePrefix(BnoisRoutePrefix.PreviousLeaves)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public  class PreviousLeavesController:PermissionController
    {
        private readonly IPreviousLeaveService PreviousLeaveService;
        private readonly ILeaveTypeService leaveTypeService;
        public PreviousLeavesController(IPreviousLeaveService PreviousLeaveService, ILeaveTypeService leaveTypeService,IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.PreviousLeaveService = PreviousLeaveService;
            this.leaveTypeService = leaveTypeService;
        }

      

        [HttpGet]
        [Route("get-previous-leaves")]
        public IHttpActionResult GetPreviousLeaves(int employeeId)
        {
            List<PreviousLeaveModel> models = PreviousLeaveService.GetPreviousLeaves(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<PreviousLeaveModel>>()
            {
                Result = models,
                Permission = permission
            });

        }
        [HttpGet]
        [Route("get-previous-leave")]
        public async Task<IHttpActionResult> GetPreviousLeave( int previousLeaveId)
        {
            PreviousLeaveViewModel vm = new PreviousLeaveViewModel();
            vm.LeaveTypes = await leaveTypeService.GetLeaveTypeSelectModel();
            vm.PreviousLeave = await PreviousLeaveService.GetPreviousLeave(previousLeaveId);
            return Ok(new ResponseMessage<PreviousLeaveViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-previous-leave/{employeeId}")]
        public async Task<IHttpActionResult> SavePreviousLeave(int employeeId, [FromBody] PreviousLeaveModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<PreviousLeaveModel>()
            {
                Result = await PreviousLeaveService.SavePreviousLeave(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-previous-leave/{previousLeaveId}")]
        public async Task<IHttpActionResult> UpdatePreviousLeave(int PreviousLeaveId, [FromBody] PreviousLeaveModel model)
        {
            return Ok(new ResponseMessage<PreviousLeaveModel>()
            {
                Result = await PreviousLeaveService.SavePreviousLeave(PreviousLeaveId, model)
            });
        }


        [HttpDelete]
        [Route("delete-previous-leave/{id}")]
        public async Task<IHttpActionResult> DeletePreviousLeave(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await PreviousLeaveService.DeletePreviousLeave(id)
            });
        }
    }
}
