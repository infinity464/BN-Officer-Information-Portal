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
    public class FlashNoticeService : IFlashNoticeService
    {
        private readonly IBnoisRepository<FlashNotice> _flashNoticeRepository;
        BnoisDbContext db = new BnoisDbContext();

        public FlashNoticeService(IBnoisRepository<FlashNotice> flashNoticeRepository)
        {
            _flashNoticeRepository = flashNoticeRepository;
        }
        
        public async Task<bool> DeleteFlashNotice(int flashNoticeId)
        {
            if (flashNoticeId < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            FlashNotice flashNotice = await _flashNoticeRepository.FindOneAsync(x => x.FlashNoticeId == flashNoticeId);
            if (flashNotice == null)
            {
                throw new InfinityNotFoundException("Flash Notice not found");
            }
            else
            {
                var query = $"Update FlashNotice set IsActive=0 where FlashNoticeId="+flashNoticeId;
                int noOfRowUpdated1 = db.Database.ExecuteSqlCommand(query);
                bool res = noOfRowUpdated1 > 0 ? true : false;
                return res;
            }
        }

        public FlashNoticeModel GetFlashNotice(int flashNoticeId)
        {
            if (flashNoticeId <= 0)
            {
                return new FlashNoticeModel();
            }
            FlashNotice flashNotice = _flashNoticeRepository.FindOne(x => x.FlashNoticeId == flashNoticeId);
            if (flashNotice == null)
            {
                throw new InfinityNotFoundException("Flash Notice not found");
            }


            FlashNoticeModel model = ObjectConverter<FlashNotice, FlashNoticeModel>.Convert(flashNotice);
            return model;
        }

        public List<FlashNoticeModel> GetFlashNoticeForMemberUser()
        {
            List<FlashNotice> flashNoticeModels = _flashNoticeRepository.Where(x=>x.IsActive == true).ToList();
            List<FlashNoticeModel> models = ObjectConverter<FlashNotice, FlashNoticeModel>.ConvertList(flashNoticeModels).ToList();
            return models;
        }

        public List<FlashNoticeModel> GetFlashNotices()
        {
            List<FlashNotice> flashNoticeModels = db.FlashNotice.ToList();
            List<FlashNoticeModel> models = ObjectConverter<FlashNotice, FlashNoticeModel>.ConvertList(flashNoticeModels).ToList();
            return models;
        }

        public async Task<FlashNoticeModel> SaveFlashNotice(int flashNoticeId, FlashNoticeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Flash notice data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            FlashNotice flashNotice = ObjectConverter<FlashNoticeModel, FlashNotice>.Convert(model);
            if (flashNoticeId > 0)
            {
                flashNotice = await _flashNoticeRepository.FindOneAsync(x => x.FlashNoticeId == flashNoticeId);
                if (flashNotice == null)
                {
                    throw new InfinityNotFoundException("Flash notice not found !");
                }

                flashNotice.ModifiedDate = DateTime.Now;
                flashNotice.ModifiedBy = userId;
            }
            else
            {
                flashNotice.IsActive = true;
                flashNotice.CreatedDate = DateTime.Now;
                flashNotice.CreatedBy = userId;
            }
            flashNotice.FlashNotice1 = model.FlashNotice1;
            flashNotice.FromDate = model.FromDate;
            flashNotice.ToDate = model.ToDate;
            
            await _flashNoticeRepository.SaveAsync(flashNotice);
            model.FlashNoticeId = flashNotice.FlashNoticeId;
            return model;
        }
    }
}
