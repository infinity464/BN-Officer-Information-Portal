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
    [RoutePrefix(BnoisRoutePrefix.Upazilas)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.UPAZILAS)]

    public class UpazilasController : PermissionController
    {
        private readonly IUpazilaService upazilaService;
        private readonly IDistrictService districtService;
        private readonly IDivisionService divisionService;
       

        public UpazilasController(IUpazilaService upazilaService, IDistrictService districtService, IDivisionService divisionService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.upazilaService = upazilaService;
            this.districtService = districtService;
            this.divisionService = divisionService;
           
        }

        [HttpGet]
        [Route("get-upazilas")]
        public IHttpActionResult GetUpazilas(int ps, int pn, string qs)
        {
            int total = 0;
            List<UpazilaModel> models = upazilaService.GetUpazilas(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.UPAZILAS);
            return Ok(new ResponseMessage<List<UpazilaModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-upazila")]
        public async Task<IHttpActionResult> GetUpazila(int id)
        {
            UpazilaViewModel vm = new UpazilaViewModel();
            vm.Upazila = await upazilaService.GetUpazila(id);
            vm.Divisions = await divisionService.GetDivisionSelectModels();
            if (vm.Upazila.DistrictId > 0)
            {
                vm.Districts = await districtService.GetDistrictByDivisionSelectModels(vm.Upazila.DistrictId);

            }
            return Ok(new ResponseMessage<UpazilaViewModel>
            {
                Result = vm
            });
        }


        [HttpGet]
        [Route("get-district-by-division")]
        public async Task<IHttpActionResult> GetDistrictByDivision(int id)
        {
            UpazilaViewModel vm = new UpazilaViewModel();
            vm.Districts = await districtService.GetDistrictByDivisionSelectModels(id);
            return Ok(new ResponseMessage<UpazilaViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-upazila")]
        public async Task<IHttpActionResult> SaveUpazila([FromBody] UpazilaModel model)
        {
            return Ok(new ResponseMessage<UpazilaModel>
            {
                Result = await upazilaService.SaveUpazila(0, model)
            });
        }

        [HttpPut]
        [Route("update-upazila/{id}")]
        public async Task<IHttpActionResult> UpdateUpazila(int id, [FromBody] UpazilaModel model)
        {
            return Ok(new ResponseMessage<UpazilaModel>
            {
                Result = await upazilaService.SaveUpazila(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-upazila/{id}")]
        public async Task<IHttpActionResult> DeleteUpazila(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await upazilaService.DeleteUpazila(id)
            });
        }
    }
}