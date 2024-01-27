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
    public class PoorCourseResultService: IPoorCourseResultService
    {
        private readonly IBnoisRepository<PoorCourseResult> poorCourseRepository;
        public PoorCourseResultService(IBnoisRepository<PoorCourseResult> poorCourseRepository)
        {
            this.poorCourseRepository = poorCourseRepository;
        }

        public async Task<PoorCourseResultModel> GetPoorCourseResult(int poorCourseResultId)
        {
            if (poorCourseResultId <= 0)
            {
                return new PoorCourseResultModel();
            }
            PoorCourseResult poorCourseResult = await poorCourseRepository.FindOneAsync(x => x.PoorCourseResultId == poorCourseResultId, new List<string> { "ResultType" });

            if (poorCourseResult == null)
            {
                throw new InfinityNotFoundException("Poor Course Result not found!");
            }
            PoorCourseResultModel model = ObjectConverter<PoorCourseResult, PoorCourseResultModel>.Convert(poorCourseResult);
            return model;
        }

        public List<PoorCourseResultModel> GetPoorCourseResults(int traceSettingId)
        {
            List<PoorCourseResult> poorCourseResults = poorCourseRepository.FilterWithInclude(x => x.TraceSettingId == traceSettingId, "ResultType").ToList();
            List<PoorCourseResultModel> models = ObjectConverter<PoorCourseResult, PoorCourseResultModel>.ConvertList(poorCourseResults.ToList()).ToList();
            return models;
        }

        public async Task<PoorCourseResultModel> SavePoorCourseResult(int poorCourseResultId, PoorCourseResultModel model)
        {
            bool isExist = await poorCourseRepository.ExistsAsync(x =>x.TraceSettingId==model.TraceSettingId && x.CountryType == model.CountryType && x.PoorCourseResultId != poorCourseResultId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Poor Course Result data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Poor Course Result data missing");
            }
            PoorCourseResult poorCourseResult = ObjectConverter<PoorCourseResultModel, PoorCourseResult>.Convert(model);

            if (poorCourseResultId > 0)
            {
                poorCourseResult = await poorCourseRepository.FindOneAsync(x => x.PoorCourseResultId == poorCourseResultId);
                if (poorCourseResult == null)
                {
                    throw new InfinityNotFoundException("Course Point not found!");
                }
                poorCourseResult.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                poorCourseResult.ModifiedDate = DateTime.Now;
            }
            else
            {
                poorCourseResult.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                poorCourseResult.CreatedDate = DateTime.Now;
            }
            poorCourseResult.TraceSettingId = model.TraceSettingId;
            poorCourseResult.ResultTypeId = model.ResultTypeId;
            poorCourseResult.DeductPoint = model.DeductPoint;
            poorCourseResult.PoorCourseReport = model.PoorCourseReport;
            poorCourseResult.CountryType = model.CountryType;

            await poorCourseRepository.SaveAsync(poorCourseResult);
            model.PoorCourseResultId = poorCourseResult.PoorCourseResultId;
            return model;
        }
    }
}
