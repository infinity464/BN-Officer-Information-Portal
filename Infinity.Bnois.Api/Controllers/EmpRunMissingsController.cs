
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
    [RoutePrefix(BnoisRoutePrefix.EmpRunMissings)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMP_RUN_MISSINGS)]
    public class EmpRunMissingsController : PermissionController
    {
        private readonly IEmpRunMissingService empRunMissingService;

        public EmpRunMissingsController(IEmpRunMissingService empRunMissingService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.empRunMissingService = empRunMissingService;
        }

        [HttpGet]
        [Route("get-employee-run-missings")]
        public IHttpActionResult GetEmpRunMissings(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmpRunMissingModel> models = empRunMissingService.GetEmpRunMissings(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMP_RUN_MISSINGS);
            return Ok(new ResponseMessage<List<EmpRunMissingModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-employee-run-missing")]
        public async Task<IHttpActionResult> GetEmpRunMissing(int id)
        {
            EmpRunMissingViewModel vm = new EmpRunMissingViewModel();
            vm.EmpRunMissing = await empRunMissingService.GetEmpRunMissing(id);
            vm.StatusTypes = empRunMissingService.GetStatusTypeSelectModels();
            return Ok(new ResponseMessage<EmpRunMissingViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-employee-run-missing")]
        public async Task<IHttpActionResult> SaveEmpRunMissing([FromBody] EmpRunMissingModel model)
        {
            return Ok(new ResponseMessage<EmpRunMissingModel>
            {
                Result = await empRunMissingService.SaveEmpRunMissing(0, model)
            });
        }



        [HttpPut]
        [Route("update-employee-run-missing/{id}")]
        public async Task<IHttpActionResult> UpdateEmpRunMissing(int id, [FromBody] EmpRunMissingModel model)
        {
            return Ok(new ResponseMessage<EmpRunMissingModel>
            {
                Result = await empRunMissingService.SaveEmpRunMissing(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-employee-run-missing/{id}")]
        public async Task<IHttpActionResult> DeleteEmpRunMissing(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await empRunMissingService.DeleteEmpRunMissing(id)
            });
        }

        //---Employee Back to Unit----------------
        [HttpGet]
        [Route("get-emp-back-to-units")]
        public IHttpActionResult GetBackToUnits(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmpRunMissingModel> models = empRunMissingService.GetBackToUnits(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMP_RUN_MISSINGS);
            return Ok(new ResponseMessage<List<EmpRunMissingModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-emp-back-to-unit")]
        public async Task<IHttpActionResult> GetBackToUnit(int id)
        {
            EmpRunMissingViewModel vm = new EmpRunMissingViewModel();
            vm.EmpBackToUnit = await empRunMissingService.GetEmpRunMissing(id);
            return Ok(new ResponseMessage<EmpRunMissingViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-emp-back-to-unit")]
        public async Task<IHttpActionResult> SaveEmpBackToUnit([FromBody] EmpRunMissingModel model)
        {
            return Ok(new ResponseMessage<EmpRunMissingModel>
            {
                Result = await empRunMissingService.SaveEmpBackToUnit(0, model)
            });
        }

        [HttpPut]
        [Route("update-emp-back-to-unit/{id}")]
        public async Task<IHttpActionResult> UpdateEmpBackToUnit(int id, [FromBody] EmpRunMissingModel model)
        {
            return Ok(new ResponseMessage<EmpRunMissingModel>
            {
                Result = await empRunMissingService.SaveEmpBackToUnit(id, model)
            });
        }

    }
}