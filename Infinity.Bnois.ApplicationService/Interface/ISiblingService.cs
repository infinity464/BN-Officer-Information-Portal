using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISiblingService
    {
        List<SiblingModel> GetSiblings(int employeeId);
        Task<SiblingModel> GetSibling(int siblingId);
        Task<SiblingModel> SaveSibling(int siblingId, SiblingModel model);
        List<SelectModel> GetSiblingTypeSelectModels();

        Task<bool> DeleteSibling(int id);
        Task<SiblingModel> UpdateSibling(SiblingModel sibling);
    }
}