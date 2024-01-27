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
    [RoutePrefix(BnoisRoutePrefix.InstituteTypes)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.INSTITUTE_TYPES)]

    public class InstituteTypesController : PermissionController
    {
        private readonly IInstituteTypeService instituteTypeService;
        public InstituteTypesController(IInstituteTypeService instituteTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.instituteTypeService = instituteTypeService;
        }


        [HttpGet]
        [Route("get-institute-types")]
        public IHttpActionResult InstituteTypes(int ps, int pn, string qs)
        {
            int total = 0;
            List<InstituteTypeModel> models = instituteTypeService.InstituteTypes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.INSTITUTE_TYPES);
            return Ok(new ResponseMessage<List<InstituteTypeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-institute-type")]
        public async Task<IHttpActionResult> GetInstituteType(int id)
        {
            InstituteTypeModel model = await instituteTypeService.GetInstituteType(id);
            return Ok(new ResponseMessage<InstituteTypeModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-institute-type")]
        public async Task<IHttpActionResult> SaveInstituteType([FromBody] InstituteTypeModel model)
        {
            return Ok(new ResponseMessage<InstituteTypeModel>
            {
                Result = await instituteTypeService.SaveInstituteType(0, model)
            });
        }

        [HttpPut]
        [Route("update-institute-type/{id}")]
        public async Task<IHttpActionResult> UpdateInstituteType(int id, [FromBody] InstituteTypeModel model)
        {
            return Ok(new ResponseMessage<InstituteTypeModel>
            {
                Result = await instituteTypeService.SaveInstituteType(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-institute-type/{id}")]
        public async Task<IHttpActionResult> DeleteInstituteType(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await instituteTypeService.DeleteInstituteType(id)
            });
        }
    }
}
