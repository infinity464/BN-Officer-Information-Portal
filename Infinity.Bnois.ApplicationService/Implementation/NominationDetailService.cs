using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
	public class NominationDetailService : INominationDetailService
	{
		private readonly IBnoisRepository<NominationDetail> nominationDetailRepository;
		private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
		private readonly IBnoisRepository<Nomination> nominationRepository;
		private readonly IBnoisRepository<TrainingBranch> trainingBranchRepository;
		private readonly IBnoisRepository<TrainingRank> trainingRankRepository;
		private readonly IBnoisRepository<MissionAppRank> missionAppRankRepository;
		private readonly IBnoisRepository<MissionAppBranch> missionAppBranchRepository;
	    private readonly IBnoisRepository<TrainingPlan> trainingPlanRepository;
	    private readonly IBnoisRepository<NominationSchedule> nominationScheduleRepository;
        public NominationDetailService(IBnoisRepository<NominationDetail> nominationDetailRepository, IBnoisRepository<TrainingBranch> trainingBranchRepository,
		    IBnoisRepository<TrainingRank> trainingRankRepository, IBnoisRepository<EmployeeGeneral> employeeGeneralRepository, IBnoisRepository<Nomination> nominationRepository,
		    IBnoisRepository<MissionAppRank> missionAppRankRepository, IBnoisRepository<MissionAppBranch> missionAppBranchRepository, 
            IBnoisRepository<TrainingPlan> trainingPlanRepository, IBnoisRepository<NominationSchedule> nominationScheduleRepository)
		{
			this.nominationDetailRepository = nominationDetailRepository;
			this.trainingBranchRepository = trainingBranchRepository;
			this.trainingRankRepository = trainingRankRepository;
			this.nominationRepository = nominationRepository;
			this.employeeGeneralRepository = employeeGeneralRepository;
			this.missionAppRankRepository = missionAppRankRepository;
			this.missionAppBranchRepository = missionAppBranchRepository;
			this.trainingPlanRepository = trainingPlanRepository;
			this.nominationScheduleRepository = nominationScheduleRepository;
		}
        public async Task<NominationDetailModel> GetNominationDetail(int id)
        {
            if (id <= 0)
            {
                return new NominationDetailModel();
            }
            NominationDetail nominationDetail = await nominationDetailRepository.FindOneAsync(x => x.NominationDetailId == id, new List<string> { "Employee","Employee.Rank", "Employee.Batch" });
            if (nominationDetail == null)
            {
                throw new InfinityNotFoundException("Nomination Detail not found");
            }
            NominationDetailModel model = ObjectConverter<NominationDetail, NominationDetailModel>.Convert(nominationDetail);
            return model;
        }



	    //public async Task<List<SelectModel>> GetNominatedList(int nominationId)
	    //{
	    //    ICollection<NominationDetail> models = await nominationDetailRepository.FilterWithInclude(x => x.IsActive && x.IsApporved==true && x.NominationId == nominationId, "Employee").ToListAsync();
	    //    return models.Select(x => new SelectModel()
	    //    {
	    //        Text = x.Employee.PNo,
	    //        Value = x.Employee.PNo
	    //    }).ToList();

	    //}

        public List<NominationDetailModel> GetNominationDetails(int id)
        {
            List<NominationDetail> nominationDetails = nominationDetailRepository.FilterWithInclude(x => x.NominationId == id && x.IsActive, "Nomination","Employee", "Employee.Rank").ToList();
            List<NominationDetailModel> models = ObjectConverter<NominationDetail, NominationDetailModel>.ConvertList(nominationDetails.ToList()).ToList();
            return models;

        }


        public async Task<NominationDetailModel> SaveNominationDetail(int id,int type, NominationDetailModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Nomination Detail data missing");
            }
            bool isExist = nominationDetailRepository.Exists(x => x.EmployeeId == model.EmployeeId && x.NominationId==model.NominationId && x.NominationDetailId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Officer already nominated.");
            }

            EmployeeGeneral employeeGeneral =
                await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
            Nomination nomination =
                await nominationRepository.FindOneAsync(x => x.NominationId == model.NominationId);


            int nominationDetailCount =
                nominationDetailRepository.Count(x => x.NominationId == model.NominationId);


            //Course  1
            if (type == 1)
            {
                

                TrainingPlan trainingPlan = await trainingPlanRepository.FindOneAsync(x =>
                    x.TrainingPlanId == nomination.EntityId && nomination.EnitityType == 1);

                if (trainingPlan.MaxParticipant <= nominationDetailCount)
                {
                    throw new InfinityNotFoundException("Post limit exceeded.!! Max Limit: "+ trainingPlan.MaxParticipant);
                }

                if (nomination.EnitityType == type)
                {

                    bool trainingRank =
                        trainingRankRepository.Exists(x => x.TrainingPlanId == nomination.EntityId && x.RankId==model.Employee.RankId);


                    bool trainingBranch =
                        trainingBranchRepository.Exists(x => x.TrainingPlanId == nomination.EntityId && x.BranchId == employeeGeneral.BranchId);

                    if (!trainingBranch && !trainingRank)
                    {
                      throw  new InfinityNotFoundException("Officer Not Allowed");
                    }
                    else if (trainingBranch && !trainingRank)
                    {
                        throw new InfinityNotFoundException("Officer Not Allowed");
                    }
                    else if (trainingRank && !trainingBranch)
                    {
                        throw new InfinityNotFoundException("Officer Not Allowed");
                    }
                }
            }
            //Mission   2
            if (type == 2)
            {

               

                NominationSchedule nominationSchedule = await nominationScheduleRepository.FindOneAsync(x =>
                    x.NominationScheduleId == nomination.EntityId && nomination.EnitityType == 2);

                if (nominationSchedule.AssignPost <= nominationDetailCount)
                {
                    throw new InfinityNotFoundException("Post limit Exceeded.!! Max Limit: " + nominationSchedule.AssignPost);
                }


                if (nomination.EnitityType == type && nomination.WithoutAppointment==false)
                {

                    bool missionAppRank =
                        missionAppRankRepository.Exists(x => x.MissionAppointmentId == nomination.MissoinAppointmentId && x.RankId == model.Employee.RankId);


                    bool missionAppBranch =
                        missionAppBranchRepository.Exists(x => x.MissionAppointmentId == nomination.MissoinAppointmentId && x.BranchId == employeeGeneral.BranchId);

                    if (!missionAppRank && !missionAppBranch)
                    {
                        throw new InfinityNotFoundException("Officer Not Allowed");
                    }
                    else if (missionAppBranch && !missionAppRank)
                    {
                        throw new InfinityNotFoundException("Officer Not Allowed");
                    }
                    else if (missionAppRank && !missionAppBranch)
                    {
                        throw new InfinityNotFoundException("Officer Not Allowed");
                    }
                   
                }
            }
          




            NominationDetail nominationDetail = ObjectConverter<NominationDetailModel, NominationDetail>.Convert(model);
          
            if (id > 0)
            {
                nominationDetail = await nominationDetailRepository.FindOneAsync(x => x.NominationDetailId == id);
                if (nominationDetail == null)
                {
                    throw new InfinityNotFoundException("Nomination Detail not found !");
                }
                nominationDetail.IsApporved = model.IsApporved;
            }
            else
            {
                nominationDetail.IsActive = true;
                nominationDetail.IsApporved = true;
            }

            nominationDetail.EmployeeId = model.EmployeeId;
            nominationDetail.NominationId = model.NominationId;
            
            nominationDetail.Remarks = model.Remarks;

            nominationDetail.RankId = model.Employee.RankId;
            nominationDetail.TransferId = model.Employee.TransferId;

            //nominationDetail.Employee = null;
            await nominationDetailRepository.SaveAsync(nominationDetail);
            model.NominationDetailId = nominationDetail.NominationDetailId;
            return model;
        }


	    public async Task<List<NominationDetailModel>> UpdateNominationDetails(int id, List<NominationDetailModel> models)
	    {
	        if (!models.Any())
	        {
	            throw new InfinityArgumentMissingException("Promotion Execution List not found !");
	        }

	        string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();

	        List<NominationDetail> nominationDetails = nominationDetailRepository.Where(x => x.NominationId == id).ToList();
	        for (int i = 0; i < nominationDetails.Count; i++)
	        {
	            nominationDetails[i].IsApporved = models[i].IsApporved;
	            nominationDetails[i].Remarks = models[i].Remarks;

	        }
	        nominationDetailRepository.UpdateAll(nominationDetails);
	        models = ObjectConverter<NominationDetail, NominationDetailModel>.ConvertList(nominationDetails).ToList();
	        return models;
	    }


        public async Task<bool> DeleteNominationDetail(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            NominationDetail nominationDetail = await nominationDetailRepository.FindOneAsync(x => x.NominationDetailId == id);
            if (nominationDetail == null)
            {
                throw new InfinityNotFoundException("Nomination Detail not found");
            }
            else
            {
                return await nominationDetailRepository.DeleteAsync(nominationDetail);
            }
        }

        public Task<List<SelectModel>> GetNominatedList(int nominationId)
        {
            throw new NotImplementedException();
        }
    }
}
