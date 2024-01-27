
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
using Infinity.Bnois.Data;
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
    [RoutePrefix(BnoisRoutePrefix.OfficerTransfers)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.OFFICER_TRANSFERS)]

    public class OfficerTransfersController : PermissionController
    {
        private readonly ITransferService transferService;
        private readonly IOfficeService officeService;
        private readonly IOfficeAppointmentService officeAppointmentService;
        private readonly IRankService rankService;
        private readonly IDistrictService districtService;

        public OfficerTransfersController(ITransferService transferService, IOfficeService officeService,
            IOfficeAppointmentService officeAppointmentService, IRankService rankService, IDistrictService districtService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.transferService = transferService;
            this.officeService = officeService;
            this.officeAppointmentService = officeAppointmentService;
            this.rankService = rankService;
            this.districtService = districtService;
             


        }

        [HttpGet]
        [Route("get-officer-transfers")]
        public IHttpActionResult GetTransfers(int employeeId, int type)
        {
            TransferViewModel vm=new TransferViewModel();
            vm.OfficerTransfers = transferService.GetTransfers(employeeId, type,(int)TransferMode.Permanent);
            vm.OfficerTemporaryTransfers = transferService.GetTransfers(employeeId, type,(int)TransferMode.Temporary);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.OFFICER_TRANSFERS);
            return Ok(new ResponseMessage<TransferViewModel>()
            {
                Result = vm,
                Permission = permission

            });
        }

        [HttpGet]
        [Route("get-last-officer-transfer")]
        public async Task<IHttpActionResult> GetLastOfficerTransfer(int employeeId)
        {
            TransferViewModel vm = new TransferViewModel();
            vwTransfer lastTransfer = transferService.GetLastTransfer(employeeId);
            if (lastTransfer.EmployeeStatus==1 || lastTransfer.EmployeeStatus==6)
            {
                vm.PreBornOffice = lastTransfer.BornOffice;
                vm.PreAttachOffice = lastTransfer.CurrentAttach;
                vm.PreAppointment = lastTransfer.Appointment;
            }
          

            return Ok(new ResponseMessage<TransferViewModel>
            {
                Result = vm
            });
        }


        [HttpGet]
        [Route("get-officer-transfer")]
        public async Task<IHttpActionResult> GetTransfer(int id)
        {
            TransferViewModel vm = new TransferViewModel();
            vm.Transfer = await transferService.GetTransfer(id);
            vm.TransferModes = transferService.GetTransferModeSelectModels();
            vm.TransferTypes = transferService.GetTransferTypeSelectModels();
            vm.TemporaryTransferTypes = transferService.GetTemporaryTransferTypeSelectModels();
            vm.NewBorns = await officeService.GetBornOfficeSelectModel();
            vm.Districts = await districtService.GetDistrictSelectModels();
            vm.Ranks = await rankService.GetRanksSelectModel();
           

            if (vm.Transfer.TransferId > 0)
            {
                vm.NewAttaches = await officeService.GetOfficeSelectModel(vm.Transfer.TransferId);
            }
            return Ok(new ResponseMessage<TransferViewModel>
            {
                Result = vm
            });
        }




        [HttpGet]
        [Route("get-officer-from-select-models")]
        public  IHttpActionResult GetOfficerFromSelectModels()
        {

            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result =  transferService.GetTransferTypeSelectModels()

            });
        }




        [HttpPost]
        [ModelValidation]
        [Route("save-officer-transfer")]
        public async Task<IHttpActionResult> SaveTransfer([FromBody] TransferModel model)
        {

            return Ok(new ResponseMessage<TransferModel>
            {
                Result = await transferService.SaveTransfer(0, model)
            });
        }

        [HttpPut]
        [Route("update-officer-transfer/{id}")]
        public async Task<IHttpActionResult> UpdateTransfer(int id, [FromBody] TransferModel model)
        {
            return Ok(new ResponseMessage<TransferModel>
            {
                Result = await transferService.SaveTransfer(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-officer-transfer/{id}")]
        public async Task<IHttpActionResult> DeleteTransfer(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await transferService.DeleteTransfer(id)
            });
        }

        [HttpPost]
        [Route("upload-transfer-file")]
        public async Task<IHttpActionResult> UploadTransferFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            string fileSaveLocation = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;
            fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/File");

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(provider);

            fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
            filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/File", fileName);

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


        [HttpGet]
        [Route("downlaod-transfer-file")]
        public async Task<HttpResponseMessage> DownlaodTransferFile(int id)
        {
            TransferModel model = await transferService.GetTransfer(id);

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