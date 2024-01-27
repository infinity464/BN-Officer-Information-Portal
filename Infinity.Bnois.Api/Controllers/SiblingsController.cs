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
    [RoutePrefix(BnoisRoutePrefix.Siblings)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]

    public class SiblingsController : PermissionController
    {
        private readonly ISiblingService  siblingService;
        private readonly IOccupationService occupationService;

        public SiblingsController(ISiblingService siblingService, IOccupationService occupationService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.siblingService = siblingService;
            this.occupationService = occupationService;
        }

        [HttpGet]
        [Route("get-siblings")]
        public IHttpActionResult GetSiblings(int employeeId)
        {
            int total = 0;
            List<SiblingModel> models = siblingService.GetSiblings(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<SiblingModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-sibling")]
        public async Task<IHttpActionResult> GetSibling(int employeeId, int siblingId)
        {
            SiblingViewModel vm = new SiblingViewModel();
            vm.Sibling = await siblingService.GetSibling(siblingId);
            vm.Occupations = await occupationService.GetOccupationSelectModels();
            vm.SiblingTypes = siblingService.GetSiblingTypeSelectModels();
            vm.File = new FileModel {FileName=vm.Sibling.FileName,FilePath=Documents.RemoteImageUrl+vm.Sibling.FileName };
            return Ok(new ResponseMessage<SiblingViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-sibling/{employeeId}")]
        public async Task<IHttpActionResult> SaveSibling(int employeeId, [FromBody] SiblingModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<SiblingModel>
            {
                Result = await siblingService.SaveSibling(0, model)
            });
        }

        [HttpPut]
        [Route("update-sibling/{siblingId}")]
        public async Task<IHttpActionResult> UpdateSibling(int siblingId, [FromBody] SiblingModel model)
        {
            return Ok(new ResponseMessage<SiblingModel>
            {
                Result = await siblingService.SaveSibling(siblingId, model)

            });
        }


        [HttpDelete]
        [Route("delete-sibling/{id}")]
        public async Task<IHttpActionResult> DeleteSibling(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await siblingService.DeleteSibling(id)
            });
        }

        [HttpPost]
        [Route("upload-sibling-image")]
        public async Task<IHttpActionResult> UploadSiblingImage(int employeeId, int siblingId)
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


           SiblingViewModel vm = new SiblingViewModel();
            vm.Sibling = new SiblingModel();
            vm.Sibling.EmployeeId = employeeId;
            vm.Sibling.SiblingId = siblingId;
            vm.Sibling.FileName = fileName;

            vm.Sibling = await siblingService.UpdateSibling(vm.Sibling);

            vm.File = new FileModel
            {
                FileName = vm.Sibling.FileName,
                FilePath = Documents.RemoteImageUrl + vm.Sibling.FileName
            };
            return Ok(new ResponseMessage<SiblingViewModel>
            {
                Result = vm
            });
        }
    }
}