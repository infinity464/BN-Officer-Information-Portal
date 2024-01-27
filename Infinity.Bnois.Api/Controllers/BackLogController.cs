
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

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.BackLogs)]
    [EnableCors("*","*","*")]
    public class BackLogController : BaseController
    {
        private readonly IRankService rankService;
        private readonly ITransferService transferService;
       

        public BackLogController(IRankService rankService, ITransferService transferService)
        {
            this.rankService = rankService;
            this.transferService = transferService;
           
        }

      
        [HttpGet]
        [Route("get-back-log-select-models")]
        public async Task<IHttpActionResult> GetBackLogRankAndTransfer(int employeeId)
        {
            BackLogViewModel vm = new BackLogViewModel();
            vm.Ranks = await rankService.GetRanksSelectModel();
            vm.Transfers = await  transferService.GetTransferHistory(employeeId);
            return Ok(new ResponseMessage<BackLogViewModel>
            {
                Result = vm
            });
        }

      
    }
}