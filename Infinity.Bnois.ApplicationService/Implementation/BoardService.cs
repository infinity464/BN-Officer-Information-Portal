using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class BoardService : IBoardService
    {
        private readonly IBnoisRepository<ExamCategory> examCategoryRepository;
        private readonly IBnoisRepository<Board> boardRepository;
        public BoardService(IBnoisRepository<Board> boardRepository, IBnoisRepository<ExamCategory> examCategoryRepository)
        {
            this.boardRepository = boardRepository;
            this.examCategoryRepository = examCategoryRepository;
        }

        public List<BoardModel> GetBoards(int ps, int pn, string qs, out int total)
        {
            IQueryable<Board> boards = boardRepository.FilterWithInclude(x => x.IsActive && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs)) || (x.BoardCode.Contains(qs) || String.IsNullOrEmpty(qs)))).AsQueryable();
            total = boards.Count();
            boards = boards.OrderByDescending(x => x.BoardType).Skip((pn - 1) * ps).Take(ps);
            List<BoardModel> models = ObjectConverter<Board, BoardModel>.ConvertList(boards.ToList()).ToList();
            models = models.Select(x =>
            {
                x.BoardTypeName = Enum.GetName(typeof(BoardType), x.BoardType);
                return x;
            }).ToList();
            return models;
        }
        public async Task<BoardModel> GetBoard(int boardId)
        {
            if (boardId <= 0)
            {
                return new BoardModel();
            }
            Board Board = await boardRepository.FindOneAsync(x => x.BoardId == boardId);
            if (Board == null)
            {
                throw new InfinityNotFoundException("Board not Found!");
            }
            BoardModel model = ObjectConverter<Board, BoardModel>.Convert(Board);
            return model;
        }
        public async Task<BoardModel> SaveBoard(int boardId, BoardModel model)
        {
            Board oldData = await boardRepository.FindOneAsync(x => x.BoardId == boardId);
            if (oldData != null)
            {
                if ((oldData.BoardCode == model.BoardCode || oldData.Name == model.Name) && oldData.BoardId != model.BoardId)
                {
                    throw new InfinityInvalidDataException("Board data already exist !");
                }
            }

            Board board = ObjectConverter<BoardModel, Board>.Convert(model);
            if (boardId > 0)
            {
                board = await boardRepository.FindOneAsync(x => x.BoardId == boardId);
                if (board == null)
                {
                    throw new InfinityNotFoundException("Board not found!");
                }
                board.ModifiedBy = model.ModifiedBy;
                board.ModifiedDate = DateTime.Now;
            }
            else
            {
                board.CreatedBy = model.CreatedBy;
                board.CreatedDate = DateTime.Now;
            }
            board.BoardType = model.BoardType;
            board.BoardCode = model.BoardCode;
            board.Name = model.Name;
            board.Remarks = model.Remarks;
            board.IsActive = true;
            await boardRepository.SaveAsync(board);
            model.BoardId = board.BoardId;
            return model;

        }

        public async Task<bool> DeleteBoard(int boardId)
        {
            if (boardId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Board board = await boardRepository.FindOneAsync(x => x.BoardId == boardId);
            if (board == null)
            {
                throw new InfinityNotFoundException("Board not found");
            }
            else
            {
                return await boardRepository.DeleteAsync(board);
            }
        }

        public List<SelectModel> getBoardTypesSelectModel()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(BoardType)).Cast<BoardType>()
                     .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                     .ToList();
            return selectModels;
        }

        public async Task<List<SelectModel>> GetBoardsSelectModelByBoardType(int boardType)
        {
            ICollection<Board> boards = await boardRepository.FilterAsync(x => x.BoardType == boardType);
            return boards.OrderBy(x => x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.BoardId
            }).ToList();
        }

        public async Task<List<SelectModel>> BoardsSelectModel(int? examCategoryId)
        {
            if (examCategoryId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }

            ExamCategory examCategory = await examCategoryRepository.FindOneAsync(x => x.ExamCategoryId == examCategoryId);

            if (examCategory == null)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            IQueryable<Board> queryable = boardRepository.Where(x => x.IsActive && x.BoardType == examCategory.BoardType).OrderBy(x => x.Name);
            List<Board> boards = await queryable.ToListAsync();
            List<SelectModel> boardModels = boards.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.BoardId
            }).ToList();
            return boardModels;
        }
    }
}
