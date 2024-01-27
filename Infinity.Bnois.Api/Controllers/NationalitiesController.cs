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
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Nationalities)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.NATIONALITY)]

    public class NationalitiesController : PermissionController
    {
        private readonly INationalityService nationalityService;
        public NationalitiesController(INationalityService nationalityService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.nationalityService = nationalityService;
        }

        [HttpGet]
        [Route("get-nationalities")]

        public IHttpActionResult GetNationalities(int ps, int pn, string qs)
        {
            int total = 0;
            List<NationalityModel> models = nationalityService.GetNationalities(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.NATIONALITY);
            return Ok(new ResponseMessage<List<NationalityModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-nationality")]

        public async Task<IHttpActionResult> GetNationality(int id)
        {
            NationalityModel model = await nationalityService.GetNationality(id);
            return Ok(new ResponseMessage<NationalityModel>
            {
                Result = model
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-nationality")]

        public async Task<IHttpActionResult> SaveNationality([FromBody] NationalityModel model)
        {
            return Ok(new ResponseMessage<NationalityModel>
            {
                Result = await nationalityService.SaveNationality(0, model)
            });
        }

        [HttpPut]
        [Route("update-nationality/{id}")]

        public async Task<IHttpActionResult> UpdateNationality(int id, [FromBody] NationalityModel model)
        {
            return Ok(new ResponseMessage<NationalityModel>
            {
                Result = await nationalityService.SaveNationality(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-nationality/{id}")]

        public async Task<IHttpActionResult> DeleteNationality(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await nationalityService.DeleteNationality(id)
            });
        }
    }
}
