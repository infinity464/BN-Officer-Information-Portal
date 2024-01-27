using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ResultGrades)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RESULT_GRADES)]

    public class ResultGradesController : PermissionController
    {
        private readonly IResultGradeService resultGradeService;

        public ResultGradesController(IResultGradeService resultGradeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.resultGradeService = resultGradeService;
        }

        [HttpGet]
        [Route("get-result-grades")]
        public IHttpActionResult GetResultGrades(int ps, int pn, string qs)
        {
            int total = 0;
            List<ResultGradeModel> models = resultGradeService.GetResultGrades(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.RESULT_GRADES);
            return Ok(new ResponseMessage<List<ResultGradeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-result-grade")]
        public async Task<IHttpActionResult> GetResultGrade(int id)
        {
            ResultGradeModel model = await resultGradeService.GetResultGrade(id);
            return Ok(new ResponseMessage<ResultGradeModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-result-grade")]
        public async Task<IHttpActionResult> SaveResultGrade([FromBody] ResultGradeModel model)
        {
            return Ok(new ResponseMessage<ResultGradeModel>
            {
                Result = await resultGradeService.SaveResultGrade(0, model)
            });
        }



        [HttpPut]
        [Route("update-result-grade/{id}")]
        public async Task<IHttpActionResult> UpdateResultGrade(int id, [FromBody] ResultGradeModel model)
        {
            return Ok(new ResponseMessage<ResultGradeModel>
            {
                Result = await resultGradeService.SaveResultGrade(id, model)
            });
        }



        [HttpDelete]
        [Route("delete-result-grade/{id}")]
        public async Task<IHttpActionResult> DeleteResultGrade(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await resultGradeService.DeleteResultGrade(id)
            });
        }


    }
}