using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.DailyProcesses)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.DAILY_PROCESSES)]
    public class DailyProcessesController : BaseController
    {
        private readonly IPromotionNominationService promotionNominationService;
        private readonly IPunishmentAccidentService punishmentAccidentService;
        private readonly IEmployeeService employeeService;
        private readonly IPhotoService photoService;
        private readonly ITransferService transferService;
        private readonly IPromotionBoardService promotionBoardService;
        private readonly IAdvanceSearchService advanceSearchService;
        public DailyProcessesController(IPromotionBoardService promotionBoardService,IPhotoService photoService,IEmployeeService employeeService,IPromotionNominationService promotionNominationService, 
            IPunishmentAccidentService punishmentAccidentService,ITransferService transferService, IAdvanceSearchService advanceSearchService)
        {
            this.promotionNominationService = promotionNominationService;
            this.punishmentAccidentService = punishmentAccidentService;
            this.employeeService = employeeService;
            this.transferService = transferService;
            this.photoService = photoService;
            this.promotionBoardService = promotionBoardService;
            this.advanceSearchService = advanceSearchService;
        }

        [HttpGet]
        [Route("get-daily-processes")]
        public async Task<IHttpActionResult> GetDailyProcesses()
        {
            List<SelectModel> promotionBoards = promotionBoardService.GetDailyProcesPromotionBoardSelectModels();
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = promotionBoards
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("execute-promotion")]
        public async Task<IHttpActionResult> ExecutePromotion(int promotionBoardId)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await promotionNominationService.ExecutePromotion(promotionBoardId)
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("execute-data-base-backup")]
        public async Task<IHttpActionResult> ExecuteDataBaseBackup()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await promotionNominationService.ExecuteDatabaseBackup()
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("execute-promotion-without-board")]
        public async Task<IHttpActionResult> ExecutePromotionWithOutBoard()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await promotionNominationService.ExecutePromotionWithOutBoard()
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("execute-transfer")]
        public  IHttpActionResult ExecuteTransfer()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result =  transferService.ExecuteTransfer()
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("execute-advance-search")]
        public IHttpActionResult ExecuteAdvanceSearch()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = advanceSearchService.ExecuteAdvanceSearch()
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("update-sea-service")]
        public IHttpActionResult UpdateSeaService()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = advanceSearchService.UpdateSeaService()
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("update-sea-cmd-service")]
        public IHttpActionResult UpdateSeaCmdService()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = advanceSearchService.UpdateSeaCmdService()
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("update-sea-service-days")]
        public IHttpActionResult UpdateSeaServiceDays()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = advanceSearchService.UpdateSeaServiceDays()
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("update-sea-service-years")]
        public IHttpActionResult UpdateSeaServiceYears()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = advanceSearchService.UpdateSeaServiceYears()
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("execute-naming-convention")]
        public IHttpActionResult ExecuteNamingConvention()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = employeeService.ExecuteNamingConvention()
            });
        }



        [HttpGet]
        [Route("get-update-punishment")]
        public async Task<IHttpActionResult> GetPunishmentUpdate()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await punishmentAccidentService.ExecutePunishmentProcess()
            });
        }

        [HttpGet]
        [Route("get-execute-seniority")]
        public async Task<IHttpActionResult> GetSeniorityUpdate()
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeService.ExecuteSeniorityProcess()
            });
        }

        [HttpPut]
        [Route("update-age-service-policy")]
        public async Task<IHttpActionResult> UpdateAgeServicePolicy()
        {
            return Ok(new ResponseMessage<string>
            {
                Result = await employeeService.UpdateAgeServicePolicy()
            });
        }


        [HttpPost]
        [Route("upload-image-into-folder")]
        public async Task<IHttpActionResult> UploadImageToFolder()
        {
            return Ok(new ResponseMessage<string>
            {
                Result = await photoService.UploadImageToFolder()
            });
        }


    }
}
