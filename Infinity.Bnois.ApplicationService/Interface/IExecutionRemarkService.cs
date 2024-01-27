using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IExecutionRemarkService
    {
        List<ExecutionRemarkModel> GetExecutionRemarks(int ps, int pn, string qs, out int total,int type);
        Task<ExecutionRemarkModel> GetExecutionRemark(int id);
        Task<ExecutionRemarkModel> SaveExecutionRemark(int id, ExecutionRemarkModel model);
        Task<bool> DeleteExecutionRemark(int id);
        Task<List<SelectModel>> GetExecutionRemarkSelectModels(int type);
    }
}
