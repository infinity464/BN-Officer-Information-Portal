using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
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

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Download)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.DOWNLOAD)]
    public class DownloadController : BaseController
    {
        private readonly IDownloadService _downloadService;

        public DownloadController(IDownloadService downloadService)
        {
            _downloadService = downloadService;
        }
        [HttpGet]
        [Route("get-downloads")]
        public IHttpActionResult GetDownloads(int ps, int pn, string qs)
        {
            int total = 0;
            List<DownloadModel> models = _downloadService.GetDownloads(ps, pn, qs, out total);
            return Ok(new ResponseMessage<List<DownloadModel>>()
            {
                Result = models,
                Total = total
            });
        }
        [HttpGet]
        [Route("get-downloads-for-view")]
        public IHttpActionResult GetDownloadsForView()
        {
            List<DownloadModel> models = _downloadService.GetDownloadsForView();
            return Ok(new ResponseMessage<List<DownloadModel>>()
            {
                Result = models,
            });
        }
        [HttpGet]
        [Route("get-file-for-download-by-id")]
        public async Task<IHttpActionResult> GetDownloadById(int id)
        {
            DownloadModel model = await _downloadService.GetDownload(id);
            return Ok(new ResponseMessage<DownloadModel>
            {
                Result = model
            });
        }


        //[HttpGet]
        //[Route("get-file-for-download")]
        //public async Task<IHttpActionResult> GetFileDownload(int id)
        //{
        //    DownloadModel model = await _downloadService.GetDownload(id);
        //    return Ok(new ResponseMessage<DownloadModel>
        //    {
        //        Result = model
        //    });
        //}

        [HttpPost]
        [ModelValidation]
        [Route("save-download")]
        public async Task<IHttpActionResult> SaveDownload([FromBody] DownloadModel model)
        {
            return Ok(new ResponseMessage<DownloadModel>
            {
                Result = await _downloadService.SaveDownload(0, model)
            });
        }

        [HttpPut]
        [Route("update-download/{id}")]
        public async Task<IHttpActionResult> UpdateDownload(int id, [FromBody] DownloadModel model)
        {
            return Ok(new ResponseMessage<DownloadModel>
            {
                Result = await _downloadService.SaveDownload(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-download/{id}")]
        public async Task<IHttpActionResult> DeleteDownload(int id)
        {
            bool res;
            DownloadModel model = await _downloadService.GetDownload(id);
            if(model.DownloadId == id)
            {
                res = await _downloadService.DeleteDownload(id);
                string fileName = HttpContext.Current.Server.MapPath("~/Documents/Migration/" + model.FileName);
                System.IO.File.Delete(fileName);
            }
            else
            {
                res = false;
            }
            return Ok(new ResponseMessage<bool>
            {
                Result = res
            });
        }

        [HttpPost]
        [Route("upload-download-file")]
        public async Task<IHttpActionResult> UploadFile()
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
        [HttpPost]
        [Route("migration-upload-download-file")]
        public async Task<IHttpActionResult> MigrationUploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            string fileSaveLocation = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;
            fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/Migration");

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(provider);

            fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
            filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/Migration", fileName);

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
        [Route("data-migration-sql-file")]
        public async Task<IHttpActionResult> dataMigration(string dataSource)
        {
            string fileName = HttpContext.Current.Server.MapPath("~/Documents/Migration/" + dataSource);
            string res = _downloadService.DataMigration(fileName);
            System.IO.File.Delete(fileName);
            return Ok(new ResponseMessage<string>
            {
                Result = res
            });
        }
    }
}
