using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPhysicalConditionService
    {
        Task<PhysicalConditionModel> GetPhysicalConditions(int employeeId);
        Task<PhysicalConditionModel> UpdatePhysicalCondition(PhysicalConditionModel model);
        Task<PhysicalConditionModel> SavePhysicalCondition(int employeeId, PhysicalConditionModel model);
    }
}
