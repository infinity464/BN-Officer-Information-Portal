using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IParentService
    {
        Task<ParentModel> Parents(int employeeId, int relationType);
        Task<ParentModel> SaveParents(int employeeId, ParentModel model);
        Task<ParentModel> UpdateParent(ParentModel parent);
    }
}
