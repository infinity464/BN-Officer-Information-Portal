using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IBatchService
    {
        List<BatchModel> GetBatches(int ps, int pn, string qs, out int total);
        Task<BatchModel> GetBatch(int id);
        Task<BatchModel> SaveBatch(int v, BatchModel model);
        Task<bool> DeleteBatch(int id);
        Task<List<SelectModel>> GetBatchSelectModels();
    }
}
