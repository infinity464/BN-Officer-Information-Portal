using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Controllers;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;


namespace Infinity.Ers.Admin.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ExamSubjects)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EXAM_SUBJECTS)]
    public class ExamSubjectsController: PermissionController
    {
        private readonly IExamSubjectService examSubjectService;
        private readonly IExamCategoryService examCategoryService;
        private readonly IExaminationService examinationService;
        public ExamSubjectsController(IExamSubjectService examSubjectService, IExamCategoryService examCategoryService, IExaminationService examinationService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.examSubjectService = examSubjectService;
            this.examCategoryService = examCategoryService;
            this.examinationService = examinationService;
        }

        [HttpGet]
        [Route("get-exam-subjects")]
        public IHttpActionResult GetSubjects(int ps, int pn, string qs)
        {
            int total = 0;
            List<ExamSubjectModel> examSubjectModel = examSubjectService.GetExamSubjects(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EXAM_SUBJECTS);
            return Ok(new ResponseMessage<List<ExamSubjectModel>>()
            {
                Result = examSubjectModel,
                Total = total, Permission=permission
            });
        }
        [HttpGet]
        [Route("get-exam-subject")]
        public async Task<IHttpActionResult> GetExamSubject(int examSubjectId)
        {
            ExamSubjectViewModel model = new ExamSubjectViewModel();
            model.ExamCategories = await examCategoryService.GetExamCategorySelectModels();
            model.ExamSubject = await examSubjectService.GetExamSubject(examSubjectId);
            if(examSubjectId>0&& model.ExamSubject != null)
            {
                model.Examinations = await examinationService.GetExaminationSelectModelByExamCategory(model.ExamSubject.Examination.ExamCategoryId);
            }
            else
            {
                model.Examinations = new List<SelectModel>();
            }
            return Ok(new ResponseMessage<ExamSubjectViewModel>()
            {
                Result = model
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-exam-subject")]
        public async Task<IHttpActionResult> SaveExamSubject([FromBody] ExamSubjectModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<ExamSubjectModel>()
            {
                Result = await examSubjectService.SaveExamSubject(0, model)
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-exam-subject/{examSubjectId}")]
        public async Task<IHttpActionResult> UpdateGender(int examSubjectId, [FromBody] ExamSubjectModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<ExamSubjectModel>()
            {
                Result = await examSubjectService.SaveExamSubject(examSubjectId, model)
            });
        }

        [HttpDelete]
        [Route("delete-exam-subject/{examSubjectId}")]
        public async Task<IHttpActionResult> DeleteExamSubject(int examSubjectId)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = await examSubjectService.DeleteExamSubject(examSubjectId)
            });
        }

        [HttpGet]
        [Route("get-examination-select-models/{examcategoryId}")]

        public async Task<IHttpActionResult> GetExaminationSelectModelByExamCategory(int examcategoryId)
        {
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await examinationService.GetExaminationSelectModelByExamCategory(examcategoryId)
            });
        }
    }
}
