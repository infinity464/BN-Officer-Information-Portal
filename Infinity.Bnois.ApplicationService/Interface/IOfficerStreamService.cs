using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IOfficerStreamService
    {
        List<OfficerStreamModel> GetOfficerStreams(int ps, int pn, string qs, out int total);
        Task<OfficerStreamModel> GetOfficerStream(int id);
        Task<OfficerStreamModel> SaveOfficerStream(int v, OfficerStreamModel model);
        Task<bool> DeleteOfficerStream(int id);
        Task<List<SelectModel>> GetOfficerStreamSelectModels();
    }
}
