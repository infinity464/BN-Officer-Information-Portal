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
    [RoutePrefix(BnoisRoutePrefix.BonusPtComApps)]
    [EnableCors("*", "*", "*")]
    public class BonusPtComAppsController : BaseController
    {
        private readonly IBonusPtComAppService bonusPtComAppService;
        private readonly ICommendationService commendationService;
        public BonusPtComAppsController(IBonusPtComAppService bonusPtComAppService,
            ICommendationService commendationService)
        {
            this.bonusPtComAppService = bonusPtComAppService;
            this.commendationService = commendationService;
 
        }

        [HttpGet]
        [Route("get-bonus-point-com-apps")]
        public IHttpActionResult GetBonusPtComApps(int id)
        {
            List<BonusPtComAppModel> models = bonusPtComAppService.GetBonusPtComApps(id);
            return base.Ok(new ResponseMessage<List<BonusPtComAppModel>>()
            {
                Result = models
            });

        }
        [HttpGet]
        [Route("get-bonus-point-com-app")]
        public async Task<IHttpActionResult> GetBonusPtComApp(int traceSettingId, int bonusPtComAppId)
        {
            BonusPtComAppViewModel vm = new BonusPtComAppViewModel();
            vm.BonusPtComApp = await bonusPtComAppService.GetBonusPtComApp(bonusPtComAppId);
            vm.Commendations = await commendationService.GetCommendationAppreciationSelectModels();
            return Ok(new ResponseMessage<BonusPtComAppViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-bonus-point-com-app/{traceSettingId}")]
        public async Task<IHttpActionResult> SaveBonusPtComApp(int traceSettingId, [FromBody] BonusPtComAppModel model)
        {
            model.TraceSettingId = traceSettingId;
            return base.Ok(new ResponseMessage<Bnois.ApplicationService.Models.BonusPtComAppModel>()
            {
                Result = await bonusPtComAppService.SaveBonusPtComApp(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-bonus-point-com-app/{bonusPtComAppId}")]
        public async Task<IHttpActionResult> UpdateBonusPtComApp(int bonusPtComAppId, [FromBody] BonusPtComAppModel model)
        {
            return base.Ok(new ResponseMessage<Bnois.ApplicationService.Models.BonusPtComAppModel>()
            {
                Result = await bonusPtComAppService.SaveBonusPtComApp(bonusPtComAppId, model)
            });
        }
    }
}
