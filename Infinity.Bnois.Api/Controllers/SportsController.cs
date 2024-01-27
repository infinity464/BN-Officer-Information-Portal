using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Sports)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SPORTS)]
    public class SportsController : PermissionController
    {
        private readonly ISportService sportService;
        public SportsController(ISportService sportService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.sportService = sportService;
        }

        [HttpGet]
        [Route("get-sports")]
        public IHttpActionResult GetSports(int ps, int pn, string qs)
        {
            int total = 0;
            List<SportModel> models = sportService.GetSports(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.SPORTS);
            return Ok(new ResponseMessage<List<SportModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-sport")]
        public async Task<IHttpActionResult> GetSport(int id)
        {
            SportModel model = await sportService.GetSport(id);
            return Ok(new ResponseMessage<SportModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-sport")]
        public async Task<IHttpActionResult> SaveSport([FromBody] SportModel model)
        {
            return Ok(new ResponseMessage<SportModel>
            {
                Result = await sportService.SaveSport(0, model)
            });
        }



        [HttpPut]
        [Route("update-sport/{id}")]
        public async Task<IHttpActionResult> UpdateSport(int id, [FromBody] SportModel model)
        {
            return Ok(new ResponseMessage<SportModel>
            {
                Result = await sportService.SaveSport(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-sport/{id}")]
        public async Task<IHttpActionResult> DeleteSport(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await sportService.DeleteSport(id)
            });
        }
    }
}
