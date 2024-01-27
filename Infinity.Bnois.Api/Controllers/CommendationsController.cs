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

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Commendations)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.COMMENDATIONS)]
    public class CommendationController : PermissionController
    {
        private readonly ICommendationService commendationService;

        public CommendationController(ICommendationService commendationService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.commendationService = commendationService;
        }

        [HttpGet]
        [Route("get-commendations")]
        public IHttpActionResult GetPublicationCategories(int ps, int pn, string qs)
        {
            var total = 0;
            var models = commendationService.GetCommendations(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.COMMENDATIONS);
            return Ok(new ResponseMessage<List<CommendationModel>>
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-commendation")]
        public async Task<IHttpActionResult> GetCommendation(int id)
        {
            CommendationViewModel vm=new CommendationViewModel();
            vm.Commendation= await commendationService.GetCommendation(id);
            vm.CommendationTypes = commendationService.GetCommendationTypeSelectModels();
            return Ok(new ResponseMessage<CommendationViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-commendation")]
        public async Task<IHttpActionResult> SaveCommendation([FromBody] CommendationModel model)
        {
            model.CreatedBy = UserId;
            return Ok(new ResponseMessage<CommendationModel>
            {
                Result = await commendationService.SaveCommendation(0, model)
            });
        }


        [HttpPut]
        [ModelValidation]
        [Route("update-commendation/{id}")]
        public async Task<IHttpActionResult> UpdateCommendation(int id, [FromBody] CommendationModel model)
        {
            model.ModifiedBy = UserId;
            return Ok(new ResponseMessage<CommendationModel>
            {
                Result = await commendationService.SaveCommendation(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-commendation/{id}")]
        public async Task<IHttpActionResult> DeleteCommendation(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await commendationService.DeleteCommendation(id)
            });
        }
    }
}