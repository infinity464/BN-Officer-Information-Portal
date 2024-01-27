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

  public  class PreCommissionCourseDetailService: IPreCommissionCourseDetailService
    {
        private readonly IBnoisRepository<PreCommissionCourseDetail> preCommissionCourseDetailRepository;
        public PreCommissionCourseDetailService(IBnoisRepository<PreCommissionCourseDetail> preCommissionCourseDetailRepository)
        {
            this.preCommissionCourseDetailRepository = preCommissionCourseDetailRepository;
        }

        public async Task<PreCommissionCourseDetailModel> GetPreCommissionCourseDetail(int preCommissionCourseDetailId)
        {
            if (preCommissionCourseDetailId <= 0)
            {
                return new PreCommissionCourseDetailModel();
            }
            PreCommissionCourseDetail preCommissionCourseDetail = await preCommissionCourseDetailRepository.FindOneAsync(x => x.PreCommissionCourseDetailId == preCommissionCourseDetailId);
            if (preCommissionCourseDetail == null)
            {
                throw new InfinityNotFoundException("Pre Commission Course Detail not found !");
            }
            PreCommissionCourseDetailModel model = ObjectConverter<PreCommissionCourseDetail, PreCommissionCourseDetailModel>.Convert(preCommissionCourseDetail);
            return model;
        }

        public List<PreCommissionCourseDetailModel> GetPreCommissionCourseDetails(int preCommissionCourseId)
        {
            ICollection<PreCommissionCourseDetail> preCommissionCourseDetails = preCommissionCourseDetailRepository.FilterWithInclude(x => x.IsActive && x.PreCommissionCourseId == preCommissionCourseId).ToList();
            List<PreCommissionCourseDetailModel> models = ObjectConverter<PreCommissionCourseDetail, PreCommissionCourseDetailModel>.ConvertList(preCommissionCourseDetails).ToList();
            return models;
        }

        public async Task<PreCommissionCourseDetailModel> SavePreCommissionCourseDetail(int preCommissionCourseDetailId, PreCommissionCourseDetailModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Pre Commission Course Detail data missing !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            PreCommissionCourseDetail preCommissionCourseDetail = ObjectConverter<PreCommissionCourseDetailModel, PreCommissionCourseDetail>.Convert(model);
            if (preCommissionCourseDetailId > 0)
            {
                preCommissionCourseDetail = await preCommissionCourseDetailRepository.FindOneAsync(x => x.PreCommissionCourseDetailId == preCommissionCourseDetailId);
                if (preCommissionCourseDetail == null)
                {
                    throw new InfinityNotFoundException("Pre Commission Course Detail not found !");
                }

                preCommissionCourseDetail.ModifiedDate = DateTime.Now;
                preCommissionCourseDetail.ModifiedBy = userId;
            }
            else
            {
                preCommissionCourseDetail.IsActive = true;
                preCommissionCourseDetail.CreatedDate = DateTime.Now;
                preCommissionCourseDetail.CreatedBy = userId;
            }
            preCommissionCourseDetail.PreCommissionCourseId = model.PreCommissionCourseId;
            preCommissionCourseDetail.ModuleName = model.ModuleName;
            preCommissionCourseDetail.OutOfMark = model.OutOfMark;
            preCommissionCourseDetail.AchievedMark = model.AchievedMark;
            preCommissionCourseDetail.Position = model.Position;
            preCommissionCourseDetail.Remarks = model.Remarks;

            await preCommissionCourseDetailRepository.SaveAsync(preCommissionCourseDetail);
            model.PreCommissionCourseDetailId = preCommissionCourseDetail.PreCommissionCourseDetailId;
            return model;
        }
    }
}
