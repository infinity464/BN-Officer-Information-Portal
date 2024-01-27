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
    [RoutePrefix(BnoisRoutePrefix.EyeVisions)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EYE_VISIONS)]

    public class EyeVisionsController : PermissionController
    {
        private readonly IEyeVisionService eyeVisionService;

        public EyeVisionsController(IEyeVisionService eyeVisionService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.eyeVisionService = eyeVisionService;
        }

        [HttpGet]
        [Route("get-eye-visions")]
        public IHttpActionResult GetEyeVisions(int ps, int pn, string qs)
        {
            int total = 0;
            List<EyeVisionModel> models = eyeVisionService.GetEyeVisions(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EYE_VISIONS);
            return Ok(new ResponseMessage<List<EyeVisionModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-eye-vision")]
        public async Task<IHttpActionResult> GetEyeVision(int eyeVisionId)
        {
            EyeVisionModel model = await eyeVisionService.GetEyeVision(eyeVisionId);
            return Ok(new ResponseMessage<EyeVisionModel>()
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-eye-vision")]
        public async Task<IHttpActionResult> SaveEyeVision([FromBody] EyeVisionModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<EyeVisionModel>()
            {
                Result = await eyeVisionService.SaveEyeVision(0, model)
            });
        }


        [HttpPut]
        [ModelValidation]
        [Route("update-eye-vision/{id}")]
        public async Task<IHttpActionResult> UpdateEyeVision(int id, [FromBody] EyeVisionModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<EyeVisionModel>()
            {
                Result = await eyeVisionService.SaveEyeVision(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-eye-vision/{id}")]
        public async Task<IHttpActionResult> DeleteEyeVision(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = await eyeVisionService.DeleteEyeVision(id)
            });
        }
    }
}