
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Ers.ApplicationService;
using System.Net.Http;
using Infinity.Bnois.ExceptionHelper;
using Infinity.Bnois.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.TrainingResults)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.TRAINING_RESULTS)]

    public class TrainingResultsController : PermissionController
    {
        private readonly ITrainingResultService trainingResultService;
        private readonly IResultTypeService resultTypeService;
        private readonly ITrainingPlanService trainingPlanService;
       


        public TrainingResultsController(ITrainingResultService trainingResultService, IResultTypeService resultTypeService,
            ITrainingPlanService trainingPlanService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.trainingResultService = trainingResultService;
            this.resultTypeService = resultTypeService;
            this.trainingPlanService = trainingPlanService;
         


        }

        [HttpGet]
        [Route("get-training-results")]
        public IHttpActionResult GetTrainingResults(int ps, int pn, string qs)
        {
            int total = 0;
            List<TrainingResultModel> models = trainingResultService.GetTrainingResults(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.TRAINING_RESULTS);
            return Ok(new ResponseMessage<List<TrainingResultModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-training-result")]
        public async Task<IHttpActionResult> GetTrainingResult(int id)
        {
            TrainingResultViewModel vm = new TrainingResultViewModel();
            vm.TrainingResult = await trainingResultService.GetTrainingResult(id);
            vm.ResultStatus = trainingResultService.GetResultStatusSelectModels();
            vm.ResultTypes = await resultTypeService.GetResultTypeSelectModels();
            vm.TrainingPlans = await trainingPlanService.GetTrainingPlanSelectModels();



            vm.File = new FileModel { FileName = vm.TrainingResult.ResultSection, FilePath = Documents.RemoteImageUrl + vm.TrainingResult.ResultSection };

            return Ok(new ResponseMessage<TrainingResultViewModel>
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("get-training-result-by-employee")]
        public async Task<IHttpActionResult> GetTrainingResultByEmployee(int id)
        {
            TrainingResultViewModel vm = new TrainingResultViewModel();
            vm.TrainingResult = await trainingResultService.GetTrainingResultByEmployee(id);
            vm.ResultStatus = trainingResultService.GetResultStatusSelectModels();
            vm.ResultTypes = await resultTypeService.GetResultTypeSelectModels();
            vm.TrainingPlans = await trainingPlanService.GetTrainingPlanSelectModels();


            return Ok(new ResponseMessage<TrainingResultViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-training-result")]
        public async Task<IHttpActionResult> SaveTrainingResult([FromBody] TrainingResultModel model)
        {
            return Ok(new ResponseMessage<TrainingResultModel>
            {
                Result = await trainingResultService.SaveTrainingResult(0, model)
            });
        }

        [HttpPut]
        [Route("update-training-result/{id}")]
        public async Task<IHttpActionResult> UpdateTrainingResult(int id, [FromBody] TrainingResultModel model)
        {
            return Ok(new ResponseMessage<TrainingResultModel>
            {
                Result = await trainingResultService.SaveTrainingResult(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-training-result/{id}")]
        public async Task<IHttpActionResult> DeleteTrainingResult(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await trainingResultService.DeleteTrainingResult(id)
            });
        }




        [HttpPost]
        [Route("training-result-upload")]
        public async Task<IHttpActionResult> UploadTrainingResult(int id, int type)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            string fileSaveLocation = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;

            if (type.Equals(1))
            {
                fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/File");
            }
            else
            {
                fileSaveLocation = HttpContext.Current.Server.MapPath("~/Documents/Image");
            }

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            await Request.Content.ReadAsMultipartAsync(provider);

            if (type.Equals(1))
            {
                fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
                filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/File", fileName);
            }
            else
            {
                fileName = System.IO.Path.GetFileName(provider.FileData[0].LocalFileName);
                filePath = string.Format("{0}://{1}/{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority, "Documents/Image", fileName);
            }

            TrainingResultViewModel vm = new TrainingResultViewModel();
            vm.TrainingResult = new TrainingResultModel();
            vm.TrainingResult.TrainingResultId = id;
            if (type.Equals(1))
            {
                vm.TrainingResult.FileName = fileName;
            }
            if (type.Equals(2))
            {
                vm.TrainingResult.ResultSection = fileName;
            }

            vm.TrainingResult = await trainingResultService.UpdateTrainingResult(vm.TrainingResult);


            vm.File = new FileModel
            {
                FileName = vm.TrainingResult.FileName,
                FilePath = Documents.RemoteImageUrl + vm.TrainingResult.ResultSection
            };

            return Ok(new ResponseMessage<TrainingResultViewModel>
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("downlaod-training-result")]
        public async Task<HttpResponseMessage> DownlaodTrainingResult(int trainingResultId)
        {
            TrainingResultModel model = await trainingResultService.GetTrainingResult(trainingResultId);

            if (string.IsNullOrWhiteSpace(model.FileName))
            {
                throw new InfinityArgumentMissingException("File not found");
            }
            string root = ConfigurationResolver.Get().DocumentUploadFolder;
            string filePath = Path.Combine(root, model.FileName);
            if (!File.Exists(filePath))
            {
                throw new InfinityArgumentMissingException("File not found");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);

                    HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                    httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = model.FileName;
                    httpResponseMessage.StatusCode = HttpStatusCode.OK;
                    return httpResponseMessage;
                }

            }
        }
    }
}