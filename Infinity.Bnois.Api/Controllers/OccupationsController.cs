using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
	[RoutePrefix(BnoisRoutePrefix.Occupations)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.OCCUPATIONS)]

    public class OccupationsController : PermissionController
    {
		private readonly IOccupationService occupationService;
		public OccupationsController(IOccupationService occupationService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.occupationService = occupationService;
		}
		[HttpGet]
		[Route("get-occupations")]
		public IHttpActionResult GetOccupations(int ps, int pn, string qs)
		{
			int total = 0;
			List<OccupationModel> occupations = occupationService.GetOccupations(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.OCCUPATIONS);
            return Ok(new ResponseMessage<List<OccupationModel>>()
			{
				Result = occupations,
				Total = total, Permission=permission
			});
		}

		[HttpGet]
		[Route("get-occupation")]
		public async Task<IHttpActionResult> GetOccupation(int id)
		{
			OccupationModel model = await occupationService.GetOccupation(id);
			return Ok(new ResponseMessage<OccupationModel>
			{
				Result = model
			});
		}

		[HttpPost]
		[ModelValidation]
		[Route("save-occupation")]
		public async Task<IHttpActionResult> SaveOccupation([FromBody] OccupationModel model)
		{
			return Ok(new ResponseMessage<OccupationModel>
			{
				Result = await occupationService.SaveOccupation(0, model)
			});
		}

		[HttpPut]
		[Route("update-occupation/{id}")]
		public async Task<IHttpActionResult> UpdateOccupation(int id, [FromBody] OccupationModel model)
		{
			return Ok(new ResponseMessage<OccupationModel>
			{
				Result = await occupationService.SaveOccupation(id, model)
			});
		}

		[HttpDelete]
		[Route("delete-occupation/{id}")]
		public async Task<IHttpActionResult> DeleteOccupation(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await occupationService.DeleteOccupation(id)
			});
		}

	}
}
