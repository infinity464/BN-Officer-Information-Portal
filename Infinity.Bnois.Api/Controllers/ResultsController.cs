using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Controllers;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Ers.Admin.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Results)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RANKS)]
    public class ResultsController : PermissionController
    {
        private readonly IResultService resultService;
        private readonly IExamCategoryService examCategoryService;
        public ResultsController(IResultService resultService, IExamCategoryService examCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.resultService = resultService;
            this.examCategoryService = examCategoryService;
        }

        [HttpGet]
        [Route("get-results")]
        public IHttpActionResult GetResults(int ps, int pn, string qs)
        {
            int total = 0;
            List<ResultModel> models = resultService.GetResults(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.RANKS);
            return Ok(new ResponseMessage<List<ResultModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-result")]
        public async Task<IHttpActionResult> GetResult(int resultId)
        {
            ResultModel model = await resultService.GetResult(resultId);
            List<SelectModel> examCategories = examCategoryService.GetExamCategories();
            return Ok(new ResponseMessage<ResultViewModel>
            {
                Result = new ResultViewModel
                {
                    ExamCategories = examCategories,
                    Result = model
                }
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-result")]
        public async Task<IHttpActionResult> SaveResult([FromBody]ResultModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<ResultModel>()
            {
                Result = await resultService.SaveResult(0, model)
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-result/{resultId}")]
        public async Task<IHttpActionResult> UpdateResult(int resultId, [FromBody]ResultModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<ResultModel>()
            {
                Result = await resultService.SaveResult(resultId, model)
            });
        }

        [HttpDelete]
        [Route("delete-result/{resultId}")]
        public async Task<IHttpActionResult> DeleteResult(int resultId)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await resultService.DeleteResult(resultId)
            });
        }
    }
}
