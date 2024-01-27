using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
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
    [RoutePrefix(BnoisRoutePrefix.FeedBack)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.FEEDBACK)]
    public class FeedBackController : BaseController
    {
        private readonly IFeedBackService _feedBackService;
        private readonly IAdminFeedBackService _adminFeedBackService;

        public FeedBackController(IFeedBackService feedBackService, IAdminFeedBackService adminFeedBackService)
        {
            _feedBackService = feedBackService;
            _adminFeedBackService = adminFeedBackService;
        }
        [HttpGet]
        [Route("get-feed-backs")]
        public IHttpActionResult GetFeedBacks(int ps, int pn, string qs)
        {
            int total = 0;
            List<FeedBackModel> models = _feedBackService.GetFeedBacks(ps, pn, qs, out total);
            return Ok(new ResponseMessage<List<FeedBackModel>>()
            {
                Result = models,
                Total = total
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-feed-back")]
        public async Task<IHttpActionResult> SaveFeedBack([FromBody] FeedBackModel model)
        {
            return Ok(new ResponseMessage<FeedBackModel>
            {
                Result = await _feedBackService.SaveFeedBack(0, model)
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-admin-feed-back")]
        public async Task<IHttpActionResult> SaveAdminFeedBack([FromBody] AdminFeedBackModel model)
        {
            return Ok(new ResponseMessage<AdminFeedBackModel>
            {
                Result = await _adminFeedBackService.SaveAdminFeedBack(0, model)
            });
        }
        [HttpGet]
        [Route("get-feed-back-by-feedback-id")]
        public async Task<IHttpActionResult> GetFeedbackById(int id)
        {
            List<object> feedBack = await _feedBackService.GetFeedBackById(id);
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = feedBack
            });
        }
        [HttpGet]
        [Route("get-feed-back-for-user")]
        public IHttpActionResult GetFeedbackForUser()
        {
            string notification = _feedBackService.GetFeedBackForUser();
            return Ok(new ResponseMessage<string>()
            {
                Result = notification,

            });
        }
        [HttpGet]
        [Route("get-feed-back-for-user-notification")]
        public IHttpActionResult GetFeedbackForUserNotification()
        {
            int notification = _feedBackService.GetFeedBackForUserNotification();
            return Ok(new ResponseMessage<int>()
            {
                Result = notification,

            });
        }
        [HttpDelete]
        [Route("delete-feed-back")]
        public async Task<IHttpActionResult> DeleteFeedback(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await _feedBackService.DeleteFeedback(id)
            });
        }
    }
}
