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
    [RoutePrefix(BnoisRoutePrefix.CommissionTypes)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.COMMISION_TYPES)]

    public class CommissionTypesController: PermissionController
    {
        private readonly ICommissionTypeService commissionTypeService;
        public CommissionTypesController(ICommissionTypeService commissionTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.commissionTypeService = commissionTypeService;
        }

        [HttpGet]
        [Route("get-commission-types")]
        public IHttpActionResult GetCommissionTypes(int ps, int pn, string qs)
        {
            int total = 0;
            List<CommissionTypeModel> models = commissionTypeService.CommissionTypes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.COMMISION_TYPES);
            return Ok(new ResponseMessage<List<CommissionTypeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-commission-type")]
        public async Task<IHttpActionResult> GetCommissionType(int id)
        {
            CommissionTypeModel model = await commissionTypeService.GetCommissionType(id);
            return Ok(new ResponseMessage<CommissionTypeModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-commission-type")]
        public async Task<IHttpActionResult> SaveCommissionType([FromBody] CommissionTypeModel model)
        {
            return Ok(new ResponseMessage<CommissionTypeModel>
            {
                Result = await commissionTypeService.SaveCommissionType(0, model)
            });
        }

        [HttpPut]
        [Route("update-commission-type/{id}")]
        public async Task<IHttpActionResult> UpdateCommissionType(int id, [FromBody] CommissionTypeModel model)
        {
            return Ok(new ResponseMessage<CommissionTypeModel>
            {
                Result = await commissionTypeService.SaveCommissionType(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-commission-type/{id}")]
        public async Task<IHttpActionResult> DeleteCommissionType(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await commissionTypeService.DeleteCommissionType(id)
            });
        }
    }
}
