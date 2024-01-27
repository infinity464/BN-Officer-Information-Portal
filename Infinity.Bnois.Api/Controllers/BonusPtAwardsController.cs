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
    [RoutePrefix(BnoisRoutePrefix.BonusPtAwards)]
    [EnableCors("*", "*", "*")]
    public class BonusPtAwardsController : BaseController
    {
        private readonly IBonusPtAwardService bonusPtAwardService;
        private readonly IAwardService awardService;
        public BonusPtAwardsController(IBonusPtAwardService bonusPtAwardService, IAwardService awardService)
        {
            this.bonusPtAwardService = bonusPtAwardService;
            this.awardService = awardService;
 
        }

        [HttpGet]
        [Route("get-bonus-point-awards")]
        public IHttpActionResult GetBonusPtAwards(int id)
        {
            List<BonusPtAwardModel> models = bonusPtAwardService.GetBonusPtAwards(id);
            return Ok(new ResponseMessage<List<BonusPtAwardModel>>()
            {
                Result = models
            });

        }
        [HttpGet]
        [Route("get-bonus-point-award")]
        public async Task<IHttpActionResult> GetBonusPtAward(int traceSettingId, int bonusPtAwardId)
        {
            BonusPtAwardViewModel vm = new BonusPtAwardViewModel();
            vm.BonusPtAward = await bonusPtAwardService.GetBonusPtAward(bonusPtAwardId);
            vm.Awards = await awardService.GetAwardSelectModels();
            return Ok(new ResponseMessage<BonusPtAwardViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-bonus-point-award/{traceSettingId}")]
        public async Task<IHttpActionResult> SaveBonusPtAward(int traceSettingId, [FromBody] BonusPtAwardModel model)
        {
            model.TraceSettingId = traceSettingId;
            return Ok(new ResponseMessage<BonusPtAwardModel>()
            {
                Result = await bonusPtAwardService.SaveBonusPtAward(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-bonus-point-award/{bonusPtAwardId}")]
        public async Task<IHttpActionResult> UpdateBonusPtAward(int bonusPtAwardId, [FromBody] BonusPtAwardModel model)
        {
            return Ok(new ResponseMessage<BonusPtAwardModel>()
            {
                Result = await bonusPtAwardService.SaveBonusPtAward(bonusPtAwardId, model)
            });
        }
    }
}
