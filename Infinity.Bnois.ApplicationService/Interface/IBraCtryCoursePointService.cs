using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IBraCtryCoursePointService
    {
        List<BraCtryCoursePointModel> GetBraCtryCoursePoints(int id);
        Task<BraCtryCoursePointModel> GetBraCtryCoursePoint(int braCtryCoursePointId);
        Task<BraCtryCoursePointModel> SaveBraCtryCoursePoint(int v, BraCtryCoursePointModel model);
    }
}
