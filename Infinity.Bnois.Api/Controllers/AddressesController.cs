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
    [RoutePrefix(BnoisRoutePrefix.Addresses)]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    [EnableCors("*", "*", "*")]
    public class AddressesController : PermissionController
    {
        private readonly IAddressService addressService;
        private readonly IDivisionService divisionService;
        private readonly IDistrictService districtService;
        private readonly IUpazilaService upazilaService;
        public AddressesController(IAddressService addressService, IDivisionService divisionService,
            IDistrictService districtService, IUpazilaService upazilaService, IRoleFeatureService featureService):base(featureService)
        {
            this.addressService = addressService;
            this.divisionService = divisionService;
            this.districtService = districtService;
            this.upazilaService = upazilaService;
        }

        [HttpGet]
        [Route("get-addresses")]
        public IHttpActionResult GetAddresses(int employeeId)
        {
            
            List<AddressModel> models = addressService.GetAddresses(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<AddressModel>>()
            {
                Result = models,
                Permission=permission
            });
        }

        [HttpGet]
        [Route("get-address")]
        public async Task<IHttpActionResult> GetAddress(int employeeId, int addressId)
        {
            AddressViewModel vm = new AddressViewModel();
            vm.Address = await addressService.GetAddress(addressId);
            vm.AddressTypes = addressService.GetAddressTypeSelectModels();
            vm.Divisions = await divisionService.GetDivisionSelectModels();
            if (addressId>0)
            {
                vm.Districts = await districtService.GetDistrictByDivisionSelectModels(vm.Address.DivisionId);
                vm.Upazilas = await upazilaService.GetUpazilaByDistrictSelectModels(vm.Address.DistrictId);
            }
            return Ok(new ResponseMessage<AddressViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-address/{employeeId}")]
        public async Task<IHttpActionResult> SaveAddress(int employeeId,[FromBody] AddressModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<AddressModel>
            {
                Result = await addressService.SaveAddress(0, model)
            });
        }

        [HttpPut]
        [Route("update-address/{addressId}")]
        public async Task<IHttpActionResult> UpdateAddress(int addressId, [FromBody] AddressModel model)
        {
            return Ok(new ResponseMessage<AddressModel>
            {
                Result = await addressService.SaveAddress(addressId, model)

            });
        }

        [HttpGet]
        [Route("get-districts/{divisionId}")]
        public async Task<IHttpActionResult> GetDistrictsSelectModelByDivion(int divisionId)
        {
            List<SelectModel> districts = await districtService.GetDistrictsSelectModelByDivion(divisionId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = districts
            });
        }

        [HttpGet]
        [Route("get-upazilas/{districtId}")]
        public async Task<IHttpActionResult> GetUpazilasSelectModelByDistrict(int districtId)
        {
            List<SelectModel> upazilas = await upazilaService.GetUpazilasSelectModelByDistrict(districtId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = upazilas
            });
        }
    }
}
