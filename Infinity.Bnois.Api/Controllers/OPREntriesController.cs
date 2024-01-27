
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Ers.ApplicationService;
using System.Net.Http;
using Infinity.Bnois.ExceptionHelper;
using Infinity.Bnois.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.OPREntries)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.OPR_Entries)]

    public class OPREntriesController : PermissionController
    {

        private readonly IOprOccasionService oprOccasionService;
        private readonly IEmployeeOprService employeeOprService;
        private readonly IRankService rankService;
        private readonly ISuitabilityService suitabilityService;
        private readonly ISpecialAptTypeService specialAptTypeService;
        private readonly IOprGradingService oprGradingService;
        private readonly IOfficeService officeService;
        private readonly IOfficeAppointmentService officeAppointmentService;
        private readonly IRecomandationTypeService recomandationTypeService;


        public OPREntriesController(IOprOccasionService oprOccasionService, IRankService rankService,
            ISuitabilityService suitabilityService, ISpecialAptTypeService specialAptTypeService,
            IOprGradingService oprGradingService, IOfficeService officeService,
            IOfficeAppointmentService officeAppointmentService, IRecomandationTypeService recomandationTypeService, IEmployeeOprService employeeOprService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.employeeOprService = employeeOprService;
            this.oprOccasionService = oprOccasionService;
            this.rankService = rankService;
            this.suitabilityService = suitabilityService;
            this.specialAptTypeService = specialAptTypeService;
            this.oprGradingService = oprGradingService;
            this.officeService = officeService;
            this.officeAppointmentService = officeAppointmentService;
            this.recomandationTypeService = recomandationTypeService;


        }

        [HttpGet]
        [Route("get-opr-entries")]
        public IHttpActionResult GetOprEntries(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmployeeOprModel> models = employeeOprService.GetEmployeeOprs(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.OPR_Entries);
            return Ok(new ResponseMessage<List<EmployeeOprModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-opr-entry")]
        public async Task<IHttpActionResult> GetOprEntry(int id)
        {
            OPREntryViewModel vm = new OPREntryViewModel();
            vm.EmployeeOpr = await employeeOprService.GetEmployeeOpr(id);
            vm.Ranks = await rankService.GetRankSelectModels();
            vm.SpecialAptTypes = await specialAptTypeService.GetSpecialAptTypeSelectModels();
            vm.Suitabilities = await suitabilityService.GetSuitabilitySelectModels();

            vm.Appointments = await officeAppointmentService.GetOfficeAppointmentSelectModels();
            vm.AttachOffices = await officeService.GetParentOfficeSelectModel();
            vm.BornOffices = await officeService.GetBornOfficeSelectModel();
            vm.Occasions = await oprOccasionService.GetOprOccasionSelectModels();
            vm.OprGradings = oprGradingService.GetOprGradings();
            vm.RecommendationTypes = await recomandationTypeService.GetRecomandationTypeSelectModels();


            return Ok(new ResponseMessage<OPREntryViewModel>
            {
                Result = vm
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("save-opr-entry")]
        public async Task<IHttpActionResult> SaveOprEntry([FromBody] EmployeeOprModel model)
        {
            return Ok(new ResponseMessage<EmployeeOprModel>
            {
                Result = await employeeOprService.SaveEmployeeOpr(0, model)
            });
        }

        [HttpPut]
        [Route("update-opr-entry/{id}")]
        public async Task<IHttpActionResult> UpdateOprEntry(int id, [FromBody] EmployeeOprModel model)
        {
            return Ok(new ResponseMessage<EmployeeOprModel>
            {
                Result = await employeeOprService.SaveEmployeeOpr(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-opr-entry/{id}")]
        public async Task<IHttpActionResult> DeleteTOprEntry(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeOprService.DeleteEmployeeOpr(id)
            });
        }

        [HttpGet]
        [Route("get-opr-file-upload")]
        public async Task<IHttpActionResult> GetOprFileUpload(int id)
        {
            OPREntryViewModel vm = new OPREntryViewModel();
            vm.EmployeeOpr = await employeeOprService.GetEmployeeOpr(id);
            vm.EmployeeOpr.FileUrl = Documents.RemoteFileUrl + vm.EmployeeOpr.ImageSec2;
            vm.EmployeeOpr.Section2ImageUrl = Documents.RemoteImageUrl + vm.EmployeeOpr.ImageSec2;
            vm.EmployeeOpr.Section4ImageUrl = Documents.RemoteImageUrl + vm.EmployeeOpr.ImageSec4;
            return Ok(new ResponseMessage<OPREntryViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [Route("upload-opr-section")]
        public async Task<IHttpActionResult> UploadOprSection(int id, int oprSection)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            string fileSaveLocation = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;

            if (oprSection.Equals(1))
            {
                fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/File");
            }
            else
            {
                fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/Image");
            }

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(provider);

            if (oprSection.Equals(1))
            {
                fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
                filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/File", fileName);
            }
            else
            {
                fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
                filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/Image", fileName);
            }

            EmployeeOprModel model = new EmployeeOprModel();
            model.Id = id;
            if (oprSection.Equals(1))
            {
                model.FileName = fileName;
            }
            if (oprSection.Equals(2))
            {
                model.ImageSec2 = fileName;
            }
            if (oprSection.Equals(4))
            {
                model.ImageSec4 = fileName;
            }

            model = await employeeOprService.UpdateEmployeeOpr(model);
            model.Section2ImageUrl = Documents.RemoteImageUrl + model.ImageSec2;
            model.Section4ImageUrl = Documents.RemoteImageUrl + model.ImageSec4; ;
            return Ok(new ResponseMessage<EmployeeOprModel>
            {
                Result = model
            });
        }

        [HttpGet]
        [Route("downlaod-opr-file")]
        public async Task<HttpResponseMessage> DownlaodOprFile(int id)
        {
            EmployeeOprModel model =await employeeOprService.GetEmployeeOpr(id);

            if (string.IsNullOrWhiteSpace(model.FileName))
            {
                throw new InfinityArgumentMissingException("File not found");
            }
            string root = ConfigurationResolver.Get().DocumentUploadFolder;
            string filePath = Path.Combine(root, model.FileName);
            if (!File.Exists(filePath))
            {
                throw new InfinityArgumentMissingException("File not found");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);

                    HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                    httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = model.FileName;
                    httpResponseMessage.StatusCode = HttpStatusCode.OK;
                    return httpResponseMessage;
                }

            }

        }

    }
}