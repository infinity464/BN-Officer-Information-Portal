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
    [RoutePrefix(BnoisRoutePrefix.BonusPtPublics)]
    [EnableCors("*", "*", "*")]
    public class BonusPtPublicsController : BaseController
    {
        private readonly IBonusPtPublicService bonusPtPublicService;
        private readonly IPublicationCategoryService publicationCategoryService;
        public BonusPtPublicsController(IBonusPtPublicService bonusPtPublicService,
            IPublicationCategoryService publicationCategoryService)
        {
            this.bonusPtPublicService = bonusPtPublicService;
            this.publicationCategoryService = publicationCategoryService;
 
        }

        [HttpGet]
        [Route("get-bonus-point-publics")]
        public IHttpActionResult GetBonusPtPublics(int id)
        {
            List<Bnois.ApplicationService.Models.BonusPtPublicModel> models = bonusPtPublicService.GetBonusPtPublics(id);
            return base.Ok(new ResponseMessage<List<BonusPtPublicModel>>()
            {
                Result = models
            });

        }
        [HttpGet]
        [Route("get-bonus-point-public")]
        public async Task<IHttpActionResult> GetBonusPtPublic(int traceSettingId, int bonusPtPublicId)
        {
            BonusPtPublicViewModel vm = new BonusPtPublicViewModel();
            vm.BonusPtPublic = await bonusPtPublicService.GetBonusPtPublic(bonusPtPublicId);
            vm.PublicationCategories = await publicationCategoryService.GetTracePublicationCategorySelectModels();
            return Ok(new ResponseMessage<BonusPtPublicViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-bonus-point-public/{traceSettingId}")]
        public async Task<IHttpActionResult> SaveBonusPtPublic(int traceSettingId, [FromBody] BonusPtPublicModel model)
        {
            model.TraceSettingId = traceSettingId;
            return base.Ok(new ResponseMessage<Bnois.ApplicationService.Models.BonusPtPublicModel>()
            {
                Result = await bonusPtPublicService.SaveBonusPtPublic(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-bonus-point-public/{bonusPtPublicId}")]
        public async Task<IHttpActionResult> UpdateBonusPtPublic(int bonusPtPublicId, [FromBody] BonusPtPublicModel model)
        {
            return base.Ok(new ResponseMessage<Bnois.ApplicationService.Models.BonusPtPublicModel>()
            {
                Result = await bonusPtPublicService.SaveBonusPtPublic(bonusPtPublicId, model)
            });
        }
    }
}
