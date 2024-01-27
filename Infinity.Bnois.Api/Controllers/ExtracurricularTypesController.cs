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
    [RoutePrefix(BnoisRoutePrefix.ExtracurricularTypes)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EXTRACURRICULAR_TYPES)]

    public class ExtracurricularTypesController: PermissionController
    {

        private readonly IExtracurricularTypeService extracurricularTypeService;
        public ExtracurricularTypesController(IExtracurricularTypeService extracurricularTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.extracurricularTypeService = extracurricularTypeService;
        }

        [HttpGet]
        [Route("get-extracurricular-types")]
        public IHttpActionResult GetExtracurricularTypes(int ps, int pn, string qs)
        {
            int total = 0;
            List<ExtracurricularTypeModel> models = extracurricularTypeService.GetExtracurricularTypes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EXTRACURRICULAR_TYPES);
            return Ok(new ResponseMessage<List<ExtracurricularTypeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-extracurricular-type")]
        public async Task<IHttpActionResult> GetExtracurricularType(int id)
        {
            ExtracurricularTypeModel  model= await extracurricularTypeService.GetExtracurricularType(id);
            return Ok(new ResponseMessage<ExtracurricularTypeModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-extracurricular-type")]
        public async Task<IHttpActionResult> SaveExtracurricularType([FromBody] ExtracurricularTypeModel model)
        {
            return Ok(new ResponseMessage<ExtracurricularTypeModel>
            {
                Result = await extracurricularTypeService.SaveExtracurricularType(0, model)
            });
        }

        [HttpPut]
        [Route("update-extracurricular-type/{id}")]
        public async Task<IHttpActionResult> UpdateExtracurricularType(int id, [FromBody] ExtracurricularTypeModel model) => Ok(new ResponseMessage<ExtracurricularTypeModel>
        {
            Result = await extracurricularTypeService.SaveExtracurricularType(id, model)
        });

        [HttpDelete]
        [Route("delete-extracurricular-type/{id}")]
        public async Task<IHttpActionResult> DeleteExtracurricularType(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await extracurricularTypeService.DeleteExtracurricularType(id)
            });
        }

    }
}
