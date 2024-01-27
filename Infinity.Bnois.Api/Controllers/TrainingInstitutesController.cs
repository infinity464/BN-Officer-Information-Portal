

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Ers.ApplicationService;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.TrainingInstitutes)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.TRAINING_INSTITUTES)]

    public class TrainingInstitutesController : PermissionController
    {
        private readonly ITrainingInstituteService trainingInstituteService;
        private readonly ICountryService countryService;
 

        public TrainingInstitutesController(ITrainingInstituteService trainingInstituteService, ICountryService countryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.trainingInstituteService = trainingInstituteService;
            this.countryService = countryService;
        }

        [HttpGet]
        [Route("get-training-institutes")]
        public IHttpActionResult GetTrainingInstitutes(int ps, int pn, string qs)
        {
            int total = 0;
            List<TrainingInstituteModel> models = trainingInstituteService.GetTrainingInstitutes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.TRAINING_INSTITUTES);
            return Ok(new ResponseMessage<List<TrainingInstituteModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-training-institute")]
        public async Task<IHttpActionResult> GetTrainingInstitute(int id)
        {
            TrainingInstituteViewModel vm = new TrainingInstituteViewModel();
            vm.TrainingInstitute = await trainingInstituteService.GetTrainingInstitute(id);
            vm.CountryTypes = trainingInstituteService.GetCountryTypeSelectModels();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();

            return Ok(new ResponseMessage<TrainingInstituteViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-training-institute")]
        public async Task<IHttpActionResult> SaveTrainingInstitute([FromBody] TrainingInstituteModel model)
        {
            return Ok(new ResponseMessage<TrainingInstituteModel>
            {
                Result = await trainingInstituteService.SaveTrainingInstitute(0, model)
            });
        }

        [HttpPut]
        [Route("update-training-institute/{id}")]
        public async Task<IHttpActionResult> UpdateTrainingInstitute(int id, [FromBody] TrainingInstituteModel model)
        {
            return Ok(new ResponseMessage<TrainingInstituteModel>
            {
                Result = await trainingInstituteService.SaveTrainingInstitute(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-training-institute/{id}")]
        public async Task<IHttpActionResult> DeleteTrainingInstitute(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await trainingInstituteService.DeleteTrainingInstitute(id)
            });
        }


        [HttpGet]
        [Route("get-training-institute-select-model")]
        public async Task<IHttpActionResult> GetTrainingInstituteSelectModel(int id)
        {
           
            List<SelectModel> model = await trainingInstituteService.GetTrainingInstituteSelectModels(id);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = model
            });
        }

    }
}