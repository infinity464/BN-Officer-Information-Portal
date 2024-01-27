using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Ers.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Controllers;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Ers.Admin.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Countries)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.COUNTRIES)]

    public class CountrysController : PermissionController
    {
        private readonly ICountryService countryService;      

        public CountrysController(ICountryService countryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        [Route("get-countries")]
        public IHttpActionResult GetCountries(int ps, int pn, string qs)
        {
            int total = 0;
            List<CountryModel> models = countryService.GetCountries(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.COUNTRIES);
            return Ok(new ResponseMessage<List<CountryModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }
      
        [HttpGet]
        [Route("get-country")]
        public async Task<IHttpActionResult> GetCountry(int id)
        {
            CountryModel model = await countryService.GetCountry(id);
            return Ok(new ResponseMessage<CountryModel>
            {
                Result = model
            });
        }
      
        [HttpPost]
        [ModelValidation]
        [Route("save-country")]
        public async Task<IHttpActionResult> SaveCountry([FromBody] CountryModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<CountryModel>
            {
                Result = await countryService.SaveCountry(0, model)
            });
        }
       
        [HttpPut]
        [ModelValidation]
        [Route("update-country/{countryId}")]
        public async Task<IHttpActionResult> UpdateCountry(int countryId, [FromBody] CountryModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<CountryModel>
            {
                Result = await countryService.SaveCountry(countryId, model)

            });
        }
    
        [HttpDelete]
        [Route("delete-country/{id}")]
        public async Task<IHttpActionResult> DeleteCountry(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await countryService.DeleteCountry(id)
            });
        }
    }
}
