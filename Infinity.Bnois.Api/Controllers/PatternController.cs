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
	[RoutePrefix(BnoisRoutePrefix.Pattern)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.PATTERN)]

	public class PatternController : PermissionController
    {
		private readonly IPatternService patternService;
		public PatternController(IPatternService patternService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.patternService = patternService;
		}

		[HttpGet]
		[Route("get-patterns")]
		public IHttpActionResult GetPatterns(int ps, int pn, string qs)
		{
			int total = 0;
			List<PatternModel> models = patternService.GetPatterns(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.PATTERN);
            return Ok(new ResponseMessage<List<PatternModel>>()
			{
				Result = models,
				Total = total, Permission=permission
			});
		}

		[HttpGet]
		[Route("get-pattern")]
		public async Task<IHttpActionResult> GetPattern(int id)
		{
			PatternModel model = await patternService.GetPattern(id);
			return Ok(new ResponseMessage<PatternModel>
			{
				Result = model
			});
		}

		[HttpPost]
		[ModelValidation]
		[Route("save-pattern")]
		public async Task<IHttpActionResult> SavePattern([FromBody] PatternModel model)
		{
			return Ok(new ResponseMessage<PatternModel>
			{
				Result = await patternService.SavePattern(0, model)
			});
		}

		[HttpPut]
		[Route("update-pattern/{id}")]
		public async Task<IHttpActionResult> UpdatePattern(int id, [FromBody] PatternModel model)
		{
			return Ok(new ResponseMessage<PatternModel>
			{
				Result = await patternService.SavePattern(id, model)
			});
		}

		[HttpDelete]
		[Route("delete-pattern/{id}")]
		public async Task<IHttpActionResult> DeletePattern(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await patternService.DeletePattern(id)
			});
		}
	}
}
