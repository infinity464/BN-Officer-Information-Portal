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
   public class TraceSettingService: ITraceSettingService
    {
        private readonly IBnoisRepository<TraceSetting> traceSettingRepository;
        public TraceSettingService(IBnoisRepository<TraceSetting> traceSettingRepository)
        {
            this.traceSettingRepository = traceSettingRepository;
        }
        

        public List<TraceSettingModel> GetTraceSettings(int ps, int pn, string qs, out int total)
        {
            IQueryable<TraceSetting> traceSettings = traceSettingRepository.FilterWithInclude(x => x.IsActive);
            total = traceSettings.Count();
            traceSettings = traceSettings.OrderByDescending(x => x.TraceSettingId).Skip((pn - 1) * ps).Take(ps);
            List<TraceSettingModel> models = ObjectConverter<TraceSetting, TraceSettingModel>.ConvertList(traceSettings.ToList()).ToList();
            return models;
        }

        public async Task<TraceSettingModel> GetTraceSetting(int id)
        {
            if (id <= 0)
            {
                return new TraceSettingModel();
            }
            TraceSetting traceSetting = await traceSettingRepository.FindOneAsync(x => x.TraceSettingId == id);
            if (traceSetting == null)
            {
                throw new InfinityNotFoundException("Trace Setting not found");
            }
            TraceSettingModel model = ObjectConverter<TraceSetting, TraceSettingModel>.Convert(traceSetting);
            return model;
        }

        public async Task<TraceSettingModel> SaveTraceSetting(int id, TraceSettingModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Trace Setting data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            TraceSetting traceSetting = ObjectConverter<TraceSettingModel, TraceSetting>.Convert(model);
            if (id > 0)
            {
                traceSetting = await traceSettingRepository.FindOneAsync(x => x.TraceSettingId == id);
                if (traceSetting == null)
                {
                    throw new InfinityNotFoundException("Trace Setting not found !");
                }

                traceSetting.ModifiedDate = DateTime.Now;
                traceSetting.ModifiedBy = userId;
            }
            else
            {
                traceSetting.IsActive = true;
                traceSetting.CreatedDate = DateTime.Now;
                traceSetting.CreatedBy = userId;
            }
            traceSetting.Active = model.Active;
            traceSetting.CreationDate = model.CreationDate;
            traceSetting.OPR = model.OPR;
            traceSetting.Course = model.Course;
            traceSetting.PFT = model.PFT;
            traceSetting.TotalPoint = model.TotalPoint;
            traceSetting.WeightPreRank = model.WeightPreRank;
            traceSetting.WeightPrevRank = model.WeightPrevRank;
            traceSetting.OprCount = model.OprCount;
            traceSetting.OprLastYear = model.OprLastYear;
            traceSetting.DivisionalFactor = model.DivisionalFactor;
            traceSetting.PftCountYear = model.PftCountYear;
            traceSetting.DductPtPerPft = model.DductPtPerPft;
            traceSetting.OWPenalCLOpr = model.OWPenalCLOpr;
            traceSetting.DductPtPerOWKG = model.DductPtPerOWKG;
       
            await traceSettingRepository.SaveAsync(traceSetting);
            model.TraceSettingId = traceSetting.TraceSettingId;
            return model;
        }

        public async Task<bool> DeleteTraceSetting(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            TraceSetting traceSetting = await traceSettingRepository.FindOneAsync(x => x.TraceSettingId == id);
            if (traceSetting == null)
            {
                throw new InfinityNotFoundException("Trace Setting not found");
            }
            else
            {
                return await traceSettingRepository.DeleteAsync(traceSetting);
            }
        }
    }
}
