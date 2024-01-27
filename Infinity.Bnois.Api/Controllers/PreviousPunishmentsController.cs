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
    [RoutePrefix(BnoisRoutePrefix.PreviousPunishments)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public  class PreviousPunishmentsController:PermissionController
    {
        private readonly IPreviousPunishmentService previousPunishmentService;
        public PreviousPunishmentsController(IPreviousPunishmentService previousPunishmentService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.previousPunishmentService = previousPunishmentService;
           
        }

      

        [HttpGet]
        [Route("get-previous-punishments")]
        public IHttpActionResult GetPreviousPunishments(int employeeId)
        {
            List<PreviousPunishmentModel> models = previousPunishmentService.GetPreviousPunishments(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<PreviousPunishmentModel>>()
            {
                Result = models,
                Permission = permission
            });

        }
        [HttpGet]
        [Route("get-previous-punishment")]
        public async Task<IHttpActionResult> GetPreviousPunishment( int previousPunishmentId)
        {

            return Ok(new ResponseMessage<PreviousPunishmentModel>()
            {
                Result = await previousPunishmentService.GetPreviousPunishment(previousPunishmentId)
        });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-previous-punishment/{employeeId}")]
        public async Task<IHttpActionResult> SavePreviousPunishment(int employeeId, [FromBody] PreviousPunishmentModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<PreviousPunishmentModel>()
            {
                Result = await previousPunishmentService.SavePreviousPunishment(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-previous-punishment/{previousPunishmentId}")]
        public async Task<IHttpActionResult> UpdatePreviousPunishment(int previousPunishmentId, [FromBody] PreviousPunishmentModel model)
        {
            return Ok(new ResponseMessage<PreviousPunishmentModel>()
            {
                Result = await previousPunishmentService.SavePreviousPunishment(previousPunishmentId, model)
            });
        }


        [HttpDelete]
        [Route("delete-previous-punishment/{id}")]
        public async Task<IHttpActionResult> DeletePreviousPunishment(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await previousPunishmentService.DeletePreviousPunishment(id)
            });
        }
    }
}
