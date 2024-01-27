using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPromotionNominationService
    {
        List<PromotionNominationModel> GetPromotionNominations(int promotionBoardId, int ps, int pn, string qs, out int total,int type);
        Task<PromotionNominationModel> GetPromotionNomination(int employeePromotionId);
        Task<PromotionNominationModel> SavePromotionNomination(int v, PromotionNominationModel model);
        Task<bool> DeletePromotionNomination(int id);
        List<PromotionNominationModel> GetPromotionNominations(int promotionBoardId);
        Task<List<PromotionNominationModel>> UpdatePromotionNominations(int promotionBoardId, List<PromotionNominationModel> models);
        List<PromotionNominationModel> GetPromotionExecutionWithoutBoards(int ps, int pn, string qs, out int total);
        Task<bool> ExecutePromotion(int promotinExecutionDate);
        Task<bool> ExecutePromotionWithOutBoard();
        Task<bool> ExecuteDatabaseBackup();
    }
}
