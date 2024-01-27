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
    public class QuickLinkService : IQuickLinkService
    {
        private readonly IBnoisRepository<QuickLink> quickLinkRepository;
		public QuickLinkService(IBnoisRepository<QuickLink> quickLinkRepository)
        {
            this.quickLinkRepository = quickLinkRepository;
        }

        public List<QuickLinkModel> GetQuickLinks(int ps, int pn, string qs, out int total)
        {
            IQueryable<QuickLink> QuickLinks = quickLinkRepository.FilterWithInclude(x => x.IsActive
                &&  x.FileName.Contains(qs)  || String.IsNullOrEmpty(qs));
            total = QuickLinks.Count();
            QuickLinks = QuickLinks.OrderByDescending(x => x.QuickLinkId).Skip((pn - 1) * ps).Take(ps);
            List<QuickLinkModel> models = ObjectConverter<QuickLink, QuickLinkModel>.ConvertList(QuickLinks.ToList()).ToList();
            return models;
        }

        public List<QuickLinkModel> GetDashboardQuickLinks()
        {
            IQueryable<QuickLink> quickLinks = quickLinkRepository.FilterWithInclude(x => x.IsActive && x.FileName !=null).OrderBy(x=>x.Name);

            List<QuickLinkModel> models = ObjectConverter<QuickLink, QuickLinkModel>.ConvertList(quickLinks.ToList()).ToList();
            return models;
        }



        public async Task<QuickLinkModel> GetQuickLink(int id)
        {
            if (id <= 0)
            {
                return new QuickLinkModel();
            }
            QuickLink quickLink = await quickLinkRepository.FindOneAsync(x => x.QuickLinkId == id);
            if (quickLink == null)
            {
                throw new InfinityNotFoundException("Quick Link not found");
            }
            QuickLinkModel model = ObjectConverter<QuickLink, QuickLinkModel>.Convert(quickLink);
            return model;
        }

    
        public async Task<QuickLinkModel> SaveQuickLink(int id, QuickLinkModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Medal, Award & Publication  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            QuickLink quickLink = ObjectConverter<QuickLinkModel, QuickLink>.Convert(model);
            if (id > 0)
            {
                quickLink = await quickLinkRepository.FindOneAsync(x => x.QuickLinkId == id);
                if (quickLink == null)
                {
                    throw new InfinityNotFoundException("Medal, Award & Publication not found !");
                }

                quickLink.ModifiedDate = DateTime.Now;
                quickLink.ModifiedBy = userId;
            }
            else
            {
                quickLink.IsActive = true;
                quickLink.CreatedDate = DateTime.Now;
                quickLink.CreatedBy = userId;
            }

            quickLink.Remarks = model.Remarks;
            quickLink.FileName = model.FileName;
            quickLink.Name = model.Name;
            quickLink.Extention = model.Extention;

         
            await quickLinkRepository.SaveAsync(quickLink);
            model.QuickLinkId = quickLink.QuickLinkId;

			return model;
        }


        public async Task<bool> DeleteQuickLink(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            QuickLink quickLink = await quickLinkRepository.FindOneAsync(x => x.QuickLinkId == id);
            if (quickLink == null)
            {
                throw new InfinityNotFoundException("Medal, Award & Publication not found");
            }
            else
            {
	           var result= await quickLinkRepository.DeleteAsync(quickLink);
	            return result;

            }
        }

    
      
    }
}
