
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
    [RoutePrefix(BnoisRoutePrefix.MedalAwards)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.MEDAL_AWARDS)]

    public class MedalAwardsController : PermissionController
    {
        private readonly IMedalAwardService medalAwardService;
        private readonly IAwardService awardService;
        private readonly IMedalService medalService;
        private readonly IPublicationService publicationService;
        private readonly IPublicationCategoryService publicationCategoryService;
 

        
        public MedalAwardsController(IMedalAwardService medalAwardService, IAwardService awardService,
            IMedalService medalService, IPublicationService publicationService, IPublicationCategoryService publicationCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.medalAwardService = medalAwardService;
            this.awardService = awardService;
            this.medalService = medalService;
            this.publicationService = publicationService;
            this.publicationCategoryService = publicationCategoryService;
    

        }

        [HttpGet]
        [Route("get-medal-awards")]
        public IHttpActionResult GetMedalAwards(int ps, int pn, string qs)
        {
            int total = 0;
            List<MedalAwardModel> models = medalAwardService.GetMedalAwards(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.MEDAL_AWARDS);
            return Ok(new ResponseMessage<List<MedalAwardModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-medal-award")]
        public async Task<IHttpActionResult> GetMedalAward(int id)
        {
            MedalAwardViewModel vm = new MedalAwardViewModel();
            vm.MedalAward = await medalAwardService.GetMedalAward(id);
            vm.Awards = await  awardService.GetAwardSelectModels();
            vm.Medals = await  medalService.GetMedalSelectModels(Convert.ToInt16(MedalType.PostCommission));
            vm.PublicationCategories = await publicationCategoryService.GetPublicationCategorySelectModels();
            vm.MedalAwardTypes =  medalAwardService.GetMedalAwardTypeSelectModels();


            if (vm.MedalAward.PublicationCategoryId > 0)
            {
                vm.Publications = await publicationService.GetPublicationSelectModelsByPublicationCategory(vm.MedalAward.PublicationCategoryId ?? 0);

            }


            return Ok(new ResponseMessage<MedalAwardViewModel>
            {
                Result = vm
            });
        }


        [HttpGet]
        [Route("get-publications-by-categories")]
        public async Task<IHttpActionResult> GetPublicationsByCategories(int id)
        {
            MedalAwardViewModel vm = new MedalAwardViewModel();
            vm.Publications = await publicationService.GetPublicationSelectModelsByPublicationCategory(id);

            return Ok(new ResponseMessage<MedalAwardViewModel>
            {
                Result = vm
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("save-medal-award")]
        public async Task<IHttpActionResult> SaveMedalAward([FromBody] MedalAwardModel model)
        {
            return Ok(new ResponseMessage<MedalAwardModel>
            {
                Result = await medalAwardService.SaveMedalAward(0, model)
            });
        }

        [HttpPut]
        [Route("update-medal-award/{id}")]
        public async Task<IHttpActionResult> UpdateMedalAward(int id, [FromBody] MedalAwardModel model)
        {
            return Ok(new ResponseMessage<MedalAwardModel>
            {
                Result = await medalAwardService.SaveMedalAward(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-medal-award/{id}")]
        public async Task<IHttpActionResult> DeleteMedalAward(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await medalAwardService.DeleteMedalAward(id)
            });
        }


        [HttpPost]
        [Route("upload-medal-award-file")]
        public async Task<IHttpActionResult> UploadMedalAwardFile()
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