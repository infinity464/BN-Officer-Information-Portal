using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IRecomandationTypeService
    {
        Task<List<SelectModel>> GetRecomandationTypeSelectModels();
    }
}