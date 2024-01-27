using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
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
    [RoutePrefix(BnoisRoutePrefix.FlashNotice)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.FLASHNOTICE)]

    public class FlashNoticeController: BaseController
    {
        private readonly IFlashNoticeService _flashNoticeService;
        public FlashNoticeController(IFlashNoticeService flashNoticeService)
        {
            _flashNoticeService = flashNoticeService;
        }
        [HttpGet]
        [Route("get-flash-notice")]
        public IHttpActionResult GetFlashNotices()
        {
            List<FlashNoticeModel> models = _flashNoticeService.GetFlashNotices();

            List<FlashNoticeModel> activeModels = models.Where(x => x.IsActive == true).ToList();
            List<FlashNoticeModel> inActiveModels = models.Where(x => x.IsActive == false).ToList();

            FlashNoticeViewModel viewModel = new FlashNoticeViewModel();

            viewModel.ActiveFlashNotice = activeModels;
            viewModel.InActiveFlashNotice = inActiveModels;

            return Ok(new ResponseMessage<FlashNoticeViewModel>()
            {
                Result = viewModel
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-flash-notice")]
        public async Task<IHttpActionResult> SaveFlashNotice([FromBody] FlashNoticeModel model)
        {
            return Ok(new ResponseMessage<FlashNoticeModel>
            {
                Result = await _flashNoticeService.SaveFlashNotice(0, model)
            });
        }
        [HttpPut]
        [Route("update-flash-notice/{id}")]
        public async Task<IHttpActionResult> UpdateFlashNotice(int id, [FromBody] FlashNoticeModel model)
        {
            return Ok(new ResponseMessage<FlashNoticeModel>
            {
                Result = await _flashNoticeService.SaveFlashNotice(id, model)
            });
        }

        [HttpGet]
        [Route("get-flash-notice-by-id")]
        public async Task<IHttpActionResult> GetFlashNoticeById(int id)
        {
            return Ok(new ResponseMessage<FlashNoticeModel>()
            {
                Result =  _flashNoticeService.GetFlashNotice(id)
            });
        }
        
        [HttpDelete]
        [Route("delete-flash-notice")]
        public async Task<IHttpActionResult> DeleteFlashNotice(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await _flashNoticeService.DeleteFlashNotice(id)
            });
        }

        [HttpGet]
        [Route("get-flash-notice-memberuser")]
        public IHttpActionResult GetFlashNoticeForMemberUser()
        {
            List<FlashNoticeModel> models = _flashNoticeService.GetFlashNoticeForMemberUser();

            return Ok(new ResponseMessage<List<FlashNoticeModel>>()
            {
                Result = models
            });
        }
    }
}
