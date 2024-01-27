using Infinity.Bnois.Api.Core;
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
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.NominationDetails)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.NOMINATIONS)]

    public class NominationDetailsController : PermissionController
    {
        private readonly INominationDetailService nominationDetailService;
        public NominationDetailsController(INominationDetailService nominationDetailService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.nominationDetailService = nominationDetailService;
        }

        [HttpGet]
        [Route("get-nomination-details")]
        public IHttpActionResult GetNominationDetails(int id)
        {
            int total = 0;
            List<NominationDetailModel> models = nominationDetailService.GetNominationDetails(id);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.NOMINATIONS);
            return Ok(new ResponseMessage<List<NominationDetailModel>>()
            {
                Result = models,
                Total = total,
                Permission = permission
            });
        }


        [HttpGet]
        [Route("get-nomination-detail")]
        public async Task<IHttpActionResult> GetNominationDetail(int id)
        {
            NominationDetailModel model = await nominationDetailService.GetNominationDetail(id);
            return Ok(new ResponseMessage<NominationDetailModel>
            {
                Result = model
            });
        }



        [HttpGet]
        [Route("get-nominated-list")]
        public async Task<IHttpActionResult> GetNominatedList(int nominationId)
        {
            List<SelectModel> model = await nominationDetailService.GetNominatedList(nominationId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-nomination-detail")]
        public async Task<IHttpActionResult> SaveNominationDetail(int type,[FromBody] NominationDetailModel model)
        {
            return Ok(new ResponseMessage<NominationDetailModel>
            {
                Result = await nominationDetailService.SaveNominationDetail(0,type, model)
            });
        }

        [HttpPut]
        [Route("update-nomination-detail/{id}")]
        public async Task<IHttpActionResult> UpdateNominationDetail(int id, [FromBody] List<NominationDetailModel> models)
        {
            return Ok(new ResponseMessage<List<NominationDetailModel>>
            {
                Result = await nominationDetailService.UpdateNominationDetails(id, models)
            });
        }

        [HttpDelete]
        [Route("delete-nomination-detail/{id}")]
        public async Task<IHttpActionResult> DeleteNominationDetail(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await nominationDetailService.DeleteNominationDetail(id)
            });
        }
    }
}
