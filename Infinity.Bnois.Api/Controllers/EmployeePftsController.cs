
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
    [RoutePrefix(BnoisRoutePrefix.EmployeePfts)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEE_PFTS)]

    public class EmployeePftsController : PermissionController
    {
        private readonly IEmployeePftService employeePftService;
        private readonly IPftTypeService pftTypeService;
        private readonly IPftResultService pftResultService;
 

        
        public EmployeePftsController(IEmployeePftService employeePftService,
            IPftTypeService pftTypeService, IPftResultService pftResultService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.employeePftService = employeePftService;
            this.pftTypeService = pftTypeService;
            this.pftResultService = pftResultService;
    

        }

        [HttpGet]
        [Route("get-employee-pfts")]
        public IHttpActionResult GetEmployeePfts(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmployeePftModel> models = employeePftService.GetEmployeePfts(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEE_PFTS);
            return Ok(new ResponseMessage<List<EmployeePftModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-employee-pft")]
        public async Task<IHttpActionResult> GetEmployeePft(int id)
        {
            EmployeePftViewModel vm = new EmployeePftViewModel();
            vm.EmployeePft = await employeePftService.GetEmployeePft(id);
            vm.PftTypes = await pftTypeService.GetPftTypeSelectModels();
            vm.PftResults = await pftResultService.GetPftResultSelectModels();


            return Ok(new ResponseMessage<EmployeePftViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-employee-pft")]
        public async Task<IHttpActionResult> SaveEmployeePft([FromBody] EmployeePftModel model)
        {
            return Ok(new ResponseMessage<EmployeePftModel>
            {
                Result = await employeePftService.SaveEmployeePft(0, model)
            });
        }

        [HttpPut]
        [Route("update-employee-pft/{id}")]
        public async Task<IHttpActionResult> UpdateEmployeePft(int id, [FromBody] EmployeePftModel model)
        {
            return Ok(new ResponseMessage<EmployeePftModel>
            {
                Result = await employeePftService.SaveEmployeePft(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-employee-pft/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeePft(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeePftService.DeleteEmployeePft(id)
            });
        }
    }
}