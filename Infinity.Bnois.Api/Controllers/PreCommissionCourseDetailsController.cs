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

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.PreCommissionCourseDetails)]
    [EnableCors("*", "*", "*")]
    public class PreCommissionCourseDetailsController : BaseController
    {
        private readonly IPreCommissionCourseDetailService preCommissionCourseDetailService;
        public PreCommissionCourseDetailsController(IPreCommissionCourseDetailService preCommissionCourseDetailService)
        {
            this.preCommissionCourseDetailService = preCommissionCourseDetailService;
        }       
        [HttpGet]
        [Route("get-pre-commission-course-details")]
        public IHttpActionResult GetPreCommissionCourseDetails(int preCommissionCourseId)
        {
            List<PreCommissionCourseDetailModel> models = preCommissionCourseDetailService.GetPreCommissionCourseDetails(preCommissionCourseId);
            return Ok(new ResponseMessage<List<PreCommissionCourseDetailModel>>()
            {
                Result = models,

            });
        }

        [HttpGet]
        [Route("get-pre-commission-course-detail")]
        public async Task<IHttpActionResult> GetPreCommissionCourseDetail(int preCommissionCourseId, int preCommissionCourseDetailId)
        {
            PreCommissionCourseDetailModel model = await preCommissionCourseDetailService.GetPreCommissionCourseDetail(preCommissionCourseDetailId);
            return Ok(new ResponseMessage<PreCommissionCourseDetailModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-pre-commission-course-detail")]
        public async Task<IHttpActionResult> SavePreCommissionCourseDetail([FromBody] PreCommissionCourseDetailModel model)
        {
            return Ok(new ResponseMessage<PreCommissionCourseDetailModel>
            {
                Result = await preCommissionCourseDetailService.SavePreCommissionCourseDetail(0, model)
            });
        }

        [HttpPut]
        [Route("update-pre-commission-course-detail/{preCommissionCourseDetailId}")]
        public async Task<IHttpActionResult> UpdatePreCommissionCourseDetail(int PreCommissionCourseDetailId, [FromBody] PreCommissionCourseDetailModel model)
        {
            return Ok(new ResponseMessage<PreCommissionCourseDetailModel>
            {
                Result = await preCommissionCourseDetailService.SavePreCommissionCourseDetail(PreCommissionCourseDetailId, model)
            });
        }
    }
}
