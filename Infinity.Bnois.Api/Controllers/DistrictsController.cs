using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Districts)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.DISTRICTS)]

    public class DistrictsController : PermissionController
    {
        private readonly IDistrictService districtService;
        private readonly IDivisionService divisionService;

        public DistrictsController(IDistrictService districtService, IDivisionService divisionService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.districtService = districtService;
            this.divisionService = divisionService;
        }

        [HttpGet]
        [Route("get-districts")]
        public IHttpActionResult GetDistricts(int ps, int pn, string qs)
        {
            int total = 0;
            List<DistrictModel> models = districtService.GetDistricts(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.DISTRICTS);
            return Ok(new ResponseMessage<List<DistrictModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-district")]
        public async Task<IHttpActionResult> GetDistrict(int id)
        {
            DistrictViewModel vm = new DistrictViewModel();
            vm.District = await districtService.GetDistrict(id);
            vm.Districts = await divisionService.GetDivisionSelectModels();
            return Ok(new ResponseMessage<DistrictViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-district")]
        public async Task<IHttpActionResult> SaveDistrict([FromBody] DistrictModel model)
        {
            return Ok(new ResponseMessage<DistrictModel>
            {
                Result = await districtService.SaveDistrict(0, model)
            });
        }

        [HttpPut]
        [Route("update-district/{id}")]
        public async Task<IHttpActionResult> UpdateDistrict(int id, [FromBody] DistrictModel model)
        {
            return Ok(new ResponseMessage<DistrictModel>
            {
                Result = await districtService.SaveDistrict(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-district/{id}")]
        public async Task<IHttpActionResult> DeleteDistrict(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await districtService.DeleteDistrict(id)
            });
        }
    }
}