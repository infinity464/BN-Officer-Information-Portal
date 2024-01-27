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
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ShipCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SHIP_CATEGORIES)]

    public class ShipCategorysController : PermissionController
    {
        private readonly IShipCategoryService shipCategoryService;
        public ShipCategorysController(IShipCategoryService shipCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.shipCategoryService = shipCategoryService;
        }

        [HttpGet]
        [Route("get-ship-categories")]
        public IHttpActionResult GetShipCategorys(int ps, int pn, string qs)
        {
            int total = 0;
            List<ShipCategoryModel> models = shipCategoryService.GetShipCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.SHIP_CATEGORIES);
            return Ok(new ResponseMessage<List<ShipCategoryModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-ship-category")]
        public async Task<IHttpActionResult> GetShipCategory(int id)
        {
            ShipCategoryModel model = await shipCategoryService.GetShipCategory(id);
            return Ok(new ResponseMessage<ShipCategoryModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-ship-category")]
        public async Task<IHttpActionResult> SaveShipCategory([FromBody] ShipCategoryModel model)
        {
            return Ok(new ResponseMessage<ShipCategoryModel>
            {
                Result = await shipCategoryService.SaveShipCategory(0, model)
            });
        }

        [HttpPut]
        [Route("update-ship-category/{id}")]
        public async Task<IHttpActionResult> UpdateShipCategory(int id, [FromBody] ShipCategoryModel model)
        {
            return Ok(new ResponseMessage<ShipCategoryModel>
            {
                Result = await shipCategoryService.SaveShipCategory(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-ship-category/{id}")]
        public async Task<IHttpActionResult> DeleteShipCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await shipCategoryService.DeleteShipCategory(id)
            });
        }
    }
}
