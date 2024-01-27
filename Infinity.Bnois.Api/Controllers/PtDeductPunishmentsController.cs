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
    [RoutePrefix(BnoisRoutePrefix.PtDeductPunishments)]
    [EnableCors("*", "*", "*")]
    public class PtDeductPunishmentsController : BaseController
    {
        private readonly IPtDeductPunishmentService ptDeductPunishmentService;
        private readonly IPunishmentCategoryService punishmentCategoryService;
        private readonly IPunishmentSubCategoryService punishmentSubCategoryService;
        private readonly IPunishmentNatureService punishmentNatureService;
        public PtDeductPunishmentsController(IPtDeductPunishmentService ptDeductPunishmentService,
            IPunishmentCategoryService punishmentCategoryService,
            IPunishmentSubCategoryService punishmentSubCategoryService,
            IPunishmentNatureService punishmentNatureService)
        {
            this.ptDeductPunishmentService = ptDeductPunishmentService;
            this.punishmentCategoryService = punishmentCategoryService;
            this.punishmentSubCategoryService = punishmentSubCategoryService;
            this.punishmentNatureService = punishmentNatureService;
        }

        [HttpGet]
        [Route("get-point-deduction-for-punishments")]
        public IHttpActionResult GetPtDeductPunishments(int id)
        {
            List<PtDeductPunishmentModel> models = ptDeductPunishmentService.GetPtDeductPunishments(id);
            return Ok(new ResponseMessage<List<PtDeductPunishmentModel>>()
            {
                Result = models
            });

        }
        [HttpGet]
        [Route("get-point-deduction-for-punishment")]
        public async Task<IHttpActionResult> GetPtDeductPunishment(int traceSettingId, int ptDeductPunishmentId)
        {
            PtDeductPunishmentViewModel vm = new PtDeductPunishmentViewModel();
            vm.PtDeductPunishment = await ptDeductPunishmentService.GetPtDeductPunishment(ptDeductPunishmentId);
            vm.PunishmentSubCategories = await punishmentSubCategoryService.GetPunishmentSubCategoryForTrace();
            vm.PunishmentNatures = await punishmentNatureService.GetPunishmentNatureSelectModels();
            return Ok(new ResponseMessage<PtDeductPunishmentViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-point-deduction-for-punishment/{traceSettingId}")]
        public async Task<IHttpActionResult> SavePtDeductPunishment(int traceSettingId, [FromBody] PtDeductPunishmentModel model)
        {
            model.TraceSettingId = traceSettingId;
            return Ok(new ResponseMessage<PtDeductPunishmentModel>()
            {
                Result = await ptDeductPunishmentService.SavePtDeductPunishment(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-point-deduction-for-punishment/{ptDeductPunishmentId}")]
        public async Task<IHttpActionResult> UpdatePtDeductPunishment(int ptDeductPunishmentId, [FromBody] PtDeductPunishmentModel model)
        {
            return Ok(new ResponseMessage<PtDeductPunishmentModel>()
            {
                Result = await ptDeductPunishmentService.SavePtDeductPunishment(ptDeductPunishmentId, model)
            });
        }
    }
}
