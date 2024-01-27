using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IFlashNoticeService
    {
        List<FlashNoticeModel> GetFlashNotices();
        FlashNoticeModel GetFlashNotice(int flashNoticeId);
        Task<FlashNoticeModel> SaveFlashNotice(int flashNoticeId, FlashNoticeModel model);
        Task<bool> DeleteFlashNotice(int flashNoticeId);
        List<FlashNoticeModel> GetFlashNoticeForMemberUser();
    }
}
