using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
	[RoutePrefix(BnoisRoutePrefix.Religions)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.RELIGIONS)]

    public class ReligionsController : PermissionController
    {
		private readonly IReligionService religionService;
		public ReligionsController(IReligionService religionService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.religionService = religionService;
		}
		[HttpGet]
		[Route("get-religions")]
		public IHttpActionResult GetReligions(int ps, int pn, string qs)
		{
			int total = 0;
			List<ReligionModel> religions = religionService.GetReligions(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.RELIGIONS);
            return Ok(new ResponseMessage<List<ReligionModel>>()
			{
				Result = religions,
				Total = total, Permission=permission
			});
		}

		[HttpGet]
		[Route("get-religion")]
		public async Task<IHttpActionResult> GetReligion(int id)
		{
			ReligionModel model = await religionService.GetReligion(id);
			return Ok(new ResponseMessage<ReligionModel>
			{
				Result = model
			});
		}

		[HttpPost]
		[ModelValidation]
		[Route("save-religion")]
		public async Task<IHttpActionResult> SaveReligion([FromBody] ReligionModel model)
		{
			return Ok(new ResponseMessage<ReligionModel>
			{
				Result = await religionService.SaveReligion(0, model)
			});
		}

		[HttpPut]
		[Route("update-religion/{id}")]
		public async Task<IHttpActionResult> UpdateReligion(int id, [FromBody] ReligionModel model)
		{
			return Ok(new ResponseMessage<ReligionModel>
			{
				Result = await religionService.SaveReligion(id, model)
			});
		}

		[HttpDelete]
		[Route("delete-religion/{id}")]
		public async Task<IHttpActionResult> DeleteReligion(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await religionService.DeleteReligion(id)
			});
		}

	}
}
