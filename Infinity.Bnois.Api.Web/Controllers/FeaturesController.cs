using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;

namespace Infinity.Bnois.Api.Web.Controllers
{
    [RoutePrefix(IdentityRoutePrefix.Feature)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.FEATURES)]

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController'
    public class FeaturesController : BaseController
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController'
    {
        private readonly IFeatureService featureService;
        private readonly IModuleService moduleService;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.FeaturesController(IFeatureService, IModuleService)'
        public FeaturesController(IFeatureService featureService, IModuleService moduleService)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.FeaturesController(IFeatureService, IModuleService)'
        {
            this.featureService = featureService;
            this.moduleService = moduleService;
        }
        [HttpGet]
        [Route("get-features")]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.GetFeaturs(int, int, string)'
        public IHttpActionResult GetFeaturs(int pageSize, int pageNumber, string searchString)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.GetFeaturs(int, int, string)'
        {
            int total = 0;
            List<Infinity.Bnois.Configuration.ServiceModel.FeatureModel> modules = featureService.GetFeatures(pageSize, pageNumber, searchString, out total);

            return Ok(new ResponseMessage<List<Infinity.Bnois.Configuration.ServiceModel.FeatureModel>>()
            {
                Result = modules,
                Total = total
            });
        }

        [HttpGet]
        [Route("get-feature")]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.GetFeature(int)'
        public IHttpActionResult GetFeature(int featureId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.GetFeature(int)'
        {
            Infinity.Bnois.Configuration.ServiceModel.FeatureModel model = featureService.GetFeature(featureId);
            List<SelectModel> modules = moduleService.GetModules();
             List < SelectModel > FeatureTypes =
                Enum.GetValues(typeof(FeatureType)).Cast<FeatureType>().Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt32(v) })
               .ToList();

            return Ok(new ResponseMessage<Models.FeatureViewModel>
            {
                Result = new Models.FeatureViewModel
                {
                    Modules = modules,
                    Feature=model,
                    FeatureTypes= FeatureTypes
                }
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-feature")]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.SaveFeature(FeatureModel)'
        public IHttpActionResult SaveFeature([FromBody] Infinity.Bnois.Configuration.ServiceModel.FeatureModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.SaveFeature(FeatureModel)'
        {
            return Ok(new ResponseMessage<Infinity.Bnois.Configuration.ServiceModel.FeatureModel>
            {
                Result = featureService.Save(0, model)
            });
        }

        [HttpPut]
        [Route("update-feature/{featureId}")]
        [ModelValidation]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.UpdateFeature(int, FeatureModel)'
        public IHttpActionResult UpdateFeature(int featureId, [FromBody] Infinity.Bnois.Configuration.ServiceModel.FeatureModel model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.UpdateFeature(int, FeatureModel)'
        {
            return Ok(new ResponseMessage<Infinity.Bnois.Configuration.ServiceModel.FeatureModel>
            {
                Result = featureService.Save(featureId, model)
            });
        }

        [HttpDelete]
        [Route("delete-feature/{featureId}")]
//        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.DeleteFeatue(int)'
        public IHttpActionResult DeleteFeatue(int featureId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.DeleteFeatue(int)'
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = featureService.Delete(featureId) > 0
            });
        }
        [HttpGet]
        [Route("download-feature")]
//        [Authorize(Roles = Roles.SuperAdmin + "," + Roles.Admin + "," + Roles.User)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.downloadModuleReport()'
        public HttpResponseMessage downloadModuleReport()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeaturesController.downloadModuleReport()'
        {
            var reportData = featureService.downloadFeatureReport();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(reportData) };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Feature.pdf" };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }
    }
}
