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
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.EmployeeChildrens)]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    [EnableCors("*", "*", "*")]
    public class EmployeeChildrensController : PermissionController
    {
        private readonly IEmployeeChildrenService employeeChildrenService;
        private readonly IOccupationService occupationService;
        private readonly ISpouseService spouseService;
        public EmployeeChildrensController(IEmployeeChildrenService employeeChildrenService,
            IOccupationService occupationService, ISpouseService spouseService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.employeeChildrenService = employeeChildrenService;
            this.occupationService = occupationService;
            this.spouseService = spouseService;
        }

        [HttpGet]
        [Route("get-employee-childrens")]
        public IHttpActionResult GetEmployeeChildrens(int employeeId)
        {
            int total = 0;
            List<EmployeeChildrenModel> models = employeeChildrenService.GetEmployeeChildrens(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<EmployeeChildrenModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-employee-children")]
        public async Task<IHttpActionResult> GetEmployeeChildren(int employeeId, int employeeChildrenId)
        {
            EmployeeChildrenViewModel vm = new EmployeeChildrenViewModel();
            vm.EmployeeChildren = await employeeChildrenService.GetEmployeeChildren(employeeChildrenId);
            vm.ChildrenTypes = employeeChildrenService.GetChildrenTypeSelectModels();
            vm.Occupations = await occupationService.GetOccupationSelectModels();
            vm.Spouses = await spouseService.GetSpouseSelectModels(employeeId);
            vm.File = new FileModel { FileName=vm.EmployeeChildren.FileName,FilePath=Documents.RemoteImageUrl+ vm.EmployeeChildren.FileName};
            vm.GenFormName = new FileModel { FileName=vm.EmployeeChildren.GenFormName, FilePath=Documents.RemoteImageUrl+ vm.EmployeeChildren.GenFormName };
            return Ok(new ResponseMessage<EmployeeChildrenViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-employee-children/{employeeId}")]
        public async Task<IHttpActionResult> SaveEmployeeChildren(int employeeId,[FromBody] EmployeeChildrenModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<EmployeeChildrenModel>
            {
                Result = await employeeChildrenService.SaveEmployeeChildren(0, model)
            });
        }

        [HttpPut]
        [Route("update-employee-children/{addressId}")]
        public async Task<IHttpActionResult> UpdateEmployeeChildren(int addressId, [FromBody] EmployeeChildrenModel model)
        {
            return Ok(new ResponseMessage<EmployeeChildrenModel>
            {
                Result = await employeeChildrenService.SaveEmployeeChildren(addressId, model)

            });
        }

        [HttpDelete]
        [Route("delete-employee-children/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeeChildren(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeChildrenService.DeleteEmployeeChildren(id)
            });
        }

        [HttpPost]
        [Route("upload-children-image")]
        public async Task<IHttpActionResult> UploadChildrenImage(int employeeId, int employeeChildrenId)
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


            EmployeeChildrenViewModel vm = new EmployeeChildrenViewModel();
            vm.EmployeeChildren = new EmployeeChildrenModel();
            vm.EmployeeChildren.EmployeeId = employeeId;
            vm.EmployeeChildren.EmployeeChildrenId = employeeChildrenId;
            vm.EmployeeChildren.FileName = fileName;

            vm.EmployeeChildren = await employeeChildrenService.UpdateEmployeeChildren(vm.EmployeeChildren);

            vm.File = new FileModel
            {
                FileName = vm.EmployeeChildren.FileName,
                FilePath = Documents.RemoteImageUrl + vm.EmployeeChildren.FileName
            };
            return Ok(new ResponseMessage<EmployeeChildrenViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [Route("upload-children-gen-form")]
        public async Task<IHttpActionResult> UploadChildrenGenForm(int employeeId, int employeeChildrenId)
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


            EmployeeChildrenViewModel vm = new EmployeeChildrenViewModel();
            vm.EmployeeChildren = new EmployeeChildrenModel();
            vm.EmployeeChildren.EmployeeId = employeeId;
            vm.EmployeeChildren.EmployeeChildrenId = employeeChildrenId;
            vm.EmployeeChildren.GenFormName = fileName;

            vm.EmployeeChildren = await employeeChildrenService.UpdateEmployeeChildren(vm.EmployeeChildren);

            vm.GenFormName = new FileModel
            {
                FileName = vm.EmployeeChildren.GenFormName,
                FilePath = Documents.RemoteImageUrl + vm.EmployeeChildren.GenFormName
            };
            return Ok(new ResponseMessage<EmployeeChildrenViewModel>
            {
                Result = vm
            });
        }
    }
}
