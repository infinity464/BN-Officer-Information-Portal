using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPreCommissionCourseDetailService
    {
        List<PreCommissionCourseDetailModel> GetPreCommissionCourseDetails(int preCommissionCourseId);
        Task<PreCommissionCourseDetailModel> GetPreCommissionCourseDetail(int preCommissionCourseDetailId);
        Task<PreCommissionCourseDetailModel> SavePreCommissionCourseDetail(int v, PreCommissionCourseDetailModel model);
    }
}
