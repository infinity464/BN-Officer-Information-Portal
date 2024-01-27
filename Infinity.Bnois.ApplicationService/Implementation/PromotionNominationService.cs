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
    public class PromotionNominationService : IPromotionNominationService
    {
        private readonly IProcessRepository processRepository;
        private readonly IBnoisRepository<PromotionNomination> promotionNominationRepository;
        private readonly IBnoisRepository<PromotionBoard> promotionBoardRepository;
        private readonly IBnoisRepository<Employee> employeeRepository;

        public PromotionNominationService(IProcessRepository processRepository, IBnoisRepository<PromotionNomination> promotionNominationRepository, IBnoisRepository<PromotionBoard> promotionBoardRepository, IBnoisRepository<Employee> employeeRepository)
        {
            this.promotionNominationRepository = promotionNominationRepository;
            this.promotionBoardRepository = promotionBoardRepository;
            this.employeeRepository = employeeRepository;
            this.processRepository = processRepository;
        }


        public async Task<PromotionNominationModel> GetPromotionNomination(int promotionNominationId)
        {
            if (promotionNominationId <= 0)
            {
                return new PromotionNominationModel();
            }
            PromotionNomination promotionNomination = await promotionNominationRepository.FindOneAsync(x => x.PromotionNominationId == promotionNominationId, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (promotionNomination == null)
            {
                throw new InfinityNotFoundException("Promotion Nomination not found");
            }
            PromotionNominationModel model = ObjectConverter<PromotionNomination, PromotionNominationModel>.Convert(promotionNomination);
            return model;
        }

        //public List<PromotionNominationModel> GetPromotionNominations(int promotionBoardId, int ps, int pn, string qs, out int total,int type)
        //{
        //    //IQueryable<PromotionNomination> promotionNominations = promotionNominationRepository.FilterWithInclude(x => x.IsActive && x.PromotionBoardId == promotionBoardId || String.IsNullOrEmpty(qs)), "Employee.Rank", "ExecutionRemark", "PromotionBoard.Rank");
        //    IQueryable<PromotionNomination> promotionNominations = new IQueryable<PromotionNomination>();
        //    total = promotionNominations.Count();
        //    promotionNominations = promotionNominations.OrderByDescending(x => x.PromotionNominationId).Skip((pn - 1) * ps).Take(ps);
        //    List<PromotionNominationModel> models = ObjectConverter<PromotionNomination, PromotionNominationModel>.ConvertList(promotionNominations.ToList()).ToList();
        //    return models;
        //}

        public async Task<PromotionNominationModel> SavePromotionNomination(int promotionNominationId, PromotionNominationModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Promotion Nomination data missing !");
            }

            bool isEmployeeExist = promotionNominationRepository.Exists(x => x.EmployeeId == model.Employee.EmployeeId && x.PromotionBoardId==model.PromotionBoardId && x.FromRankId == model.FromRankId && x.ToRankId == model.ToRankId && x.PromotionNominationId != model.PromotionNominationId);

            if (isEmployeeExist)
            {
                throw new InfinityArgumentMissingException("Officer already added !");
            }
            if (model.PromotionBoardId > 0)
            {
                PromotionBoard promotionBoard = await promotionBoardRepository.FindOneAsync(x => x.PromotionBoardId == model.PromotionBoardId,new List<string> { "Rank","Rank1"});
                if (promotionBoard == null)
                {
                    throw new InfinityArgumentMissingException("Promotion Board not found !");
                }

                Employee employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == model.Employee.EmployeeId,new List<string>{ "Rank" });
                if (employee == null)
                {
                    throw new InfinityNotFoundException("Officer not found !");
                }

                //if (model.Type == 1)
                //{
                //    if (promotionBoard.Rank.RankLevel != employee.Rank.RankLevel)
                //    {
                //        throw new InfinityArgumentMissingException("Officer is not eligible for this board !");
                //    }

                //}

                bool isExist = promotionNominationRepository.Exists(x => x.EmployeeId == model.Employee.EmployeeId && x.PromotionBoardId == model.PromotionBoardId && x.PromotionNominationId != model.PromotionNominationId);
                if (isExist)
                {
                    throw new InfinityArgumentMissingException("Promotion Nomination already exists !");
                }

                model.FromRankId = promotionBoard.FromRankId;
                model.ToRankId = promotionBoard.ToRankId;
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            PromotionNomination promotionNomination = ObjectConverter<PromotionNominationModel, PromotionNomination>.Convert(model);
            if (promotionNominationId > 0)
            {
                promotionNomination = await promotionNominationRepository.FindOneAsync(x => x.PromotionNominationId == promotionNominationId);
                if (promotionNomination == null)
                {
                    throw new InfinityNotFoundException("Promotion Nomination not found !");
                }
                promotionNomination.ModifiedDate = DateTime.Now;
                promotionNomination.ModifiedBy = userId;

            }
            else
            {
                promotionNomination.IsActive = true;
                promotionNomination.CreatedDate = DateTime.Now;
                promotionNomination.CreatedBy = userId;
            }
            promotionNomination.EmployeeId = model.Employee.EmployeeId;
            promotionNomination.PromotionBoardId = model.PromotionBoardId;
            promotionNomination.Type = model.Type;
            promotionNomination.FromRankId = model.FromRankId;
            promotionNomination.ToRankId = model.ToRankId;
            promotionNomination.Remarks = model.Remarks;
            promotionNomination.ExecutionDate = model.ExecutionDate;
            promotionNomination.EffectiveDate = model.EffectiveDate ?? promotionNomination.EffectiveDate;
            promotionNomination.ExType = model.ExType;
            promotionNomination.ExecutionRemarkId = model.ExecutionRemarkId;

            promotionNomination.TransferId = model.Employee.TransferId;
            if (model.IsBackLog)
            {
                promotionNomination.TransferId = model.TransferId;
            }
           

            //promotionNomination.Employee = null;
            await promotionNominationRepository.SaveAsync(promotionNomination);
            model.PromotionNominationId = promotionNomination.PromotionNominationId;

	        await processRepository.UpdateNamingConvention(promotionNomination.EmployeeId);


			return model;
        }

        public async Task<bool> DeletePromotionNomination(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PromotionNomination promotionNomination = await promotionNominationRepository.FindOneAsync(x => x.PromotionNominationId == id);
            if (promotionNomination == null)
            {
                throw new InfinityNotFoundException("Promotion Nomination not found");
            }
            else
            {
                return await promotionNominationRepository.DeleteAsync(promotionNomination);
            }
        }

        public List<PromotionNominationModel> GetPromotionNominations(int promotionBoardId)
        {
            IQueryable<PromotionNomination> promotionNominations = promotionNominationRepository.FilterWithInclude(x => x.IsActive && x.PromotionBoardId == promotionBoardId, "Employee.Rank", "ExecutionRemark", "PromotionBoard.Rank");
            List<PromotionNominationModel> models = ObjectConverter<PromotionNomination, PromotionNominationModel>.ConvertList(promotionNominations.ToList()).ToList();
            return models;
        }

        public async Task<List<PromotionNominationModel>> UpdatePromotionNominations(int promotionBoardId, List<PromotionNominationModel> models)
        {
            if (!models.Any())
            {
                throw new InfinityArgumentMissingException("Promotion Execution List not found !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();

            List<PromotionNomination> promotionNominations = promotionNominationRepository.Where(x => x.PromotionBoardId == promotionBoardId).ToList();
            for (int i = 0; i < promotionNominations.Count; i++)
            {
                promotionNominations[i].ExecutionRemarkId = models[i].ExecutionRemarkId;
                promotionNominations[i].EffectiveDate = models[i].EffectiveDate ?? promotionNominations[i].EffectiveDate;
                promotionNominations[i].Remarks = models[i].Remarks;
                promotionNominations[i].ModifiedBy = userId;
                promotionNominations[i].ModifiedDate = DateTime.Now;
            }
            promotionNominationRepository.UpdateAll(promotionNominations);
            models = ObjectConverter<PromotionNomination, PromotionNominationModel>.ConvertList(promotionNominations).ToList();
            return models;
        }

        //public List<PromotionNominationModel> GetPromotionExecutionWithoutBoards(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<PromotionNomination> promotionNominations = promotionNominationRepository.FilterWithInclude(x => x.IsActive  && x.PromotionBoardId == null && (x.Employee.PNo == (qs) || String.IsNullOrEmpty(qs)), "Employee.Rank", "ExecutionRemark", "Rank", "Rank1");
        //    total = promotionNominations.Count();
        //    promotionNominations = promotionNominations.OrderByDescending(x => x.PromotionNominationId).Skip((pn - 1) * ps).Take(ps);
        //    List<PromotionNominationModel> models = ObjectConverter<PromotionNomination, PromotionNominationModel>.ConvertList(promotionNominations.ToList()).ToList();
        //    return models;
        //}

        public async Task<bool> ExecutePromotion(int promotionBoardId)
        {
            promotionNominationRepository.ExecWithSqlQuery(String.Format("exec [SpUpdateEmployeeCurrentStatus] {0}", promotionBoardId));
            return true;
        }

        public async Task<bool> ExecutePromotionWithOutBoard()
        {
            promotionNominationRepository.ExecWithSqlQuery(String.Format("exec [spPromotionExecutionWithoutBoard]"));
            return true;
        }

        public async Task<bool> ExecuteDatabaseBackup()
        {
            promotionNominationRepository.ExecWithSqlQuery(String.Format("exec [DataBackup]"));
            return true;
        }

        public List<PromotionNominationModel> GetPromotionNominations(int promotionBoardId, int ps, int pn, string qs, out int total, int type)
        {
            throw new NotImplementedException();
        }

        public List<PromotionNominationModel> GetPromotionExecutionWithoutBoards(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
