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
    [RoutePrefix(BnoisRoutePrefix.SpecialAptTypes)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SPECIAL_APT_TYPES)]

    public class SpecialAptTypesController: PermissionController
    {
        private readonly ISpecialAptTypeService SpecialAptTypeService;
        public SpecialAptTypesController(ISpecialAptTypeService SpecialAptTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.SpecialAptTypeService = SpecialAptTypeService;
        }

        [HttpGet]
        [Route("get-special-apt-types")]
        public IHttpActionResult GetSpecialAptTypes(int ps, int pn, string qs)
        {
            int total = 0;
            List<SpecialAptTypeModel> models = SpecialAptTypeService.GetSpecialAptTypes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.SPECIAL_APT_TYPES);
            return Ok(new ResponseMessage<List<SpecialAptTypeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-special-apt-type")]
        public async Task<IHttpActionResult> GetSpecialAptType(int id)
        {
            SpecialAptTypeModel model = await SpecialAptTypeService.GetSpecialAptType(id);
            return Ok(new ResponseMessage<SpecialAptTypeModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-special-apt-type")]
        public async Task<IHttpActionResult> SaveSpecialAptType([FromBody] SpecialAptTypeModel model)
        {
            return Ok(new ResponseMessage<SpecialAptTypeModel>
            {
                Result = await SpecialAptTypeService.SaveSpecialAptType(0, model)
            });
        }

        [HttpPut]
        [Route("update-special-apt-type/{id}")]
        public async Task<IHttpActionResult> UpdateSpecialAptType(int id, [FromBody] SpecialAptTypeModel model)
        {
            return Ok(new ResponseMessage<SpecialAptTypeModel>
            {
                Result = await SpecialAptTypeService.SaveSpecialAptType(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-special-apt-type/{id}")]
        public async Task<IHttpActionResult> DeleteSpecialAptType(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await SpecialAptTypeService.DeleteSpecialAptType(id)
            });
        }
    }
}
