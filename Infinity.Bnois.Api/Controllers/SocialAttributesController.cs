using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.SocialAttributes)]
    [EnableCors("*", "*", "*")]
    public class SocialAttributesController : BaseController
    {
        private readonly ISocialAttributeService socialAttributeService;
        public SocialAttributesController(ISocialAttributeService socialAttributeService)
        {
            this.socialAttributeService = socialAttributeService;
        }


        [HttpGet]
        [Route("get-employee-social-attribute")]
        public async Task<IHttpActionResult> GetSocialAttribute(int employeeId)
        {
            SocialAttributeModel model = await socialAttributeService.GetSocialAttribute(employeeId);
            return Ok(new ResponseMessage<SocialAttributeModel>()
            {
                Result = model
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-employee-social-attribute/{employeeId}")]
        
        public async Task<IHttpActionResult> UpdateSocialAttribute(int employeeId, [FromBody] SocialAttributeModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<SocialAttributeModel>()
            {
                Result = await socialAttributeService.SaveSocialAttribute(employeeId, model)
            });
        }
    }
}
