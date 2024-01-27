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

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Colors)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.COLORS)]

    public class ColorsController: PermissionController
    {
        private readonly IColorService colorService;
        public ColorsController(IColorService colorService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.colorService = colorService;
        }
        [HttpGet]
        [Route("get-colors")]
        public IHttpActionResult GetColors(int ps, int pn, string qs)
        {
            int total = 0;
            List<ColorModel> colors = colorService.GetColors(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.COLORS);
            return Ok(new ResponseMessage<List<ColorModel>>()
            {
                Result = colors,
                Total = total, Permission=permission
            });
        }
        [HttpGet]
        [Route("get-color")]
        public async Task<IHttpActionResult> GetColor(int id)
        {
            ColorViewModel vm = new ColorViewModel();
            vm.Color= await colorService.GetColor(id);
            vm.ColorTypes = colorService.GetColorTypeSelectModels();
            return Ok(new ResponseMessage<ColorViewModel>
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-color")]
        public async Task<IHttpActionResult> SaveColor([FromBody] ColorModel model)
        {
            return Ok(new ResponseMessage<ColorModel>
            {
                Result = await colorService.SaveColor(0, model)
            });
        }

        [HttpPut]
        [Route("update-color/{id}")]
        public async Task<IHttpActionResult> UpdateUpdate(int id, [FromBody] ColorModel model)
        {
            return Ok(new ResponseMessage<ColorModel>
            {
                Result = await colorService.SaveColor(id, model)
            });
        }
        [HttpDelete]
        [Route("delete-color/{id}")]
        public async Task<IHttpActionResult> DeleteColor(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await colorService.DeleteColor(id)
            });
        }

    }
}
