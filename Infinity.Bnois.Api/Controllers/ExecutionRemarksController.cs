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
    [RoutePrefix(BnoisRoutePrefix.ExecutionRemarks)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EXECUTION_REMARKS)]

    public class ExecutionRemarksController: PermissionController
    {
        private readonly IExecutionRemarkService executionRemarkService;
        public ExecutionRemarksController(IExecutionRemarkService executionRemarkService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.executionRemarkService = executionRemarkService;
        }

        [HttpGet]
        [Route("get-execution-remarks")]
        public IHttpActionResult GetExecutionRemarks(int ps, int pn, string qs,int type)
        {
            int total = 0;
            List<ExecutionRemarkModel> models = executionRemarkService.GetExecutionRemarks(ps, pn, qs, out total,type);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EXECUTION_REMARKS);
            return Ok(new ResponseMessage<List<ExecutionRemarkModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-execution-remark")]
        public async Task<IHttpActionResult> GetExecutionRemark(int id)
        {
            ExecutionRemarkModel model = await executionRemarkService.GetExecutionRemark(id);
            return Ok(new ResponseMessage<ExecutionRemarkModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-execution-remark")]
        public async Task<IHttpActionResult> SaveExecutionRemark([FromBody] ExecutionRemarkModel model)
        {
            return Ok(new ResponseMessage<ExecutionRemarkModel>
            {
                Result = await executionRemarkService.SaveExecutionRemark(0, model)
            });
        }

        [HttpPut]
        [Route("update-execution-remark/{id}")]
        public async Task<IHttpActionResult> UpdateExecutionRemark(int id, [FromBody] ExecutionRemarkModel model)
        {
            return Ok(new ResponseMessage<ExecutionRemarkModel>
            {
                Result = await executionRemarkService.SaveExecutionRemark(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-execution-remark/{id}")]
        public async Task<IHttpActionResult> DeleteExecutionRemark(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await executionRemarkService.DeleteExecutionRemark(id)
            });
        }
    }
}
