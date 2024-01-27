using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISocialAttributeService
    {
        Task<SocialAttributeModel> GetSocialAttribute(int employeeId);
        Task<SocialAttributeModel> SaveSocialAttribute(int employeeId, SocialAttributeModel model);
    }
}
