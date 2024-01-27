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
   public class PromotionBoardService: IPromotionBoardService
    {
        private readonly IBnoisRepository<PromotionBoard> promotionBoardRepository;
        private readonly IBnoisRepository<PromotionNomination> promotionNominationRepository;
        public PromotionBoardService(IBnoisRepository<PromotionBoard> promotionBoardRepository,
            IBnoisRepository<PromotionNomination> promotionNominationRepository)
        {
            this.promotionBoardRepository = promotionBoardRepository;
            this.promotionNominationRepository = promotionNominationRepository;
        }

      

        //public List<PromotionBoardModel> GetPromotionBoards(int ps, int pn, string qs, out int total, int type)
        //{
        //    IQueryable<PromotionBoard> promotionBoards = promotionBoardRepository.FilterWithInclude(x => x.IsActive && x.Type==type
        //        && ((x.BoardName.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.Rank.ShortName.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.Rank.FullName.Contains(qs) || String.IsNullOrEmpty(qs))), "Rank", "Rank1");
        //    total = promotionBoards.Count();
        //    promotionBoards = promotionBoards.OrderByDescending(x => x.PromotionBoardId).Skip((pn - 1) * ps).Take(ps);
        //    List<PromotionBoardModel> models = ObjectConverter<PromotionBoard, PromotionBoardModel>.ConvertList(promotionBoards.ToList()).ToList();
        //    return models;
        //}
        public async Task<PromotionBoardModel> GetPromotionBoard(int id)
        {
            if (id <= 0)
            {
                return new PromotionBoardModel();
            }
            PromotionBoard promotionBoard = await promotionBoardRepository.FindOneAsync(x => x.PromotionBoardId == id);
            if (promotionBoard == null)
            {
                throw new InfinityNotFoundException("PromotionBoard not found");
            }
            PromotionBoardModel model = ObjectConverter<PromotionBoard, PromotionBoardModel>.Convert(promotionBoard);
            return model;
        }

        public async Task<PromotionBoardModel> SavePromotionBoard(int id, PromotionBoardModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("PromotionBoard data missing");
            }

            bool isExistData = promotionBoardRepository.Exists(x => x.BoardName == model.BoardName && x.FromRankId == model.FromRankId && x.ToRankId == model.ToRankId && x.PromotionBoardId != model.PromotionBoardId);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            PromotionBoard promotionBoard = ObjectConverter<PromotionBoardModel, PromotionBoard>.Convert(model);
            if (id > 0)
            {
                promotionBoard = await promotionBoardRepository.FindOneAsync(x => x.PromotionBoardId == id);
                if (promotionBoard == null)
                {
                    throw new InfinityNotFoundException("PromotionBoard not found !");
                }
                promotionBoard.ModifiedDate = DateTime.Now;
                promotionBoard.ModifiedBy = userId;

            }
            else
            {
                promotionBoard.IsActive = true;
                promotionBoard.CreatedDate = DateTime.Now;
                promotionBoard.CreatedBy = userId;
            }

            promotionBoard.Type = model.Type;
            promotionBoard.BoardName = model.BoardName;
            promotionBoard.FormationDate =model.FormationDate ?? promotionBoard.FormationDate;
            promotionBoard.LtCdrLevel = model.LtCdrLevel;
            promotionBoard.FromRankId = model.FromRankId;
            promotionBoard.ToRankId = model.ToRankId;
            promotionBoard.EvotingDate = model.EvotingDate;
            await promotionBoardRepository.SaveAsync(promotionBoard);
            model.PromotionBoardId = promotionBoard.PromotionBoardId;
            return model;
        }

        public async Task<bool> DeletePromotionBoard(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PromotionBoard promotionBoard = await promotionBoardRepository.FindOneAsync(x => x.PromotionBoardId == id);
            if (promotionBoard == null)
            {
                throw new InfinityNotFoundException("PromotionBoard not found");
            }
            else
            {
                return await promotionBoardRepository.DeleteAsync(promotionBoard);
            }
        }

        public async Task<List<SelectModel>> GetPromotionBoardSelectModels()
        {
            ICollection<PromotionBoard> models = await promotionBoardRepository.FilterAsync(x => x.IsActive);
            return models.Select(x => new SelectModel()
            {
                Text = x.BoardName,
                Value = x.PromotionBoardId
            }).ToList();

        }

        public async Task<bool> CalculateTrace(int promotionBoardId)
        {
            int count = 0;
            IQueryable<PromotionNomination> promotionNominations = promotionNominationRepository.FilterWithInclude(x => x.PromotionBoardId == promotionBoardId);
            foreach (var promotionNomination in promotionNominations.ToList())
            {
                promotionNominationRepository.ExecWithSqlQuery(String.Format("exec spTraceCalculation {0},{1}", promotionNomination.EmployeeId, promotionNomination.PromotionBoardId));
                count++;
            }

            if (promotionNominations.Count().Equals(count))
            {
                return true;
            }
            return false; 
            
        }

        public List<SelectModel> GetDailyProcesPromotionBoardSelectModels()
        {
            List<PromotionBoard> models =  promotionBoardRepository.Where(x => x.IsActive).OrderByDescending(x=>x.FormationDate).ToList();
            return models.OrderBy(x => x.BoardName).Select(x => new SelectModel()
            {
                Text = x.BoardName+" (Formation date: "+x.FormationDate.ToString("dd-MMM-yyyy")+")",
                Value = x.PromotionBoardId
            }).ToList();
        }

        public List<PromotionBoardModel> GetPromotionBoards(int ps, int pn, string qs, out int total, int type)
        {
            throw new NotImplementedException();
        }
    }
}
