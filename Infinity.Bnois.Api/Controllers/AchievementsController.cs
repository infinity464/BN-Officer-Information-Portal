using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Ers.ApplicationService;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Achievements)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.ACHIEVEMENTS)]

    public class AchievementsController : PermissionController
    {
        private readonly IAchievementService achievementService;
        private readonly ICommendationService commendationService;
        private readonly IPatternService patternService;
        private readonly IOfficeService officeService;
        private readonly IOfficeAppointmentService officeAppointmentService;


        public AchievementsController(IOfficeAppointmentService officeAppointmentService,
            IAchievementService achievementService, ICommendationService commendationService,
            IPatternService patternService, IOfficeService officeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.achievementService = achievementService;
            this.commendationService = commendationService;
            this.patternService = patternService;
            this.officeService = officeService;
            this.officeAppointmentService = officeAppointmentService;

        }

        [HttpGet]
        [Route("get-achievements")]
        public IHttpActionResult GetAchievements(int ps, int pn, string qs)
        {
            int total = 0;
            List<AchievementModel> models = achievementService.GetAchievements(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.ACHIEVEMENTS);
            return Ok(new ResponseMessage<List<AchievementModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-achievement")]
        public async Task<IHttpActionResult> GetAchievement(int id)
        {
            AchievementViewModel vm = new AchievementViewModel();
            vm.Achievement = await achievementService.GetAchievement(id);
            vm.Commendations = await  commendationService.GetCommendationSelectModels();
            vm.Appreciations = await  commendationService.GetAppreciationSelectModels();
            vm.Patterns = await officeService.GetMinistryOfficeSelectModel();
            vm.InsideOffices = await officeService.GetParentOfficeSelectModel();
            vm.GivenByTypes =  achievementService.GetGivenByTypeSelectModels();
            vm.AchievementComTypes =  achievementService.GetAchievementComTypeSelectModels();

   

            return Ok(new ResponseMessage<AchievementViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-achievement")]
        public async Task<IHttpActionResult> SaveAchievement([FromBody] AchievementModel model)
        {
            return Ok(new ResponseMessage<AchievementModel>
            {
                Result = await achievementService.SaveAchievement(0, model)
            });
        }

        [HttpPut]
        [Route("update-achievement/{id}")]
        public async Task<IHttpActionResult> UpdateAchievement(int id, [FromBody] AchievementModel model)
        {
            return Ok(new ResponseMessage<AchievementModel>
            {
                Result = await achievementService.SaveAchievement(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-achievement/{id}")]
        public async Task<IHttpActionResult> DeleteAchievement(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await achievementService.DeleteAchievement(id)
            });
        }


        [HttpPost]
        [Route("upload-achievement-file")]
        public async Task<IHttpActionResult> UploadAchievementFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            string fileSaveLocation = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;
            fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/Image");

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(provider);

            fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
            filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/Image", fileName);

            FileModel file = new FileModel
            {
                FileName = fileName,
                FilePath = filePath
            };
            return Ok(new ResponseMessage<FileModel>
            {
                Result = file
            });
        }
    }
}