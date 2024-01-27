using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPromotionBoardService
    {
        List<PromotionBoardModel> GetPromotionBoards(int ps, int pn, string qs, out int total, int type);
        Task<PromotionBoardModel> GetPromotionBoard(int id);
        Task<PromotionBoardModel> SavePromotionBoard(int v, PromotionBoardModel model);
        Task<bool> DeletePromotionBoard(int id);
        Task<List<SelectModel>> GetPromotionBoardSelectModels();
        Task<bool> CalculateTrace(int promotionBoardId);
        List<SelectModel> GetDailyProcesPromotionBoardSelectModels();
    }
}
