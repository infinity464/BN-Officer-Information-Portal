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
using Infinity.Bnois.Data;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ShipMovement)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SHIP_MOVEMENT)]

    public class ShipMovementController:BaseController
    {
        private readonly IOfficeService officeService;
       
       

        public ShipMovementController(IOfficeService officeService)
        {
            this.officeService = officeService;
           
           
        }
      


        [HttpGet]
        [Route("get-ship-movement-select-models")]
        public async Task<IHttpActionResult> GetShipMovementSelectModels()
        {
            ShipMovementViewModel vm = new ShipMovementViewModel();
            vm.Ships= await officeService.GetShipSelectModels();
            vm.Offices =await officeService.GetOfficeWithoutShipSelectModels();
           
            return Ok(new ResponseMessage<ShipMovementViewModel>
            {
                Result = vm
            });
        }
       

        [HttpPut]
        [Route("update-ship-movement/{officeId}")]
        public async Task<IHttpActionResult> UpdateShipMovement(int officeId, [FromBody] OfficeModel model)
        {
            return Ok(new ResponseMessage<OfficeModel>
            {
                Result = await officeService.ShipMovement(officeId, model)
            });
        }
    
    }
}
