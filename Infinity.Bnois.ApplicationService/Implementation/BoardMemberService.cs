using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   public class BoardMemberService: IBoardMemberService
    {
        private readonly IBnoisRepository<BoardMember> boardMemberRepository;
        public BoardMemberService(IBnoisRepository<BoardMember> boardMemberRepository)
        {
            this.boardMemberRepository = boardMemberRepository;
        }

        public async Task<BoardMemberModel> GetBoardMember(int boardMemberId)
        {
            if (boardMemberId <= 0)
            {
                return new BoardMemberModel();
            }
            BoardMember boardMember = await boardMemberRepository.FindOneAsync(x => x.BoardMemberId == boardMemberId,new List<string> { "Employee","Employee.Batch","Employee.Rank"});
            if (boardMember == null)
            {
                throw new InfinityNotFoundException("Board Member not found");
            }
            BoardMemberModel model = ObjectConverter<BoardMember, BoardMemberModel>.Convert(boardMember);
            return model;
        }

        //public List<BoardMemberModel> GetBoardMembers(int promotionBoardId,int ps, int pn, string qs, out int total,int type)
        //{
        //    IQueryable<BoardMember> boardMembers = boardMemberRepository.FilterWithInclude(x => x.IsActive && x.PromotionBoard.Type==type && x.PromotionBoardId==promotionBoardId
        //         && ((x.PromotionBoard.BoardName.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //         (x.Employee.PNo.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //         (x.MemberRole.Name.Contains(qs) || String.IsNullOrEmpty(qs))), "PromotionBoard", "Employee", "MemberRole");
        //    total = boardMembers.Count();
        //    boardMembers = boardMembers.OrderByDescending(x => x.BoardMemberId).Skip((pn - 1) * ps).Take(ps);
        //    List<BoardMemberModel> models = ObjectConverter<BoardMember, BoardMemberModel>.ConvertList(boardMembers.ToList()).ToList();
        //    return models;
        //}

        public async Task<BoardMemberModel> SaveBoardMember(int boardMemberId, BoardMemberModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("BoardMember data missing");
            }

            bool isExistData = boardMemberRepository.Exists(x => x.EmployeeId == model.Employee.EmployeeId && x.PromotionBoardId == model.PromotionBoardId && x.BoardMemberId != model.BoardMemberId);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Board Member already exists !");
            }
            

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            BoardMember boardMember = ObjectConverter<BoardMemberModel, BoardMember>.Convert(model);
            if (boardMemberId > 0)
            {
                boardMember = await boardMemberRepository.FindOneAsync(x => x.BoardMemberId == boardMemberId);
                if (boardMember == null)
                {
                    throw new InfinityNotFoundException("Board Member not found !");
                }
                boardMember.ModifiedDate = DateTime.Now;
                boardMember.ModifiedBy = userId;

            }
            else
            {
                boardMember.IsActive = true;
                boardMember.CreatedDate = DateTime.Now;
                boardMember.CreatedBy = userId;
            }
            boardMember.PromotionBoardId = model.PromotionBoardId;
            boardMember.EmployeeId = model.Employee.EmployeeId;
            boardMember.MemberRoleId = model.MemberRoleId;
            boardMember.MemberRoleId = model.MemberRoleId;
            boardMember.IsVoteAllowed = model.IsVoteAllowed;
            //boardMember.Employee = null;
            await boardMemberRepository.SaveAsync(boardMember);
            model.BoardMemberId =  boardMember.BoardMemberId;
            return model;
        }

        public async Task<bool> DeleteBoardMember(int boardMemberId)
        {
            if (boardMemberId < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            BoardMember boardMember = await boardMemberRepository.FindOneAsync(x => x.BoardMemberId == boardMemberId);
            if (boardMember == null)
            {
                throw new InfinityNotFoundException("Board Member not found");
            }
            else
            {
                return await boardMemberRepository.DeleteAsync(boardMember);
            }
        }

        public List<BoardMemberModel> GetBoardMembers(int promotionBoardId, int ps, int pn, string qs, out int total, int type)
        {
            throw new NotImplementedException();
        }
    }
}
