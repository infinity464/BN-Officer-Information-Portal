using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IBoardMemberService
    {
        Task<BoardMemberModel> GetBoardMember(int boardMemberId);
        List<BoardMemberModel> GetBoardMembers(int promotionBoardId,int ps, int pn, string qs, out int total,int type);
        Task<BoardMemberModel> SaveBoardMember(int v, BoardMemberModel model);
        Task<bool> DeleteBoardMember(int id);
    }
}
