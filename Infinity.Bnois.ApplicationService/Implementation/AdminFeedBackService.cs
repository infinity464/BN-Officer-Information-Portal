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
    public class AdminFeedBackService : IAdminFeedBackService
    {
        private readonly IBnoisRepository<AdminFeedBack> _adminFeedBackRepository;
        BnoisDbContext db = new BnoisDbContext();
        public AdminFeedBackService(IBnoisRepository<AdminFeedBack> adminFeedBackRepository)
        {
            _adminFeedBackRepository = adminFeedBackRepository;
        }

        public async Task<AdminFeedBackModel> SaveAdminFeedBack(int id, AdminFeedBackModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Data missing");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            AdminFeedBack adminFeedBack = ObjectConverter<AdminFeedBackModel, AdminFeedBack>.Convert(model);

            if (id > 0)
            {
                adminFeedBack = await _adminFeedBackRepository.FindOneAsync(x => x.AdminFeedBackId == id);
                if (adminFeedBack == null)
                {
                    throw new InfinityNotFoundException("Data not found !");
                }

                adminFeedBack.ModifiedDate = DateTime.Now;
                adminFeedBack.ModifiedBy = userId;
            }
            else
            {
                adminFeedBack.IsActive = true;
                adminFeedBack.CreatedDate = DateTime.Now;
                adminFeedBack.CreatedBy = userId;
            }

            adminFeedBack.FeedBackId = model.FeedBackId;
            adminFeedBack.PNo = db.Database.SqlQuery<string>("Select PNo from FeedBack where FeedBackId = '" + model.FeedBackId + "'").SingleOrDefault();
            adminFeedBack.Message = model.Message;
            adminFeedBack.UserView = false;
            adminFeedBack.Status = 0;

            await _adminFeedBackRepository.SaveAsync(adminFeedBack);
            model.AdminFeedBackId = adminFeedBack.AdminFeedBackId;
            return model;
        }
    }
}
