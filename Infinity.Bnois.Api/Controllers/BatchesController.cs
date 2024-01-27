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
    [RoutePrefix(BnoisRoutePrefix.Batches)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.BATCHES)]

    public class BatchesController: PermissionController
    {
        private readonly IBatchService batchService;
        public BatchesController(IBatchService batchService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.batchService = batchService;
        }

        [HttpGet]
        [Route("get-batches")]
        public IHttpActionResult GetBatches(int ps, int pn, string qs)
        {
            int total = 0;
            List<BatchModel> models = batchService.GetBatches(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.BATCHES);
            return Ok(new ResponseMessage<List<BatchModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-batch")]
        public async Task<IHttpActionResult> GetBatch(int id)
        {
            BatchModel model = await batchService.GetBatch(id);
            return Ok(new ResponseMessage<BatchModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-batch")]
        public async Task<IHttpActionResult> SaveBatch([FromBody] BatchModel model)
        {
            return Ok(new ResponseMessage<BatchModel>
            {
                Result = await batchService.SaveBatch(0, model)
            });
        }

        [HttpPut]
        [Route("update-batch/{id}")]
        public async Task<IHttpActionResult> UpdateBatch(int id, [FromBody] BatchModel model)
        {
            return Ok(new ResponseMessage<BatchModel>
            {
                Result = await batchService.SaveBatch(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-batch/{id}")]
        public async Task<IHttpActionResult> DeleteBatch(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await batchService.DeleteBatch(id)
            });
        }
    }
}
