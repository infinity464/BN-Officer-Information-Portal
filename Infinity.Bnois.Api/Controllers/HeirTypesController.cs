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
    [RoutePrefix(BnoisRoutePrefix.HeirTypes)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.HEIR_TYPES)]

    public class HeirTypesController : PermissionController
    {
        private readonly IHeirTypeService heirTypeService;
        public HeirTypesController(IHeirTypeService heirTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.heirTypeService = heirTypeService;
        }

        [HttpGet]
        [Route("get-heir-types")]
        public IHttpActionResult GetHeirTypes(int ps, int pn, string qs)
        {
            int total = 0;
            List<HeirTypeModel> models = heirTypeService.GetHeirTypes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.HEIR_TYPES);
            return Ok(new ResponseMessage<List<HeirTypeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-heir-type")]
        public async Task<IHttpActionResult> GetHeirType(int id)
        {
            HeirTypeModel model = await heirTypeService.GetHeirType(id);
            return Ok(new ResponseMessage<HeirTypeModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-heir-type")]
        public async Task<IHttpActionResult> SaveHeirType([FromBody] HeirTypeModel model)
        {
            return Ok(new ResponseMessage<HeirTypeModel>
            {
                Result = await heirTypeService.SaveHeirType(0, model)
            });
        }

        [HttpPut]
        [Route("update-heir-type/{id}")]
        public async Task<IHttpActionResult> UpdateHeirType(int id, [FromBody] HeirTypeModel model)
        {
            return Ok(new ResponseMessage<HeirTypeModel>
            {
                Result = await heirTypeService.SaveHeirType(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-heir-type/{id}")]
        public async Task<IHttpActionResult> DeleteHeirType(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await heirTypeService.DeleteHeirType(id)
            });
        }
    }
}
