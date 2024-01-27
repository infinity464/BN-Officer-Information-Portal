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
    [RoutePrefix(BnoisRoutePrefix.BonusPtMedals)]
    [EnableCors("*", "*", "*")]
    public class BonusPtMedalsController : BaseController
    {
        private readonly IBonusPtMedalService bonusPtMedalService;
        private readonly IMedalService medalService;
        public BonusPtMedalsController(IBonusPtMedalService bonusPtMedalService,
            IMedalService medalService)
        {
            this.bonusPtMedalService = bonusPtMedalService;
            this.medalService = medalService;
 
        }

        [HttpGet]
        [Route("get-bonus-point-medals")]
        public IHttpActionResult GetBonusPtMedals(int id)
        {
            List<BonusPtMedalModel> models = bonusPtMedalService.GetBonusPtMedals(id);
            return Ok(new ResponseMessage<List<BonusPtMedalModel>>()
            {
                Result = models
            });

        }
        [HttpGet]
        [Route("get-bonus-point-medal")]
        public async Task<IHttpActionResult> GetBonusPtMedal(int traceSettingId, int bonusPtMedalId)
        {
            BonusPtMedalViewModel vm = new BonusPtMedalViewModel();
            vm.BonusPtMedal = await bonusPtMedalService.GetBonusPtMedal(bonusPtMedalId);
            vm.Medals = await medalService.GetTraceMedalSelectModels((int)MedalType.PostCommission);
            return Ok(new ResponseMessage<BonusPtMedalViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-bonus-point-medal/{traceSettingId}")]
        public async Task<IHttpActionResult> SaveBonusPtMedal(int traceSettingId, [FromBody] BonusPtMedalModel model)
        {
            model.TraceSettingId = traceSettingId;
            return Ok(new ResponseMessage<BonusPtMedalModel>()
            {
                Result = await bonusPtMedalService.SaveBonusPtMedal(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-bonus-point-medal/{bonusPtMedalId}")]
        public async Task<IHttpActionResult> UpdateBonusPtMedal(int bonusPtMedalId, [FromBody] BonusPtMedalModel model)
        {
            return Ok(new ResponseMessage<BonusPtMedalModel>()
            {
                Result = await bonusPtMedalService.SaveBonusPtMedal(bonusPtMedalId, model)
            });
        }
    }
}
