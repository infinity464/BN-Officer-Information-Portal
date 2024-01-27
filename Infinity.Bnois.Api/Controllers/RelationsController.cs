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
    [RoutePrefix(BnoisRoutePrefix.Relations)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RELATIONS)]

    public class RelationsController : PermissionController
    {
        private readonly IRelationService relationService;
        public RelationsController(IRelationService relationService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.relationService = relationService;
        }

        [HttpGet]
        [Route("get-relations")]
        public IHttpActionResult GetRelations(int ps, int pn, string qs)
        {
            int total = 0;
            List<RelationModel> models = relationService.GetRelations(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.RELATIONS);
            return Ok(new ResponseMessage<List<RelationModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-relation")]
        public async Task<IHttpActionResult> GetRelation(int id)
        {
            RelationModel model = await relationService.GetRelation(id);
            return Ok(new ResponseMessage<RelationModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-relation")]
        public async Task<IHttpActionResult> SaveRelation([FromBody] RelationModel model)
        {
            return Ok(new ResponseMessage<RelationModel>
            {
                Result = await relationService.SaveRelation(0, model)
            });
        }

        [HttpPut]
        [Route("update-relation/{id}")]
        public async Task<IHttpActionResult> UpdateRelation(int id, [FromBody] RelationModel model)
        {
            return Ok(new ResponseMessage<RelationModel>
            {
                Result = await relationService.SaveRelation(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-relation/{id}")]
        public async Task<IHttpActionResult> DeleteRelation(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await relationService.DeleteRelation(id)
            });
        }
    }
}
