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
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.SpouseForeignVisits)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class SpouseForeignVisitsController : PermissionController
    {
        private readonly ISpouseForeignVisitService spouseForeignVisitService;
        private readonly ICountryService countryService;
        public SpouseForeignVisitsController(ISpouseForeignVisitService spouseForeignVisitService, ICountryService countryService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.spouseForeignVisitService = spouseForeignVisitService;
            this.countryService = countryService;
        }

        [HttpGet]
        [Route("get-spouse-foreign-visits")]
        public IHttpActionResult GetSpouseForeignVisits(int spouseId)
        {
            List<SpouseForeignVisitModel> models = spouseForeignVisitService.GetSpouseForeignVisits(spouseId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<SpouseForeignVisitModel>>()
            {
                Result = models,
                Permission = permission

            });
        }

        [HttpGet]
        [Route("get-spouse-foreign-visit")]
        public async Task<IHttpActionResult> GetSpouseForeignVisit(int spouseId, int spouseForeignVisitId)
        {
            SpouseForeignVisitViewModel vm = new SpouseForeignVisitViewModel();
            vm.SpouseForeignVisit = await spouseForeignVisitService.GetSpouseForeignVisit(spouseForeignVisitId);
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            return Ok(new ResponseMessage<SpouseForeignVisitViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-spouse-foreign-visit")]
        public async Task<IHttpActionResult> SaveSpouseForeignVisit([FromBody] SpouseForeignVisitModel model)
        {
            return Ok(new ResponseMessage<SpouseForeignVisitModel>
            {
                Result = await spouseForeignVisitService.SaveSpouseForeignVisit(0, model)
            });
        }

        [HttpPut]
        [Route("update-spouse-foreign-visit/{spouseForeignVisitId}")]
        public async Task<IHttpActionResult> UpdateSpouseForeignVisit(int spouseForeignVisitId, [FromBody] SpouseForeignVisitModel model)
        {
            return Ok(new ResponseMessage<SpouseForeignVisitModel>
            {
                Result = await spouseForeignVisitService.SaveSpouseForeignVisit(spouseForeignVisitId, model)
            });
        }
    }
}
