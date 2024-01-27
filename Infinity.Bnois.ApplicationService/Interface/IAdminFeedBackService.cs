using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IAdminFeedBackService
    {
        Task<AdminFeedBackModel> SaveAdminFeedBack(int id, AdminFeedBackModel model);
    }
}
