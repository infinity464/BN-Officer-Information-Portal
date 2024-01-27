using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Medals)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.MEDALS)]

    public class MedalsController : PermissionController
    {
        private readonly IMedalService medalService;

        public MedalsController(IMedalService medalService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.medalService = medalService;
        }

        [HttpGet]
        [Route("get-medals")]
        public IHttpActionResult GetMedals(int ps, int pn, string qs)
        {
            int total = 0;
            List<MedalModel> models = medalService.GetMedals(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.MEDALS);
            return Ok(new ResponseMessage<List<MedalModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-medal")]
        public async Task<IHttpActionResult> GetMedal(int id)
        {
			MedalViewModel vm = new MedalViewModel();
	        vm.Medal = await medalService.GetMedal(id);
	        vm.MedalTypes = medalService.GetMedalTypeSelectModels();
			return Ok(new ResponseMessage<MedalViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-medal")]
        public async Task<IHttpActionResult> SaveMedal([FromBody] MedalModel model)
        {
            return Ok(new ResponseMessage<MedalModel>
            {
                Result = await medalService.SaveMedal(0, model)
            });
        }



        [HttpPut]
        [Route("update-medal/{id}")]
        public async Task<IHttpActionResult> UpdateMedal(int id, [FromBody] MedalModel model)
        {
            return Ok(new ResponseMessage<MedalModel>
            {
                Result = await medalService.SaveMedal(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-medal/{id}")]
        public async Task<IHttpActionResult> DeleteMedal(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await medalService.DeleteMedal(id)
            });
        }


    }
}