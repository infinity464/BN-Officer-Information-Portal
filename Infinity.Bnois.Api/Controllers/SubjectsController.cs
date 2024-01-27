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
	[RoutePrefix(BnoisRoutePrefix.Subjects)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.SUBJECTS)]

    public class SubjectsController : PermissionController
    {
		private readonly ISubjectService subjectService;
		public SubjectsController(ISubjectService subjectService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.subjectService = subjectService;
		}
		[HttpGet]
		[Route("get-subjects")]
		public IHttpActionResult GetSubjects(int ps, int pn, string qs)
		{
			int total = 0;
			List<SubjectModel> subjects = subjectService.GetSubjects(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.SUBJECTS);
            return Ok(new ResponseMessage<List<SubjectModel>>()
			{
				Result = subjects,
				Total = total, Permission=permission
			});
		}

		[HttpGet]
		[Route("get-subject")]
		public async Task<IHttpActionResult> GetSubject(int id)
		{
			SubjectModel model = await subjectService.GetSubjects(id);
			return Ok(new ResponseMessage<SubjectModel>
			{
				Result = model
			});
		}

		[HttpPost]
		[ModelValidation]
		[Route("save-subject")]
		public async Task<IHttpActionResult> SaveSubject([FromBody] SubjectModel model)
		{
			return Ok(new ResponseMessage<SubjectModel>
			{
				Result = await subjectService.SaveSubject(0, model)
			});
		}

		[HttpPut]
		[Route("update-subject/{id}")]
		public async Task<IHttpActionResult> UpdateSubject(int id, [FromBody] SubjectModel model)
		{
			return Ok(new ResponseMessage<SubjectModel>
			{
				Result = await subjectService.SaveSubject(id, model)
			});
		}

		[HttpDelete]
		[Route("delete-subject/{id}")]
		public async Task<IHttpActionResult> DeleteSubject(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await subjectService.DeleteSubject(id)
			});
		}

        [HttpGet]
        [Route("filter-subjects")]
        public async Task<IHttpActionResult> FilterSubjects(string searchStr)
        {
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = await subjectService.FilterSubjects(searchStr)
            });
        }

    }
}
