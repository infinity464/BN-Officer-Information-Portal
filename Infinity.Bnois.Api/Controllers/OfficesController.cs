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
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Offices)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.OFFICES)]

    public class OfficesController:PermissionController
    {
        private readonly IOfficeService officeService;
        private readonly ICountryService countryService;
        private readonly IZoneService zoneService;
        private readonly IShipCategoryService shipCategoryService;
        private readonly IPatternService patternService;

        public OfficesController(IOfficeService officeService, ICountryService countryService, IZoneService zoneService,
            IShipCategoryService shipCategoryService, IPatternService patternService,IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.officeService = officeService;
            this.countryService = countryService;
            this.zoneService = zoneService;
            this.shipCategoryService = shipCategoryService;
            this.patternService = patternService;
        }
        [HttpGet]
        [Route("get-offices")]
        public IHttpActionResult GetOffices()
        {
        
            List<OfficeModel> offices = officeService.GetOffices();
            RoleFeature permission = base.GetFeature(MASTER_SETUP.OFFICES);
            return Ok(new ResponseMessage<List<OfficeModel>>()
            {
                Result = offices,
                Permission = permission
            });

        }

        [HttpGet]
        [Route("get-office-structures")]
        public IHttpActionResult GetOfficeStructures()
        {

            List<OfficeModel> offices = officeService.GetOfficeStructures();
            return Ok(new ResponseMessage<List<OfficeModel>>()
            {
                Result = offices
            });

        }



        [HttpGet]
        [Route("get-office")]
        public async Task<IHttpActionResult> GetOffice(int id)
        {
            OfficeViewModel vm = new OfficeViewModel();
            vm.Office= await officeService.GetOffice(id);
            vm.Countries =await countryService.GetCountriesTypeSelectModel();
            vm.ShipTypes = officeService.GetShipTypeSelectModels();
            vm.Objectives = officeService.GetObjectiveSelectModels();
            vm.AdminAuthorities =await officeService.GetAdminAuthoritySelectModel();
            vm.Patterns =await patternService.GetPatternTypeSelectModels();
            vm.Zones = await zoneService.GetZoneSelectModels();
            vm.ShipCategories = await shipCategoryService.GetShipCategorySelectModels();
            vm.BornOffices = await officeService.GetBornOfficeSelectModel();
            vm.ParentOffices = await officeService.GetParentOfficeSelectModel();
            return Ok(new ResponseMessage<OfficeViewModel>
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-office")]
        public async Task<IHttpActionResult> SaveOffice([FromBody] OfficeModel model)
        {
            return Ok(new ResponseMessage<OfficeModel>
            {
                Result = await officeService.SaveOffice(0, model)
            });
        }

        [HttpPut]
        [Route("update-office/{id}")]
        public async Task<IHttpActionResult> UpdateUpdate(int id, [FromBody] OfficeModel model)
        {
            return Ok(new ResponseMessage<OfficeModel>
            {
                Result = await officeService.SaveOffice(id, model)
            });
        }
        [HttpDelete]
        [Route("delete-office/{id}")]
        public async Task<IHttpActionResult> DeleteOffice(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await officeService.DeleteOffice(id)
            });
        }


        [HttpGet]
        [Route("get-office-select-model-by-type")]
        public async Task<IHttpActionResult> GetOfficeSelectModelByType(int type)
        {
            List<SelectModel> selectModels = await officeService.GetOfficeSelectModel(type);
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = selectModels
            });
        }


        [HttpGet]
        [Route("get-office-select-model-by-ship")]
        public async Task<IHttpActionResult> GetOfficeSelectModelByShip(int ship)
        {
            List<SelectModel> selectModels = await officeService.GetOfficeSelectModelByShip(ship);
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = selectModels
            });
        }


     

        [HttpGet]
        [Route("get-ministry-office-select-models")]
        public async Task<IHttpActionResult> GetMinistryOfficeSelectModel()
        {
            List<SelectModel> selectModels = await officeService.GetMinistryOfficeSelectModel();
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = selectModels
            });
        }



        [HttpGet]
        [Route("get-child-office-select-models")]
        public async Task<IHttpActionResult> GetChildOfficeSelectModel(int parentId)
        {
            List<SelectModel> selectModels = await officeService.GetChildOfficeSelectModel(parentId);
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = selectModels
            });
        }




        [HttpGet]
        [Route("get-office-appointment-details")]
        public async Task<IHttpActionResult> GetOfficeAppointmentDetails(int officeId)
        {
            OfficeViewModel vm=new OfficeViewModel();
            vm.Office=await officeService.GetOffice(officeId);
            vm.AppointedOfficers = officeService.GetAppointedOfficer(officeId);
            vm.VacantAppointments = officeService.GetVacantAppointment(officeId);
            return Ok(new ResponseMessage<object>()
            {
                Result = vm
            });
        }

    }
}
