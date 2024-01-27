using Infinity.Bnois.Api;
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
using Infinity.Bnois.Api.Controllers;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Ers.Admin.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ExamCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EXAMINATION_CATEGORIES)]

    public class ExamCategoriesController : PermissionController
    {
        private readonly IExamCategoryService _examCategoryService;
        public ExamCategoriesController(IExamCategoryService examCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            _examCategoryService = examCategoryService;
        }

        [HttpGet]
        [Route("get-exam-categories")]
        public IHttpActionResult GetExamCategories(int ps, int pn,string qs)
        {
            int total = 0;
            List<ExamCategoryModel> models = _examCategoryService.GetExamCategories(ps, pn,qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EXAMINATION_CATEGORIES);
            return Ok(new ResponseMessage<List<ExamCategoryModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }
        [HttpGet]
        [Route("get-exam-category")]
        public async Task<IHttpActionResult> GetExamCategory(int id)
        {
            ExamCategoryModel model = await _examCategoryService.GetExamCategory(id);
            return Ok(new ResponseMessage<ExamCategoryModel>()
            {
                Result = model
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-exam-category")]
        public async Task<IHttpActionResult> SaveExamCategory([FromBody] ExamCategoryModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<ExamCategoryModel>()
            {
                Result = await _examCategoryService.SaveExamCategory(0, model)
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-exam-category/{id}")]
        public async Task<IHttpActionResult> UpdateExamCategory(int id, [FromBody] ExamCategoryModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<ExamCategoryModel>()
            {
                Result = await _examCategoryService.SaveExamCategory(id, model)
            });
        }
        [HttpDelete]
        [Route("delete-exam-category/{id}")]
        public async Task<IHttpActionResult> DeleteExamCategory(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result=await _examCategoryService.DeleteExamCategory(id)
            });
        }
    }
}
