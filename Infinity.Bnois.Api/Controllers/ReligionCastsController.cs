using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ReligionCasts)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RELEGION_CAST)]

    public class ReligionCastsController : PermissionController
    {
        private readonly IReligionService religionService;
        private readonly IReligionCastService religionCastService;
        public ReligionCastsController(IReligionService religionService, IReligionCastService religionCastService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.religionService = religionService;
            this.religionCastService = religionCastService;
        }

        [HttpGet]
        [Route("get-religion-casts")]
        
        public IHttpActionResult GetReligionCasts(int ps, int pn, string qs)
        {
            int total = 0;
            List<ReligionCastModel> models = religionCastService.GetReligionCasts(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.RELEGION_CAST);
            return Ok(new ResponseMessage<List<ReligionCastModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-religion-cast")]
        
        public async Task<IHttpActionResult> GetReligionCast(int id)
        {
            ReligionCastViewModel vm = new ReligionCastViewModel();
            vm.ReligionCast = await religionCastService.GetReligionCast(id);
            vm.Religions = await religionService.GetReligionSelectModels();
            return Ok(new ResponseMessage<ReligionCastViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-religion-cast")]
        
        public async Task<IHttpActionResult> SaveReligionCast([FromBody] ReligionCastModel model)
        {
            return Ok(new ResponseMessage<ReligionCastModel>
            {
                Result = await religionCastService.SaveReligionCast(0, model)
            });
        }

        [HttpPut]
        [Route("update-religion-cast/{id}")]
        
        public async Task<IHttpActionResult> UpdateReligionCast(int id, [FromBody] ReligionCastModel model)
        {
            return Ok(new ResponseMessage<ReligionCastModel>
            {
                Result = await religionCastService.SaveReligionCast(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-religion-cast/{id}")]
        
        public async Task<IHttpActionResult> DeleteReligionCast(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await religionCastService.DeleteReligionCast(id)
            });
        }

        [HttpGet]
        [Route("get-religion-cast-by-religion")]
        public async Task<IHttpActionResult> GetReligionCastByReligion(int religionId)
            {
            List<SelectModel> religionCasts = await religionCastService.GetReligionCasts(religionId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = religionCasts
            });
        }

    }
}
