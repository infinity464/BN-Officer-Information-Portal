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
    [RoutePrefix(BnoisRoutePrefix.SecurityClearanceReasons)]
    [EnableCors("*", "*", "*")]
   [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEE_SECURTIY_CLEARANCES)]

    public class SecurityClearanceReasonsController : PermissionController
    {
        private readonly ISecurityClearanceReasonService securityClearanceReasonService;

        public SecurityClearanceReasonsController(ISecurityClearanceReasonService securityClearanceReasonService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.securityClearanceReasonService = securityClearanceReasonService;
        }

        [HttpGet]
        [Route("get-security-clearance-reasons")]
        public IHttpActionResult GetSecurityClearanceReasons(int ps, int pn, string qs)
        {
            int total = 0;
            List<SecurityClearanceReasonModel> models = securityClearanceReasonService.GetSecurityClearanceReasons(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEE_SECURTIY_CLEARANCES);
            return Ok(new ResponseMessage<List<SecurityClearanceReasonModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-security-clearance-reason")]
        public async Task<IHttpActionResult> GetSecurityClearanceReason(int id)
        {
            SecurityClearanceReasonModel model = await securityClearanceReasonService.GetSecurityClearanceReason(id);
            return Ok(new ResponseMessage<SecurityClearanceReasonModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-security-clearance-reason")]
        public async Task<IHttpActionResult> SaveSecurityClearanceReason([FromBody] SecurityClearanceReasonModel model)
        {
            return Ok(new ResponseMessage<SecurityClearanceReasonModel>
            {
                Result = await securityClearanceReasonService.SaveSecurityClearanceReason(0, model)
            });
        }



        [HttpPut]
        [Route("update-security-clearance-reason/{id}")]
        public async Task<IHttpActionResult> UpdateSecurityClearanceReason(int id, [FromBody] SecurityClearanceReasonModel model)
        {
            return Ok(new ResponseMessage<SecurityClearanceReasonModel>
            {
                Result = await securityClearanceReasonService.SaveSecurityClearanceReason(id, model)
            });
        }



        [HttpDelete]
        [Route("delete-security-clearance-reason/{id}")]
        public async Task<IHttpActionResult> DeleteSecurityClearanceReason(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await securityClearanceReasonService.DeleteSecurityClearanceReason(id)
            });
        }


    }
}