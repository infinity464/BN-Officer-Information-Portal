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
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Zones)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.ZONES)]

    public class ZonesController : PermissionController
    {
        private readonly IZoneService zoneService;
        public ZonesController(IZoneService zoneService ,IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.zoneService = zoneService;
        }

        [HttpGet]
        [Route("get-zones")]
        public IHttpActionResult GetZones(int ps, int pn, string qs)
        {
            int total = 0;
            List<ZoneModel> models = zoneService.GetZones(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.ZONES);
            return Ok(new ResponseMessage<List<ZoneModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-zone")]
        public async Task<IHttpActionResult> GetZone(int id)
        {
            ZoneModel model = await zoneService.GetZone(id);
            return Ok(new ResponseMessage<ZoneModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-zone")]
        public async Task<IHttpActionResult> SaveZone([FromBody] ZoneModel model)
        {
            return Ok(new ResponseMessage<ZoneModel>
            {
                Result = await zoneService.SaveZone(0, model)
            });
        }

        [HttpPut]
        [Route("update-zone/{id}")]
        public async Task<IHttpActionResult> UpdateZone(int id, [FromBody] ZoneModel model)
        {
            return Ok(new ResponseMessage<ZoneModel>
            {
                Result = await zoneService.SaveZone(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-zone/{id}")]
        public async Task<IHttpActionResult> DeleteZone(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await zoneService.DeleteZone(id)
            });
        }
    }
}
