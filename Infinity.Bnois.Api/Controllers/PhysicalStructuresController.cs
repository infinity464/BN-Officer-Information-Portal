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
    [RoutePrefix(BnoisRoutePrefix.PhysicalStructures)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PHYSICAL_STRUCTURES)]

    public class PhysicalStructuresController: PermissionController
    {
        private readonly IPhysicalStructureService physicalStructureService;
        public PhysicalStructuresController(IPhysicalStructureService physicalStructureService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.physicalStructureService = physicalStructureService;
        }

        [HttpGet]
        [Route("get-physical-structures")]
        public IHttpActionResult GetPhysicalStructures(int ps, int pn, string qs)
        {
            int total = 0;
            List<PhysicalStructureModel> models = physicalStructureService.GetPhysicalStructures(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PHYSICAL_STRUCTURES);
            return Ok(new ResponseMessage<List<PhysicalStructureModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-physical-structure")]
        public async Task<IHttpActionResult> GetPhysicalStructure(int id)
        {
            PhysicalStructureModel model = await physicalStructureService.GetPhysicalStructure(id);
            return Ok(new ResponseMessage<PhysicalStructureModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-physical-structure")]
        public async Task<IHttpActionResult> SavePhysicalStructure([FromBody] PhysicalStructureModel model)
        {
            return Ok(new ResponseMessage<PhysicalStructureModel>
            {
                Result = await physicalStructureService.SavePhysicalStructure(0, model)
            });
        }

        [HttpPut]
        [Route("update-physical-structure/{id}")]
        public async Task<IHttpActionResult> UpdatePhysicalStructure(int id, [FromBody] PhysicalStructureModel model)
        {
            return Ok(new ResponseMessage<PhysicalStructureModel>
            {
                Result = await physicalStructureService.SavePhysicalStructure(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-physical-structure/{id}")]
        public async Task<IHttpActionResult> DeletePhysicalStructure(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await physicalStructureService.DeletePhysicalStructure(id)
            });
        }
    }
}
