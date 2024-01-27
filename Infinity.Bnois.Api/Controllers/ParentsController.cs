using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Parents)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class ParentsController : BaseController
    {
        private readonly IParentService parentService;
        private readonly IOccupationService occupationService;
        private readonly ICountryService countryService;
        private readonly INationalityService nationalityService;
        private readonly IReligionService religionService;
        private readonly IReligionCastService religionCastService;
        private readonly IRankCategoryService rankCategoryService;
        public ParentsController(IParentService parentService,
            ICountryService countryService,
            INationalityService nationalityService,
            IReligionService religionService,
            IReligionCastService religionCastService,
            IOccupationService occupationService,
            IRankCategoryService rankCategoryService
            )
        {
            this.parentService = parentService;
            this.countryService = countryService;
            this.nationalityService = nationalityService;
            this.religionService = religionService;
            this.religionCastService = religionCastService;
            this.occupationService = occupationService;
            this.rankCategoryService = rankCategoryService;
        }

        [HttpGet]
        [Route("get-employee-parents")]
        public async Task<IHttpActionResult> GetParents(int employeeId, int relationType)
        {
            ParentViewModel vm = new ParentViewModel();
            vm.Parent = await parentService.Parents(employeeId, relationType);
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            vm.Nationalities = await nationalityService.GetNationalitySelectModels();
            vm.Occupations = await occupationService.GetOccupationSelectModels();
            vm.Religions = await religionService.GetReligionSelectModels();
            vm.RankCategories= await rankCategoryService.GetRankCategorySelectModels();
            if (vm.Parent.ParentId > 0)
            {
                vm.ReligionCasts = await religionCastService.GetReligionCastSelectModels();
            }

            vm.File = new FileModel { FileName=vm.Parent.FileName,FilePath=Documents.RemoteImageUrl+ vm.Parent.FileName};

            return Ok(new ResponseMessage<ParentViewModel>()
            {
                Result = vm
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-employee-parents/{employeeId}")]
        public async Task<IHttpActionResult> UpdateParents(int employeeId, [FromBody] ParentModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<ParentModel>()
            {
                Result = await parentService.SaveParents(employeeId, model)
            });
        }

        [HttpPost]
        [Route("upload-parent-image")]
        public async Task<IHttpActionResult> UploadParentImage(int employeeId, int relationType)
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


            ParentViewModel vm = new ParentViewModel();
            vm.Parent = new ParentModel();
            vm.Parent.EmployeeId = employeeId;
            vm.Parent.RelationType = relationType;
            vm.Parent.FileName = fileName;

            vm.Parent = await parentService.UpdateParent(vm.Parent);

            vm.File = new FileModel
            {
                FileName = vm.Parent.FileName,
                FilePath = Documents.RemoteImageUrl + vm.Parent.FileName
            };
            return Ok(new ResponseMessage<ParentViewModel>
            {
                Result = vm
            });
        }
    }
}
