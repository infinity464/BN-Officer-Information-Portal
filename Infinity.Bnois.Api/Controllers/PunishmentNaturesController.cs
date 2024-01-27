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
    [RoutePrefix(BnoisRoutePrefix.PunishmentNatures)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PUNISHMENT_NATURES)]
    public class PunishmentNaturesController : PermissionController
    {
        private readonly IPunishmentNatureService punishmentNatureService;

        public PunishmentNaturesController(IPunishmentNatureService punishmentNatureService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.punishmentNatureService = punishmentNatureService;
        }

        [HttpGet]
        [Route("get-punishment-natures")]
        public IHttpActionResult GetPunishmentCategories(int ps, int pn, string qs)
        {
            var total = 0;
            var models = punishmentNatureService.GetPunishmentNatures(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PUNISHMENT_NATURES);
            return Ok(new ResponseMessage<List<PunishmentNatureModel>>
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-punishment-nature")]
        public async Task<IHttpActionResult> GetPunishmentNature(int id)
        {
            var model = await punishmentNatureService.GetPunishmentNature(id);
            return Ok(new ResponseMessage<PunishmentNatureModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-punishment-nature")]
        public async Task<IHttpActionResult> SavePunishmentNature([FromBody] PunishmentNatureModel model)
        {
            model.CreatedBy = UserId;
            return Ok(new ResponseMessage<PunishmentNatureModel>
            {
                Result = await punishmentNatureService.SavePunishmentNature(0, model)
            });
        }


        [HttpPut]
        [ModelValidation]
        [Route("update-punishment-nature/{id}")]
        public async Task<IHttpActionResult> UpdatePunishmentNature(int id, [FromBody] PunishmentNatureModel model)
        {
            model.ModifiedBy = UserId;
            return Ok(new ResponseMessage<PunishmentNatureModel>
            {
                Result = await punishmentNatureService.SavePunishmentNature(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-punishment-nature/{id}")]
        public async Task<IHttpActionResult> DeletePunishmentNature(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await punishmentNatureService.DeletePunishmentNature(id)
            });
        }
    }
}