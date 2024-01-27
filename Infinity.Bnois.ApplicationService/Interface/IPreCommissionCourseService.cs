using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPreCommissionCourseService
    {
        List<PreCommissionCourseModel> GetPreCommissionCourses(int employeeId);
        Task<PreCommissionCourseModel> GetPreCommissionCourse(int preCommissionCourseId);
        Task<PreCommissionCourseModel> SavePreCommissionCourse(int v, PreCommissionCourseModel model);
        Task<bool> DeletePreCommissionCourse(int preCommissionCourseId);
    }
}
