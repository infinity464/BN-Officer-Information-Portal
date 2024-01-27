using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Controllers;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Ers.Applicant.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Educations)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class EducationalsController : PermissionController
    {
        private readonly IEducationService educationService;
        private readonly IExamCategoryService examCategoryService;
        private readonly IExaminationService examinationService;
        private readonly IBoardService boardService;
        private readonly ISubjectService subjectService;
        private readonly IResultService resultService;
        private readonly IInstituteService instituteService;
        private readonly IResultGradeService resultGradeService;

        public EducationalsController(IResultGradeService resultGradeService,IInstituteService instituteService, IEducationService educationService, IExamCategoryService examCategoryService, IExaminationService examinationService, 
            IBoardService boardService, ISubjectService subjectService, IResultService resultService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.instituteService = instituteService;
            this.educationService = educationService;
            this.examCategoryService = examCategoryService;
            this.examinationService = examinationService;
            this.boardService = boardService;
            this.subjectService = subjectService;
            this.resultService = resultService;
            this.resultGradeService = resultGradeService;
        }

        [HttpGet]
        [Route("get-educations")]
        public IHttpActionResult GetEducations(int employeeId)
        {
            List<EducationModel> models = educationService.GetEducations(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<EducationModel>>()
            {
                Result = models,
                Permission = permission
            });

        }
        [HttpGet]
        [Route("get-education")]
        public async Task<IHttpActionResult> GetEducation(int employeeId, int educationId)
        {
            EducationViewModel vm = new EducationViewModel();
            vm.Education = await educationService.GetEducation(educationId);
            vm.ExamCategories = await examCategoryService.GetExamCategorySelectModels();
            vm.Years = educationService.GetYearSelectModel();
            vm.CourseDurations = educationService.GetDurationSelectModel();
            vm.Grades = await resultGradeService.getGradeSelectModels();
            if (educationId > 0)
            {
                vm.Examinations = await examinationService.GetExaminationSelectModelByExamCategory(vm.Education.ExamCategoryId);
                vm.Results = await resultService.ResultsSelectModel(vm.Education.ExamCategoryId);
                vm.Boards = await boardService.BoardsSelectModel(vm.Education.ExamCategoryId);
                vm.Institutes =await  instituteService.GetInstitutesSelectModelByBoard(vm.Education.BoardId);
                vm.Subjects = await subjectService.GetSubjectsSelectModelByExamination(vm.Education.ExaminationId);
                vm.Years = educationService.GetYearSelectModel();
                vm.CourseDurations = educationService.GetDurationSelectModel();

            }

            vm.File = new FileModel {FileName=vm.Education.FileName,FilePath=Documents.RemoteImageUrl+vm.Education.FileName };

            return Ok(new ResponseMessage<EducationViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-education/{employeeId}")]
        public async Task<IHttpActionResult> SaveEducation(int employeeId, [FromBody] EducationModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<EducationModel>()
            {
                Result = await educationService.SaveEducation(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-education/{educationId}")]
        public async Task<IHttpActionResult> UpdateEducation(int educationId, [FromBody] EducationModel model)
        {
            return Ok(new ResponseMessage<EducationModel>()
            {
                Result = await educationService.SaveEducation(educationId, model)
            });
        }


        [HttpGet]
        [Route("get-examinations/{examcategoryId}")]
        public async Task<IHttpActionResult> GetExaminationSelectModelByExamCategory(int examcategoryId)
        {
            EducationViewModel model = new EducationViewModel();
            model.ExamCategory = await examCategoryService.GetExamCategory(examcategoryId);
            model.Examinations = await examinationService.GetExaminationSelectModelByExamCategory(examcategoryId);
            model.Boards = await boardService.BoardsSelectModel(examcategoryId);
            model.Results = await resultService.ResultsSelectModel(examcategoryId);
            return Ok(new ResponseMessage<EducationViewModel>
            {
                Result = model
            });
        }
        [HttpGet]
        [Route("get-subjects/{examinationId}")]
        public async Task<IHttpActionResult> GetSubjectsSelectModelByExamination(int examinationId)
        {
            List<SelectModel> subjects = await subjectService.GetSubjectsSelectModelByExamination(examinationId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = subjects
            });
        }

        [HttpGet]
        [Route("get-institutes/{boardId}")]
        public async Task<IHttpActionResult> GetInstitutesSelectModelByBoard(long boardId)
        {
            List<SelectModel> subjects = await instituteService.GetInstitutesSelectModelByBoard(boardId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = subjects
            });
        }


        [HttpDelete]
        [Route("delete-education/{id}")]
        public async Task<IHttpActionResult> DeleteEducation(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await educationService.DeleteEducation(id)
            });
        }

        [HttpPost]
        [Route("upload-education-certificate")]
        public async Task<IHttpActionResult> UploadEducationCertificate(int employeeId, int educationId)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            string fileSaveLocation = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;
            fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/Image");

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(provider);

            fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
            filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/Image", fileName);


           EducationViewModel vm = new EducationViewModel();
            vm.Education = new EducationModel();
            vm.Education.EmployeeId = employeeId;
            vm.Education.EducationId = educationId;
            vm.Education.FileName = fileName;

            vm.Education = await educationService.UpdateEducation(vm.Education);

            vm.File = new FileModel
            {
                FileName = vm.Education.FileName,
                FilePath = Documents.RemoteImageUrl + vm.Education.FileName
            };
            return Ok(new ResponseMessage<EducationViewModel>
            {
                Result = vm
            });
        }
    }
}
