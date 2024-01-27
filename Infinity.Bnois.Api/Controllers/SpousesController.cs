using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Spouses)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]

    public class SpousesController : PermissionController
    {
        private readonly ISpouseService spouseService;
        private readonly IOccupationService occupationService;
        private readonly IRankCategoryService rankCategoryService;

        public SpousesController(ISpouseService spouseService, IOccupationService occupationService, IRankCategoryService rankCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.spouseService = spouseService;
            this.occupationService = occupationService;
            this.rankCategoryService = rankCategoryService;
        }

        [HttpGet]
        [Route("get-spouses")]
        public IHttpActionResult GetSpouses(int employeeId)
        {
            int total = 0;
            List<SpouseModel> models = spouseService.GetSpouses(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<SpouseModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-spouse")]
        public async Task<IHttpActionResult> GetSpouse(int employeeId, int spouseId)
        {
            SpouseViewModel vm = new SpouseViewModel();
            vm.Spouse = await spouseService.GetSpouse(spouseId);
            vm.Occupations = await occupationService.GetOccupationSelectModels();
            vm.CurrentStatus = spouseService.GetCurrentStatusSelectModels();
            vm.RelationTypes = spouseService.GetRelationTypeSelectModels();
            vm.RankCategories = await rankCategoryService.GetRankCategorySelectModels();
            vm.File = new FileModel { FileName=vm.Spouse.FileName,FilePath=Documents.RemoteImageUrl+vm.Spouse.FileName};
            vm.GenFormFile = new FileModel { FileName=vm.Spouse.GenFormFileName,FilePath=Documents.RemoteImageUrl+vm.Spouse.GenFormFileName};
            return Ok(new ResponseMessage<SpouseViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-spouse/{employeeId}")]
        public async Task<IHttpActionResult> SaveSpouse(int employeeId, [FromBody] SpouseModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<SpouseModel>
            {
                Result = await spouseService.SaveSpouse(0, model)
            });
        }

        [HttpPut]
        [Route("update-spouse/{spouseId}")]
        public async Task<IHttpActionResult> UpdateSpouse(int spouseId, [FromBody] SpouseModel model)
        {
            return Ok(new ResponseMessage<SpouseModel>
            {
                Result = await spouseService.SaveSpouse(spouseId, model)

            });
        }

        [HttpDelete]
        [Route("delete-spouse/{id}")]
        public async Task<IHttpActionResult> DeleteSpouse(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await spouseService.DeleteSpouse(id)
            });
        }

        [HttpPost]
        [Route("spouse-image-upload")]
        public async Task<IHttpActionResult> UploadSpouseImage(int employeeId,int spouseId)
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


            SpouseViewModel vm = new SpouseViewModel();
            vm.Spouse = new SpouseModel();
            vm.Spouse.EmployeeId = employeeId;
            vm.Spouse.SpouseId = spouseId;
            vm.Spouse.FileName = fileName;

            vm.Spouse = await spouseService.UpdateSpouse(vm.Spouse);


            vm.File = new FileModel
            {
                FileName = vm.Spouse.FileName,
                FilePath = Documents.RemoteImageUrl + vm.Spouse.FileName
            };
            return Ok(new ResponseMessage<SpouseViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
        [Route("spouse-gen-form-upload")]
        public async Task<IHttpActionResult> UploadSpouseGenForm(int employeeId, int spouseId)
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


            SpouseViewModel vm = new SpouseViewModel();
            vm.Spouse = new SpouseModel();
            vm.Spouse.EmployeeId = employeeId;
            vm.Spouse.SpouseId = spouseId;
            vm.Spouse.GenFormFileName = fileName;

            vm.Spouse = await spouseService.UpdateSpouse(vm.Spouse);


            vm.GenFormFile = new FileModel
            {
                FileName = vm.Spouse.FileName,
                FilePath = Documents.RemoteImageUrl + vm.Spouse.GenFormFileName
            };
            return Ok(new ResponseMessage<SpouseViewModel>
            {
                Result = vm
            });
        }


    }
}