using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Controllers;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Ers.Admin.Api.Controllers
{
 
    [RoutePrefix(BnoisRoutePrefix.MaritalTypes)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.MARITAL_TYPES)]

    public class MaritalTypesController : PermissionController
    {
        private readonly IMaritalTypeService _maritalTypeService;
  
        public MaritalTypesController(IMaritalTypeService maritalTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            _maritalTypeService = maritalTypeService;
        }

        [HttpGet]
        [Route("get-marital-types")]
        public IHttpActionResult GetMaritalTypes(int ps, int pn, string qs)
        {
            int total = 0;
            List<MaritalTypeModel> maritalTypes = _maritalTypeService.GetMaritalTypes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.MARITAL_TYPES);
            return Ok(new ResponseMessage<List<MaritalTypeModel>>()
            {
                Result = maritalTypes,
                Total = total, Permission=permission
            });
        }

      
        [HttpGet]
        [Route("get-marital-type")]
        public async Task<IHttpActionResult> GetMaritalType(int id)
        {
            MaritalTypeModel model = await _maritalTypeService.GetMaritalType(id);
            return Ok(new ResponseMessage<MaritalTypeModel>
            {
                Result = model
            });
        }

        
        [HttpPost]
        [ModelValidation]
        [Route("save-marital-type")]
        public async Task<IHttpActionResult> SaveMaritalType([FromBody] MaritalTypeModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<MaritalTypeModel>
            {
                Result = await _maritalTypeService.SaveMaritalType(0, model)
            });
        }

       
        [HttpPut]
        [ModelValidation]
        [Route("update-marital-type/{id}")]
        public async Task<IHttpActionResult> UpdateMaritalType(int id, [FromBody] MaritalTypeModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<MaritalTypeModel>
            {
                Result = await _maritalTypeService.SaveMaritalType(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-marital-type/{id}")]
        public async Task<IHttpActionResult> DeleteMaritalType(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await _maritalTypeService.DeleteMaritalType(id)
            });
        }

    }
}
