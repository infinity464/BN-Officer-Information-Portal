
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Ers.ApplicationService;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.QuickLinks)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.QUICK_LINKS)]

    public class QuickLinksController : PermissionController
    {
        private readonly IQuickLinkService quickLinkService;
       
 

        
        public QuickLinksController(IQuickLinkService quickLinkService,IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.quickLinkService = quickLinkService;
           

        }

        [HttpGet]
        [Route("get-quick-links")]
        public IHttpActionResult GetQuickLinks(int ps, int pn, string qs)
        {
            int total = 0;
            List<QuickLinkModel> models = quickLinkService.GetQuickLinks(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.QUICK_LINKS);
            return Ok(new ResponseMessage<List<QuickLinkModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-dashboard-quick-links")]
        [AllowAnonymous]
        public IHttpActionResult GetDashboardQuickLinks()
        {
            int total = 0;
            List<QuickLinkModel> models = quickLinkService.GetDashboardQuickLinks();
            return Ok(new ResponseMessage<List<QuickLinkModel>>()
            {
                Result = models

            });
        }



        [HttpGet]
        [Route("get-quick-link")]
        public async Task<IHttpActionResult> GetQuickLink(int id)
        {
            QuickLinkModel vm = await quickLinkService.GetQuickLink(id);
            return Ok(new ResponseMessage<QuickLinkModel>
            {
                Result = vm
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("save-quick-link")]
        public async Task<IHttpActionResult> SaveQuickLink([FromBody] QuickLinkModel model)
        {
            return Ok(new ResponseMessage<QuickLinkModel>
            {
                Result = await quickLinkService.SaveQuickLink(0, model)
            });
        }

        [HttpPut]
        [Route("update-quick-link/{id}")]
        public async Task<IHttpActionResult> UpdateQuickLink(int id, [FromBody] QuickLinkModel model)
        {
            return Ok(new ResponseMessage<QuickLinkModel>
            {
                Result = await quickLinkService.SaveQuickLink(id, model)

            });
        }


        [HttpDelete]
        [Route("delete-quick-link/{id}")]
        public async Task<IHttpActionResult> DeleteQuickLink(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await quickLinkService.DeleteQuickLink(id)
            });
        }


        [HttpPost]
        [Route("upload-quick-link-file")]
        public async Task<IHttpActionResult> UploadQuickLinkFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            string fileSaveLocation = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;
            string fileExtention = string.Empty;
            fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/File");

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(provider);

            fileName =Path.GetFileName(provider.FileData[0].LocalFileName);
            fileExtention = Path.GetExtension(provider.FileData[0].LocalFileName);
           
//            filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/File", fileName);

            FileModel file = new FileModel
            {
                FileName = fileName,
                FilePath = fileExtention
            };
         
          
            return Ok(new ResponseMessage<FileModel>
            {
                Result = file
            });
       
        }
    }
}