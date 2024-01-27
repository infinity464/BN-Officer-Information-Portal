using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Ers.Applicant.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.PoorCourseResults)]
    [EnableCors("*", "*", "*")]
    public class PoorCourseResultsController : BaseController
    {
        private readonly IPoorCourseResultService poorCourseResultService;
        private readonly IResultTypeService resultTypeService;
        public PoorCourseResultsController(IPoorCourseResultService poorCourseResultService,
            IResultTypeService resultTypeService)
        {
            this.poorCourseResultService = poorCourseResultService;
            this.resultTypeService = resultTypeService;
        }

        [HttpGet]
        [Route("get-poor-course-results")]
        public IHttpActionResult GetPoorCourseResults(int id)
        {
            List<PoorCourseResultModel> models = poorCourseResultService.GetPoorCourseResults(id);
            return Ok(new ResponseMessage<List<PoorCourseResultModel>>()
            {
                Result = models
            });

        }
        [HttpGet]
        [Route("get-poor-course-result")]
        public async Task<IHttpActionResult> GetPoorCourseResult(int traceSettingId, int poorCourseResultId)
        {
            PoorCourseResultViewModel vm = new PoorCourseResultViewModel();
            vm.PoorCourseResult = await poorCourseResultService.GetPoorCourseResult(poorCourseResultId);
            vm.ResultTypes = await resultTypeService.GetResultTypeSelectModels();
            return Ok(new ResponseMessage<PoorCourseResultViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-poor-course-result/{traceSettingId}")]
        public async Task<IHttpActionResult> SavePoorCourseResult(int traceSettingId, [FromBody] PoorCourseResultModel model)
        {
            model.TraceSettingId = traceSettingId;
            return Ok(new ResponseMessage<PoorCourseResultModel>()
            {
                Result = await poorCourseResultService.SavePoorCourseResult(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-poor-course-result/{poorCourseResultId}")]
        public async Task<IHttpActionResult> UpdatePoorCourseResult(int PoorCourseResultId, [FromBody] PoorCourseResultModel model)
        {
            return Ok(new ResponseMessage<PoorCourseResultModel>()
            {
                Result = await poorCourseResultService.SavePoorCourseResult(PoorCourseResultId, model)
            });
        }
    }
}
