using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Data;
using PhysicalConditionViewModel = Infinity.Bnois.Api.Models.PhysicalConditionViewModel;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.PhysicalConditions)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class PhysicalConditionsController : BaseController
    {
        private readonly IPhysicalConditionService physicalConditionService;
        private readonly IColorService colorService;
        private readonly IEyeVisionService eyeVisionService;
        private readonly IBloodGroupService bloodGroupService;
        private readonly IPhysicalStructureService physicalStructureService;
        private readonly IMedicalCategoryService medicalCategoryService;   
        public PhysicalConditionsController(
            IPhysicalConditionService physicalConditionService,
            IColorService colorService,
            IEyeVisionService eyeVisionService,
            IBloodGroupService bloodGroupService,
            IPhysicalStructureService physicalStructureService,
            IMedicalCategoryService medicalCategoryService)
        {
            this.physicalConditionService = physicalConditionService;
            this.colorService = colorService;
            this.eyeVisionService = eyeVisionService;
            this.bloodGroupService = bloodGroupService;
            this.physicalStructureService = physicalStructureService;
            this.medicalCategoryService = medicalCategoryService;
        }

        [HttpGet]
        [Route("get-physical-conditions")]
        public async Task<IHttpActionResult> GetPhysicalConditions(int employeeId)
        {
            PhysicalConditionViewModel vm = new PhysicalConditionViewModel();
            vm.PhysicalCondition = await physicalConditionService.GetPhysicalConditions(employeeId);
            vm.File = new FileModel { FileName = vm.PhysicalCondition.FileName, FilePath = Documents.RemoteImageUrl + vm.PhysicalCondition.FileName };
            return Ok(new ResponseMessage<PhysicalConditionViewModel>()
            {
                Result = vm
            });
        }
        [HttpGet]
        [Route("get-physical-condition")]
        public async Task<IHttpActionResult> GetPhysicalCondition(int employeeId)
        {
            PhysicalConditionViewModel vm = new PhysicalConditionViewModel();
            vm.PhysicalCondition = await physicalConditionService.GetPhysicalConditions(employeeId);       
            vm.EyeColors = await colorService.GetColorSelectModel((int)ColorType.Eyes);
            vm.HairColors = await colorService.GetColorSelectModel((int)ColorType.Hair);
            vm.SkinColors = await colorService.GetColorSelectModel((int)ColorType.Skin);
            vm.EyeVisions = await eyeVisionService.GetEyeVisionSelectModels();
            vm.BloodGroups = await bloodGroupService.GetBloodGroupSelectModels();
            vm.PhysicalStructures = await physicalStructureService.GetPhysicalStructureSelectModels();
            vm.MedicalCategories = await medicalCategoryService.GetMedicalCategorySelectModels();
            return Ok(new ResponseMessage<PhysicalConditionViewModel>()
            {
                Result = vm
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-physical-condition/{employeeId}")]
        public async Task<IHttpActionResult> UpdatePhysicalCondition(int employeeId, [FromBody] PhysicalConditionModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<PhysicalConditionModel>()
            {
                Result = await physicalConditionService.SavePhysicalCondition(employeeId, model)
            });
        }



        [HttpPost]
        [Route("upload-medical-category-image")]
        public async Task<IHttpActionResult> UploadMedicalCategoryImage(int employeeId)
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


            PhysicalConditionViewModel vm = new PhysicalConditionViewModel();
            vm.PhysicalCondition = new PhysicalConditionModel();
            vm.PhysicalCondition.EmployeeId = employeeId;
            vm.PhysicalCondition.FileName = fileName;

            vm.PhysicalCondition = await physicalConditionService.UpdatePhysicalCondition(vm.PhysicalCondition);

            vm.File = new FileModel
            {
                FileName = vm.PhysicalCondition.FileName,
                FilePath = Documents.RemoteImageUrl + vm.PhysicalCondition.FileName
            };
            return Ok(new ResponseMessage<PhysicalConditionViewModel>
            {
                Result = vm
            });
        }

    }
}
