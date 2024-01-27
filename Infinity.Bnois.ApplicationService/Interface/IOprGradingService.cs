using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IOprGradingService
    {
        Task<List<OprGradingModel>> GetOprGradings();
    }
}