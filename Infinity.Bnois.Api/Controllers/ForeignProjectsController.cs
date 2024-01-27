
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
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Ers.ApplicationService;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ForeignProject)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.FOREIGN_PPROJECTS)]

    public class ForeignProjectsController : PermissionController
    {
        private readonly IForeignProjectService foreignProjectService;
        private readonly ICountryService countryService;
      
 

        
        public ForeignProjectsController(IForeignProjectService foreignProjectService, ICountryService countryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.foreignProjectService = foreignProjectService;
            this.countryService = countryService;
          
    

        }

        [HttpGet]
        [Route("get-foreign-projects")]
        public IHttpActionResult GetAdditonalForeignProjects(int ps, int pn, string qs)
        {
            int total = 0;
            List<ForeignProjectModel> models = foreignProjectService.GetForeignProjects(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.FOREIGN_PPROJECTS);
            return Ok(new ResponseMessage<List<ForeignProjectModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-foreign-project")]
        public async Task<IHttpActionResult> GetForeignProject(int id)
        {
            ForeignProjectViewModel vm = new ForeignProjectViewModel();
            vm.ForeignProject = await foreignProjectService.GetForeignProject(id);
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
           
   

            return Ok(new ResponseMessage<ForeignProjectViewModel>
            {
                Result = vm
            });
        }

  

        [HttpPost]
        [ModelValidation]
        [Route("save-foreign-project")]
        public async Task<IHttpActionResult> SaveForeignProject([FromBody] ForeignProjectModel model)
        {
            return Ok(new ResponseMessage<ForeignProjectModel>
            {
                Result = await foreignProjectService.SaveForeignProject(0, model)
            });
        }

        [HttpPut]
        [Route("update-foreign-project/{id}")]
        public async Task<IHttpActionResult> UpdateForeignProject(int id, [FromBody] ForeignProjectModel model)
        {
            return Ok(new ResponseMessage<ForeignProjectModel>
            {
                Result = await foreignProjectService.SaveForeignProject(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-foreign-project/{id}")]
        public async Task<IHttpActionResult> DeleteForeignProject(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await foreignProjectService.DeleteForeignProject(id)
            });
        }
    }
}