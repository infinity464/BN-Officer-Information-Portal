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
    public class MissionAppointmentService : IMissionAppointmentService
    {
        private readonly IBnoisRepository<MissionAppointment> missionAppointmentRepository;
        private readonly IBnoisRepository<MissionAppRank> missionAppRankRepository;
        private readonly IBnoisRepository<MissionAppBranch> missionAppBranchRepository;
        private readonly IBnoisRepository<NominationSchedule> nominationScheduleRepository;
        public MissionAppointmentService(IBnoisRepository<MissionAppRank> missionAppRankRepository, IBnoisRepository<MissionAppBranch> missionAppBranchRepository,
            IBnoisRepository<MissionAppointment> missionAppointmentRepository, IBnoisRepository<NominationSchedule> nominationScheduleRepository)
        {
            this.missionAppointmentRepository = missionAppointmentRepository;
            this.missionAppRankRepository = missionAppRankRepository;
            this.missionAppBranchRepository = missionAppBranchRepository;
            this.nominationScheduleRepository = nominationScheduleRepository;
        }

        //public List<MissionAppointmentModel> GetMissionAppointments(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<MissionAppointment> missionAppointments = missionAppointmentRepository.FilterWithInclude(x => x.IsActive
        //        && (  x.AptNat.ANatNm.Contains(qs) || x.Name.Contains(qs) || x.NominationSchedule.TitleName.Contains(qs) ||
        //        x.AptCat.ACatShNm.Contains(qs) || String.IsNullOrEmpty(qs)), "NominationSchedule", "AptNat", "AptCat", "NominationSchedule.Country");
        //    total = missionAppointments.Count();
        //    missionAppointments = missionAppointments.OrderByDescending(x => x.MissionAppointmentId).Skip((pn - 1) * ps).Take(ps);
        //    List<MissionAppointmentModel> models = ObjectConverter<MissionAppointment, MissionAppointmentModel>.ConvertList(missionAppointments.ToList()).ToList();
        //    return models;
        //}


        public async Task<List<SelectModel>> GetMissionAppointmentsByMissionId(int missionId)
        {
            ICollection<MissionAppointment> missionAppointments = await missionAppointmentRepository.FilterAsync(x => x.IsActive && x.MissionScheduleId == missionId);
            List<SelectModel> selectModels = missionAppointments.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.MissionAppointmentId
            }).ToList();
            return selectModels;
        }



        public async Task<MissionAppointmentModel> GetMissionAppointment(int id)
        {
            if (id <= 0)
            {
                return new MissionAppointmentModel();
            }
            MissionAppointment missionAppointment = await missionAppointmentRepository.FindOneAsync(x => x.MissionAppointmentId == id);
            if (missionAppointment == null)
            {
                throw new InfinityNotFoundException("Mission Appointment not found");
            }
            int[] rankIds = missionAppRankRepository.Where(x => x.MissionAppointmentId == id).Select(x => x.RankId).ToArray();
            int[] branchIds = missionAppBranchRepository.Where(x => x.MissionAppointmentId == id).Select(x => x.BranchId).ToArray();
            MissionAppointmentModel model = ObjectConverter<MissionAppointment, MissionAppointmentModel>.Convert(missionAppointment);
            model.RankIds = rankIds;
            model.BranchIds = branchIds;
            return model;
        }

        //public async Task<MissionAppointmentModel> SaveMissionAppointment(int id, MissionAppointmentModel model)
        //{
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Mission Appointment  data missing");
        //    }


        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    MissionAppointment missionAppointment = ObjectConverter<MissionAppointmentModel, MissionAppointment>.Convert(model);
        //    if (id > 0)
        //    {
        //        missionAppointment = await missionAppointmentRepository.FindOneAsync(x => x.MissionAppointmentId == id);
        //        if (missionAppointment == null)
        //        {
        //            throw new InfinityNotFoundException("Mission Appointment not found !");
        //        }
        //        ICollection<MissionAppRank> trainingRanks = await missionAppRankRepository.FilterAsync(x => x.MissionAppointmentId == id);
        //        missionAppRankRepository.RemoveRange(trainingRanks);

        //        ICollection<MissionAppBranch> trainingBranches = await missionAppBranchRepository.FilterAsync(x => x.MissionAppointmentId == id);
        //        missionAppBranchRepository.RemoveRange(trainingBranches);

        //        missionAppointment.ModifiedDate = DateTime.Now;
        //        missionAppointment.ModifiedBy = userId;
        //    }
        //    else
        //    {
        //        missionAppointment.IsActive = true;
        //        missionAppointment.CreatedDate = DateTime.Now;
        //        missionAppointment.CreatedBy = userId;

        //    }
        //    if (model.RankIds.Any())
        //    {
        //        missionAppointment.MissionAppRanks = model.RankIds.Select(x => new MissionAppRank() { RankId = x }).ToList();
        //    }
        //    if (model.BranchIds.Any())
        //    {
        //        missionAppointment.MissionAppBranches = model.BranchIds.Select(x => new MissionAppBranch() { BranchId = x }).ToList();
        //    }
        //    missionAppointment.MissionScheduleId = model.MissionScheduleId;
        //    missionAppointment.AppointmentCategoryId = model.AppointmentCategoryId;
        //    missionAppointment.AppointmentNatureId = model.AppointmentNatureId;
        //    missionAppointment.Name = model.Name;

        //    await missionAppointmentRepository.SaveAsync(missionAppointment);
        //    model.MissionAppointmentId = missionAppointment.MissionAppointmentId;
        //    return model;
        //}


        public async Task<bool> DeleteMissionAppointment(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            MissionAppointment missionAppointment = await missionAppointmentRepository.FindOneAsync(x => x.MissionAppointmentId == id);
            if (missionAppointment == null)
            {
                throw new InfinityNotFoundException("Mission Appointment not found");
            }
            else
            {
                missionAppRankRepository.Delete(x => x.MissionAppointmentId == missionAppointment.MissionAppointmentId);
                missionAppBranchRepository.Delete(x => x.MissionAppointmentId == missionAppointment.MissionAppointmentId);

                return await missionAppointmentRepository.DeleteAsync(missionAppointment);
            }
        }





        public string GetMissionSchedule(int missionId)
        {

            NominationSchedule mission = nominationScheduleRepository.FindOne(x => x.NominationScheduleId == missionId && x.NominationScheduleType==1 );
            if (mission == null)
            {
                throw new InfinityNotFoundException("Mission not found");
            }
            return String.Format("{0} {1}|{2}", mission.TitleName,  String.Format("{0:dd-MM-yyyy}", mission.FromDate), String.Format("{0:dd-MM-yyyy}", mission.ToDate));
        }

        public List<MissionAppointmentModel> GetMissionAppointments(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        public Task<MissionAppointmentModel> SaveMissionAppointment(int v, MissionAppointmentModel model)
        {
            throw new NotImplementedException();
        }
    }




}
