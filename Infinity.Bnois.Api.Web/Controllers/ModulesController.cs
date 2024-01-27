using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.ServiceModel;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;

namespace Infinity.Bnois.Api.Web.Controllers
{
    [RoutePrefix(IdentityRoutePrefix.Module)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.MODULES)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController'
    public class ModulesController : BaseController
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController'
    {
        private readonly IModuleService moduleService;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.ModulesController(IModuleService)'
        public ModulesController(IModuleService moduleService)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.ModulesController(IModuleService)'
        {
            this.moduleService = moduleService;
        }
        [HttpGet]
        [Route("get-modules")]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetModules(int, int, string)'
        public IHttpActionResult GetModules(int pageSize, int pageNumber, string searchString)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetModules(int, int, string)'
        {

            int total = 0;
            List<ModuleModel> modules = moduleService.GetModules(pageSize, pageNumber, searchString, out total);
            return Ok(new ResponseMessage<List<ModuleModel>>()
            {
                Result = modules,
                Total = total
            });
        }

        [HttpGet]
        [Route("get-module")]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetModule(int)'
        public IHttpActionResult GetModule(int moduleId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetModule(int)'
        {
            ModuleModel model = moduleService.GetModule(moduleId);
            return Ok(new ResponseMessage<ModuleModel>
            {
                Result = model
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-module")]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.SaveModule(ModuleModel)'
        public IHttpActionResult SaveModule([FromBody] ModuleModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.SaveModule(ModuleModel)'
        {
            return Ok(new ResponseMessage<ModuleModel>
            {
                Result = moduleService.Save(0, model)
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-module/{moduleId}")]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.UpdateModule(int, ModuleModel)'
        public IHttpActionResult UpdateModule(int moduleId, [FromBody] ModuleModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.UpdateModule(int, ModuleModel)'
        {
            return Ok(new ResponseMessage<ModuleModel>
            {
                Result = moduleService.Save(moduleId, model)
            });
        }

        [HttpDelete]
        [Route("delete-module/{moduleId}")]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.DeleteModule(int)'
        public IHttpActionResult DeleteModule(int moduleId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.DeleteModule(int)'
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = moduleService.Delete(moduleId) > 0
            });
        }

        [HttpGet]
        [Route("get-module-features")]
        [AllowAnonymous]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetModuleFeatures()'
        public IHttpActionResult GetModuleFeatures()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetModuleFeatures()'
        {
            
            return Ok(new ResponseMessage<List<ModuleModel>>
            {
                Result = moduleService.GetModuleFeatures(FeatureType.Feature)
            });
        }


        [HttpGet]
        [Route("get-current-status-menu")]
        [AllowAnonymous]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetCurrentStatusMenu()'
        public async Task<IHttpActionResult> GetCurrentStatusMenu()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetCurrentStatusMenu()'
        {

            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await moduleService.GetCurrentStatusMenu()
            });
        }


        [HttpGet]
        [Route("get-module-reports")]
        [AllowAnonymous]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetModuleReports(FeatureType)'
        public IHttpActionResult GetModuleReports(FeatureType featureType)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.GetModuleReports(FeatureType)'
        {
           
            return Ok(new ResponseMessage<List<Configuration.Models.Node>>
            {
                Result = moduleService.GetModuleReports(featureType)
            });
        }
        [HttpGet]
        [Route("download-module")]
        [AllowAnonymous]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.DownloadModuleReport()'
        public HttpResponseMessage DownloadModuleReport()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModulesController.DownloadModuleReport()'
        {
            var reportData = moduleService.downloadModuleReport();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(reportData) };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Module.pdf" };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }

    }
}
