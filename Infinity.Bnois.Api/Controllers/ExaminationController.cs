using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
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
    [RoutePrefix(BnoisRoutePrefix.Examinations)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EXAMINATION)]

    public class ExaminationController: PermissionController
    {
        private readonly IExaminationService _examinationService;
        private readonly IExamCategoryService _examCategoryService;
        public ExaminationController(IExaminationService examinationService, IExamCategoryService examCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            _examinationService = examinationService;
            _examCategoryService = examCategoryService;
        }
        [HttpGet]
        [Route("get-examinations")]
        public IHttpActionResult GetExaminations(int ps,int pn,string qs)
        {
            int total = 0;
            List<ExaminationModel> models = _examinationService.GetExaminations(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EXAMINATION);
            return Ok(new ResponseMessage<List<ExaminationModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }
        [HttpGet]
        [Route("get-examination")]
        public async Task<IHttpActionResult> GetExamination(int examinationId)
        {
            ExaminationModel model = await _examinationService.GetExamination(examinationId);
            List<SelectModel> models = _examCategoryService.GetExamCategories();
            return Ok(new ResponseMessage<ExaminationViewModel>()
            {
               Result=new ExaminationViewModel()
               {
                   ExamCategories = models,
                   Examination = model
               }
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-examination")]
        public async Task<IHttpActionResult> SaveExamination([FromBody] ExaminationModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<ExaminationModel>()
            {
                Result = await _examinationService.SaveExamination(0,model)
            });
        }
        [HttpPut]
        [Route("update-examination/{examinationId}")]
        public async Task<IHttpActionResult> UpdateExamination(int examinationId,[FromBody] ExaminationModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<ExaminationModel>()
            {
                Result = await _examinationService.SaveExamination(examinationId,model)
            });
        }
        [HttpDelete]
        [Route("delete-examination/{examinationId}")]
        public async Task<IHttpActionResult> DeleteExamination(int examinationId)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result=await _examinationService.DeleteExamination(examinationId)
            });
        }
    }
}
