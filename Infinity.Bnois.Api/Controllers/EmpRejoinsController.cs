

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

    [RoutePrefix(BnoisRoutePrefix.EmpRejoins)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMP_REJOINS)]

    public class EmpRejoinsController : PermissionController
    {
        private readonly IEmpRejoinService empRejoinService;
        private readonly IRankService rankService;

        public EmpRejoinsController( IEmpRejoinService empRejoinService, IRankService rankService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.empRejoinService = empRejoinService;
            this.rankService = rankService;
        }

        [HttpGet]
        [Route("get-employee-rejoins")]
        public IHttpActionResult GetEmpRejoins(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmpRejoinModel> models = empRejoinService.GetEmpRejoins(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMP_REJOINS);
            return Ok(new ResponseMessage<List<EmpRejoinModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-employee-rejoin")]
        public async Task<IHttpActionResult> GetEmpRejoin(int id)
        {
            EmpRejoinViewModel vm = new EmpRejoinViewModel();
            vm.EmpRejoin = await empRejoinService.GetEmpRejoin(id);
            vm.Ranks = await rankService.GetRankSelectModels();
            return Ok(new ResponseMessage<EmpRejoinViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-employee-rejoin")]
        public async Task<IHttpActionResult> SaveEmpRejoin([FromBody] EmpRejoinModel model)
        {
            return Ok(new ResponseMessage<EmpRejoinModel>
            {
                Result = await empRejoinService.SaveEmpRejoin(0, model)
            });
        }

        [HttpPut]
        [Route("update-employee-rejoin/{id}")]
        public async Task<IHttpActionResult> UpdateEmpRejoin(int id, [FromBody] EmpRejoinModel model)
        {
            return Ok(new ResponseMessage<EmpRejoinModel>
            {
                Result = await empRejoinService.SaveEmpRejoin(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-employee-rejoin/{id}")]
        public async Task<IHttpActionResult> DeleteEmpRejoin(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await empRejoinService.DeleteEmpRejoin(id)
            });
        }

    }
}