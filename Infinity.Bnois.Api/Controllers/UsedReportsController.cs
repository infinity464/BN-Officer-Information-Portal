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
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.UsedReports)]
    [EnableCors("*", "*", "*")]
    

    public class UsedReportsController : PermissionController
    {
        private readonly IUsedReportService usedReportService;
        public UsedReportsController(IUsedReportService usedReportService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.usedReportService = usedReportService;
        }

        [HttpGet]
        [Route("get-used-reports")]
        public IHttpActionResult GetUsedReports(int ps, int pn, string qs)
        {
            int total = 0;
            List<UsedReportModel> models = usedReportService.GetUsedReports(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<UsedReportModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-used-report")]
        public async Task<IHttpActionResult> GetUsedReport(int id)
        {
            UsedReportModel model = await usedReportService.GetUsedReport(id);
            return Ok(new ResponseMessage<UsedReportModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-used-report")]
        public async Task<IHttpActionResult> SaveUsedReport([FromBody] UsedReportModel model)
        {
            return Ok(new ResponseMessage<UsedReportModel>
            {
                Result = await usedReportService.SaveUsedReport(0, model)
            });
        }

        [HttpPut]
        [Route("update-used-report/{id}")]
        public async Task<IHttpActionResult> UpdateUsedReport(int id, [FromBody] UsedReportModel model)
        {
            return Ok(new ResponseMessage<UsedReportModel>
            {
                Result = await usedReportService.SaveUsedReport(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-used-report/{id}")]
        public async Task<IHttpActionResult> DeleteUsedReport(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await usedReportService.DeleteUsedReport(id)
            });
        }
    }
}
