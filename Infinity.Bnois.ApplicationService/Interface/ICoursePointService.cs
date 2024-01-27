using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ICoursePointService
    {
        List<CoursePointModel> GetCoursePoints(int traceSettingId);
        Task<CoursePointModel> GetCoursePoint(int coursePointId);
        Task<CoursePointModel> SaveCoursePoint(int v, CoursePointModel model);
    }
}
