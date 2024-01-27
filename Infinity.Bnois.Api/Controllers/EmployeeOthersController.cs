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

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.EmployeeOthers)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class EmployeeOthersController : BaseController
    {
        private readonly IEmployeeOtherService employeeOtherService;
        public EmployeeOthersController(IEmployeeOtherService employeeOtherService)
        {
            this.employeeOtherService = employeeOtherService;
        }
        [HttpGet]
        [Route("get-employee-others")]
        
        public async Task<IHttpActionResult> GetEmployeeOthers(int employeeId)
        {
            EmployeeOtherModel model = await employeeOtherService.GetEmployeeOthers(employeeId);
            return Ok(new ResponseMessage<EmployeeOtherModel>()
            {
                Result = model
            });
        }

        [HttpGet]
        [Route("get-employee-other")]
        
        public async Task<IHttpActionResult> GetEmployeeOther(int employeeId)
        {
            EmployeeOtherViewModel vm = new EmployeeOtherViewModel();
            vm.EmployeeOther = await employeeOtherService.GetEmployeeOthers(employeeId);
            vm.EmployeeOther.NationalIdImageUrl = Documents.RemoteImageUrl + vm.EmployeeOther.NIdFileName;
            vm.EmployeeOther.DrivingLicenseImageUrl = Documents.RemoteImageUrl + vm.EmployeeOther.DLFileName;
            vm.EmployeeOther.PassportImageUrl = Documents.RemoteImageUrl + vm.EmployeeOther.PassportFIleName;
            return Ok(new ResponseMessage<EmployeeOtherViewModel>()
            {
                Result = vm
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-employee-other/{employeeId}")]
        
        public async Task<IHttpActionResult> UpdateEmployeeOther(int employeeId, [FromBody] EmployeeOtherModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<EmployeeOtherModel>()
            {
                Result = await employeeOtherService.SaveEmployeeOther(employeeId, model)
            });
        }

        [HttpPost]
        [Route("upload-employee-other-image")]
        public async Task<IHttpActionResult> UploadOtherImage(int employeeId, int imageType)
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

            EmployeeOtherModel model = new EmployeeOtherModel();
            model.EmployeeId = employeeId;


            if (imageType.Equals(1))
            {
                model.NIdFileName = fileName;
            }
            if (imageType.Equals(2))
            {
                model.DLFileName = fileName;
            }
            if (imageType.Equals(3))
            {
                model.PassportFIleName = fileName;
            }
            model = await employeeOtherService.UpdateEmployeeOther(model);
            model.NationalIdImageUrl = Documents.RemoteImageUrl + model.NIdFileName;
            model.DrivingLicenseImageUrl = Documents.RemoteImageUrl + model.DLFileName; ;
            model.PassportImageUrl = Documents.RemoteImageUrl + model.PassportFIleName; ;
            return Ok(new ResponseMessage<EmployeeOtherModel>
            {
                Result = model
            });
        }

    }
}
