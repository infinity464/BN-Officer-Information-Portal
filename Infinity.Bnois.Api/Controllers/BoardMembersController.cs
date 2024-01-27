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
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.BoardMembers)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PROMOTION_BOARDS)]
    public class BoardMembersController: PermissionController
    {
        private readonly IBoardMemberService boardMemberService;
        private readonly IMemberRoleService memberRoleService;
        public BoardMembersController(IMemberRoleService memberRoleService, IBoardMemberService boardMemberService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.memberRoleService = memberRoleService;
            this.boardMemberService = boardMemberService;
        }

        [HttpGet]
        [Route("get-board-members")]
        public IHttpActionResult GetBoardMembers(int promotionBoardId,int ps, int pn, string qs,int type)
        {
            int total = 0;
            List<BoardMemberModel> models = boardMemberService.GetBoardMembers(promotionBoardId,ps, pn, qs, out total,type);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PROMOTION_BOARDS);
            return Ok(new ResponseMessage<List<BoardMemberModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });

            
        }

        [HttpGet]
        [Route("get-board-member")]
        public async Task<IHttpActionResult> GetBoardMember(int promotionBoardId,int boardMemberId)
        {
            BoardMemberViewModel vm = new BoardMemberViewModel();
            vm.BoardMember = await boardMemberService.GetBoardMember(boardMemberId);
            vm.MemberRoles = await memberRoleService.GetMemberRoleSelectModels();
            return Ok(new ResponseMessage<BoardMemberViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [Route("save-board-member")]
        public async Task<IHttpActionResult> SaveBoardMember([FromBody] BoardMemberModel model)
        {
            return Ok(new ResponseMessage<BoardMemberModel>
            {
                Result = await boardMemberService.SaveBoardMember(0, model)
            });
        }

        [HttpPut]
        [Route("update-board-member/{boardMemberId}")]
        public async Task<IHttpActionResult> UpdateBoardMember(int boardMemberId, [FromBody] BoardMemberModel model)
        {
            return Ok(new ResponseMessage<BoardMemberModel>
            {
                Result = await boardMemberService.SaveBoardMember(boardMemberId, model)
            });
        }

        [HttpDelete]
        [Route("delete-board-member/{boardMemberId}")]
        public async Task<IHttpActionResult> DeleteBoardMember(int boardMemberId)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await boardMemberService.DeleteBoardMember(boardMemberId)
            });
        }
    }
}
