using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.PreviousMissions)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public  class PreviousMissionsController:PermissionController
    {
        private readonly IPreviousMissionService previousMissionService;
        private readonly ICountryService countryService;
        public PreviousMissionsController(IPreviousMissionService previousMissionService, ICountryService countryService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.previousMissionService = previousMissionService;
            this.countryService = countryService;
           
        }

      

        [HttpGet]
        [Route("get-previous-missions")]
        public IHttpActionResult GetPreviousMissions(int employeeId)
        {
            List<PreviousMissionModel> models = previousMissionService.GetPreviousMissions(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<PreviousMissionModel>>()
            {
                Result = models,
                Permission = permission
            });

        }
        [HttpGet]
        [Route("get-previous-mission")]
        public async Task<IHttpActionResult> GetPreviousMission( int previousMissionId)
        {
            PreviousMissionViewModel vm=new PreviousMissionViewModel();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            vm.PreviousMission = await previousMissionService.GetPreviousMission(previousMissionId);

            return Ok(new ResponseMessage<PreviousMissionViewModel>()
            {
                Result = vm
        });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-previous-mission/{employeeId}")]
        public async Task<IHttpActionResult> SavePreviousMission(int employeeId, [FromBody] PreviousMissionModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<PreviousMissionModel>()
            {
                Result = await previousMissionService.SavePreviousMission(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-previous-mission/{previousMissionId}")]
        public async Task<IHttpActionResult> UpdatePreviousMission(int previousMissionId, [FromBody] PreviousMissionModel model)
        {
            return Ok(new ResponseMessage<PreviousMissionModel>()
            {
                Result = await previousMissionService.SavePreviousMission(previousMissionId, model)
            });
        }


        [HttpDelete]
        [Route("delete-previous-mission/{id}")]
        public async Task<IHttpActionResult> DeletePreviousMission(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await previousMissionService.DeletePreviousMission(id)
            });
        }
    }
}
