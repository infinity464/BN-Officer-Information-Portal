using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Extracurriculars)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class ExtracurricularsController : PermissionController
    {
        private readonly IExtracurricularService extracurricularService;
        private readonly IExtracurricularTypeService extracurricularTypeService;
        public ExtracurricularsController(IExtracurricularService extracurricularService,
            IExtracurricularTypeService extracurricularTypeService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.extracurricularService = extracurricularService;
            this.extracurricularTypeService = extracurricularTypeService;
        }

        [HttpGet]
        [Route("get-extracurriculars")]
        public IHttpActionResult GetExtracurriculars(int employeeId)
        {
            List<ExtracurricularModel> models = extracurricularService.GetExtracurriculars(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<ExtracurricularModel>>()
            {
                Result = models,
                Permission = permission
            });

        }
        [HttpGet]
        [Route("get-extracurricular")]
        public async Task<IHttpActionResult> GetExtracurricular(int employeeId, int extracurricularId)
        {
            ExtracurricularViewModel vm = new ExtracurricularViewModel();
            vm.Extracurricular = await extracurricularService.GetExtracurricular(extracurricularId);
            vm.ExtracurricularTypes = await extracurricularTypeService.GetExtracurricularTypeSelectModels();
            
            return Ok(new ResponseMessage<ExtracurricularViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-extracurricular/{employeeId}")]
        public async Task<IHttpActionResult> SaveExtracurricular(int employeeId, [FromBody] ExtracurricularModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<ExtracurricularModel>()
            {
                Result = await extracurricularService.SaveExtracurricular(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-extracurricular/{extracurricularId}")]
        public async Task<IHttpActionResult> UpdateExtracurricular(int extracurricularId, [FromBody] ExtracurricularModel model)
        {
            return Ok(new ResponseMessage<ExtracurricularModel>()
            {
                Result = await extracurricularService.SaveExtracurricular(extracurricularId, model)
            });
        }


        [HttpDelete]
        [Route("delete-extracurricular/{id}")]
        public async Task<IHttpActionResult> DeleteExtracurricular(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await extracurricularService.DeleteExtraCurricular(id)
            });
        }
    }
}
