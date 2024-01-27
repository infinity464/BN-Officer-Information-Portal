
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
    [RoutePrefix(BnoisRoutePrefix.PunishmentAccidents)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PUNISHMENT_ACCIDENTS)]

    public class PunishmentAccidentsController : PermissionController
    {
        private readonly IPunishmentAccidentService punishmentAccidentService;
        private readonly IPunishmentCategoryService publicationCategoryService;
        private readonly IPunishmentSubCategoryService punishmentSubCategoryService;
        private readonly IPunishmentNatureService punishmentNatureService;
       


        public PunishmentAccidentsController(IPunishmentAccidentService punishmentAccidentService,
            IPunishmentCategoryService publicationCategoryService, IPunishmentSubCategoryService punishmentSubCategoryService,
            IPunishmentNatureService punishmentNatureService,
            IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.punishmentAccidentService = punishmentAccidentService;
            this.publicationCategoryService = publicationCategoryService;
            this.punishmentSubCategoryService = punishmentSubCategoryService;
            this.punishmentNatureService = punishmentNatureService;
          

        }

        [HttpGet]
        [Route("get-punishment-accidents")]
        public IHttpActionResult GetPunishmentAccidents(int ps, int pn, string qs)
        {
            int total = 0;
            List<PunishmentAccidentModel> models = punishmentAccidentService.GetPunishmentAccidents(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PUNISHMENT_ACCIDENTS);
            return Ok(new ResponseMessage<List<PunishmentAccidentModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-punishment-accident")]
        public async Task<IHttpActionResult> GetPunishmentAccident(int id)
        {
            PunishmentAccidentViewModel vm = new PunishmentAccidentViewModel();
            vm.PunishmentAccident = await punishmentAccidentService.GetPunishmentAccident(id);
            vm.PunishmentCategories = await  publicationCategoryService.GetPunishmentCategorySelectModels();
            vm.PunishmentNatures = await  punishmentNatureService.GetPunishmentNatureSelectModels();
            vm.AccidentTypes =  punishmentAccidentService.GetAccidentTypeSelectModels();
            vm.PunishmentAccidentTypes = punishmentAccidentService.GetPunishmentAccidentTypeSelectModels();
            

            if (vm.PunishmentAccident.PunishmentCategoryId > 0)
            {
                vm.PunishmentSubCategories = await punishmentSubCategoryService.GetPunishmentSubCategorySelectModelsByPunishmentCategory(vm.PunishmentAccident.PunishmentCategoryId ?? 0);
            }
            
            return Ok(new ResponseMessage<PunishmentAccidentViewModel>
            {
                Result = vm
            });
        }


        [HttpGet]
        [Route("get-punishment-sub-categories-by-category")]
        public async Task<IHttpActionResult> GetPunishmentSubCategoryByCategories(int id)
        {
            PunishmentAccidentViewModel vm = new PunishmentAccidentViewModel();
            vm.PunishmentSubCategories = await punishmentSubCategoryService.GetPunishmentSubCategorySelectModelsByPunishmentCategory(id);

            return Ok(new ResponseMessage<PunishmentAccidentViewModel>
            {
                Result = vm
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("save-punishment-accident")]
        public async Task<IHttpActionResult> SavePunishmentAccident([FromBody] PunishmentAccidentModel model)
        {
            return Ok(new ResponseMessage<PunishmentAccidentModel>
            {
                Result = await punishmentAccidentService.SavePunishmentAccident(0, model)
            });
        }

        [HttpPut]
        [Route("update-punishment-accident/{id}")]
        public async Task<IHttpActionResult> UpdatePunishmentAccident(int id, [FromBody] PunishmentAccidentModel model)
        {
            return Ok(new ResponseMessage<PunishmentAccidentModel>
            {
                Result = await punishmentAccidentService.SavePunishmentAccident(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-punishment-accident/{id}")]
        public async Task<IHttpActionResult> DeletePunishmentAccident(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await punishmentAccidentService.DeletePunishmentAccident(id)
            });
        }



        [HttpPost]
        [Route("upload-punishment-file")]
        public async Task<IHttpActionResult> UploadPunishmentFile()
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