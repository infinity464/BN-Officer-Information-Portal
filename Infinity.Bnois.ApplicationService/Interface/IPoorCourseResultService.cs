using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPoorCourseResultService
    {
        List<PoorCourseResultModel> GetPoorCourseResults(int id);
        Task<PoorCourseResultModel> GetPoorCourseResult(int poorCourseResultId);
        Task<PoorCourseResultModel> SavePoorCourseResult(int v, PoorCourseResultModel model);
    }
}
