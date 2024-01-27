using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Photos)]
    [EnableCors("*", "*", "*")]
    public class PhotosController : BaseController
    {
        private readonly IPhotoService photoService;
        public PhotosController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }
        [HttpGet]
        [Route("get-photo")]
        public async Task<IHttpActionResult> GetPhoto(int employeeId, int type)
        {
            PhotoModel photo = await photoService.GetFile(employeeId, type);
            string filePath = string.Empty;
            if (type == 1)
            {
                filePath = string.Format("{0}://{1}/{2}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/OfficerPicture/");
            }
            if (type == 2)
            {
                filePath = string.Format("{0}://{1}/{2}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/OfficerSignature/");
            }

            var fileModel = new FileModel();

            if (photo.FileName == null)
            {
                photo.FileName = "officer.jpg";
            }
            fileModel.FilePath = filePath + photo.FileName;
            fileModel.FileName = photo.FileName;

            return Ok(new ResponseMessage<FileModel>
            {
                Result = fileModel
            });
        }


        [HttpPost]
        [Route("upload-photo")]
        public async Task<IHttpActionResult> UploadPhoto(int employeeId, int photoType)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            PhotoModel model = new PhotoModel();
            string fileSaveLocation = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;

            if (photoType == (int)PhotoType.Image)
            {
                fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/OfficerPicture");
            }
            if (photoType == (int)PhotoType.Signature)
            {
                fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/OfficerSignature");
            }

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(provider);

            if (photoType == (int)PhotoType.Image)
            {
                fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
                filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/OfficerPicture", fileName);
            }
            if (photoType == (int)PhotoType.Signature)
            {
                fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
                filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/OfficerSignature", fileName);
            }

            MultipartFileData fileInfo = provider.FileData[0];
            model.ContentType = fileInfo.Headers.ContentType.ToString();
            model.FileExtension = Path.GetExtension(fileInfo.LocalFileName);
            model.EmployeeId = employeeId;
            model.PhotoType = photoType;
            model.FileName = fileName;
            await photoService.SavePhoto(employeeId, model);
            var fileModel = new FileModel();
            if (model.PhotoId > 0)
            {
                fileModel.FilePath = filePath;
                fileModel.FileName = model.FileName;

            }
            return Ok(new ResponseMessage<FileModel>
            {
                Result = fileModel
            });
        }
    }
}
