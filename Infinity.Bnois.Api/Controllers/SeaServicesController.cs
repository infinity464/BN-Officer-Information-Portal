
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
    [RoutePrefix(BnoisRoutePrefix.SeaServices)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SEA_SERVICES)]

    public class SeaServicesController : PermissionController
    {
        private readonly ISeaServiceService seaServiceService;
        private readonly ICountryService countryService;
      
 

        
        public SeaServicesController(ISeaServiceService seaServiceService, ICountryService countryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.seaServiceService = seaServiceService;
            this.countryService = countryService;
          
    

        }

        [HttpGet]
        [Route("get-sea-services")]
        public IHttpActionResult GetAdditonalSeaServices(int ps, int pn, string qs)
        {
            int total = 0;
            List<SeaServiceModel> models = seaServiceService.GetSeaServices(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.SEA_SERVICES);
            return Ok(new ResponseMessage<List<SeaServiceModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-sea-service")]
        public async Task<IHttpActionResult> GetSeaService(int id)
        {
            SeaServiceViewModel vm = new SeaServiceViewModel();
            vm.SeaService = await seaServiceService.GetSeaService(id);
            vm.ShipTypes = seaServiceService.GetShipTypeSelectModels();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
           
   

            return Ok(new ResponseMessage<SeaServiceViewModel>
            {
                Result = vm
            });
        }

  

        [HttpPost]
        [ModelValidation]
        [Route("save-sea-service")]
        public async Task<IHttpActionResult> SaveSeaService([FromBody] SeaServiceModel model)
        {
            return Ok(new ResponseMessage<SeaServiceModel>
            {
                Result = await seaServiceService.SaveSeaService(0, model)
            });
        }

        [HttpPut]
        [Route("update-sea-service/{id}")]
        public async Task<IHttpActionResult> UpdateSeaService(int id, [FromBody] SeaServiceModel model)
        {
            return Ok(new ResponseMessage<SeaServiceModel>
            {
                Result = await seaServiceService.SaveSeaService(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-sea-service/{id}")]
        public async Task<IHttpActionResult> DeleteSeaService(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await seaServiceService.DeleteSeaService(id)
            });
        }
    }
}