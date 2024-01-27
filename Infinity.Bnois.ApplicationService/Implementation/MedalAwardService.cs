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
    public class MedalAwardService : IMedalAwardService
    {
        private readonly IBnoisRepository<MedalAward> medalAwardRepository;
	    private readonly IProcessRepository processRepository;
		public MedalAwardService(IBnoisRepository<MedalAward> medalAwardRepository, IProcessRepository processRepository)
        {
            this.medalAwardRepository = medalAwardRepository;
            this.processRepository = processRepository;
        }

        //public List<MedalAwardModel> GetMedalAwards(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<MedalAward> medalAwards = medalAwardRepository.FilterWithInclude(x => x.IsActive
        //        && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) ||x.Medal.NameEng == (qs) || x.Award.NameEng.Contains(qs) || x.Publication.Name == (qs)  || String.IsNullOrEmpty(qs)), "Employee", "Award", "Medal","Publication","PublicationCategory");
        //    total = medalAwards.Count();
        //    medalAwards = medalAwards.OrderByDescending(x => x.MedalAwardId).Skip((pn - 1) * ps).Take(ps);
        //    List<MedalAwardModel> models = ObjectConverter<MedalAward, MedalAwardModel>.ConvertList(medalAwards.ToList()).ToList();

        //    models = models.Select(x =>
        //    {
        //        x.TypeName = Enum.GetName(typeof(MedalAwardType), x.Type);
        //        return x;
        //    }).ToList();
        //    return models;
        //}

        public async Task<MedalAwardModel> GetMedalAward(int id)
        {
            if (id <= 0)
            {
                return new MedalAwardModel();
            }
            MedalAward medalAward = await medalAwardRepository.FindOneAsync(x => x.MedalAwardId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (medalAward == null)
            {
                throw new InfinityNotFoundException("Medal, Award & Publication not found");
            }
            MedalAwardModel model = ObjectConverter<MedalAward, MedalAwardModel>.Convert(medalAward);
            return model;
        }

    
        public async Task<MedalAwardModel> SaveMedalAward(int id, MedalAwardModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Medal, Award & Publication  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            MedalAward medalAward = ObjectConverter<MedalAwardModel, MedalAward>.Convert(model);
            if (id > 0)
            {
                medalAward = await medalAwardRepository.FindOneAsync(x => x.MedalAwardId == id);
                if (medalAward == null)
                {
                    throw new InfinityNotFoundException("Medal, Award & Publication not found !");
                }

                medalAward.ModifiedDate = DateTime.Now;
                medalAward.ModifiedBy = userId;
            }
            else
            {
                medalAward.IsActive = true;
                medalAward.CreatedDate = DateTime.Now;
                medalAward.CreatedBy = userId;
            }
            medalAward.EmployeeId = model.EmployeeId;
            medalAward.AwardId = model.AwardId;
            medalAward.MedalId = model.MedalId;
            medalAward.PublicationId = model.PublicationId;
            medalAward.PublicationCategoryId = model.PublicationCategoryId;
            medalAward.Type = model.Type;
            medalAward.Date =model.Date ?? medalAward.Date;
            medalAward.Remarks = model.Remarks;
            //medalAward.Employee = null;
            medalAward.IsBackLog = model.IsBackLog;
            medalAward.FileName = model.FileName;

            medalAward.RankId = model.Employee.RankId; ;
            medalAward.TransferId = model.Employee.TransferId;
            if (model.IsBackLog)
            {
                medalAward.RankId = model.RankId;
                medalAward.TransferId = model.TransferId;
                await medalAwardRepository.SaveAsync(medalAward);
                model.MedalAwardId = medalAward.MedalAwardId;
                return model;
            }

            await medalAwardRepository.SaveAsync(medalAward);
            model.MedalAwardId = medalAward.MedalAwardId;

	        await processRepository.UpdateNamingConvention(medalAward.EmployeeId);
			return model;
        }


        public async Task<bool> DeleteMedalAward(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            MedalAward medalAward = await medalAwardRepository.FindOneAsync(x => x.MedalAwardId == id);
            if (medalAward == null)
            {
                throw new InfinityNotFoundException("Medal, Award & Publication not found");
            }
            else
            {
	           var result= await medalAwardRepository.DeleteAsync(medalAward);
				await processRepository.UpdateNamingConvention(medalAward.EmployeeId);
	            return result;

            }
        }

        public List<SelectModel> GetMedalAwardTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(MedalAwardType)).Cast<MedalAwardType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public List<MedalAwardModel> GetMedalAwards(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
