
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
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Remarks)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.REMARKS)]

    public class RemarksController : PermissionController
    {
        private readonly IRemarkService remarkService;

       


        public RemarksController(IRemarkService remarkService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.remarkService = remarkService;

            


        }

        [HttpGet]
        [Route("get-remarks")]
        public IHttpActionResult GetRemarks(string pNo,int type)
        {
            List<RemarkModel> models = remarkService.GetRemarks(pNo,type);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.REMARKS);
            return Ok(new ResponseMessage<List<RemarkModel>>()
            {
                Result = models,
                Permission = permission
               
            });
        }

        [HttpGet]
        [Route("get-remark")]
        public async Task<IHttpActionResult> GetRemark(int id)
        {
            RemarkModel model = await remarkService.GetRemark(id);
         

            return Ok(new ResponseMessage<RemarkModel>
            {
                Result = model
            });
        }

       
        [HttpPost]
        [ModelValidation]
        [Route("save-remark")]
        public async Task<IHttpActionResult> SaveRemark([FromBody] RemarkModel model)
        {
            return Ok(new ResponseMessage<RemarkModel>
            {
                Result = await remarkService.SaveRemark(0, model)
            });
        }

        [HttpPut]
        [Route("update-remark/{id}")]
        public async Task<IHttpActionResult> UpdateRemark(int id, [FromBody] RemarkModel model)
        {
            return Ok(new ResponseMessage<RemarkModel>
            {
                Result = await remarkService.SaveRemark(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-remark/{id}")]
        public async Task<IHttpActionResult> DeleteRemark(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await remarkService.DeleteRemark(id)
            });
        }

       
    }
}