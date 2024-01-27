using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ITraceSettingService
    {
        List<TraceSettingModel> GetTraceSettings(int ps, int pn, string qs, out int total);
        Task<TraceSettingModel> GetTraceSetting(int id);
        Task<TraceSettingModel> SaveTraceSetting(int v, TraceSettingModel model);
        Task<bool> DeleteTraceSetting(int id);
    }
}
