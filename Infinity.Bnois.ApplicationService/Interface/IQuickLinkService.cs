using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IQuickLinkService
    {
        List<QuickLinkModel> GetQuickLinks(int ps, int pn, string qs, out int total);
        List<QuickLinkModel> GetDashboardQuickLinks();
        Task<QuickLinkModel> GetQuickLink(int id);
        Task<QuickLinkModel> SaveQuickLink(int id, QuickLinkModel model);
        Task<bool> DeleteQuickLink(int id);
      
 
    }
}
