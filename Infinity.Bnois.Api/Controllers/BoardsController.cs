using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
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
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Ers.Admin.Api.Controllers
{

    [RoutePrefix(BnoisRoutePrefix.Boards)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.BOARDS)]
    public class BoardsController : PermissionController
    {
       
        private readonly IBoardService boardService;

        public BoardsController(IBoardService boardService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.boardService = boardService;
        }


        [HttpGet]
        [Route("get-boards")]
        public IHttpActionResult GetBoards(int ps, int pn, string qs)
        {
            int total = 0;
            List<BoardModel> models = boardService.GetBoards(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.BOARDS);
            return Ok(new ResponseMessage<List<BoardModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });

        }

        [HttpGet]
        [ModelValidation]
        [Route("get-board")]
        public async Task<IHttpActionResult> GetBoard(int id)
        {
            BoardViewModel vm = new BoardViewModel();
            vm.Board = await boardService.GetBoard(id);
            vm.BoardTypes = boardService.getBoardTypesSelectModel();
            return Ok(new ResponseMessage<BoardViewModel>()
            {
                Result = vm

            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-board")]
        public async Task<IHttpActionResult> SaveBoard([FromBody]BoardModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<BoardModel>()
            {
                Result = await boardService.SaveBoard(0, model)
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-board/{id}")]
        public async Task<IHttpActionResult> UpdateBoard(int id, [FromBody]BoardModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<BoardModel>()
            {
                Result = await boardService.SaveBoard(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-board/{id}")]
        public async Task<IHttpActionResult> DeleteBoard(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await boardService.DeleteBoard(id)
            });
        }
    }
}
