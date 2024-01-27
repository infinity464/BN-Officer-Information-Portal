using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IFeedBackService
    {
        Task<FeedBackModel> SaveFeedBack(int id, FeedBackModel model);
        List<FeedBackModel> GetFeedBacks(int ps, int pn, string qs, out int total);
        Task<List<object>> GetFeedBackById(int Id);
        string GetFeedBackForUser();
        int GetFeedBackForUserNotification();
        Task<bool> DeleteFeedback(int id);
    }
}
