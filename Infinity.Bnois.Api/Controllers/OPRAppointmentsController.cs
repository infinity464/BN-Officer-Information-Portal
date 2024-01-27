using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.OPRAppointments)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.OPR_Entries)]

    public class OPRAppointmentsController : PermissionController
    {
        private readonly IOPRAppointmentService OPRAppointmentService;
        private readonly ISuitabilityService suitabilityService;
        private readonly ISpecialAptTypeService specialAptTypeService;
        public OPRAppointmentsController(IOPRAppointmentService OPRAppointmentService, ISuitabilityService suitabilityService,
            ISpecialAptTypeService specialAptTypeService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.OPRAppointmentService = OPRAppointmentService;
            this.suitabilityService = suitabilityService;
            this.specialAptTypeService = specialAptTypeService;
        }

        [HttpGet]
        [Route("get-opr-appointments")]
        public IHttpActionResult GetOPRAppointments(int id)
        {
            List<OprAptSuitabilityModel> models = OPRAppointmentService.GetOPRAppointments(id);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.OPR_Entries);
            return Ok(new ResponseMessage<List<OprAptSuitabilityModel>>()
            {
                Result = models,
                Permission = permission
               
            });
        }


        [HttpGet]
        [Route("get-opr-appointment")]
        public async Task<IHttpActionResult> GetOPRAppointment(int id)
        {
            OPRAppointmentViewModel vm=new OPRAppointmentViewModel();
           
            vm.SpecialAppointmentTypes = await specialAptTypeService.GetSpecialAptTypeSelectModels();
            vm.Suitabilities = await suitabilityService.GetSuitabilitySelectModels();

            return Ok(new ResponseMessage<OPRAppointmentViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-opr-appointment")]
        public async Task<IHttpActionResult> SaveOPRAppointment([FromBody] OprAptSuitabilityModel model)
        {
            return Ok(new ResponseMessage<OprAptSuitabilityModel>
            {
                Result = await OPRAppointmentService.SaveOPRAppointment(0, model)
            });
        }

      
        [HttpDelete]
        [Route("delete-opr-appointment/{id}")]
        public async Task<IHttpActionResult> DeleteOPRAppointment(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await OPRAppointmentService.DeleteOPRAppointment(id)
            });
        }
    }
}
