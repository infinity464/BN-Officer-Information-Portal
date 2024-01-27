using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class FeedBackService : IFeedBackService
    {
        BnoisDbContext db = new BnoisDbContext();
        private readonly IBnoisRepository<FeedBack> _feedBackRepository;
        private readonly IBnoisRepository<AdminFeedBack> _adminFeedBackRepository;

        public FeedBackService(IBnoisRepository<FeedBack> feedBackRepository, IBnoisRepository<AdminFeedBack> adminFeedBackRepository)
        {
            _feedBackRepository = feedBackRepository;
            _adminFeedBackRepository = adminFeedBackRepository;
        }

        public async Task<FeedBackModel> SaveFeedBack(int id, FeedBackModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Data missing");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            FeedBack feedBack = ObjectConverter<FeedBackModel, FeedBack>.Convert(model);
            if (id > 0)
            {
                feedBack = await _feedBackRepository.FindOneAsync(x => x.FeedBackId == id);
                if (feedBack == null)
                {
                    throw new InfinityNotFoundException("Data not found !");
                }

                feedBack.ModifiedDate = DateTime.Now;
                feedBack.ModifiedBy = userId;
            }
            else
            {
                feedBack.Active = true;
                feedBack.CreatedDate = DateTime.Now;
                feedBack.CreatedBy = userId;
                string employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + userId + "'").SingleOrDefault();

                feedBack.PNo = employeePNo;

            }
            feedBack.Subject = model.Subject;
            feedBack.Description = model.Description;
            feedBack.Status = 0;
            feedBack.AdminView = false;

            await _feedBackRepository.SaveAsync(feedBack);
            model.FeedBackId = feedBack.FeedBackId;
            return model;
        }

        public List<FeedBackModel> GetFeedBacks(int ps, int pn, string qs, out int total)
        {
            IQueryable<FeedBack> feedBacks = _feedBackRepository.FilterWithInclude(x => x.Active && (x.PNo.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = feedBacks.Count();
            feedBacks = feedBacks.OrderByDescending(x => new { x.FeedBackId, x.AdminView }).Skip((pn - 1) * ps).Take(ps);
            List<FeedBackModel> models = ObjectConverter<FeedBack, FeedBackModel>.ConvertList(feedBacks.ToList()).ToList();
            return models;
        }

        public async Task<List<object>> GetFeedBackById(int Id)
        {
            DataTable dataTable = _feedBackRepository.ExecWithSqlQuery(String.Format("exec [spFeedBackById] {0}", Id));
            return dataTable.ToJson().ToList();
        }

        public string GetFeedBackForUser()
        {
            string msg;
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + userId + "'").SingleOrDefault();
            if(employeePNo != null || employeePNo != "0")
            {
                Int64 feedBackId = db.Database.SqlQuery<Int64>("Select top(1) FeedBackId from FeedBack where PNo ='" + employeePNo + "' order by CreatedDate desc").SingleOrDefault();
                if(feedBackId > 0)
                {
                    Int64 adminFeedBackId = db.Database.SqlQuery<Int64>("Select top(1)  AdminFeedBackId from AdminFeedBack where FeedBackId ='" + feedBackId + "' order by CreatedDate desc").SingleOrDefault();
                    if(adminFeedBackId > 1)
                    {
                        msg = db.Database.SqlQuery<string>("Select top(1)  Message from AdminFeedBack where FeedBackId ='" + feedBackId + "' order by CreatedDate desc").SingleOrDefault();
                        var query = $"Update AdminFeedBack set UserView = 1 where AdminFeedBackId=" + adminFeedBackId;
                        int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                    }
                    else
                    {
                        msg = "Currently you have no new notifications";
                    }
                }
                else
                {
                    msg = "Currently you have no new notifications";
                }
            }
            else
            {
                msg = "Currently you have no new notifications";
            }
            return msg;
        }

        public int GetFeedBackForUserNotification()
        {
            int msg;
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string employeePNo = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + userId + "'").SingleOrDefault();
            if (employeePNo != null || employeePNo != "0")
            {
                Int64 feedBackId = db.Database.SqlQuery<Int64>("Select top(1) FeedBackId from FeedBack where PNo ='" + employeePNo + "' order by CreatedDate desc").SingleOrDefault();
                if (feedBackId > 0)
                {
                    Int64 adminFeedBackId = db.Database.SqlQuery<Int64>("Select top(1) AdminFeedBackId from AdminFeedBack where FeedBackId ='" + feedBackId + "' and UserView=0 order by CreatedDate desc").SingleOrDefault();
                    msg = adminFeedBackId > 0 ? 1:0;
                }
                else
                {
                    msg = 0;
                }
            }
            else
            {
                msg = 0;
            }
            return msg;
        }

        public async Task<bool> DeleteFeedback(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            FeedBack adminFeedBack = await _feedBackRepository.FindOneAsync(x=>x.FeedBackId == id && x.Active == true);
            if (adminFeedBack == null)
            {
                throw new InfinityNotFoundException("Admin feed back not found");
            }
            else
            {
                var query = $"update FeedBack set Active=0 where FeedBackId=" + id;
                int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                bool res = noOfRowUpdated > 0 ? true : false;
                return res;
            }
        }
    }
}
