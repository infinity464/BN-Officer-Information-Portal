using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.PreviousExperiences)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class PreviousExperiencesController : BaseController
    {

        private readonly IPreviousExperienceService previousExperienceService;
        private readonly ICategoryService categoryService;
        private readonly IPreCommissionRankService preCommissionRankService;
       
        public PreviousExperiencesController(IPreviousExperienceService previousExperienceService, 
            ICategoryService categoryService, IPreCommissionRankService preCommissionRankService
            )
        {
            this.previousExperienceService = previousExperienceService;
            this.categoryService = categoryService;
            this.preCommissionRankService = preCommissionRankService;
           
        }


        [HttpGet]
        [Route("get-employee-previous-experience")]
        public async Task<IHttpActionResult> GetPreviousExperience(int employeeId)
        {
            PreviousExperienceViewModel model = new PreviousExperienceViewModel();
            model.PreviousExperience = await previousExperienceService.GetPreviousExperience(employeeId);
            model.Categories = await categoryService.GetCategorySelectModels();
            model.PreCommissionRanks = await preCommissionRankService.GetPreCommissionRankSelectModels();
           
            return Ok(new ResponseMessage<PreviousExperienceViewModel>()
            {
                Result = model
            });
        }

        [HttpPut]
        [Route("update-employee-previous-experience/{employeeId}")]
        public async Task<IHttpActionResult> UpdatePreviousExperience(int employeeId, [FromBody] PreviousExperienceModel model)
        {
           
            return Ok(new ResponseMessage<PreviousExperienceModel>()
            {
                Result = await previousExperienceService.SavePreviousExperience(employeeId, model)
            });
        }
    }
}
