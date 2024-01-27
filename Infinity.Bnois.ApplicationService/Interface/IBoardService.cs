using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;


namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IBoardService
    {
        List<BoardModel> GetBoards(int pageSize, int pageNumber,string queryText, out int total);
        Task<BoardModel> SaveBoard(int boardId, BoardModel model);
        Task<BoardModel> GetBoard(int boardId);
        Task<bool> DeleteBoard(int boardId);
        List<SelectModel> getBoardTypesSelectModel();
        Task<List<SelectModel>> GetBoardsSelectModelByBoardType(int boardType);
        Task<List<SelectModel>> BoardsSelectModel(int? examCategoryId);
    }
}
