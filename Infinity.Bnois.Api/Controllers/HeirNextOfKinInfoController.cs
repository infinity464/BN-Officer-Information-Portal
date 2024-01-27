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
    [RoutePrefix(BnoisRoutePrefix.HeirNextOfKinInfo)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]

    public class HeirNextOfKinInfoController : PermissionController
    {
        private readonly IHeirNextOfKinInfoService  heirNextOfKinInfoService;
        private readonly IOccupationService occupationService;
        private readonly IRelationService relationService;
        private readonly IGenderService genderService;
        private readonly IHeirTypeService heirTypeService;

        public HeirNextOfKinInfoController(IHeirNextOfKinInfoService heirNextOfKinInfoService, IOccupationService occupationService
       ,IRelationService relationService  ,IGenderService genderService, IHeirTypeService heirTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.heirNextOfKinInfoService = heirNextOfKinInfoService;
            this.occupationService = occupationService;
            this.relationService = relationService;
            this.genderService = genderService;
            this.heirTypeService = heirTypeService;
        }

        [HttpGet]
        [Route("get-heir-next-of-kin-info-list")]
        public IHttpActionResult GetHeirNextOfKinInfoList(int employeeId)
        {
            int total = 0;
            List<HeirNextOfKinInfoModel> models = heirNextOfKinInfoService.GetHeirNextOfKinInfoList(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<HeirNextOfKinInfoModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-heir-next-of-kin-info")]
        public async Task<IHttpActionResult> GetHeirNextOfKinInfo(int employeeId, int HeirNextOfKinInfoId)
        {
            HeirNextOfKinInfoViewModel vm = new HeirNextOfKinInfoViewModel();
            vm.HeirNextOfKinInfo = await heirNextOfKinInfoService.GetHeirNextOfKinInfo(HeirNextOfKinInfoId);
            vm.Occupations = await occupationService.GetOccupationSelectModels();
            vm.Relations = await relationService.GetRelationSelectModels();
            vm.Genders = await genderService.GetGenderSelectModels();
            vm.HeirTypes = await heirTypeService.GetHeirTypeSelectModels();
            vm.HeirKinTypes = heirNextOfKinInfoService.GetHeirKinTypeSelectModels();

            vm.File = new FileModel { FileName = vm.HeirNextOfKinInfo.FileName, FilePath = Documents.RemoteImageUrl + vm.HeirNextOfKinInfo.FileName };
            return Ok(new ResponseMessage<HeirNextOfKinInfoViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-heir-next-of-kin-info/{employeeId}")]
        public async Task<IHttpActionResult> SaveHeirNextOfKinInfo(int employeeId, [FromBody] HeirNextOfKinInfoModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<HeirNextOfKinInfoModel>
            {
                Result = await heirNextOfKinInfoService.SaveHeirNextOfKinInfo(0, model)
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-heir-next-of-kin-info/{HeirNextOfKinInfoId}")]
        public async Task<IHttpActionResult> UpdateHeirNextOfKinInfo(int HeirNextOfKinInfoId, [FromBody] HeirNextOfKinInfoModel model)
        {
            return Ok(new ResponseMessage<HeirNextOfKinInfoModel>
            {
                Result = await heirNextOfKinInfoService.SaveHeirNextOfKinInfo(HeirNextOfKinInfoId, model)

            });
        }


        [HttpDelete]
        [Route("delete-heir-next-of-kin-info/{id}")]
        public async Task<IHttpActionResult> DeleteHeirNextOfKinInfo(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await heirNextOfKinInfoService.DeleteHeirNextOfKinInfo(id)
            });
        }

        [HttpPost]
        [Route("upload-heir-next-of-kin-image")]
        public async Task<IHttpActionResult> UploadHeirNextOfKinImage(int employeeId, int heirNextOfKinInfoId)
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


           HeirNextOfKinInfoViewModel vm = new HeirNextOfKinInfoViewModel();
            vm.HeirNextOfKinInfo = new HeirNextOfKinInfoModel();
            vm.HeirNextOfKinInfo.EmployeeId = employeeId;
            vm.HeirNextOfKinInfo.HeirNextOfKinInfoId = heirNextOfKinInfoId;
            vm.HeirNextOfKinInfo.FileName = fileName;

            vm.HeirNextOfKinInfo = await heirNextOfKinInfoService.UpdateHeirNextOfKinInfo(vm.HeirNextOfKinInfo);

            vm.File = new FileModel
            {
                FileName = vm.HeirNextOfKinInfo.FileName,
                FilePath = Documents.RemoteImageUrl + vm.HeirNextOfKinInfo.FileName
            };
            return Ok(new ResponseMessage<HeirNextOfKinInfoViewModel>
            {
                Result = vm
            });
        }

    }
}